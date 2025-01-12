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

    public async Task NewFanoutMessageAsync()
    {
        await _busControl.StartAsync();
        try
        {
            await _busControl.Publish<DirectMessage>(new DirectMessage
                { Title = "You are win " + new Random().Next(0, int.MaxValue) + "$ / fanout" });
        }
        finally
        {
            await _busControl.StopAsync();
        }
    }

    public async Task NewDirectMessageAsync(string routingKey)
    {
        await _busControl.StartAsync();
        try
        {
            await _busControl.Publish<DirectMessage>(new DirectMessage
                    { Title = "You are win " + new Random().Next(0, int.MaxValue) + "$ / direct" },
                context => { context.SetRoutingKey(routingKey); });
        }
        finally
        {
            await _busControl.StopAsync();
        }
    }
}