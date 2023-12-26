using FileWatcherBackups.Logic.CommandHandling;
using FileWatcherBackups.Logic.FileBackups;
using FileWatcherBackups.Logic.FileUpdates;
using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Logic.MainLogic;

internal class AppLogic(
    IFileUpdateWaiter fileUpdateWaiter,
    IBackupProvider backupProvider,
    ICommandHandler commandHandler,
    ITextInput textInput,
    ITextOutput textOutput)
    : IAppLogic
{
    public async Task RunAppLoopAsync()
    {
        var fileBackupLoopTask = RunInfiniteLoop(MakeFileBackups);
        var commandHandlingLoopTask = RunInfiniteLoop(HandleInputCommands);

        await textOutput.WriteLineAsync("Application started");

        await Task.WhenAll(fileBackupLoopTask, commandHandlingLoopTask);
    }

    private async Task MakeFileBackups()
    {
        await fileUpdateWaiter.WaitForUpdateAsync();

        var backupInfo = backupProvider.CreateBackup();

        await textOutput.WriteLineAsync($"Create backup: {backupInfo}");
    }

    private async Task HandleInputCommands()
    {
        string? command = await textInput.ReadLineAsync();

        if (string.IsNullOrWhiteSpace(command))
        {
            return;
        }

        string output = commandHandler.Handle(command);

        if (!string.IsNullOrWhiteSpace(output))
        {
            await textOutput.WriteLineAsync(output);
        }
    }

    private Task RunInfiniteLoop(Func<Task> loopOperation)
    {
        var task = Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    await loopOperation();
                }
                catch (Exception ex)
                {
                    await textOutput.WriteLineAsync($"ERROR:{Environment.NewLine}{ex}");
                }
            }
        });

        return task;
    }
}
