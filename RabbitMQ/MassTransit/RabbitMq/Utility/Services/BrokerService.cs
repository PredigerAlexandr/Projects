using MassTransit;
using Utilities.Interfaces;
using Utilities.Models;

namespace Utilities.Services;

public class BrokerService : IBrokerService
{
    private readonly IBusControl _busControl;

    public BrokerService(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task NewMessageAsync()
    {
        await _busControl.StartAsync();
        try
        {
            await _busControl.Publish<Message>(new Message
                { Title = "You are win " + new Random().Next(0, int.MaxValue) + "$ / fanout" });
        }
        finally
        {
            await _busControl.StopAsync();
        }
    }
}