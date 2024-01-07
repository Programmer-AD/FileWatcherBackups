namespace FileWatcherBackups.Shared.Surface.Wrappers;

public interface IFileSystemProvider
{
    void CopyFile(string sourcePath, string destinationPath, bool overwrite);

    void CreateDirectory(string path);

    void DeleteDirectory(string path, bool recurse);

    IEnumerable<string> EnumerateFilesInDirectory(string path);

    IEnumerable<string> EnumerateSubdirectoriesInDirectory(string path);
}
