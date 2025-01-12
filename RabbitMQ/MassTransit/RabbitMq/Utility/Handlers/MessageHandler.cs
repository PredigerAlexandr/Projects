using MassTransit;
using Utilities.Models;

namespace Utilities.Handlers;

public class MessageHandler: IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> context)
    {
        var message = context.Message;
        Console.WriteLine(message.Title);
        return Task.CompletedTask;
    }
}