using FileWatcherBackups.Console.Infrastructure.Interfaces;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class AutoBackupTaskEventHandler(
    ITextOutput textOutput)
    : IAutoBackupTaskEventHandler
{
    public async Task OnBackupCreatedAsync(Guid backupId)
    {
        await textOutput.WriteLineAsync($"Background backup service created backup with id \"{backupId:N}\"");
    }

    public async Task OnExceptionAsync(Exception exception)
    {
        await textOutput.WriteErrorLineAsync($"Exception occured in background backup service!{Environment.NewLine}{exception}");
    }

    public async Task OnStartedAsync()
    {
        await textOutput.WriteLineAsync($"Background backup service started");
    }

    public async Task OnStoppedAsync()
    {
        await textOutput.WriteLineAsync($"Background backup service stopped");
    }
}
