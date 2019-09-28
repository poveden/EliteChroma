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
        private readonly string _mainFile;
        private readonly string _overrideFile;

        private readonly EliteFileSystemWatcher _mainWatcher;
        private readonly EliteFileSystemWatcher _overrideWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsConfigWatcher"/> class
        /// with the given game installation folder and game options folder paths.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        public GraphicsConfigWatcher(string gameInstallFolder, string gameOptionsFolder)
        {
            if (!Folders.IsValidGameInstallFolder(gameInstallFolder))
            {
                throw new ArgumentException($"'{gameInstallFolder}' is not a valid Elite:Dangerous game install folder.", nameof(gameInstallFolder));
            }

            if (!Folders.IsValidGameOptionsFolder(gameOptionsFolder))
            {
                throw new ArgumentException($"'{gameOptionsFolder}' is not a valid Elite:Dangerous game options folder.", nameof(gameOptionsFolder));
            }

            _mainFile = Path.Combine(gameInstallFolder, Folders.GraphicsConfigMainFile);
            _mainWatcher = new EliteFileSystemWatcher(gameInstallFolder, Folders.GraphicsConfigMainFile);
            _mainWatcher.Changed += GraphicsConfig_Changed;

            var overridePath = Path.Combine(gameOptionsFolder, Folders.GameOptionsGraphicsFolder);
            _overrideFile = Path.Combine(overridePath, Folders.GraphicsConfigOverrideFile);
            _overrideWatcher = new EliteFileSystemWatcher(overridePath, Folders.GraphicsConfigOverrideFile);
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
            var gcMain = GraphicsConfig.FromFile(_mainFile);
            var gcOverride = GraphicsConfig.FromFile(_overrideFile);

            if (gcMain == null)
            {
                return;
            }

            if (gcOverride == null && File.Exists(_overrideFile))
            {
                return;
            }

            gcMain.OverrideWith(gcOverride);

            Changed?.Invoke(this, gcMain);
        }
    }
}
