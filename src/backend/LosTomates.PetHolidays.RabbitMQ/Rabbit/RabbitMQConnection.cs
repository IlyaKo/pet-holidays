using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.RabbitMQ.Rabbit
{
    public class RabbitMQConnection : IDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private IChannel _channel;

        public RabbitMQConnection(string hostName = "localhost", string user = "guest", string pass = "guest", string vhost = "/")
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = user,
                Password = pass,
                VirtualHost = vhost
            };
        }

        public async Task ConnectAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
        }

        public IChannel GetChannel() => _channel;

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}