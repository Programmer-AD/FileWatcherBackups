using FileWatcherBackups.Logic.FileWatching;
using FileWatcherBackups.Logic.Surface;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;

namespace FileWatcherBackups.Logic;

internal class AutoBackupTaskManager(
    IFileWatcher fileWatcher,
    IBackupManager backupManager,
    IAutoBackupTaskEventHandler taskEventHandler)
    : IAutoBackupTaskManager
{
    private CancellationTokenSource? cancellationTokenSource;
    private readonly object syncObject = new();

    public void Start()
    {
        lock (syncObject)
        {
            if (cancellationTokenSource != null)
            {
                return;
            }

            cancellationTokenSource = new();

            Task.Run(() => RunTaskLogicAsync(cancellationTokenSource.Token));
        }
    }

    public bool IsRunning
        => cancellationTokenSource != null;

    public void Stop()
    {
        lock (syncObject)
        {
            if (cancellationTokenSource == null)
            {
                return;
            }

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }

    private async Task RunTaskLogicAsync(CancellationToken cancellationToken)
    {
        await taskEventHandler.OnStartedAsync();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await fileWatcher.WaitForUpdateAsync(cancellationToken);

                Guid backupId = backupManager.CreateBackup();

                await taskEventHandler.OnBackupCreatedAsync(backupId);
            }
            catch (TaskCanceledException)
            {
                // Ignore
            }
            catch (Exception exception)
            {
                await taskEventHandler.OnExceptionAsync(exception);
            }
        }

        await taskEventHandler.OnStoppedAsync();
    }
}
