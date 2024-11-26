using RabbitMQ.Client;
using System.Text;

//bağlantı olusturma 
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new ("amqps://vmizbtab:G4DLes6vB7pmTPcanD89TdmrhCdjtzAd@shrimp.rmq.cloudamqp.com/vmizbtab"); //cloudamqp

//bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();

//kanal olusturma
using IModel channel  = connection.CreateModel();


//Queue Oluşturma 
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue'ya Mesaj Gönderme

//rabbitmq kuyruğa atacağı mesajları byte türünden kabul eder.Haliyle mesajları bizim byte'a dönüştürmemiz gerekecektir.

byte[] message = Encoding.UTF8.GetBytes("Merhaba");

// basit düzeyde bir uygulama olacağı için exchange girmedim default olarak bu durumda direct exchange yaklaşımı sağlanır.
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();









