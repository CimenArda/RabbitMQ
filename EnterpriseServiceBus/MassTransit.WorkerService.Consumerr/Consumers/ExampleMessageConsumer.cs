using MassTransit.WorkerService.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.WorkerService.Consumerr.Consumers
{
    internal class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen Mesaj:{context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
