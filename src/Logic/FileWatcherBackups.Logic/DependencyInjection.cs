using FileWatcherBackups.Logic.Backups;
using FileWatcherBackups.Logic.FileWatching;
using FileWatcherBackups.Logic.Surface;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic;

public static class DependencyInjection
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddBackups();
        services.AddFileWatching();

        services.AddSingleton<IAutoBackupTaskManager, AutoBackupTaskManager>();
    }
}
