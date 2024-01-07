namespace FileWatcherBackups.Logic.Surface;

public interface IAutoBackupTaskManager
{
    void Start();

    bool IsRunning { get; }

    void Stop();
}
