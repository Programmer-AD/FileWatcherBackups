using FileWatcherBackups.CommandHandling.Surface.Internals;

namespace FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;

public record class StartAutoBackupTaskCommandRequest
    : ICommandRequest<bool>
{
}
