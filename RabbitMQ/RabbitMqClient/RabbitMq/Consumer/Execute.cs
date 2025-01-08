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
        var messages = _brokerService.GetMessagesByHeadersAsync(
            new Dictionary<string, object>()
            {
                {"header1","type-script"},
                {"header2","rust"}
            }
            );
        await foreach (var mes in messages)
        {
            Console.WriteLine(mes);
        }
    }
}