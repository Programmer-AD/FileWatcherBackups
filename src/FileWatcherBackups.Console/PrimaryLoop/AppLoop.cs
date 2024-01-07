using FileWatcherBackups.CommandHandling.Surface;
using FileWatcherBackups.CommandHandling.Surface.Commands.ForAutoBackupTask;
using FileWatcherBackups.Console.Infrastructure.Interfaces;
using FileWatcherBackups.Console.PrimaryLoop.InputProcessing;

namespace FileWatcherBackups.Console.Primary;

internal class AppLoop(
    ITextInput textInput,
    ITextOutput textOutput,
    IInputProcessor inputProcessor,
    ICommandHandler commandHandler)
    : IAppLoop
{
    public async Task RunAsync()
    {
        commandHandler.Handle(new StartAutoBackupTaskCommandRequest());

        while (true)
        {
            try
            {
                string input = await textInput.GetLineAsync();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    string output = inputProcessor.Process(input);

                    await textOutput.WriteLineAsync(output);
                    await textOutput.WriteLineAsync(string.Empty);
                }
            }
            catch (Exception exception)
            {
                await textOutput.WriteErrorLineAsync($"An error occured during input processing{Environment.NewLine}{exception}");
            }
        }
    }
}
