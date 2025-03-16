using LosTomates.PetHolidays.FileService.WebApi.Exchange;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace LosTomates.PetHolidays.FileService.WebApi.BackgroundServices;

public sealed class RabbitEventsReceiver(ConnectionFactory factory,
    IServiceProvider serviceProvider) : BackgroundService
{
    private readonly ConnectionFactory _factory = factory;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private const int RetryDelay = 3000;

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var connection = await _factory.CreateConnectionAsync(stoppingToken);
                using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                await channel.ExchangeDeclareAsync(RabbitConstants.FanoutExchange,
                    type: ExchangeType.Fanout,
                    durable: true,
                    cancellationToken: stoppingToken);

                await channel.QueueDeclareAsync(RabbitConstants.EntityDeletedQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    cancellationToken: stoppingToken);

                await channel.QueueBindAsync(RabbitConstants.EntityDeletedQueue,
                    RabbitConstants.FanoutExchange,
                    string.Empty,
                    cancellationToken: stoppingToken);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var dto = JsonSerializer.Deserialize<EntityDeletedEventDto>(message);

                    ArgumentNullException.ThrowIfNull(dto, "Invalid message format");
                    using var scope = _serviceProvider.CreateScope();
                    var fileService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
                    try
                    {
                        await fileService.DeleteFileAsync(dto.Id, dto.Type);
                        await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                        Console.WriteLine($"File deleted: {dto.Type} - {dto.Id}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Problem with file deletion: {dto.Type} - {dto.Id}. Exception: {ex.Message}");
                    }
                };

                await channel.BasicConsumeAsync(RabbitConstants.EntityDeletedQueue,
                    autoAck: false,
                    consumer,
                    cancellationToken: stoppingToken);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rabbit exchange exception: " + ex.Message);
                await Task.Delay(RetryDelay, stoppingToken);
                continue;
            }

        }
    }
}
