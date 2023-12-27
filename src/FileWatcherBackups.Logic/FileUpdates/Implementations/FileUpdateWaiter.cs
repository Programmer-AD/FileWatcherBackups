using FileWatcherBackups.Logic.FileUpdates.Config;
using FileWatcherBackups.Logic.Utility;

namespace FileWatcherBackups.Logic.FileUpdates.Implementations;

internal class FileUpdateWaiter(
    FileUpdateWaiterConfig config)
    : IFileUpdateWaiter
{
    private FileSystemWatcher? fileSystemWatcher = null;
    private TaskCompletionSource? taskCompletionSource = null;
    private CancellationTokenSource? waitFinishCancellation = null;

    public async Task WaitForUpdateAsync()
    {
        InitForWaitForUpdate();

        await taskCompletionSource!.Task;

        CleanupAfterWaitForUpdate();
    }

    private void InitForWaitForUpdate()
    {
        if (taskCompletionSource == null)
        {
            taskCompletionSource = new TaskCompletionSource();

            // If previous file system watcher was not yet disposed on cleanup
            fileSystemWatcher?.Dispose();

            fileSystemWatcher = new FileSystemWatcher()
            {
                Path = config.WatchPath,
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            fileSystemWatcher.Created += HandleFileSystemWatcherEvent;
            fileSystemWatcher.Changed += HandleFileSystemWatcherEvent;
            fileSystemWatcher.Renamed += HandleFileSystemWatcherEvent;
            fileSystemWatcher.Deleted += HandleFileSystemWatcherEvent;
        }
    }

    private void CleanupAfterWaitForUpdate()
    {
        taskCompletionSource = null;

        fileSystemWatcher?.Dispose();
        fileSystemWatcher = null;
    }

    private void HandleFileSystemWatcherEvent(object sender, FileSystemEventArgs eventArgs)
    {
        if (taskCompletionSource != null
            && IsWaitedEvent(eventArgs))
        {
            RescheduleWaitCompletion();
        }
    }

    private void RescheduleWaitCompletion()
    {
        waitFinishCancellation?.Cancel();

        waitFinishCancellation = new CancellationTokenSource();

        Task.Delay(config.FileWatchEventGroupingMilliseconds, waitFinishCancellation.Token)
            .ContinueWith(task =>
            {
                if (!task.IsCanceled)
                {
                    taskCompletionSource?.SetResult();
                }
            });
    }

    private bool IsWaitedEvent(FileSystemEventArgs eventArgs)
    {
        string relativePath = Path.GetRelativePath(config.WatchPath, eventArgs.FullPath);

        bool result = !PathPatternMatcher.CompliesToOneOfPatterns(relativePath, config.ExcludeFilePatterns);
        return result;
    }
}
