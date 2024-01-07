using FileWatcherBackups.Logic.Surface.Models;

namespace FileWatcherBackups.Logic.Surface;

public interface IBackupManager
{
    Guid CreateBackup();

    IEnumerable<BackupInfo> GetBackups();

    void Restore(BackupInfo backupInfo);
}
