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
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost");
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