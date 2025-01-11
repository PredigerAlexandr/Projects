using Consumer;
using Microsoft.Extensions.DependencyInjection;
using Utility.Interfaces;
using Utility.Services;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddScoped<IBrokerService, BrokerService>();
        services.AddScoped<Execute>();

        var serviceProvider = services.BuildServiceProvider();

        var workClass = serviceProvider.GetService<Execute>();

        workClass.Run().GetAwaiter().GetResult();
    }
}