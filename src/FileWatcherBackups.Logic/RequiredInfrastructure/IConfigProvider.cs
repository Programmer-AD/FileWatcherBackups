using FileWatcherBackups.Logic.FileBackups.Config;
using FileWatcherBackups.Logic.FileUpdates.Config;

namespace FileWatcherBackups.Logic.RequiredInfrastructure;

public interface IConfigProvider
{
    BackupProviderConfig GetBackupProviderConfig();

    FileUpdateWaiterConfig GetFileUpdateWaiterConfig();
}
