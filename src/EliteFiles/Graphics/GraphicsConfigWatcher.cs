using System;
using System.IO;
using EliteFiles.Internal;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Watches for changes in Elite:Dangerous graphics configuration files and raises events accordingly.
    /// </summary>
    public sealed class GraphicsConfigWatcher : IDisposable
    {
        private readonly FileInfo _mainFile;
        private readonly FileInfo _overrideFile;

        private readonly EliteFileSystemWatcher _mainWatcher;
        private readonly EliteFileSystemWatcher _overrideWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsConfigWatcher"/> class
        /// with the given game installation folder and game options folder paths.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        public GraphicsConfigWatcher(GameInstallFolder gameInstallFolder, GameOptionsFolder gameOptionsFolder)
        {
            GameInstallFolder.AssertValid(gameInstallFolder);
            GameOptionsFolder.AssertValid(gameOptionsFolder);

            _mainFile = gameInstallFolder.GraphicsConfiguration;
            _mainWatcher = new EliteFileSystemWatcher(_mainFile);
            _mainWatcher.Changed += GraphicsConfig_Changed;

            _overrideFile = gameOptionsFolder.GraphicsConfigurationOverride;
            _overrideWatcher = new EliteFileSystemWatcher(_overrideFile);
            _overrideWatcher.Changed += GraphicsConfig_Changed;
        }

        /// <summary>
        /// Occurs when graphics configuration files have changed.
        /// </summary>
        public event EventHandler<GraphicsConfig> Changed;

        /// <summary>
        /// Starts watching for changes in the graphics configuration files.
        /// </summary>
        public void Start()
        {
            _mainWatcher.Stop();
            _overrideWatcher.Stop();

            Reload();

            _mainWatcher.Start();
            _overrideWatcher.Start();
        }

        /// <summary>
        /// Stops watching for changes in the graphics configuration files.
        /// </summary>
        public void Stop()
        {
            _mainWatcher.Stop();
            _overrideWatcher.Stop();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="GraphicsConfigWatcher"/>.
        /// </summary>
        public void Dispose()
        {
            Stop();
            _mainWatcher.Dispose();
            _overrideWatcher.Dispose();
        }

        private void GraphicsConfig_Changed(object sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            var gcMain = GraphicsConfig.FromFile(_mainFile.FullName);
            var gcOverride = GraphicsConfig.FromFile(_overrideFile.FullName);

            if (gcMain == null)
            {
                return;
            }

            if (gcOverride == null && _overrideFile.Exists)
            {
                return;
            }

            gcMain.OverrideWith(gcOverride);

            Changed?.Invoke(this, gcMain);
        }
    }
}
