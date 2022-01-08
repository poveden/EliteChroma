using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TestUtils
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Shared test code.")]
    internal sealed class TestFolder : IDisposable
    {
        private readonly DirectoryInfo _di;

        private bool _disposed;

        public TestFolder()
            : this(null)
        {
        }

        public TestFolder(string? templatePath)
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            _di = Directory.CreateDirectory(path);

            if (templatePath != null)
            {
                var srcDir = new DirectoryInfo(templatePath);
                Copy(srcDir, _di);
            }
        }

        public string Name => _di.FullName;

        public string Resolve(string relativePath)
        {
            return Path.Combine(Name, relativePath);
        }

        public string[] ResolveFiles(string searchPattern)
        {
            return Directory.GetFiles(Name, searchPattern, SearchOption.AllDirectories);
        }

        public string ReadText(string path)
        {
            return File.ReadAllText(Resolve(path));
        }

        public void WriteText(string path, string contents, bool append = false)
        {
            using var sw = new StreamWriter(Resolve(path), append);
            sw.Write(contents);
        }

        public void DeleteFile(string path)
        {
            File.Delete(Resolve(path));
        }

        public int DeleteFiles(string searchPattern)
        {
            string[] files = ResolveFiles(searchPattern);

            foreach (string file in files)
            {
                File.Delete(file);
            }

            return files.Length;
        }

        public void DeleteFolder(string path)
        {
            Directory.Delete(Resolve(path), true);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _di?.Delete(true);
            _disposed = true;
        }

        private static void Copy(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (var subdir in source.EnumerateDirectories())
            {
                Copy(subdir, target.CreateSubdirectory(subdir.Name));
            }

            foreach (var file in source.EnumerateFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }
    }
}
