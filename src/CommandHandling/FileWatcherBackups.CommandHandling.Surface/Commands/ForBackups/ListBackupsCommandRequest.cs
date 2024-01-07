using FileWatcherBackups.CommandHandling.Surface.Internals;
using FileWatcherBackups.Logic.Surface.Models;

namespace FileWatcherBackups.CommandHandling.Surface.Commands.ForBackups;

public record class ListBackupsCommandRequest
    : ICommandRequest<IEnumerable<BackupInfo>>
{
}
