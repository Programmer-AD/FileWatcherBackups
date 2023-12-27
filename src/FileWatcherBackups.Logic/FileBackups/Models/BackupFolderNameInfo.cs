namespace FileWatcherBackups.Logic.FileBackups.Models;

internal record struct BackupDirectoryNameInfo(
    Guid BackupId,
    DateTime CreationDate)
{
}
