using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EliteFiles.Internal;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Watches for changes in Elite:Dangerous binding preset files and raises events accordingly.
    /// </summary>
    public sealed class BindingsWatcher : IDisposable
    {
        private readonly string _presetsPath;
        private readonly string _customBindingsPath;

        private readonly EliteFileSystemWatcher _startPresetWatcher;
        private readonly EliteFileSystemWatcher _customBindsWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingsWatcher"/> class
        /// with the given game installation folder and game options folder paths.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        public BindingsWatcher(string gameInstallFolder, string gameOptionsFolder)
        {
            if (!Folders.IsValidGameInstallFolder(gameInstallFolder))
            {
                throw new ArgumentException($"'{gameInstallFolder}' is not a valid Elite:Dangerous game install folder.", nameof(gameInstallFolder));
            }

            if (!Folders.IsValidGameOptionsFolder(gameOptionsFolder))
            {
                throw new ArgumentException($"'{gameOptionsFolder}' is not a valid Elite:Dangerous game options folder.", nameof(gameOptionsFolder));
            }

            _presetsPath = Path.Combine(gameInstallFolder, Folders.ControlSchemesFolder);
            _customBindingsPath = Path.Combine(gameOptionsFolder, Folders.GameOptionsBindingsFolder);

            _startPresetWatcher = new EliteFileSystemWatcher(_customBindingsPath, Folders.GameOptionsBindingsStartPresetFile);
            _startPresetWatcher.Changed += Bindings_Changed;

            _customBindsWatcher = new EliteFileSystemWatcher(_customBindingsPath);
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
            Reload();
            _startPresetWatcher.Start();
        }

        /// <summary>
        /// Stops watching for changes in the binding preset files.
        /// </summary>
        public void Stop()
        {
            _startPresetWatcher.Stop();
            _customBindsWatcher.Stop();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="BindingsWatcher"/>.
        /// </summary>
        public void Dispose()
        {
            _startPresetWatcher.Dispose();
            _customBindsWatcher.Dispose();
        }

        private static string TryGetBindingsFilePath(string path, string bindsName)
        {
            var matches =
                from file in Directory.EnumerateFiles(path, $"{bindsName}.*")
                let filename = Path.GetFileName(file)
                let m = Regex.Match(filename, @"(?:\.(\d\.\d))?\.binds$")
                where m.Success
                orderby Version.Parse(m.Groups[1].Length != 0 ? m.Groups[1].Value : "1.0") descending
                select file;

            return matches.FirstOrDefault();
        }

        private void Bindings_Changed(object sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            string bindsName;

            using (var fs = File.Open(Path.Combine(_customBindingsPath, Folders.GameOptionsBindingsStartPresetFile), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    bindsName = sr.ReadToEnd();
                }
            }

            var bindsFile = TryGetBindingsFilePath(_customBindingsPath, bindsName);

            var isCustom = bindsFile != null;

            if (!isCustom)
            {
                bindsFile = TryGetBindingsFilePath(_presetsPath, bindsName);
            }

            var bindingPreset = BindingPreset.FromFile(bindsFile);

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
