using FileWatcherBackups.CommandHandling.Surface.Internals;

namespace FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;

public record class RestoreBackupCommandRequest(
    string BackupIdPrefix)
    : ICommandRequest<Guid?>
{
}
