using FileWatcherBackups.Logic.FileBackups;

namespace FileWatcherBackups.Logic.CommandHandling.Implementations;

internal class CommandHandler(
    IBackupProvider backupProvider)
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
            _ => "Unknown command"
        };

        return output;
    }

    private static string HandleHelp()
    {
        const string commandNameDelimiter = " - ";
        const string helpDescription = $"help{commandNameDelimiter}shows this message\r\n";
        const string createDescription = $"create{commandNameDelimiter}manually creates backup\r\n";
        const string listDescription = $"list{commandNameDelimiter}shows list of backups that you have\r\n";
        const string restoreDescription = $"restore {{id prefix}}{commandNameDelimiter}restores specified backup\r\n";

        const string result = $"{helpDescription}{createDescription}{listDescription}{restoreDescription}";

        return result;
    }

    private string HandleBackupCreate()
    {
        Guid backupId = backupProvider.CreateBackup();

        return $"Created backup with id {backupId:N}";
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
            return "Backup such id prefix was not found";
        }

        backupProvider.Restore(backup);
        return "Backup restored successfully";
    }
}
