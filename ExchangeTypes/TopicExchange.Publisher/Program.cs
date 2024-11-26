using RabbitMQ.Client;
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

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhabaa {i}");
    Console.Write("Mesajın Gönderileceği Topic Formatını Belirtiniz:");
    string topic = Console.ReadLine();
    channel.BasicPublish
        (
            exchange: "topicexchange-example",
            routingKey: topic,
            body:message
        );

}





Console.Read();