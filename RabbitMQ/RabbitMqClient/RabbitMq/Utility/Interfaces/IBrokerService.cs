namespace Publisher.Interfaces;

public interface IBrokerService
{
    Task NewMessageAsync();
    Task<List<string>> GetMessagesAsync();
    Task NewDirectMessageAsync(string routingKey);
    Task NewTopicMessageAsync(string routingKey);
    Task NewHeadersMessageAsync();
    Task NewFanoutMessageAsync();
    IAsyncEnumerable<string> GetMessagesByDirectAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByTopicAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByFanoutAsync();
    IAsyncEnumerable<string> GetMessagesByHeadersAsync(Dictionary<string, object> headers);
}