namespace FileWatcherBackups.Logic.Surface.Config;

public record class BackupConfig(
    string PrimaryDirectoryPath,
    string BackupsDirectoryPath,
    int MaximumBackupCount,
    IEnumerable<string> ExcludeFilePatterns)
{
}
