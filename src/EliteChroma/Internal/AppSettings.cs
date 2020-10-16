using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EliteChroma.Internal
{
    internal sealed class AppSettings
    {
        private AppSettings()
        {
        }

        public string GameInstallFolder { get; set; }

        public string GameOptionsFolder { get; set; }

        public string JournalFolder { get; set; }

        public bool ForceEnUSKeyboardLayout { get; set; }

        public static AppSettings Load(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<AppSettings>(json);
            }
            catch (IOException) {}
            catch (JsonException) {}

            return BuildDefaultSettings();
        }

        public bool IsValid()
        {
            if (GameInstallFolder == null || !new EliteFiles.GameInstallFolder(GameInstallFolder).IsValid)
            {
                return false;
            }

            if (GameOptionsFolder == null || !new EliteFiles.GameOptionsFolder(GameOptionsFolder).IsValid)
            {
                return false;
            }

            if (JournalFolder == null || !new EliteFiles.JournalFolder(JournalFolder).IsValid)
            {
                return false;
            }

            return true;
        }

        public void Save(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }

        public static string GetDefaultPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = Path.Combine(appData, "EliteChroma");
            return Path.Combine(folder, "Settings.json");
        }

        private static AppSettings BuildDefaultSettings()
        {
            var gameInstall = EliteFiles.GameInstallFolder.DefaultPaths
                .Concat(EliteFiles.GameInstallFolder.GetAlternatePaths())
                .FirstOrDefault(Directory.Exists)
                ?? EliteFiles.GameInstallFolder.DefaultPaths.First();
            var gameOptions = EliteFiles.GameOptionsFolder.DefaultPath;
            var journal = EliteFiles.JournalFolder.DefaultPath;

            return new AppSettings
            {
                GameInstallFolder = gameInstall,
                GameOptionsFolder = gameOptions,
                JournalFolder = journal,
            };
        }
    }
}
