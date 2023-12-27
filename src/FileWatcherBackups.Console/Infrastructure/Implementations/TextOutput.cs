using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class TextOutput : ITextOutput
{
    public async Task WriteLineAsync(string text)
    {
        await System.Console.Out.WriteLineAsync(text);
    }
}
