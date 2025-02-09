using LosTomates.PetHolidays.RabbitMQ.Rabbit;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System.Text;
using System.Threading.Tasks;

namespace LosTomates.PetHolidays.RabbitMQ.PubSub;

public class MessagePublisher
{
    private readonly RabbitMQConnection _rabbitConnection;

    public MessagePublisher(RabbitMQConnection rabbitConnection)
    {
        _rabbitConnection = rabbitConnection;
    }

    public async Task PublishAsync(string exchangeName, string routingKey, string message)
    {
        await _rabbitConnection.ConnectAsync();  // Asegurar conexión antes de publicar
        var channel = _rabbitConnection.GetChannel();

        await channel.QueueDeclareAsync(queue: "pichulas");

        // Definir propiedades del mensaje (ejemplo: persistente y con content-type)
        var properties = new BasicProperties
        {
            ContentType = "text/plain",
            DeliveryMode = (DeliveryModes)2 // 2 significa "persistente" en RabbitMQ
        };

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange:"", "pichulas", mandatory: false, properties, body);
    }
}