using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class TextInput : ITextInput
{
    public async Task<string?> ReadLineAsync()
    {
        var result = await System.Console.In.ReadLineAsync();
        return result;
    }
}
