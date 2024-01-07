using FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;
using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface;
using FileWatcherBackups.Logic.Surface.Models;

namespace FileWatcherBackups.CommandHandling.CommandExecutors.ForBackups;

public class ListBackupsCommandExecutor(
    IBackupManager backupManager)
    : ICommandExecutor<ListBackupsCommandRequest, IEnumerable<BackupInfo>>
{
    public IEnumerable<BackupInfo> Execute(ListBackupsCommandRequest _)
    {
        var backups = backupManager
            .GetBackups()
            .OrderByDescending(backupInfo => backupInfo.CreationTime)
            .AsEnumerable();

        return backups;
    }
}
