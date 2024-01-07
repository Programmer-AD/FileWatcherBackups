using FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForAutoBackupTask;

public class StopAutoBackupTaskCommandExecutor(
    IAutoBackupTaskManager autoBackupTaskManager)
    : ICommandExecutor<StopAutoBackupTaskCommandRequest, bool>
{
    public bool Execute(StopAutoBackupTaskCommandRequest _)
    {
        autoBackupTaskManager.Stop();
        return true;
    }
}
