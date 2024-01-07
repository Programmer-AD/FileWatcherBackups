using FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForBackups;

public class RestoreBackupCommandExecutor(
    IBackupManager backupManager)
    : ICommandExecutor<RestoreBackupCommandRequest, Guid?>
{
    public Guid? Execute(RestoreBackupCommandRequest request)
    {
        var backup = backupManager
            .GetBackups()
            .Where(backupInfo => backupInfo.Id.ToString().StartsWith(request.BackupIdPrefix))
            .OrderByDescending(backupInfo => backupInfo.CreationTime)
            .FirstOrDefault();

        if (backup == null)
        {
            return null;
        }

        backupManager.Restore(backup);
        return backup.Id;
    }
}
