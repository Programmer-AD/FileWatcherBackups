namespace FileWatcherBackups.Logic.FileUpdates.Config;

public record class FileUpdateWaiterConfig(
    string WatchPath,
    int WaitEventGroupingMilliseconds,
    IEnumerable<string> ExcludeFilePatterns)
{
}
