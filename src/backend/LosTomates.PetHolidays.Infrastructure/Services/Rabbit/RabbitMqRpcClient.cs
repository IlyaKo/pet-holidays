using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using LosTomates.PetHolidays.RabbitMQ.Core.Services.Rabbit;

namespace LosTomates.PetHolidays.Infrastructure.Services.Rabbit
{
    public class RabbitMqRpcClient : IRpcClient, IDisposable
    {
        private IConnection _connection = null!;
        private IChannel _channel = null!;
        private readonly AsyncEventingBasicConsumer _consumer = null!;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private IBasicProperties _props = null!;
        private const int MaxRequeueCount = 2;
        private ulong _deliveryTag = 0;
        private string _agentId;
        private string _queue;
        private string correlationId;
        private readonly RabbitMqSettings _settings;
        private int callRetry = 0;
        public RabbitMqRpcClient(IOptions<RabbitMqSettings> settings)
        {
            _settings = settings.Value;

            InitializeConnection().GetAwaiter().GetResult();
        }
        private async Task InitializeConnection()
        {
            try
            {
                var _factory = new ConnectionFactory()
                {
                    HostName = _settings.Host,
                    UserName = _settings.UserName,
                    Password = _settings.Password,
                    ClientProvidedName = "MSU Client " + _agentId
                };

                _queue = _settings.ClientQueue;

                _connection = await _factory.CreateConnectionAsync();

                _channel = await _connection.CreateChannelAsync();

                _channel.ContinuationTimeout = TimeSpan.FromSeconds(180);

                correlationId = Guid.NewGuid().ToString();

                _props = new BasicProperties
                {
                    CorrelationId = correlationId,
                    ReplyTo = _queue
                };

                await _channel.QueueDeclareAsync(
                    queue: _queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                StartConsuming();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing connection: {ex.Message}");
                Thread.Sleep(5000); // Wait before retrying
                await InitializeConnection();
            }
        }

        private void StartConsuming()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnMessageReceived;
            _channel.BasicConsumeAsync(queue: _queue, autoAck: false, consumer: consumer);
        }
        private Task OnMessageReceived(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    _deliveryTag = ea.DeliveryTag;
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    _respQueue.Add(response);

                    _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            }
            catch (Exception ex)
            {
                RetryAcknowledgment(ea.DeliveryTag);

            }
            return Task.CompletedTask;
        }

        private void RetryAcknowledgment(ulong deliveryTag)
        {
            int retryCount = 3;
            int delayMilliseconds = 1000;

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    _channel.BasicAckAsync(deliveryTag: deliveryTag, multiple: false);
                    Console.WriteLine("Message acknowledged successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Retry {i + 1} failed: {ex.Message}");
                    Thread.Sleep(delayMilliseconds);
                    delayMilliseconds *= 2; // Exponential backoff
                }
            }

            Console.WriteLine("Failed to acknowledge message after multiple retries.");
        }

        public async Task<string> Call(string message, string queue)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            try
            {

                // Check if the queue is declared and open
                var _ = _channel.QueueDeclarePassiveAsync(_queue).Result.QueueName;

                await _channel.BasicPublishAsync(exchange: "", routingKey: queue, mandatory: true, basicProperties: (BasicProperties)_props, body: messageBytes);
            }
            catch (Exception ex)
            {

                if (callRetry < 5)
                {
                    Console.WriteLine($"Error publishing message: {ex.Message}");
                    InitializeConnection();
                    return await Call(message, queue);
                }

                callRetry++;
            }

            if (_respQueue.TryTake(out string response, TimeSpan.FromSeconds(10)))
            {
                return response;
            }
            await _channel.BasicRejectAsync(_deliveryTag, false);

            throw new TimeoutException("RPC call timed out");
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
