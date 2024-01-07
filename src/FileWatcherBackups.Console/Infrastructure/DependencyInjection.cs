using FileWatcherBackups.Console.Infrastructure.ForConsole;
using FileWatcherBackups.Console.Infrastructure.Implementations;
using FileWatcherBackups.Console.Infrastructure.Interfaces;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Console.Infrastructure;

internal static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAutoBackupTaskEventHandler, AutoBackupTaskEventHandler>();
        services.AddSingleton<IConfigProvider, ConfigProvider>();

        services.AddSingleton<ITextInput, TextInput>();
        services.AddSingleton<ITextOutput, TextOutput>();
    }
}
