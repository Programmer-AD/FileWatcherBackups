using FileWatcherBackups.Logic.FileBackups.Implementations;
using FileWatcherBackups.Logic.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.FileBackups;

internal static class DependencyInjection
{
    public static void AddFileBackups(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var configProvider = services.GetRequiredService<IConfigProvider>();

            var config = configProvider.GetBackupProviderConfig();

            return config;
        });

        services.AddSingleton<IBackupProvider, BackupProvider>();
    }
}
