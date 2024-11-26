using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume
    (
        queue: queueName,
        autoAck: false,
        consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();