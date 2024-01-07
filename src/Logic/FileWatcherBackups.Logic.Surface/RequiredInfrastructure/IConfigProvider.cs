using FileWatcherBackups.Logic.Surface.Config;

namespace FileWatcherBackups.Logic.Surface.RequiredInfrastructure;

public interface IConfigProvider
{
    BackupConfig GetBackupConfig();

    FileWatchingConfig GetFileWatchingConfig();
}
