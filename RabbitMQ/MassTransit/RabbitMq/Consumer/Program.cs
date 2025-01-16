using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Handlers;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<MessageHandler>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost");
                cfg.ReceiveEndpoint("fanout-queue",e =>
                {
                    e.ConfigureConsumer<MessageHandler>(context);
                });
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var busControl = serviceProvider.GetService<IBusControl>();

        await busControl.StartAsync();
        
        while(true){}

    }
}