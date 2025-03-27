using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LosTomates.PetHolidays.Core.Core.Exchange;

public sealed class RabbitService(ConnectionFactory factory) : IRabbitService
{
    private readonly ConnectionFactory factory = factory;

    public async Task SendEntityDeletedEvent(EntityDeletedEventDto dto)
    {
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(RabbitConstants.FanoutExchange,
            type: ExchangeType.Fanout,
            durable: true);

        await channel.QueueDeclareAsync(RabbitConstants.EntityDeletedQueue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        await channel.QueueBindAsync(RabbitConstants.EntityDeletedQueue,
            RabbitConstants.FanoutExchange,
            string.Empty);

        var body = JsonSerializer.Serialize(dto);
        var bodyBytes = Encoding.UTF8.GetBytes(body);
        await channel.BasicPublishAsync(RabbitConstants.FanoutExchange,
            string.Empty,
            bodyBytes);
    }
}
