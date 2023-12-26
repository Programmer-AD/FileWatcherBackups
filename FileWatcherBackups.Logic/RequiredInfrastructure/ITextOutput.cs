namespace FileWatcherBackups.Logic.RequiredInfrastructure;

public interface ITextOutput
{
    Task WriteLineAsync(string text);
}
