using FileWatcherBackups.Logic.CommandHandling.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.CommandHandling;

internal static class DependencyInjection
{
    public static void AddCommandHandling(this IServiceCollection services)
    {
        services.AddSingleton<ICommandHandler, CommandHandler>();
    }
}
