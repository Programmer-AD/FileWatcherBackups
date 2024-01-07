namespace FileWatcherBackups.Logic.Surface.RequiredInfrastructure;

public interface IAutoBackupTaskEventHandler
{
    Task OnStartedAsync();

    Task OnBackupCreatedAsync(Guid backupId);

    Task OnExceptionAsync(Exception exception);

    Task OnStoppedAsync();
}
