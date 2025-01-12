using MassTransit;
using Utilities.Models;

namespace Utilities.Handlers;

public class MessageFanoutHandler : IConsumer<DirectMessage>
{
    public Task Consume(ConsumeContext<DirectMessage> context)
    {
        var message = context.Message;
        Console.WriteLine(message.Title);
        return Task.CompletedTask;
    }
}