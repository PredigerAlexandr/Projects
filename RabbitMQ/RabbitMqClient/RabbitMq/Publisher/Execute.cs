using Publisher.Interfaces;

namespace Publisher;

public class Execute
{
    private readonly IBrokerService _brokerService;
    
    public Execute(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    public async Task RunAsync()
    {
        await _brokerService.NewMessageAsync();
    }
}