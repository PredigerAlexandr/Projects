using Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Publisher.Interfaces;
using Publisher.Services;
using Publisher.Utility;


namespace Publisher;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName)
            .AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IBrokerService, BrokerService>();
        serviceCollection.AddScoped<Execute>();
        serviceCollection.Configure<BrokerOptions>(options =>
            options.BrokerHost = configuration.GetConnectionString("BrokerHostConnection"));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var workClass = serviceProvider.GetService<Execute>();

        workClass?.RunAsync().GetAwaiter().GetResult();
    }
}