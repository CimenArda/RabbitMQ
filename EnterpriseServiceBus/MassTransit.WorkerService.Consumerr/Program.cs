using MassTransit;
using MassTransit.WorkerService.Consumerr.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {

            configurator.AddConsumer<ExampleMessageConsumer>();
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://zmohqwps:2E2lCfk_9olDcVer7FAFesF0w_z_3lgo@shrimp.rmq.cloudamqp.com/zmohqwps");

                _configurator.ReceiveEndpoint("example-message-queue",e=>e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();
