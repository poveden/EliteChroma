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
        private const int _reloadRetries = 2;

        private readonly FileInfo _mainFile;
        private readonly FileInfo _overrideFile;

        private readonly EliteFileSystemWatcher _mainWatcher;
        private readonly EliteFileSystemWatcher _overrideWatcher;

        private bool _running;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsConfigWatcher"/> class
        /// with the given game installation folder and game options folder paths.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        public GraphicsConfigWatcher(GameInstallFolder gameInstallFolder, GameOptionsFolder gameOptionsFolder)
        {
            _ = GameInstallFolder.AssertValid(gameInstallFolder);
            _ = GameOptionsFolder.AssertValid(gameOptionsFolder);

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
            if (_running)
            {
                return;
            }

            Reload();

            _mainWatcher.Start();
            _overrideWatcher.Start();
            _running = true;
        }

        /// <summary>
        /// Stops watching for changes in the graphics configuration files.
        /// </summary>
        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _mainWatcher.Stop();
            _overrideWatcher.Stop();
            _running = false;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="GraphicsConfigWatcher"/>.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Stop();
            _mainWatcher.Dispose();
            _overrideWatcher.Dispose();
            _disposed = true;
        }

        private void GraphicsConfig_Changed(object sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            GraphicsConfig gcMain = FileOperations.RetryIfNull(
                () => GraphicsConfig.FromFile(_mainFile.FullName),
                _reloadRetries);

            GraphicsConfig gcOverride = FileOperations.RetryIfNull(
                () => GraphicsConfig.FromFile(_overrideFile.FullName),
                _reloadRetries);

            if (gcMain == null)
            {
                return;
            }

            if (gcOverride != null)
            {
                gcMain.OverrideWith(gcOverride);
            }

            Changed?.Invoke(this, gcMain);
        }
    }
}
