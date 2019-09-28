using System;
using System.IO;
using System.Text.Json;

namespace EliteChroma.Internal
{
    internal sealed class AppSettings
    {
        private AppSettings()
        {
        }

        public static AppSettings Default { get; } = Load();

        public string GameInstallFolder { get; set; }

        public string GameOptionsFolder { get; set; }

        public string JournalFolder { get; set; }

        private static AppSettings Load()
        {
            var path = GettAppSettingsPath();

            try
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<AppSettings>(json);
            }
            catch (FileNotFoundException)
            {
                return new AppSettings();
            }
        }

        public void Save()
        {
            var path = GettAppSettingsPath();
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }

        private static string GettAppSettingsPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = Path.Combine(appData, "EliteChroma");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "Settings.json");
        }
    }
}
