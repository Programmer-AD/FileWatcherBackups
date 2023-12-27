namespace FileWatcherBackups.Logic.Wrappers.Implementations;

internal class GuidFactory : IGuidFactory
{
    public Guid CreateGuid()
        => Guid.NewGuid();
}
