using Utility.Constants;
using Utility.Interfaces;

namespace Kafka;

public class Execute
{
    private readonly IBrokerService _brokerService;

    public Execute(IBrokerService brokerService)
    {
        _brokerService = brokerService;
    }

    public async Task Run()
    {
        await _brokerService.NewTopicAsync(KafkaConstants.KafkaTopicName);
        await _brokerService.NewMessageAsync("Hello Consumer!", KafkaConstants.KafkaTopicName);
    }
}