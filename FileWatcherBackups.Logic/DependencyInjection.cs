using FileWatcherBackups.Logic.CommandHandling;
using FileWatcherBackups.Logic.FileBackups;
using FileWatcherBackups.Logic.FileUpdates;
using FileWatcherBackups.Logic.MainLogic;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic;

public static class DependencyInjection
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddFileBackups();
        services.AddFileUpdates();
        services.AddCommandHandling();

        services.AddSingleton<IAppLogic, AppLogic>();
    }
}
