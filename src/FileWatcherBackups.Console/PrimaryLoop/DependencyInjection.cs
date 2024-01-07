using FileWatcherBackups.Console.PrimaryLoop.InputProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Console.Primary;

internal static class DependencyInjection
{
    public static void AddPrimaryLoop(this IServiceCollection services)
    {
        services.AddSingleton<IAppLoop, AppLoop>();

        services.AddSingleton<IInputProcessor, InputProcessor>();
    }
}
