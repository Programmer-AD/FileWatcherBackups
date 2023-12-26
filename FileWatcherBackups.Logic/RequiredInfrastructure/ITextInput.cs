namespace FileWatcherBackups.Logic.RequiredInfrastructure;

public interface ITextInput
{
    Task<string?> ReadLineAsync();
}
