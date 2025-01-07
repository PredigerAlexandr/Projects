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

    public async Task<List<string>> GetMessagesAsync()
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "dev-queue", exclusive: false, autoDelete: false, durable: true);

        var consumer = new AsyncEventingBasicConsumer(channel);

        var result = new List<string>();

        consumer.Received += async (sender, m) => { result.Add(Encoding.UTF8.GetString(m.Body.ToArray())); };

        await channel.BasicConsumeAsync("dev-queue", true, consumer);

        return result;
    }

    public async IAsyncEnumerable<string> GetMessagesByDirectAsync(string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: "custom-exchange", type: ExchangeType.Direct);

        var queueName = (await channel.QueueDeclareAsync()).QueueName;

        await channel.QueueBindAsync(queue: queueName,
            exchange: "custom-exchange",
            routingKey: routingKey);

        var consumer = new AsyncEventingBasicConsumer(channel);

        var queueMessage = new Queue<string>();
        
        consumer.Received += async (sender, e) =>
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            queueMessage.Enqueue(message);
        };

        await channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine($"Subscribed to the queue '{queueName}'");

        while (true)
        {
            if (queueMessage.Count > 0)
            {
                yield return queueMessage.Dequeue();
            }
        }
    }
    
    public async IAsyncEnumerable<string> GetMessagesByTopicAsync(string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: "topic-exchange", type: ExchangeType.Topic);

        var queueName = (await channel.QueueDeclareAsync()).QueueName;

        await channel.QueueBindAsync(queue: queueName,
            exchange: "topic-exchange",
            routingKey: routingKey);

        var consumer = new AsyncEventingBasicConsumer(channel);

        var queueMessage = new Queue<string>();
        
        consumer.Received += async (sender, e) =>
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            queueMessage.Enqueue(message);
        };

        await channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine($"Subscribed to the queue '{queueName}'");

        while (true)
        {
            if (queueMessage.Count > 0)
            {
                yield return queueMessage.Dequeue();
            }
        }
    }
    
    public async IAsyncEnumerable<string> GetMessagesByFunoutAsync()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: "funout-exchange", type: ExchangeType.Fanout);

        var queueName = (await channel.QueueDeclareAsync()).QueueName;

        await channel.QueueBindAsync(queue: queueName,
            exchange: "funout-exchange",
            routingKey: string.Empty);

        var consumer = new AsyncEventingBasicConsumer(channel);

        var queueMessage = new Queue<string>();
        
        consumer.Received += async (sender, e) =>
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            queueMessage.Enqueue(message);
        };

        await channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine($"Subscribed to the queue '{queueName}'");

        while (true)
        {
            if (queueMessage.Count > 0)
            {
                yield return queueMessage.Dequeue();
            }
        }
    }

    public async Task NewMessageAsync()
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "dev-queue", exclusive: false, autoDelete: false, durable: true);

        var message = "You are wining " + new Random().Next(0, int.MaxValue) + "$";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("", "dev-queue", true, body);
    }

    public async Task NewDirectMessageAsync(string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchange: "custom-exchange", ExchangeType.Direct);

        var message = "You are wining " + new Random().Next(0, int.MaxValue) + $"$ / routing key:{routingKey}";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("custom-exchange", routingKey, true, body);
    }
    
    public async Task NewTopicMessageAsync(string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchange: "topic-exchange", ExchangeType.Topic);

        var message = "You are wining " + new Random().Next(0, int.MaxValue) + $"$ / routing key:{routingKey}";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("topic-exchange", routingKey, true, body);
    }
    
    public async Task NewFunoutMessageAsync()
    {
        var factory = new ConnectionFactory() { HostName = _brokerHostString };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.ExchangeDeclareAsync(exchange: "funout-exchange", ExchangeType.Fanout);

        var message = "You are wining " + new Random().Next(0, int.MaxValue) + $"$ / routing key:fanout";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("funout-exchange", String.Empty, true, body);
    }
}