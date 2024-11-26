using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "p2pExample";

channel.QueueDeclare
    (
        queue: queueName,
        durable: false,
        exclusive: false,
        autoDelete: false
    );

byte[] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish
    (
        exchange: string.Empty,
        routingKey: queueName,
        body: message
    );

Console.Read();