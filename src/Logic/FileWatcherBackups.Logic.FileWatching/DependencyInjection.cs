using FileWatcherBackups.Logic.FileWatching.Implementations;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.FileWatching;

public static class DependencyInjection
{
    public static void AddFileWatching(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var configProvider = services.GetRequiredService<IConfigProvider>();

            var config = configProvider.GetFileWatchingConfig();
            return config;
        });

        services.AddSingleton<IFileWatcher, FileWatcher>();
    }
}
