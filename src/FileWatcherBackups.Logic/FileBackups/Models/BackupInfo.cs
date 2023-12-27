namespace FileWatcherBackups.Logic.FileBackups.Models;

public record class BackupInfo(
    Guid Id,
    DateTime CreationTime,
    string DirectoryPath)
{
    public override string ToString()
        => $"{Id:N} {CreationTime:O}";
}
