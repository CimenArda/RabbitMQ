using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "directExchange-Example", type: ExchangeType.Direct);

while (true)
{
    Console.WriteLine("Mesaj:");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
          exchange: "directExchange-Example"
          , routingKey: "directQueue"
          , body: byteMessage

          );
}

Console.Read();
