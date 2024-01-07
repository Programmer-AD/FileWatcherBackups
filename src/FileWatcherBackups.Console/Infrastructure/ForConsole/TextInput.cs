using FileWatcherBackups.Console.Infrastructure.Interfaces;

namespace FileWatcherBackups.Console.Infrastructure.ForConsole;

internal class TextInput : ITextInput
{
    public async Task<string> GetLineAsync()
    {
        string result = await System.Console.In.ReadLineAsync() ?? string.Empty;
        return result;
    }
}
