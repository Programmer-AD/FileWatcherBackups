using FileWatcherBackups.Logic.FileBackups.Models;

namespace FileWatcherBackups.Logic.FileBackups;

public interface IBackupProvider
{
    Guid CreateBackup();

    IEnumerable<BackupInfo> GetBackups();

    void Restore(BackupInfo backupInfo);
}
