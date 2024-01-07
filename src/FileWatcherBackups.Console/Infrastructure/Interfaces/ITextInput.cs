namespace FileWatcherBackups.Console.Infrastructure.Interfaces;

public interface ITextInput
{
    Task<string> GetLineAsync();
}
