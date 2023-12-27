namespace FileWatcherBackups.Console.Infrastructure.Models;

public record class GeneralAppConfig(
    string WatchDirectoryPath,
    string BackupsDirectoryPath,
    int FileWatchEventGroupingMilliseconds,
    int MaximumBackupCount,
    IEnumerable<string> ExcludeFilePatterns)
{
}
