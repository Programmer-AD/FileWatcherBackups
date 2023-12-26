namespace FileWatcherBackups.Logic.CommandHandling;

public interface ICommandHandler
{
    string Handle(string command);
}
