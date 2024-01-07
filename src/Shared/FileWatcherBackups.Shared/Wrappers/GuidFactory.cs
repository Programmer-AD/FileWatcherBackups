using FileWatcherBackups.Shared.Surface.Wrappers;

namespace FileWatcherBackups.Shared.Wrappers;

internal class GuidFactory : IGuidFactory
{
    public Guid CreateGuid()
        => Guid.NewGuid();
}
