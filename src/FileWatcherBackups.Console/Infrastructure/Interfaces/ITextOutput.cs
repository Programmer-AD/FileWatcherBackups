namespace FileWatcherBackups.Console.Infrastructure.Interfaces;

public interface ITextOutput
{
    Task WriteLineAsync(string message);

    Task WriteErrorLineAsync(string error);
}
