using FileWatcherBackups.Shared.Surface.Wrappers;

namespace FileWatcherBackups.Shared.Wrappers;

internal class FileSystemProvider : IFileSystemProvider
{
    public void CopyFile(string sourcePath, string destinationPath, bool overwrite)
        => File.Copy(sourcePath, destinationPath, overwrite);

    public void CreateDirectory(string path)
        => Directory.CreateDirectory(path);

    public void DeleteDirectory(string path, bool recurse)
        => Directory.Delete(path, recurse);

    public IEnumerable<string> EnumerateFilesInDirectory(string path)
        => Directory.EnumerateFiles(path);

    public IEnumerable<string> EnumerateSubdirectoriesInDirectory(string path)
        => Directory.EnumerateDirectories(path);
}
