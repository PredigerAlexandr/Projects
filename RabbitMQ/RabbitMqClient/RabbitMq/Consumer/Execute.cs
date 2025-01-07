using Publisher.Interfaces;

namespace Consumer;

public class Execute
{
    private readonly IBrokerService _brokerService;

    public Execute(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    public async Task RunAsync()
    {
        var messages = _brokerService.GetMessagesByFunoutAsync();
        await foreach (var mes in messages)
        {
            Console.WriteLine(mes);
        }
    }
}