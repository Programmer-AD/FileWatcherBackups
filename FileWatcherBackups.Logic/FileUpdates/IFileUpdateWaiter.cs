namespace FileWatcherBackups.Logic.FileUpdates;

public interface IFileUpdateWaiter
{
    Task WaitForUpdateAsync();
}
