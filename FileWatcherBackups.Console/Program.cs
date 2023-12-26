using FileWatcherBackups.Console.Infrastructure;
using FileWatcherBackups.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Console;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            using var serviceProvider = GetServiceProvider();

            var appLogic = serviceProvider.GetRequiredService<IAppLogic>();

            await appLogic.RunAppLoopAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Critical exception:{Environment.NewLine}{ex}");
            System.Console.ReadLine();
        }
    }

    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddLogic();
        services.AddInfrastructure();

        var provider = services.BuildServiceProvider();
        return provider;
    }
}
