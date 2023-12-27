using FileWatcherBackups.Logic.FileBackups.Models;

namespace FileWatcherBackups.Logic.FileBackups.Utility;

internal static class BackupDirectoryNameProvider
{
    private const string BackupNameSeparator = "__";
    private const string DoubleDotsReplacor = "_d";

    public static string GenerateDirectoryName(BackupDirectoryNameInfo backupDirectoryNameInfo)
    {
        (Guid backupId, DateTime creationTime) = backupDirectoryNameInfo;

        string rawName = $"{backupId:N}{BackupNameSeparator}{creationTime:O}";

        string result = rawName.Replace(":", DoubleDotsReplacor);
        return result;
    }

    public static bool TryParseDirectoryName(string? directoryName, out BackupDirectoryNameInfo backupDirectoryNameInfo)
    {
        backupDirectoryNameInfo = default;

        if (string.IsNullOrEmpty(directoryName))
        {
            return false;
        }

        string refubrishedName = directoryName.Replace(DoubleDotsReplacor, ":");
        string[] nameParts = refubrishedName.Split(BackupNameSeparator);

        if (nameParts.Length != 2)
        {
            return false;
        }

        if (!Guid.TryParse(nameParts[0], out var backupId)
            || !DateTime.TryParse(nameParts[1], out var creationDate))
        {
            return false;
        }

        backupDirectoryNameInfo = new(backupId, creationDate);
        return true;
    }
}
