using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare
    (
    exchange:"headerExchange-example", 
    type: ExchangeType.Headers
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    Console.Write("Lütfen header valuesunu giriniz:");
    string value = Console.ReadLine();

    IBasicProperties basicProperties =  channel.CreateBasicProperties();

    basicProperties.Headers = new Dictionary<string, object>
    {
        ["no"] = value
    };


    channel.BasicPublish
        (
            exchange: "headerExchange-example",
            routingKey: string.Empty,
            body: message,
            basicProperties: basicProperties
        );    
}


Console.Read();