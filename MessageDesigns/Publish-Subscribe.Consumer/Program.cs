using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


string exchangeName = "Example-publish-subscribe-example";
channel.ExchangeDeclare
    (
        exchange: exchangeName,
        type: ExchangeType.Fanout
    );


string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind
    (
        queue: queueName,
        exchange: exchangeName,
        routingKey: string.Empty
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