using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.Infrastructure.Services.Rabbit
{
    public class RabbitMqRpcServer : BackgroundService
    {
        private readonly RabbitMqSettings _serviceSettings;
        private readonly ILogger<RabbitMqRpcServer> _logger;

        private IConnection _connection;
        private IChannel _channel;

        public RabbitMqRpcServer(
            IOptions<RabbitMqSettings> mqSettings,
            ILogger<RabbitMqRpcServer> logger)
        {
            _logger = logger;
            _serviceSettings = mqSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await EnsureConnectionAndChannelAsync();

                    var consumer = new AsyncEventingBasicConsumer(_channel);

                    consumer.ReceivedAsync += async (_, ea) =>
                    {
                        var props = ea.BasicProperties;
                        var replyProps = new BasicProperties();
                        replyProps.CorrelationId = props.CorrelationId;

                        replyProps.ReplyTo ??= "dead.messages";

                        string info = "";

                        try
                        {
                            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };
                            var message = JsonSerializer.Deserialize<object>(content, options);

                            //Handle the message



                            //Response here
                            var result = new { Result = "OK", };

                            var responseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));

                            info = content;

                            if (message != null)
                            {
                                await _channel.BasicPublishAsync(
                                        exchange: "",
                                        routingKey: props.ReplyTo,
                                        mandatory: true,
                                        basicProperties: replyProps,
                                        body: responseBytes);

                                await _channel.BasicAckAsync(ea.DeliveryTag, false);
                            }
                            else
                            {
                                _logger.LogWarning("Received null or invalid message: {Content}", content);

                                responseBytes = Encoding.UTF8.GetBytes(info);

                                await _channel.BasicPublishAsync(
                                    exchange: "",
                                    routingKey: props.ReplyTo,
                                    basicProperties: replyProps,
                                    mandatory: true,
                                    body: responseBytes);

                                await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error processing message");

                            var responseBytes = Encoding.UTF8.GetBytes(info);

                            await _channel.BasicPublishAsync(
                                    exchange: "",
                                    routingKey: props.ReplyTo,
                                    mandatory: true,
                                    basicProperties: replyProps,
                                    body: responseBytes);

                            await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
                        }
                    };

                    await _channel.BasicConsumeAsync(_serviceSettings.WorkQueue, false, consumer);

                    await Task.Delay(Timeout.Infinite, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in RabbitMQ RPC server, restarting...");
                    await Task.Delay(5000, stoppingToken); // Wait for 5 seconds before retrying
                }
            }
        }

        private async Task EnsureConnectionAndChannelAsync()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _serviceSettings.Host,
                    UserName = _serviceSettings.UserName,
                    Password = _serviceSettings.Password,
                    ClientProvidedName = "API RPC"
                };

                _connection = await factory.CreateConnectionAsync();
            }

            if (_channel == null || !_channel.IsOpen)
            {
                _channel = await _connection.CreateChannelAsync();

                _channel.ContinuationTimeout = TimeSpan.FromSeconds(180);

                await _channel.QueueDeclareAsync(
                    queue: _serviceSettings.WorkQueue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await _channel.BasicQosAsync(0, 1, false);

                try
                {
                    await _channel.QueueDeclareAsync(
                        queue: "dead.messages",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                }
                catch (Exception ex)
                {

                }
            }

            await Task.Yield();
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }
}
