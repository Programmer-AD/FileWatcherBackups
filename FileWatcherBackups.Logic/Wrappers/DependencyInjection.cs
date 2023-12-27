using FileWatcherBackups.Logic.Wrappers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Logic.Wrappers;

internal static class DependencyInjection
{
    public static void AddWrappers(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IFileSystemProvider, FileSystemProvider>();
        services.AddSingleton<IGuidFactory, GuidFactory>();
    }
}
