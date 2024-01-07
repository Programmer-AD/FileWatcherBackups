using FileWatcherBackups.Logic.Backups.Implementations;
using FileWatcherBackups.Logic.Surface;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.Backups;

public static class DependencyInjection
{
    public static void AddBackups(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var configProvider = services.GetRequiredService<IConfigProvider>();

            var config = configProvider.GetBackupConfig();
            return config;
        });

        services.AddSingleton<IBackupManager, BackupManager>();
    }
}
