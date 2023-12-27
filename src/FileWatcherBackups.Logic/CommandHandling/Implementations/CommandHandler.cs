using FileWatcherBackups.Logic.FileBackups;
using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Logic.CommandHandling.Implementations;

internal class CommandHandler(
    IBackupProvider backupProvider,
    IMessageProvider messageProvider)
    : ICommandHandler
{
    public string Handle(string command)
    {
        var commandParts = command.Split(' ');

        string output = commandParts[0] switch
        {
            "help" => HandleHelp(),
            "create" => HandleBackupCreate(),
            "list" => HandleBackupList(),
            "restore" => HandleBackupRestore(commandParts[1]),
            _ => messageProvider.GetUnknownCommand(),
        };

        return output;
    }

    private string HandleHelp()
        => messageProvider.GetHelp();

    private string HandleBackupCreate()
    {
        Guid backupId = backupProvider.CreateBackup();

        return messageProvider.GetBackupCreated(backupId);
    }

    private string HandleBackupList()
    {
        var backups = backupProvider.GetBackups();

        var backupInfos = backups
            .OrderByDescending(backupInfo => backupInfo.CreationTime)
            .Select(backupInfo => backupInfo.ToString());

        var result = string.Join(Environment.NewLine, backupInfos);
        return result;
    }

    private string HandleBackupRestore(string backupIdPrefix)
    {
        var backup = backupProvider
            .GetBackups()
            .Where(backupInfo => backupInfo.Id.ToString().StartsWith(backupIdPrefix))
            .OrderByDescending(backupInfo => backupInfo.CreationTime)
            .FirstOrDefault();

        if (backup == null)
        {
            return messageProvider.GetBackupNotFoundByIdPrefix();
        }

        backupProvider.Restore(backup);
        return messageProvider.GetBackupRestoredSuccessfully();
    }
}
