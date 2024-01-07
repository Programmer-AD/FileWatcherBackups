using FileWatcherBackups.Logic.Backups.Utility;
using FileWatcherBackups.Logic.Surface;
using FileWatcherBackups.Logic.Surface.Config;
using FileWatcherBackups.Logic.Surface.Models;
using FileWatcherBackups.Logic.Utility;
using FileWatcherBackups.Shared.Surface.Wrappers;

namespace FileWatcherBackups.Logic.Backups.Implementations;

internal class BackupManager(
    BackupConfig config,
    IGuidFactory guidFactory,
    IDateTimeProvider dateTimeProvider,
    IFileSystemProvider fileSystemProvider)
    : IBackupManager
{
    private readonly object lockObjectForCreate = new();
    private readonly object lockObjectForRemove = new();

    public Guid CreateBackup()
    {
        lock (lockObjectForCreate)
        {
            var backupId = guidFactory.CreateGuid();

            string destinationDirectoryPath = CreateDestinationDirectory(backupId);

            PerformPartialCopy(config.PrimaryDirectoryPath, destinationDirectoryPath);

            lock (lockObjectForRemove)
            {
                EnsureBackupLimitNotExceed();
            }

            return backupId;
        }
    }

    public IEnumerable<BackupInfo> GetBackups()
    {
        if (!Directory.Exists(config.BackupsDirectoryPath))
        {
            yield break;
        }

        lock (lockObjectForRemove)
        {
            var directoryPaths = Directory.EnumerateDirectories(config.BackupsDirectoryPath);

            foreach (string directoryPath in directoryPaths)
            {
                string? directoryName = Path.GetFileName(directoryPath);

                if (BackupDirectoryNameProvider.TryParseDirectoryName(directoryName, out var backupDirectoryNameInfo))
                {
                    yield return new BackupInfo(backupDirectoryNameInfo.BackupId, backupDirectoryNameInfo.CreationDate, directoryPath);
                }
            }
        }
    }

    public void Restore(BackupInfo backupInfo)
    {
        lock (lockObjectForRemove)
        {
            PerformPartialCopy(backupInfo.DirectoryPath, config.PrimaryDirectoryPath);
        }
    }

    private string CreateDestinationDirectory(Guid backupId)
    {
        DateTime creationDate = dateTimeProvider.GetNow();
        string directoryName = BackupDirectoryNameProvider.GenerateDirectoryName(new(backupId, creationDate));
        string destinationDirectoryPath = Path.Combine(config.BackupsDirectoryPath, directoryName);

        fileSystemProvider.CreateDirectory(destinationDirectoryPath);

        return destinationDirectoryPath;
    }

    private void PerformPartialCopy(
        string sourceParentDirectoryPath,
        string destinationParentDirectoryPath,
        string? sourceRootDirectoryPath = null)
    {
        if (string.IsNullOrEmpty(sourceRootDirectoryPath))
        {
            sourceRootDirectoryPath = sourceParentDirectoryPath;
        }

        fileSystemProvider.CreateDirectory(destinationParentDirectoryPath);

        PerformCopyInner(
            fileSystemProvider.EnumerateFilesInDirectory(sourceParentDirectoryPath),
            sourceRootDirectoryPath,
            destinationParentDirectoryPath,
            performCopyAction: (sourceFilePath, destinationFilePath)
                => fileSystemProvider.CopyFile(sourceFilePath, destinationFilePath, overwrite: true));

        PerformCopyInner(
            fileSystemProvider.EnumerateSubdirectoriesInDirectory(sourceParentDirectoryPath),
            sourceRootDirectoryPath,
            destinationParentDirectoryPath,
            performCopyAction: (sourceDirectoryPath, destinationDirectoryPath)
                => PerformPartialCopy(sourceDirectoryPath, destinationDirectoryPath, sourceRootDirectoryPath));
    }

    private void PerformCopyInner(
        IEnumerable<string> sourcePaths,
        string sourceRootDirectoryPath,
        string destinationParentDirectoryPath,
        Action<string, string> performCopyAction)
    {
        foreach (string sourcePath in sourcePaths)
        {
            string sourceRelativePath = Path.GetRelativePath(sourceRootDirectoryPath, sourcePath);

            if (NeedToCopy(sourceRelativePath))
            {
                string fileEntryName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(destinationParentDirectoryPath, fileEntryName);

                performCopyAction(sourcePath, destinationPath);
            }
        }
    }

    private bool NeedToCopy(string relativePath)
    {
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

    private void RemoveBackup(BackupInfo backupInfo)
    {
        fileSystemProvider.DeleteDirectory(backupInfo.DirectoryPath, recurse: true);
    }
}
