using MassTransit;
using Utilities.Constants;
using Utilities.Interfaces;

namespace RabbitMq;

public class Execute
{
    private readonly IBrokerService _brokerService;
    
    public Execute(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    public async Task Run()
    {
        await _brokerService.NewFanoutMessageAsync();
        await _brokerService.NewDirectMessageAsync(RoutingKeyConstants.RoutingKey1);
    }
}