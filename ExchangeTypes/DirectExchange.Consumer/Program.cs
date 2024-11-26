using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//1.Adım
channel.ExchangeDeclare(exchange: "directExchange-Example", type: ExchangeType.Direct);

//2.Adım 
string queueName = channel.QueueDeclare().QueueName;

//3.Adım
channel.QueueBind(
     queue: queueName,
     exchange: "directExchange-Example",
     routingKey: "directQueue"
    );

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
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

/* 1. Adım:Publisher'daki exchange ile birebir aynı isim ve type'a sahip bir exchange tanımlanmalı.
 
  2. Adım:Publisher tarafından routing key'e bulunan değerdeki kuyruga gönderilen   mesajları kendi oluşturdugumuz kuyruga yönlendirerek tüketmemiz gerekecektir.
    Bunun için öncelikle bir kuyruk oluşturulmalıdır.

    3. Adım:



*/