namespace FileWatcherBackups.Logic.Wrappers.Implementations;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow()
        => DateTime.Now;
}
