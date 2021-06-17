using System;
using System.IO;
using System.Linq;
using EliteChroma.Core;
using EliteChroma.Internal.Json;
using Newtonsoft.Json;

namespace EliteChroma.Internal
{
    internal sealed class AppSettings
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new AppSettingsContractResolver(),
            Formatting = Formatting.Indented,
        };

        private AppSettings()
        {
        }

        public string? GameInstallFolder { get; set; }

        public string? GameOptionsFolder { get; set; }

        public string? JournalFolder { get; set; }

        public bool DetectGameInForeground { get; set; } = true;

        public bool ForceEnUSKeyboardLayout { get; set; }

        public ChromaColors Colors { get; } = new ChromaColors();

        public static AppSettings Load(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<AppSettings>(json, _settings) ?? BuildDefaultSettings();
            }
            catch (IOException)
            {
            }
            catch (JsonException)
            {
            }

            return BuildDefaultSettings();
        }

        public static string GetDefaultPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "EliteChroma");
            return Path.Combine(folder, "Settings.json");
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
            string? dirName = Path.GetDirectoryName(path);
            if (dirName != null)
            {
                _ = Directory.CreateDirectory(dirName);
            }

            string json = JsonConvert.SerializeObject(this, _settings);
            File.WriteAllText(path, json);
        }

        private static AppSettings BuildDefaultSettings()
        {
            string gameInstall = EliteFiles.GameInstallFolder.DefaultPaths
                .Concat(EliteFiles.GameInstallFolder.GetAlternatePaths())
                .FirstOrDefault(Directory.Exists)
                ?? EliteFiles.GameInstallFolder.DefaultPaths.First();
            string gameOptions = EliteFiles.GameOptionsFolder.DefaultPath;
            string journal = EliteFiles.JournalFolder.DefaultPath;

            return new AppSettings
            {
                GameInstallFolder = gameInstall,
                GameOptionsFolder = gameOptions,
                JournalFolder = journal,
            };
        }
    }
}
