using System.Text.Json;
using FileWatcherBackups.Console.Infrastructure.Models;
using FileWatcherBackups.Logic.FileBackups.Config;
using FileWatcherBackups.Logic.FileUpdates.Config;
using FileWatcherBackups.Logic.RequiredInfrastructure;

namespace FileWatcherBackups.Console.Infrastructure.Implementations;

internal class ConfigProvider : IConfigProvider
{
    private const string ConfigFileName = "configuration.json";

    public BackupProviderConfig GetBackupProviderConfig()
    {
        var generalConfig = GetGeneralAppConfig();

        var result = new BackupProviderConfig(
            generalConfig.WatchFolderPath,
            generalConfig.BackupsFolderPath,
            generalConfig.MaximumBackupCount,
            generalConfig.ExcludeFilePatterns);

        return result;
    }

    public FileUpdateWaiterConfig GetFileUpdateWaiterConfig()
    {
        var generalConfig = GetGeneralAppConfig();

        var result = new FileUpdateWaiterConfig(
            generalConfig.WatchFolderPath,
            generalConfig.WaitEventGroupingMilliseconds,
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
