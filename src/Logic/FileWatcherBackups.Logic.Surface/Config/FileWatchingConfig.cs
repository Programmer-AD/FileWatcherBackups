namespace FileWatcherBackups.Logic.Surface.Config;

public record class FileWatchingConfig(
    string WatchPath,
    int FileWatchEventGroupingMilliseconds,
    IEnumerable<string> ExcludeFilePatterns)
{
}
