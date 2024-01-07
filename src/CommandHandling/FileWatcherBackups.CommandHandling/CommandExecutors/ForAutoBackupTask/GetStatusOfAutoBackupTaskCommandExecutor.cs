using FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForAutoBackupTask;

public class GetStatusOfAutoBackupTaskCommandExecutor(
    IAutoBackupTaskManager autoBackupTaskManager)
    : ICommandExecutor<GetStatusOfAutoBackupTaskCommandRequest, bool>
{
    public bool Execute(GetStatusOfAutoBackupTaskCommandRequest _)
    {
        bool status = autoBackupTaskManager.IsRunning;
        return status;
    }
}

