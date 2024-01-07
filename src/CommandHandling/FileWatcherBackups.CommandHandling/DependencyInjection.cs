using FileWatcherBackups.CommandHandling.CommandExecutors;
using FileWatcherBackups.CommandHandling.Surface;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.CommandHandling;

public static class DependencyInjection
{
    public static void AddCommandHandling(this IServiceCollection services)
    {
        services.AddCommandExecutors();

        services.AddSingleton<ICommandHandler, CommandHandler>();
    }
}
