using FileWatcherBackups.Logic.CommandHandling;
using FileWatcherBackups.Logic.FileBackups;
using FileWatcherBackups.Logic.FileUpdates;
using FileWatcherBackups.Logic.MainLogic;
using FileWatcherBackups.Logic.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic;

public static class DependencyInjection
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddWrappers();
        services.AddFileBackups();
        services.AddFileUpdates();
        services.AddCommandHandling();

        services.AddSingleton<IAppLogic, AppLogic>();
    }
}
