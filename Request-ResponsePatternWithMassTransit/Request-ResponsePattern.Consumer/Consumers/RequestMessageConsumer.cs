using MassTransit;
using Request_ResponsePattern.Shared.RequestResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request_ResponsePattern.Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            //....process
            Console.WriteLine(context.Message.Text);
            await context.RespondAsync<ResponseMessage>
                (new() { Text = $"{context.Message.MessageNo}. response to request" });
        }
    }
}
