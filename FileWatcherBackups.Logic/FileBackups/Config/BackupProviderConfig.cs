namespace FileWatcherBackups.Logic.FileBackups.Config;

public record class BackupProviderConfig(
    string PrimaryFolderPath,
    string BackupsFolderPath,
    int MaximumBackupCount,
    IEnumerable<string> ExcludeFilePatterns)
{
}
