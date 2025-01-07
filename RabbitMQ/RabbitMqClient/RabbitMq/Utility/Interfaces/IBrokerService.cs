namespace Publisher.Interfaces;

public interface IBrokerService
{
    Task NewMessageAsync();
    Task<List<string>> GetMessagesAsync();
    Task NewDirectMessageAsync(string routingKey);
    Task NewTopicMessageAsync(string routingKey);
    Task NewFunoutMessageAsync();
    IAsyncEnumerable<string> GetMessagesByDirectAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByTopicAsync(string routingKey);
    IAsyncEnumerable<string> GetMessagesByFunoutAsync();
}