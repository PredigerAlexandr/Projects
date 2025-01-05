using System.Text;
using Microsoft.Extensions.Options;
using Publisher.Interfaces;
using Publisher.Utility;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Publisher.Services;

public class BrokerService : IBrokerService
{
    private readonly string _brokerHostString;

    public BrokerService(IOptions<BrokerOptions> options)
    {
        _brokerHostString = options.Value.BrokerHost ?? "localhost";
    }

    public async Task NewMessageAsync()
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue:"dev-queue", exclusive:false, autoDelete:false, durable:true);
        
        var message = "You are wining " + new Random().Next(0, int.MaxValue) + "$";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("", "dev-queue",  true, body);
    }

    public async Task<List<string>> GetMessagesAsync()
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue:"dev-queue", exclusive:false, autoDelete:false, durable:true);

        var consumer = new AsyncEventingBasicConsumer(channel);

        var result = new List<string>();

        consumer.Received += async (sender, m) =>
        {
            result.Add(Encoding.UTF8.GetString(m.Body.ToArray()));
        };

        await channel.BasicConsumeAsync("dev-queue", true, consumer);

        return result;
    }
}