using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using EliteChroma.Internal;
using EliteChroma.Tests.Internal;
using EliteFiles;
using Xunit;

namespace EliteChroma.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class AppSettingsTest
    {
        private const string _appSettingsPath = @"TestFiles\Settings.json";

        [Fact]
        public void GetsTheDefaultPath()
        {
            var path = AppSettings.GetDefaultPath();
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
        }

        [Fact]
        public void LoadsSettingsFromAFile()
        {
            var settings = AppSettings.Load(_appSettingsPath);

            Assert.Equal(@"C:\ELITE_INSTALL_FOLDER", settings.GameInstallFolder);
            Assert.Equal(@"C:\GAME_OPTIONS_FOLDER", settings.GameOptionsFolder);
            Assert.Equal(@"C:\JOURNAL_FOLDER", settings.JournalFolder);
        }

        [Fact]
        public void ValidatesSettingsValues()
        {
            using var tf = new TestFolder("TestFiles");
            var settingsFile = PrepareValidSettingsFile(tf);

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
            var settingsFile = tf.Resolve(Path.GetFileName(_appSettingsPath));

            var settings = AppSettings.Load(settingsFile);
            settings.GameOptionsFolder = @"C:\ANOTHER_GAME_OPTIONS_FOLDER";
            settings.Save(settingsFile);

            var settings2 = AppSettings.Load(settingsFile);

            Assert.Equal(settings.GameInstallFolder, settings2.GameInstallFolder);
            Assert.Equal(settings.GameOptionsFolder, settings2.GameOptionsFolder);
            Assert.Equal(settings.JournalFolder, settings2.JournalFolder);
        }

        internal static string PrepareValidSettingsFile(TestFolder baseFolder)
        {
            var settingsFile = baseFolder.Resolve("Settings.json");

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
