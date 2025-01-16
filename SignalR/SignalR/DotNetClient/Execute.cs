using Microsoft.Extensions.Options;
using Utility.Interfaces;
using Utility.Models;

namespace DotNetClient;

public class Execute
{
    private readonly ISocketService _socketService;
    private readonly string _urlHubConnection;

    public Execute(ISocketService socketService, IOptions<HubSettings> settings)
    {
        _socketService = socketService;
        _urlHubConnection = settings.Value.UrlConnection;
    }

    public async Task Run()
    {
        var connection = await _socketService.CreateConnectionToHub(_urlHubConnection);

        _socketService.TransmitMessagesToConsole(connection!);

        await connection.StartAsync();

        while (true)
        {
            var consoleMessage = Console.ReadLine();
            await _socketService.SendMessage(connection!, consoleMessage!);
        }
    }
}