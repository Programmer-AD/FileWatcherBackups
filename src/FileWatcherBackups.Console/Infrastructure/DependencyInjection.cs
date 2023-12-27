using FileWatcherBackups.Console.Infrastructure.Implementations;
using FileWatcherBackups.Logic.RequiredInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Console.Infrastructure;

internal static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IConfigProvider, ConfigProvider>();
        services.AddSingleton<ITextInput, TextInput>();
        services.AddSingleton<ITextOutput, TextOutput>();
        services.AddSingleton<IMessageProvider, MessageProvider>();
    }
}
