using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Bağlantı Oluşturma

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://vmizbtab:G4DLes6vB7pmTPcanD89TdmrhCdjtzAd@shrimp.rmq.cloudamqp.com/vmizbtab");

//Bağlantı Aktifleştirme ve Kanal Açma 
using IConnection connection = factory.CreateConnection();

//kanal olusturma
using IModel channel = connection.CreateModel();

//Queue Oluşturma 
//burdaki kuyruk publisher'da ki ile aynı tanımlandırılmalıdır.
channel.QueueDeclare(queue: "example-queue", exclusive: false);



//Queue'dan Mesaj Okuma 
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    //kuyruğa gelen mesajın işlendiği yerdir.
    //e.Body: kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray : kuyruktaki mesajın byte verisini getirecektir
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();




















