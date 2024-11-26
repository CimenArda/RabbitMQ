using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanoutExchange-example", type: ExchangeType.Fanout);

Console.WriteLine("Kuyruk Adını Giriniz:");
string _queueName = Console.ReadLine();
channel.QueueDeclare
    (
    queue:_queueName,
    exclusive:false
    );


channel.QueueBind
    (
        queue: _queueName,
        exchange: "fanoutExchange-example",
        routingKey: string.Empty
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume
    (
        queue: _queueName,
        autoAck: true,
        consumer: consumer
    );

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();
