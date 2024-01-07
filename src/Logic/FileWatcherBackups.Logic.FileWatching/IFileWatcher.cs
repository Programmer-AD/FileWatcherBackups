namespace FileWatcherBackups.Logic.FileWatching;

public interface IFileWatcher
{
    Task WaitForUpdateAsync(CancellationToken cancellationToken);
}
