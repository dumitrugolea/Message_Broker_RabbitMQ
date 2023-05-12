using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirline.API.Services;

public class MessageProducer:IMessageProducer
{
    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

            //UserName = "user",
            //Password = "mypass",

        var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();
        channel.QueueDeclare("booking", durable: true, exclusive: true);

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("","booking",body:body);

    }
}