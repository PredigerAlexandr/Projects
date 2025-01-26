using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Utility.Interfaces;

namespace Utility.Services;

public class BrokerService : IBrokerService
{
    public async Task NewTopicAsync(string topicName)
    {
        var config = new AdminClientConfig
        {
            BootstrapServers = "localhost:51261"
        };
        var _adminClient = new AdminClientBuilder(config).Build();

        var metadata = _adminClient.GetMetadata(TimeSpan.FromSeconds(10));

        var topicIsExist = metadata.Topics.Select(x => x.Topic).Any(x => x == topicName);

        if (topicIsExist)
        {
            return;
        }

        var topicSpecification = new TopicSpecification
        {
            Name = topicName
        };

        _adminClient.CreateTopicsAsync(new[] { topicSpecification }).GetAwaiter().GetResult();
    }

    public async Task NewMessageAsync(string message, string topicName)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:51261"
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var result = await producer.ProduceAsync(topicName, new Message<Null, string>
                    {
                        Value = message + $" / Number:{i}"
                    });

                    Console.WriteLine($"Message sent to topic {result.Topic} with offset {result.Offset}");
                }
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
            }
        }
    }

    public async Task<IEnumerable<string>> GetMessagesAsync(string topicName)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:51261",
            GroupId = "kafka-dotnet-getting-started",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var result = new List<string>();

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe(topicName);
            try
            {
                try
                {
                    while (true)
                    {
                        var message = consumer.Consume(TimeSpan.FromMilliseconds(100));
                        if (message == null)
                        {
                            break;
                        }

                        result.Add(message.Message.Value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to get messages: {e.Message}");
                }
            }
            finally
            {
                consumer.Close();
            }
        }

        return result;
    }
}