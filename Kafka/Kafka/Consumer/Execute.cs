using Utility.Constants;
using Utility.Interfaces;

namespace Consumer;

public class Execute
{
    private readonly IBrokerService _brokerService;

    public Execute(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    public async Task Run()
    {
        var messages = await _brokerService.GetMessagesAsync(KafkaConstants.KafkaTopicName);

        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
    }
}