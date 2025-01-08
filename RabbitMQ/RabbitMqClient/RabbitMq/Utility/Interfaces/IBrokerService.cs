namespace Publisher.Interfaces;

public interface IBrokerService
{
    Task NewMessageAsync();
    Task<List<string>> GetMessagesAsync();
    Task NewDirectMessageAsync(string routingKey);
    Task NewTopicMessageAsync(string routingKey);
    Task NewHeadersMessageAsync();
    Task NewFunoutMessageAsync();
    IAsyncEnumerable<string> GetMessagesByDirectAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByTopicAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByFunoutAsync();
    IAsyncEnumerable<string> GetMessagesByHeadersAsync(Dictionary<string, object> headers);
}