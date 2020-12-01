using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EliteFiles.Graphics;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class GraphicsTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";

        private const string _mainFile = "GraphicsConfiguration.xml";
        private const string _overrideFile = @"Graphics\GraphicsConfigurationOverride.xml";

        private const string _minimalConfig = "<?xml version=\"1.0\" ?>\r\n<GraphicsConfig />";

        private readonly GameInstallFolder _gif;
        private readonly GameOptionsFolder _gof;

        public GraphicsTest()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
            _gof = new GameOptionsFolder(_gameOptionsFolder);
        }

        [Fact]
        public void DeserializesGraphicsConfigFiles()
        {
            var config = GraphicsConfig.FromFile(_gif.GraphicsConfiguration.FullName);

            Assert.NotNull(config);
            Assert.Equal(3, config.GuiColour.Count);

            var gc = config.GuiColour.Default;
            Assert.NotNull(gc);
            Assert.Equal("Standard", gc.LocalisationName);

            Assert.Equal(1, gc.MatrixRed.Red);
            Assert.Equal(1, gc.MatrixGreen.Green);
            Assert.Equal(1, gc.MatrixBlue.Blue);

            var cm = (IRgbTransformMatrix)gc;

            Assert.Equal(1, cm[0, 0]);
            Assert.Equal(1, cm[1, 1]);
            Assert.Equal(1, cm[2, 2]);
        }

        [Fact]
        public void ReturnsNullWhenTheGraphicsConfigFileIsMissingOrEmpty()
        {
            using var dir = new TestFolder();
            var status = GraphicsConfig.FromFile(dir.Resolve("NonExistingFile.xml"));
            Assert.Null(status);

            dir.WriteText("EmptyConfig.xml", string.Empty);

            status = GraphicsConfig.FromFile(dir.Resolve("EmptyConfig.xml"));
            Assert.Null(status);
        }

        [Fact]
        public void DeserializesMinimalGraphicsConfigFiles()
        {
            using var dir = new TestFolder();
            dir.WriteText("MinimalConfig.xml", _minimalConfig);

            var status = GraphicsConfig.FromFile(dir.Resolve("MinimalConfig.xml"));
            Assert.Null(status.GuiColour);
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using var watcher = new GraphicsConfigWatcher(_gif, _gof);
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Null(config.GuiColour.Default.LocalisationName);
        }

        [Fact]
        public async Task WatchesForChangesInTheGraphicsConfigurationFiles()
        {
            using var dirMain = new TestFolder(_gif.FullName);
            using var dirOpts = new TestFolder(_gof.FullName);
            using var watcher = new GraphicsConfigWatcher(new GameInstallFolder(dirMain.Name), new GameOptionsFolder(dirOpts.Name));
            watcher.Start();

            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatchesForChangesInTheGraphicsConfigurationFiles));

            var xmlMain = dirMain.ReadText(_mainFile);

            var config = await evs.WaitAsync(() => dirMain.WriteText(_mainFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Null(config);

            config = await evs.WaitAsync(() => dirMain.WriteText(_mainFile, xmlMain)).ConfigureAwait(false);
            Assert.Equal(0, config.GuiColour.Default[0, 0]);

            config = await evs.WaitAsync(() => dirOpts.WriteText(_overrideFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Equal(1, config.GuiColour.Default[0, 0]);

            config = await evs.WaitAsync(() => dirOpts.WriteText(_overrideFile, _minimalConfig)).ConfigureAwait(false);
            Assert.Equal(1, config.GuiColour.Default[0, 0]);
        }

        [Fact]
        public async Task WatcherToleratesEmptyGraphicsConfigurationOverrideFiles()
        {
            using var dirOpts = new TestFolder(_gof.FullName);
            dirOpts.WriteText(_overrideFile, string.Empty);

            using var watcher = new GraphicsConfigWatcher(_gif, new GameOptionsFolder(dirOpts.Name));
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Standard", config.GuiColour.Default.LocalisationName);
        }

        [Fact]
        public void WatcherThrowsWhenTheGameInstallFolderIsNotAValidInstallFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new GraphicsConfigWatcher(null, _gof); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new GraphicsConfigWatcher(new GameInstallFolder(@"TestFiles"), _gof); });
            Assert.Contains("' is not a valid Elite:Dangerous game install folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherThrowsWhenTheGameOptionsFolderIsNotAValidOptionsFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new GraphicsConfigWatcher(_gif, null); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new GraphicsConfigWatcher(_gif, new GameOptionsFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous game options folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new GraphicsConfigWatcher(_gif, _gof);

            bool IsRunning() => watcher.GetPrivateField<bool>("_running");

            Assert.False(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new GraphicsConfigWatcher(_gif, _gof);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
