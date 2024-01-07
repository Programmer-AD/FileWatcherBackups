using System.Text.Json;
using FileWatcherBackups.Console.Infrastructure.Models;
using FileWatcherBackups.Logic.Surface.Config;
using FileWatcherBackups.Logic.Surface.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class ConfigProvider : IConfigProvider
{
    private const string ConfigFileName = "configuration.json";

    public BackupConfig GetBackupConfig()
    {
        var generalConfig = GetGeneralAppConfig();

        var result = new BackupConfig(
            generalConfig.WatchDirectoryPath,
            generalConfig.BackupsDirectoryPath,
            generalConfig.MaximumBackupCount,
            generalConfig.ExcludeFilePatterns);

        return result;
    }

    public FileWatchingConfig GetFileWatchingConfig()
    {
        var generalConfig = GetGeneralAppConfig();

        var result = new FileWatchingConfig(
            generalConfig.WatchDirectoryPath,
            generalConfig.FileWatchEventGroupingMilliseconds,
            generalConfig.ExcludeFilePatterns);

        return result;
    }

    private static GeneralAppConfig GetGeneralAppConfig()
    {
        using var fileStream = new FileStream(ConfigFileName, FileMode.Open, FileAccess.Read);

        var config = JsonSerializer.Deserialize<GeneralAppConfig>(fileStream);

        if (config == null)
        {
            throw new ApplicationException("App configuration is wrong");
        }

        return config;
    }
}
