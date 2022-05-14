using EliteFiles.Internal;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Watches for changes in <see href="https://github.com/psychicEgg/EDHM">EDHM</see> configuration files and raises events accordingly.
    /// </summary>
    public sealed class EdhmConfigWatcher : IDisposable
    {
        private const int _reloadRetries = 2;

        private readonly FileInfo _d3dxIniFile;
        private readonly EliteFileSystemWatcher _d3dxIniWatcher;
        private readonly EliteFileSystemWatcher _includesWatcher;
        private readonly HashSet<string> _files;

        private bool _running;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdhmConfigWatcher"/> class
        /// with the given game installation folder path.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        public EdhmConfigWatcher(GameInstallFolder gameInstallFolder)
        {
            _ = GameInstallFolder.AssertValid(gameInstallFolder);

            _d3dxIniFile = gameInstallFolder.D3DXIni;

            _d3dxIniWatcher = new EliteFileSystemWatcher(_d3dxIniFile);
            _d3dxIniWatcher.Changed += D3DXIniWatcher_Changed;

            _includesWatcher = new EliteFileSystemWatcher(gameInstallFolder.FullName, "*.ini")
            {
                IncludeSubdirectories = true,
            };
            _includesWatcher.Changed += IncludesWatcher_Changed;

            _files = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Occurs when graphics configuration files have changed.
        /// </summary>
        public event EventHandler<EdhmConfig>? Changed;

        /// <summary>
        /// Starts watching for changes in EDHM configuration files.
        /// </summary>
        public void Start()
        {
            if (_running)
            {
                return;
            }

            Reload(true);

            _d3dxIniWatcher.Start();

            if (_files.Count != 0)
            {
                _includesWatcher.Start();
            }

            _running = true;
        }

        /// <summary>
        /// Stops watching for changes in EDHM configuration files.
        /// </summary>
        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _d3dxIniWatcher.Stop();
            _includesWatcher.Stop();
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
            _d3dxIniWatcher.Dispose();
            _includesWatcher.Dispose();
            _disposed = true;
        }

        private void D3DXIniWatcher_Changed(object? sender, FileSystemEventArgs e)
        {
            Reload(true);
        }

        private void IncludesWatcher_Changed(object? sender, FileSystemEventArgs e)
        {
            if (!_files.Contains(e.FullPath))
            {
                return;
            }

            Reload(false);
        }

        private void Reload(bool reconfigureIncludesWatcher)
        {
            EdhmConfig? edhmConfig = FileOperations.RetryIfNull(
                () => EdhmConfig.FromFile(_d3dxIniFile.FullName),
                _reloadRetries);

            if (reconfigureIncludesWatcher)
            {
                ReconfigureIncludesWatcher(edhmConfig);
            }

            if (edhmConfig == null)
            {
                return;
            }

            Changed?.Invoke(this, edhmConfig);
        }

        private void ReconfigureIncludesWatcher(EdhmConfig? edhmConfig)
        {
            _includesWatcher.Stop();
            _files.Clear();

            if (edhmConfig == null)
            {
                return;
            }

            foreach (string file in edhmConfig.Files)
            {
                _ = _files.Add(file);
            }

            // No point in watching the .ini file itself, as it's already being watched.
            _ = _files.Remove(_d3dxIniFile.FullName);

            if (_files.Count == 0)
            {
                return;
            }

            /*
             * Here we will examine the path of all included files, in the hope that
             * all of them are within a single folder so we can watch it.
             * If this is not the case (e.g. 2 or more subfolders, no subfolder at all),
             * we will default to watch the folder where the root .ini file is.
             **/

            _includesWatcher.Path = _files
                .Select(path => Path.GetDirectoryName(path)!)
                .Aggregate((shortest, path) =>
                {
                    if (path.StartsWith(shortest, StringComparison.OrdinalIgnoreCase))
                    {
                        return shortest;
                    }

                    if (shortest.StartsWith(path, StringComparison.OrdinalIgnoreCase))
                    {
                        return path;
                    }

                    return _d3dxIniFile.DirectoryName!;
                });

            if (_running)
            {
                _includesWatcher.Start();
            }
        }
    }
}
