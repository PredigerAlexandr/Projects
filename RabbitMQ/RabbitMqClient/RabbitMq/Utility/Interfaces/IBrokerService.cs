namespace Publisher.Interfaces;

public interface IBrokerService
{
    Task NewMessageAsync();
    Task<List<string>> GetMessagesAsync();
}