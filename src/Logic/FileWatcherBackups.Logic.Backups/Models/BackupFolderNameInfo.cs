namespace FileWatcherBackups.Logic.Backups.Models;

internal record struct BackupDirectoryNameInfo(
    Guid BackupId,
    DateTime CreationDate)
{
}
