using FileWatcherBackups.Shared.Surface.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcherBackups.Shared.Wrappers;

internal static class DependencyInjection
{
    public static void AddWrappers(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IFileSystemProvider, FileSystemProvider>();
        services.AddSingleton<IGuidFactory, GuidFactory>();
    }
}
