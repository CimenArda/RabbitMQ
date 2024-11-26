using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps"); //cloudamqp

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "Example-workqueue";
channel.QueueDeclare
    (
       queue:queueName,
       durable:false,
       exclusive:false,
       autoDelete:false
    );
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " + i);
    channel.BasicPublish
        (
            exchange: string.Empty,
            routingKey: queueName,
            body: message
        );
}

Console.Read();