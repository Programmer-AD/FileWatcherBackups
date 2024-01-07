using System.Text;
using FileWatcherBackups.CommandHandling.Surface;
using FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;
using FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;

namespace FileWatcherBackups.Console.PrimaryLoop.InputProcessing;

public class InputProcessor(
    ICommandHandler commandHandler)
    : IInputProcessor
{
    public string Process(string input)
    {
        string[] parts = input.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        string output = parts[0] switch
        {
            "help" => ProcessHelp(),
            "create" => ProcessBackupCreate(),
            "list" => ProcessBackupList(),
            "restore" => ProcessBackupRestore(parts),
            "bg-start" => ProcessBackgroundServiceStart(),
            "bg-status" => ProcessBackgroundServiceStatus(),
            "bg-stop" => ProcessBackgroundServiceStop(),
            _ => $"Unknown command \"{parts[0]}\"",
        };

        return output;
    }

    private static string ProcessHelp()
        => @"CLI Help
help - shows this message
create - immediatly creates backup
list - shows list of backups that you have
restore [id prefix] - restores latest backup wich have specified id prefix (if id prefix not sepcified - restores latest)
bg-start - starts backup autocreation background service
bg-status - get status of backup autocreation background service
bg-stop - stops backup autocreation background service";

    private string ProcessBackupCreate()
    {
        Guid backupId = commandHandler.Handle(new CreateBackupCommandRequest());

        return $"Created backup with id \"{backupId:N}\"";
    }

    private string ProcessBackupList()
    {
        var backups = commandHandler.Handle(new ListBackupsCommandRequest());

        var stringBuilder = new StringBuilder();

        stringBuilder
            .AppendLine("Backup list:")
            .AppendJoin(Environment.NewLine, backups.Select(backup => $"{backup.Id:N} {backup.CreationTime}"))
            .AppendLine();

        string output = stringBuilder.ToString();
        return output;
    }

    private string ProcessBackupRestore(string[] parts)
    {
        string idPrefix = parts.Length < 2
            ? string.Empty // Matches any, so would restore just latest backup
            : parts[1];

        Guid? restoredId = commandHandler.Handle(new RestoreBackupCommandRequest(idPrefix));

        string output = restoredId.HasValue
            ? $"Successfully restored backup with id \"{restoredId.Value}\""
            : "Restore failed, probably matching backup was not found";

        return output;
    }

    private string ProcessBackgroundServiceStart()
    {
        bool isStarted = commandHandler.Handle(new StartAutoBackupTaskCommandRequest());

        string output = isStarted
            ? "Start of backup autocreation background service [successfuly requested]"
            : "Start of backup autocreation background service [failed]";

        return output;
    }

    private string ProcessBackgroundServiceStatus()
    {
        bool isRunning = commandHandler.Handle(new GetStatusOfAutoBackupTaskCommandRequest());

        string output = isRunning
            ? "Backup autocreation background service is [running]"
            : "Backup autocreation background service is [stopped]";

        return output;
    }

    private string ProcessBackgroundServiceStop()
    {
        bool isStopped = commandHandler.Handle(new StopAutoBackupTaskCommandRequest());

        string output = isStopped
            ? "Stop of backup autocreation background service [successfuly requested]"
            : "Stop of backup autocreation background service [failed]";

        return output;
    }
}
