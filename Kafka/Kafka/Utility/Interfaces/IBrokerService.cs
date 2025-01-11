namespace Utility.Interfaces;

public interface IBrokerService
{
    Task NewTopicAsync(string topicName);
    Task NewMessageAsync(string message, string topicName);
    Task<IEnumerable<string>> GetMessagesAsync(string topicName);
}