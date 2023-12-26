namespace FileWatcherBackups.Logic.FileBackups.Models;

public record class BackupInfo(
    Guid Id,
    DateTime CreationTime,
    string FolderPath)
{
    public override string ToString()
        => $"{Id:N} {CreationTime:O}";
}
