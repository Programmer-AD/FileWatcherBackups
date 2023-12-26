namespace FileWatcherBackups.Console.Infrastructure.Models;

public record class GeneralAppConfig(
    string WatchFolderPath,
    string BackupsFolderPath,
    int WaitEventGroupingMilliseconds,
    int MaximumBackupCount,
    IEnumerable<string> ExcludeFilePatterns)
{
}
