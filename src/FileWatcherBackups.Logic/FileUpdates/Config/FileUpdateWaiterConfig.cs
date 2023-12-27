namespace FileWatcherBackups.Logic.FileUpdates.Config;

public record class FileUpdateWaiterConfig(
    string WatchPath,
    int FileWatchEventGroupingMilliseconds,
    IEnumerable<string> ExcludeFilePatterns)
{
}
