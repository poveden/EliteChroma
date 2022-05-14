namespace EliteFiles.Internal
{
    internal static class FileInfoExtensions
    {
        public static FileInfo GetFile(this DirectoryInfo directory, string fileName)
        {
            return new FileInfo(Path.Combine(directory.FullName, fileName));
        }

        public static DirectoryInfo GetDirectory(this DirectoryInfo directory, string path)
        {
            return new DirectoryInfo(Path.Combine(directory.FullName, path));
        }
    }
}
