using Microsoft.AspNetCore.SignalR.Client;

namespace Utility.Interfaces;

public interface ISocketService
{
    Task<HubConnection?> CreateConnectionToHub(string url);
    Task TransmitMessagesToConsole(HubConnection connection);
    Task SendMessage(HubConnection connection, string message);
}