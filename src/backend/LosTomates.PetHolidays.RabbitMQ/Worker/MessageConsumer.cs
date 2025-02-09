using LosTomates.PetHolidays.RabbitMQ.Rabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace LosTomates.PetHolidays.RabbitMQ.Worker;

public class MessageConsumer
{
    private readonly RabbitMQConnection _rabbitConnection;
    private string _consumerTag;

    public MessageConsumer(RabbitMQConnection rabbitConnection)
    {
        _rabbitConnection = rabbitConnection;
    }

    public async Task ConsumeAsync(string exchangeName, string queueName, string routingKey)
    {
        await _rabbitConnection.ConnectAsync();
        var channel = _rabbitConnection.GetChannel();

        // Declarar el exchange y la cola
        await channel.ExchangeDeclareAsync(exchange:"", ExchangeType.Direct);
        await channel.QueueDeclareAsync(queue:"pichulas", durable: false, exclusive: false, autoDelete: false, arguments: null);
        await channel.QueueBindAsync(queueName, exchangeName, routingKey, arguments: null);

        // Crear consumidor asíncrono
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received message: {message}");

            // Confirmar recepción del mensaje
            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
        };

        // Iniciar consumo
        _consumerTag = await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        Console.WriteLine($"[*] Listening on queue '{queueName}'...");
    }

    public async Task StopConsumingAsync()
    {
        var channel = _rabbitConnection.GetChannel();
        if (!string.IsNullOrEmpty(_consumerTag))
        {
            await channel.BasicCancelAsync(_consumerTag);
            Console.WriteLine("[*] Consumer stopped.");
        }
    }
}