using FileWatcherBackups.Logic.FileUpdates.Implementations;
using FileWatcherBackups.Logic.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.FileUpdates;

internal static class DependencyInjection
{
    public static void AddFileUpdates(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var configProvider = services.GetRequiredService<IConfigProvider>();

            var config = configProvider.GetFileUpdateWaiterConfig();
            return config;
        });

        services.AddSingleton<IFileUpdateWaiter, FileUpdateWaiter>();
    }
}
