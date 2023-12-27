using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class MessageProvider : IMessageProvider
{
    public string GetApplicationStarted()
        => "Application started";

    public string GetBackupCreated(Guid backupId)
        => $"Backup created; Id = {backupId:N}";

    public string GetBackupNotFoundByIdPrefix()
        => "Backup that has such id prefix was not found";

    public string GetBackupRestoredSuccessfully()
        => "Backup restored successfully";

    public string GetError(Exception ex)
        => $"Error occured! Details:{Environment.NewLine}{ex}";

    public string GetHelp()
        =>
@"CLI Help
help - shows this message
create - immediatly creates backup
list - shows list of backups that you have
restore {id prefix} - restores latest backup wich have specified id prefix";

    public string GetUnknownCommand()
        => "Unknown command! Type \"help\" to get list of available commands";
}
