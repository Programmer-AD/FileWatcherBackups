namespace FileWatcherBackups.Logic.FileBackups.Config;

public record class BackupProviderConfig(
    string PrimaryDirectoryPath,
    string BackupsDirectoryPath,
    int MaximumBackupCount,
    IEnumerable<string> ExcludeFilePatterns)
{
}
