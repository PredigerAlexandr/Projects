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
        while (true)
        {
            Thread.Sleep(1000);
            await _brokerService.NewMessageAsync();

            await _brokerService.NewDirectMessageAsync("error");
            await _brokerService.NewDirectMessageAsync("message");
            await _brokerService.NewDirectMessageAsync("log");

            await _brokerService.NewTopicMessageAsync("sensor.temperature.livingroom");
            await _brokerService.NewFunoutMessageAsync();
            await _brokerService.NewHeadersMessageAsync();
        }
    }
}