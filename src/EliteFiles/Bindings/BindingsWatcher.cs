using System;
using System.IO;
using EliteFiles.Internal;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Watches for changes in Elite:Dangerous binding preset files and raises events accordingly.
    /// </summary>
    public sealed class BindingsWatcher : IDisposable
    {
        private const int _reloadRetries = 2;

        private readonly GameInstallFolder _gameInstallFolder;
        private readonly GameOptionsFolder _gameOptionsFolder;

        private readonly EliteFileSystemWatcher _startPresetWatcher;
        private readonly EliteFileSystemWatcher _customBindsWatcher;

        private bool _running;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingsWatcher"/> class
        /// with the given game installation folder and game options folder paths.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        public BindingsWatcher(GameInstallFolder gameInstallFolder, GameOptionsFolder gameOptionsFolder)
        {
            _gameInstallFolder = GameInstallFolder.AssertValid(gameInstallFolder);
            _gameOptionsFolder = GameOptionsFolder.AssertValid(gameOptionsFolder);

            var customBindingsPath = gameOptionsFolder.Bindings.FullName;

            _startPresetWatcher = new EliteFileSystemWatcher(customBindingsPath, gameOptionsFolder.BindingsStartPreset.Name);
            _startPresetWatcher.Changed += Bindings_Changed;

            _customBindsWatcher = new EliteFileSystemWatcher(customBindingsPath);
            _customBindsWatcher.Changed += Bindings_Changed;
        }

        /// <summary>
        /// Occurs when binding presets have changed.
        /// </summary>
        public event EventHandler<BindingPreset> Changed;

        /// <summary>
        /// Starts watching for changes in the binding preset files.
        /// </summary>
        public void Start()
        {
            if (_running)
            {
                return;
            }

            Reload();
            _startPresetWatcher.Start();
            _running = true;
        }

        /// <summary>
        /// Stops watching for changes in the binding preset files.
        /// </summary>
        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _startPresetWatcher.Stop();
            _customBindsWatcher.Stop();
            _running = false;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="BindingsWatcher"/>.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _startPresetWatcher.Dispose();
            _customBindsWatcher.Dispose();
            _disposed = true;
        }

        private void Bindings_Changed(object sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            var isCustom = false;

            var bindsFile = FileOperations.RetryIfNull(
                () => BindingPreset.FindActivePresetFile(_gameInstallFolder, _gameOptionsFolder, out isCustom),
                _reloadRetries);

            if (bindsFile == null)
            {
                return;
            }

            var bindingPreset = FileOperations.RetryIfNull(
                () => BindingPreset.FromFile(bindsFile),
                _reloadRetries);

            if (isCustom)
            {
                _customBindsWatcher.Filter = Path.GetFileName(bindsFile);
                _customBindsWatcher.Start();
            }

            if (bindingPreset == null)
            {
                return;
            }

            Changed?.Invoke(this, bindingPreset);
        }
    }
}
