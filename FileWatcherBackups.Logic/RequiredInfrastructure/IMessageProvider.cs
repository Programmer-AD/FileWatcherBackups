
namespace FileWatcherBackups.Logic.RequiredInfrastructure;

public interface IMessageProvider
{
    string GetApplicationStarted();

    string GetBackupCreated(Guid backupId);

    string GetBackupNotFoundByIdPrefix();

    string GetBackupRestoredSuccessfully();

    string GetError(Exception ex);

    string GetHelp();

    string GetUnknownCommand();
}
