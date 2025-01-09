namespace Utilities.Interfaces;

public interface IBrokerService
{
    Task NewFanoutMessageAsync();
    Task NewDirectMessageAsync(string routingKey);
}