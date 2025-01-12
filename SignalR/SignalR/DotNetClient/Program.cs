using DotNetClient;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utility.Interfaces;
using Utility.Models;
using Utility.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory
                .GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName)
                .FullName)
            .AddJsonFile("appsettings.json");
        
        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();
        
        services.AddScoped<ISocketService, SocketService>();
        services.AddScoped<Execute>();
        services.Configure<HubSettings>(options =>
            options.UrlConnection = configuration.GetConnectionString("HubUrlConnection"));
        
        var serviceProvider = services.BuildServiceProvider();
        
        var workClass = serviceProvider.GetService<Execute>();
        
        await workClass.Run();
    }
}