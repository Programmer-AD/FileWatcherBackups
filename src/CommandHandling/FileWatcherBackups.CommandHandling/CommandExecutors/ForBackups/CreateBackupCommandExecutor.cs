using FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForBackups;

public class CreateBackupCommandExecutor(
        IBackupManager backupManager)
    : ICommandExecutor<CreateBackupCommandRequest, Guid>
{
    public Guid Execute(CreateBackupCommandRequest _)
    {
        Guid backupId = backupManager.CreateBackup();
        return backupId;
    }
}
