using RabbitMQ.Client;
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

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " +i);
    channel.BasicPublish
        (
            exchange: exchangeName,
            routingKey: string.Empty,
            body: message
        );
}




Console.Read();