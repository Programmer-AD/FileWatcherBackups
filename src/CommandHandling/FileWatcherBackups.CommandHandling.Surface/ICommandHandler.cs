using FileWatcherBackups.CommandHandling.Surface.Internals;

namespace FileWatcherBackups.CommandHandling.Surface;

public interface ICommandHandler
{
    TResponse Handle<TResponse>(ICommandRequest<TResponse> request);
}
