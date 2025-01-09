using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq;
using Utilities.Constants;
using Utilities.Handlers;
using Utilities.Interfaces;
using Utilities.Models;
using Utilities.Services;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddMassTransit(x =>
        {
            //x.AddConsumer<MessageFanoutHandler>();
            x.AddConsumer<MessageDirectHandler>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost");

                // cfg.ReceiveEndpoint("fanout-queue", e =>
                //     e.ConfigureConsumer<MessageFanoutHandler>(context));
                
                cfg.ReceiveEndpoint("direct-queue", e =>
                    {
                        e.ConfigureConsumer<MessageDirectHandler>(context);
                        e.Bind<DirectMessage>(x =>
                            x.RoutingKey = RoutingKeyConstants.RoutingKey1);
                    }
                    );
            });
        });

        services.AddScoped<IBrokerService, BrokerService>();
        services.AddScoped<Execute>();

        var serviceProvider = services.BuildServiceProvider();

        var workClass = serviceProvider.GetService<Execute>();

        while (true)
        {
            workClass!.Run().GetAwaiter().GetResult();
            Thread.Sleep(1000);
        }
    }
}