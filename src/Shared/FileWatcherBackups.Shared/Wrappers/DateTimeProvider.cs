using FileWatcherBackups.Shared.Surface.Wrappers;

namespace FileWatcherBackups.Shared.Wrappers;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow()
        => DateTime.Now;
}
