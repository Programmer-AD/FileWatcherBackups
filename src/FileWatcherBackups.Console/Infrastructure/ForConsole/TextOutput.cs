using FileWatcherBackups.Console.Infrastructure.Interfaces;

namespace FileWatcherBackups.Console.Infrastructure.ForConsole;

internal class TextOutput : ITextOutput
{
    public async Task WriteErrorLineAsync(string error)
    {
        await System.Console.Error.WriteLineAsync(error);
    }

    public async Task WriteLineAsync(string message)
    {
        await System.Console.Out.WriteLineAsync(message);
    }
}
