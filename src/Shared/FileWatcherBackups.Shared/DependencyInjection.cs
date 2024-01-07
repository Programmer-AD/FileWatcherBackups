using FileWatcherBackups.Shared.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Shared;

public static class DependencyInjection
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddWrappers();
    }
}
