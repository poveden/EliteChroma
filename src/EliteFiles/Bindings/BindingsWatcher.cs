﻿using EliteFiles.Internal;
using static EliteFiles.Internal.LogEventSource;

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

        private readonly EliteFileSystemWatcher? _customBindsWatcher;

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

            if (!_gameOptionsFolder.Bindings.Exists)
            {
                return;
            }

            string customBindingsPath = gameOptionsFolder.Bindings.FullName;

            _customBindsWatcher = new EliteFileSystemWatcher(customBindingsPath);
            _customBindsWatcher.Changed += Bindings_Changed;
        }

        /// <summary>
        /// Occurs when binding presets have changed.
        /// </summary>
        public event EventHandler<BindingPreset>? Changed;

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
            _customBindsWatcher?.Start();
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

            _customBindsWatcher?.Stop();
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

            _customBindsWatcher?.Dispose();
            _disposed = true;
        }

        private void Bindings_Changed(object? sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            IReadOnlyDictionary<BindingCategory, string> bindsFiles = FileOperations.RetryIfFailed(
                () => BindingPreset.FindActivePresetFiles(_gameInstallFolder, _gameOptionsFolder),
                x => x.Count != 0,
                _reloadRetries);

            if (bindsFiles.Count == 0)
            {
                Log.BindingsPresetFileResolutionFailed(_reloadRetries);
                return;
            }

            var uniquePresets = new Dictionary<string, BindingPreset>(StringComparer.Ordinal);

            foreach (string bindsFile in bindsFiles.Values)
            {
                if (uniquePresets.ContainsKey(bindsFile))
                {
                    continue;
                }

                BindingPreset? bindingPreset = FileOperations.RetryIfNull(
                    () => BindingPreset.FromFile(bindsFile),
                    _reloadRetries);

                if (bindingPreset == null)
                {
                    Log.BindingsFileCannotBeRead(bindsFile, _reloadRetries);
                    return;
                }

                uniquePresets.Add(bindsFile, bindingPreset);
            }

            var binds = bindsFiles.ToDictionary(kv => kv.Key, kv => uniquePresets[kv.Value]);

            var merged = BindingPreset.MergeFromCategories(binds);

            Log.BindingsRaisingChangedEvent();
            Changed?.Invoke(this, merged);
        }
    }
}
