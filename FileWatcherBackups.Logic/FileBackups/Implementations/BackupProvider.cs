using FileWatcherBackups.Logic.FileBackups.Config;
using FileWatcherBackups.Logic.FileBackups.Models;
using FileWatcherBackups.Logic.Utility;

namespace FileWatcherBackups.Logic.FileBackups.Implementations;

internal class BackupProvider(
    BackupProviderConfig config)
    : IBackupProvider
{
    private const string BackupNameSeparator = "__";
    private const string DoubleDotsReplacor = "_d";

    private readonly object lockObjectForCreate = new();
    private readonly object lockObjectForRemove = new();

    public Guid CreateBackup()
    {
        lock (lockObjectForCreate)
        {
            var backupId = Guid.NewGuid();

            string destinationFolderPath = CreateDestinationFolder(backupId);

            PerformPartialCopy(config.PrimaryFolderPath, destinationFolderPath);

            lock (lockObjectForRemove)
            {
                EnsureBackupLimitNotExceed();
            }

            return backupId;
        }
    }

    public IEnumerable<BackupInfo> GetBackups()
    {
        if (!Directory.Exists(config.BackupsFolderPath))
        {
            yield break;
        }

        lock (lockObjectForRemove)
        {
            var directoryPaths = Directory.EnumerateDirectories(config.BackupsFolderPath);

            foreach (string directoryPath in directoryPaths)
            {
                string? directoryName = Path.GetFileName(directoryPath)?
                    .Replace(DoubleDotsReplacor, ":");

                if (!string.IsNullOrEmpty(directoryName))
                {
                    var nameParts = directoryName.Split(BackupNameSeparator);

                    if (Guid.TryParse(nameParts[0], out var backupId)
                        && DateTime.TryParse(nameParts[1], out var date))
                    {
                        yield return new BackupInfo(backupId, date, directoryPath);
                    }
                }
            }
        }
    }

    public void Restore(BackupInfo backupInfo)
    {
        lock (lockObjectForRemove)
        {
            PerformPartialCopy(backupInfo.FolderPath, config.PrimaryFolderPath);
        }
    }

    private string CreateDestinationFolder(Guid backupId)
    {
        DateTime creationTime = DateTime.Now;
        string folderName = $"{backupId:N}{BackupNameSeparator}{creationTime:O}".Replace(":", DoubleDotsReplacor);
        string destinationFolderPath = Path.Combine(config.BackupsFolderPath, folderName);

        Directory.CreateDirectory(destinationFolderPath);

        return destinationFolderPath;
    }

    private void PerformPartialCopy(
        string sourceFolderPath,
        string destinationFolderPath,
        string? rootFolderPath = null)
    {
        if (string.IsNullOrEmpty(rootFolderPath))
        {
            rootFolderPath = sourceFolderPath;
        }

        Directory.CreateDirectory(destinationFolderPath);

        var filePaths = Directory.EnumerateFiles(sourceFolderPath);

        foreach (string sourceFilePath in filePaths)
        {
            if (NeedToCopy(rootFolderPath, sourceFilePath))
            {
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

                File.Copy(sourceFilePath, destinationFilePath, true);
            }
        }

        var directoryPaths = Directory.EnumerateDirectories(sourceFolderPath);

        foreach (string sourceSubdirectoryPath in directoryPaths)
        {
            if (NeedToCopy(rootFolderPath, sourceSubdirectoryPath))
            {
                string directoryName = Path.GetFileName(sourceSubdirectoryPath);
                string destinationSubFolderPath = Path.Combine(destinationFolderPath, directoryName);

                PerformPartialCopy(rootFolderPath, sourceSubdirectoryPath, destinationSubFolderPath);
            }
        }
    }

    private bool NeedToCopy(string rootFolderPath, string filePath)
    {
        string relativePath = Path.GetRelativePath(rootFolderPath, filePath);

        bool result = !PathPatternMatcher.CompliesToOneOfPatterns(relativePath, config.ExcludeFilePatterns);
        return result;
    }

    private void EnsureBackupLimitNotExceed()
    {
        var backupsToRemove = GetBackups()
            .OrderByDescending(x => x.CreationTime)
            .Skip(config.MaximumBackupCount);

        foreach (var backupToRemove in backupsToRemove)
        {
            RemoveBackup(backupToRemove);
        }
    }

    private static void RemoveBackup(BackupInfo backupInfo)
    {
        Directory.Delete(backupInfo.FolderPath, true);
    }
}
