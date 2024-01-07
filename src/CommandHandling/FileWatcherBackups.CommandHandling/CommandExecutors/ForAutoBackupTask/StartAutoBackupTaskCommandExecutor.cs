using FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForAutoBackupTask;

public class StartAutoBackupTaskCommandExecutor(
    IAutoBackupTaskManager autoBackupTaskManager)
    : ICommandExecutor<StartAutoBackupTaskCommandRequest, bool>
{
    public bool Execute(StartAutoBackupTaskCommandRequest _)
    {
        autoBackupTaskManager.Start();
        return true;
    }
}
