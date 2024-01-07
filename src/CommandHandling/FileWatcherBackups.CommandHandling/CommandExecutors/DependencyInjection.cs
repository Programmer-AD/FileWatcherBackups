using FileWatcherBackups.CommandHandling.CommandExecutors.ForAutoBackupTask;
using FileWatcherBackups.CommandHandling.CommandExecutors.ForBackups;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.CommandHandling.CommandExecutors;

internal static class DependencyInjection
{
    public static void AddCommandExecutors(this IServiceCollection services)
    {
        services.AddCommandExecutor<GetStatusOfAutoBackupTaskCommandExecutor>();
        services.AddCommandExecutor<StartAutoBackupTaskCommandExecutor>();
        services.AddCommandExecutor<StopAutoBackupTaskCommandExecutor>();

        services.AddCommandExecutor<CreateBackupCommandExecutor>();
        services.AddCommandExecutor<ListBackupsCommandExecutor>();
        services.AddCommandExecutor<RestoreBackupCommandExecutor>();
    }

    private static void AddCommandExecutor<TExecutor>(this IServiceCollection services)
        where TExecutor : class, ICommandExecutor
    {
        services.AddSingleton<ICommandExecutor, TExecutor>();
    }
}
