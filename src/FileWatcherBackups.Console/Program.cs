using FileWatcherBackups.CommandHandling;
using FileWatcherBackups.Console.Infrastructure;
using FileWatcherBackups.Console.Primary;
using FileWatcherBackups.Logic;
using FileWatcherBackups.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Console;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            await System.Console.Out.WriteLineAsync("Application starting...");

            using var serviceProvider = GetServiceProvider();

            var appLoop = serviceProvider.GetRequiredService<IAppLoop>();

            await appLoop.RunAsync();

        }
        catch (Exception exception)
        {
            await System.Console.Error.WriteLineAsync($"Critical exception:{Environment.NewLine}{exception}");
            await System.Console.In.ReadLineAsync();
        }
    }

    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddShared();
        services.AddLogic();
        services.AddCommandHandling();

        services.AddInfrastructure();
        services.AddPrimaryLoop();

        var provider = services.BuildServiceProvider();
        return provider;
    }
}
