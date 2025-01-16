using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Utility.Interfaces;
using Utility.Models;

namespace Utility.Services;

public class SocketService : ISocketService
{
    public async Task<HubConnection?> CreateConnectionToHub(string url)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();
        return connection;
    }

    public async Task TransmitMessagesToConsole(HubConnection connection)
    {
        var queueMessages = new Queue<string>();
        connection.On<string, string>("Receive",
            (message, user) => { Console.WriteLine($"message from {user}:{message}"); });
    }

    public async Task SendMessage(HubConnection connection, string message)
    {
        await connection.InvokeAsync("Send", message, "Console User");
    }
}