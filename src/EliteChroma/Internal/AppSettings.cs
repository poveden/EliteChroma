using System.Text.Json;
using EliteChroma.Core;
using EliteChroma.Internal.Json;

namespace EliteChroma.Internal
{
    internal sealed class AppSettings
    {
        private static readonly JsonSerializerOptions _writeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonChromaColorConverter(),
                new JsonBrightnessConverter(),
            },
        };

        private static readonly JsonSerializerOptions _readOptions = new JsonSerializerOptions(_writeOptions)
        {
            Converters =
            {
                new ChromaColorsConverter(),
            },
        };

        private ChromaColors _colors = new ChromaColors();

        public string? GameInstallFolder { get; set; }

        public string? GameOptionsFolder { get; set; }

        public string? JournalFolder { get; set; }

        public bool DetectGameInForeground { get; set; } = true;

        public bool ForceEnUSKeyboardLayout { get; set; }

        public ChromaColors Colors
        {
            get => _colors;
            set
            {
                // Reference: https://github.com/dotnet/runtime/issues/30258
                ArgumentNullException.ThrowIfNull(value);
                _colors = value;
            }
        }

        public static AppSettings Load(string path)
        {
            try
            {
                using FileStream fs = File.OpenRead(path);
                return JsonSerializer.Deserialize<AppSettings>(fs, _readOptions) ?? BuildDefaultSettings();
            }
            catch (IOException)
            {
                // Skip if the file cannot be read.
            }
            catch (JsonException)
            {
                // Skip if the file contains malformed JSON.
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

            using FileStream fs = File.Create(path);
            JsonSerializer.Serialize(fs, this, _writeOptions);
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
