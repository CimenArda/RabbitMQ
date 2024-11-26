using MassTransit;
using MassTransit.Shared.Messages;

string rabbitMQUri = "amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps";

string queueName = "example-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

var sendEndpoint =await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.WriteLine("Gönderilecek Mesaj:");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});
Console.Read();