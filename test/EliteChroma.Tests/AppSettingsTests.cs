using System;
using System.Collections.Generic;
using System.IO;
using Colore.Data;
using EliteChroma.Core;
using EliteChroma.Internal;
using EliteChroma.Tests.Internal;
using EliteFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace EliteChroma.Tests
{
    public class AppSettingsTests
    {
        private const string _appSettingsPath = @"TestFiles\Settings.json";

        [Fact]
        public void GetsTheDefaultPath()
        {
            string path = AppSettings.GetDefaultPath();
            Assert.EndsWith(@"\EliteChroma\Settings.json", path, StringComparison.Ordinal);
        }

        [Fact]
        public void ReturnsDefaultSettingsWhenTheFileDoesNotExist()
        {
            var settings = AppSettings.Load(@"this-path\does-not-exist.json");

            Assert.NotNull(settings.GameInstallFolder);
            Assert.Equal(GameOptionsFolder.DefaultPath, settings.GameOptionsFolder);
            Assert.Equal(JournalFolder.DefaultPath, settings.JournalFolder);
        }

        [Fact]
        public void ReturnsDefaultSettingsWhenTheFileIsCorrupt()
        {
            using var tf = new TestFolder();
            tf.WriteText("Malformed.json", "{ \"text\": ");

            var settings = AppSettings.Load(tf.Resolve("Malformed.json"));

            Assert.NotNull(settings.GameInstallFolder);
            Assert.Equal(GameOptionsFolder.DefaultPath, settings.GameOptionsFolder);
            Assert.Equal(JournalFolder.DefaultPath, settings.JournalFolder);
            Assert.NotNull(settings.Colors);
            Assert.Equal(0.5, settings.Colors.DeviceDimBrightness);
            Assert.Equal(Color.Red, settings.Colors.HardpointsToggle);
        }

        [Fact]
        public void LoadsSettingsFromAFile()
        {
            var settings = AppSettings.Load(_appSettingsPath);

            Assert.Equal(@"C:\ELITE_INSTALL_FOLDER", settings.GameInstallFolder);
            Assert.Equal(@"C:\GAME_OPTIONS_FOLDER", settings.GameOptionsFolder);
            Assert.Equal(@"C:\JOURNAL_FOLDER", settings.JournalFolder);
            Assert.True(settings.DetectGameInForeground);
            Assert.True(settings.ForceEnUSKeyboardLayout);
            Assert.NotNull(settings.Colors);
            Assert.Equal(0.12, settings.Colors.DeviceDimBrightness);
            Assert.Equal(Color.FromRgb(0x34AB12), settings.Colors.HardpointsToggle);
        }

        [Fact]
        public void ValidatesSettingsValues()
        {
            using var tf = new TestFolder("TestFiles");
            string settingsFile = PrepareValidSettingsFile(tf);

            var actions = new List<Action<AppSettings>>
            {
                x => x.GameInstallFolder = null,
                x => x.GameInstallFolder = "invalid-folder",
                x => x.GameOptionsFolder = null,
                x => x.GameOptionsFolder = "invalid-folder",
                x => x.JournalFolder = null,
                x => x.JournalFolder = "invalid-folder",
            };

            foreach (var action in actions)
            {
                var settings = AppSettings.Load(settingsFile);
                action(settings);
                Assert.False(settings.IsValid());
            }
        }

        [Fact]
        public void SaveSettingsToAFile()
        {
            using var tf = new TestFolder(Path.GetDirectoryName(_appSettingsPath));
            string settingsFile = tf.Resolve(Path.GetFileName(_appSettingsPath));

            var settings = AppSettings.Load(settingsFile);
            Assert.True(settings.DetectGameInForeground);
            Assert.True(settings.ForceEnUSKeyboardLayout);
            Assert.NotNull(settings.Colors);
            Assert.Equal(0.12, settings.Colors.DeviceDimBrightness);
            Assert.Equal(Color.FromRgb(0x34AB12), settings.Colors.HardpointsToggle);

            settings.GameOptionsFolder = @"C:\ANOTHER_GAME_OPTIONS_FOLDER";
            settings.DetectGameInForeground = false;
            settings.ForceEnUSKeyboardLayout = false;
            settings.Colors.DeviceDimBrightness = 0.77;
            settings.Colors.HardpointsToggle = Color.FromRgb(0xA0110A);
            settings.Save(settingsFile);

            var settings2 = AppSettings.Load(settingsFile);

            Assert.Equal(settings.GameInstallFolder, settings2.GameInstallFolder);
            Assert.Equal(settings.GameOptionsFolder, settings2.GameOptionsFolder);
            Assert.Equal(settings.JournalFolder, settings2.JournalFolder);
            Assert.False(settings.DetectGameInForeground);
            Assert.False(settings.ForceEnUSKeyboardLayout);
            Assert.Equal(settings.Colors.DeviceDimBrightness, settings2.Colors.DeviceDimBrightness);
            Assert.Equal(settings.Colors.HardpointsToggle, settings2.Colors.HardpointsToggle);
        }

        [Fact]
        public void AddDefaultColorsWhenMissingFromTheSettingsFile()
        {
            using var tf = new TestFolder(Path.GetDirectoryName(_appSettingsPath));
            string settingsFile = tf.Resolve(Path.GetFileName(_appSettingsPath));

            var jo = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(settingsFile))!;
            jo.Remove("Colors");
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(jo));

            var settings = AppSettings.Load(settingsFile);

            Assert.NotNull(settings.Colors);
        }

        [Fact]
        public void InvalidBrightnessValuesFromJsonFallBackToDefaultValue()
        {
            var ccDefaults = new ChromaColors();

            using var tf = new TestFolder(Path.GetDirectoryName(_appSettingsPath));
            string settingsFile = tf.Resolve(Path.GetFileName(_appSettingsPath));

            var jo = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(settingsFile))!;
            jo["Colors"]!["DeviceDimBrightness"] = "NOT-A-NUMBER";
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(jo));

            var settings = AppSettings.Load(settingsFile);

            Assert.Equal(ccDefaults.DeviceDimBrightness, settings.Colors.DeviceDimBrightness);
        }

        [Theory]
        [InlineData(123)]
        [InlineData("NOT-A-COLOR")]
        public void InvalidColorValuesFromJsonFallBackToDefaultValue(object faultyValue)
        {
            var ccDefaults = new ChromaColors();

            using var tf = new TestFolder(Path.GetDirectoryName(_appSettingsPath));
            string settingsFile = tf.Resolve(Path.GetFileName(_appSettingsPath));

            var jo = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(settingsFile))!;
            jo["Colors"]!["HardpointsToggle"] = JToken.FromObject(faultyValue);
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(jo));

            var settings = AppSettings.Load(settingsFile);

            Assert.Equal(ccDefaults.HardpointsToggle, settings.Colors.HardpointsToggle);
        }

        internal static string PrepareValidSettingsFile(TestFolder baseFolder)
        {
            string settingsFile = baseFolder.Resolve("Settings.json");

            var settings = AppSettings.Load(settingsFile);
            settings.GameInstallFolder = baseFolder.Resolve("GameRoot");
            settings.GameOptionsFolder = baseFolder.Resolve("GameOptions");
            settings.JournalFolder = baseFolder.Resolve("Journal");
            settings.Save(settingsFile);

            Assert.True(settings.IsValid());

            return settingsFile;
        }
    }
}
