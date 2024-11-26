using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare
    (
        exchange: "topicexchange-example",
        type: ExchangeType.Topic
    );

Console.Write("Dinlenecek Topic Formatını Belirtiniz:");
string topic = Console.ReadLine();

string queueName =channel.QueueDeclare().QueueName;

channel.QueueBind
    (
        queue: queueName,
        exchange: "topicexchange-example",
        routingKey: topic
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume
    (
        queue: queueName,
        autoAck: true,
        consumer: consumer

    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};



Console.Read();