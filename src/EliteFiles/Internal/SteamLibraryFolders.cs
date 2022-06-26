using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace EliteFiles.Internal
{
    internal sealed class SteamLibraryFolders : ReadOnlyCollection<string>
    {
        private static readonly Regex _rxKeyValue = new Regex(@"^""[0-9]+""\s+""(.*?)""$");
        private static readonly SteamLibraryFolders _empty = new SteamLibraryFolders(Array.Empty<string>());

        // Reference: https://stackoverflow.com/questions/39557722/where-does-steam-store-library-directories/39557723#39557723
        private static readonly Lazy<string> _defaultPath = new Lazy<string>(() =>
        {
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            return Path.Combine(programFilesFolder, @"Steam\steamapps\libraryfolders.vdf");
        });

        private SteamLibraryFolders(IList<string> folders)
            : base(folders)
        {
        }

        public static string DefaultPath => _defaultPath.Value;

        public static SteamLibraryFolders FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return _empty;
            }

            using var sr = new StreamReader(path);

            if (sr.ReadLine()?.Trim() != "\"LibraryFolders\"")
            {
                return _empty;
            }

            if (sr.ReadLine()?.Trim() != "{")
            {
                return _empty;
            }

            var folders = new List<string>();

            string? line = sr.ReadLine()?.Trim();

            while (line is not null and not "}")
            {
                Match m = _rxKeyValue.Match(line);

                if (m.Success)
                {
                    folders.Add(m.Groups[1].Value.Replace(@"\\", @"\", StringComparison.Ordinal));
                }

                line = sr.ReadLine()?.Trim();
            }

            return new SteamLibraryFolders(folders);
        }
    }
}
