using EliteFiles.Internal;
using EliteFiles.Status.Internal;

namespace EliteFiles
{
    /// <summary>
    /// Represents an Elite:Dangerous game install folder.
    /// </summary>
    public sealed class GameInstallFolder
    {
        /// <summary>«GameInstallFolder»\EliteDangerous64.exe.</summary>
        private const string MainExecutableFileName = "EliteDangerous64.exe";

        /// <summary>«GameInstallFolder»\GraphicsConfiguration.xml.</summary>
        private const string GraphicsConfigMainFileName = "GraphicsConfiguration.xml";

        /// <summary>«GameInstallFolder»\ControlSchemes.</summary>
        private const string ControlSchemesFolderName = "ControlSchemes";

        /// <summary>«GameInstallFolder»\d3dx.ini.</summary>
        private const string D3DXIniFileName = "d3dx.ini";

        private static readonly string[] _knownProductFolderNames = new[]
        {
            "elite-dangerous-64",
            "elite-dangerous-odyssey-64",
        };

        private static readonly Lazy<string[]> _defaultPaths = new Lazy<string[]>(() =>
        {
            string programFilesX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string[] folders = new[]
            {
                Path.Combine(programFilesX86Folder, @"Frontier"),
                Path.Combine(programFilesFolder, @"Epic Games\EliteDangerous"),
                Path.Combine(programFilesX86Folder, @"Steam\steamapps\common\Elite Dangerous"),
                Path.Combine(programFilesFolder, @"Oculus\Software\frontier-developments-plc-elite-dangerous"),
                Path.Combine(localAppDataFolder, @"Frontier_Developments"),
            };

            var res = new List<string>();

            foreach (string folder in folders)
            {
                foreach (string product in _knownProductFolderNames)
                {
                    res.Add(Path.Combine(folder, "Products", product));
                }
            }

            return res.ToArray();
        });

        private readonly DirectoryInfo _di;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameInstallFolder"/> class,
        /// with the provided game install folder path.
        /// </summary>
        /// <param name="path">The path to the game install folder.</param>
        public GameInstallFolder(string path)
        {
            _di = new DirectoryInfo(path);
            MainExecutable = _di.GetFile(MainExecutableFileName);
            GraphicsConfiguration = _di.GetFile(GraphicsConfigMainFileName);
            ControlSchemes = _di.GetDirectory(ControlSchemesFolderName);
            D3DXIni = _di.GetFile(D3DXIniFileName);

            IsValid = _di.Exists
                && MainExecutable.Exists
                && GraphicsConfiguration.Exists
                && ControlSchemes.Exists
                && ControlSchemes.EnumerateFiles("*.binds").Any();
        }

        /// <summary>
        /// Gets the set of known product folder names where Elite:Dangerous may be installed.
        /// </summary>
        /// <remarks>
        /// Different versions of the game (e.g. Horizons, Odyssey) can be installed alongside each other,
        /// and they have their own folder under the <c>Products</c> folder in the game install folder.
        /// </remarks>
        public static IReadOnlyCollection<string> KnownProductFolderNames => _knownProductFolderNames;

        /// <summary>
        /// Gets the set of default folder paths where Elite:Dangerous may be installed.
        /// </summary>
        /// <remarks>
        /// Reference: <a href="https://customersupport.frontier.co.uk/hc/en-us/articles/4405700513298-Game-installation-and-file-locations-Netlog-AppConfig-Client-Log-Update-Log-Game-Folder-">Frontier Support: Game Installation and File Locations</a>.
        /// </remarks>
        public static IReadOnlyCollection<string> DefaultPaths => _defaultPaths.Value;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="GameInstallFolder"/>
        /// represents a valid Elite:Dangerous game install folder.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the full path of the game install folder.
        /// </summary>
        public string FullName => _di.FullName;

        /// <summary>
        /// Gets the file information for the main executable file.
        /// </summary>
        public FileInfo MainExecutable { get; }

        /// <summary>
        /// Gets the file information for the <c>GraphicsConfiguration.xml</c> file.
        /// </summary>
        public FileInfo GraphicsConfiguration { get; }

        /// <summary>
        /// Gets the directory information for the <c>ControlSchemes</c> folder.
        /// </summary>
        public DirectoryInfo ControlSchemes { get; }

        /// <summary>
        /// Gets the file information for <see href="https://github.com/psychicEgg/EDHM">EDHM</see>'s <c>d3dx.ini</c> file.
        /// </summary>
        public FileInfo D3DXIni { get; }

        /// <summary>
        /// Gets the set of non-default folder paths where Elite:Dangerous may be installed.
        /// </summary>
        /// <returns>The set of alternate paths.</returns>
        /// <remarks>
        /// This method looks into the Windows Registry to locate a non-default installation location.
        /// Additionally, it will include results from any Steam Libraries found.
        /// </remarks>
        public static IEnumerable<string> GetAlternatePaths()
        {
            return GetAlternatePaths(WindowsRegistry.Instance, SteamLibraryFolders.DefaultPath);
        }

        /// <summary>
        /// Asserts that the provided folder is a valid Elite:Dangerous game install folder.
        /// </summary>
        /// <param name="gameInstallFolder">The game install folder.</param>
        /// <returns>The provided folder.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="gameInstallFolder"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="gameInstallFolder"/> is not a valid game install folder.</exception>
        public static GameInstallFolder AssertValid(GameInstallFolder gameInstallFolder)
        {
            ArgumentNullException.ThrowIfNull(gameInstallFolder);

            if (!gameInstallFolder.IsValid)
            {
                throw new ArgumentException($"'{gameInstallFolder._di.FullName}' is not a valid Elite:Dangerous game install folder.", nameof(gameInstallFolder));
            }

            return gameInstallFolder;
        }

        internal static IEnumerable<string> GetAlternatePaths(IWindowsRegistry windowsRegistry, string steamLibraryPath)
        {
            // Reference: https://github.com/Bemoliph/Elite-Dangerous-Downloader/blob/master/downloader.py
            string? launcherPath = (string?)windowsRegistry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{696F8871-C91D-4CB1-825D-36BE18065575}_is1",
                "InstallLocation",
                null);

            if (launcherPath != null)
            {
                foreach (string product in _knownProductFolderNames)
                {
                    yield return Path.Combine(launcherPath, "Products", product);
                }
            }

            var steamLibraryFolders = SteamLibraryFolders.FromFile(steamLibraryPath);

            foreach (string folder in steamLibraryFolders)
            {
                foreach (string product in _knownProductFolderNames)
                {
                    yield return Path.Combine(folder, @"steamapps\common\Elite Dangerous\Products", product);
                }
            }
        }
    }
}
