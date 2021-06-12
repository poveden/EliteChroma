using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EliteFiles.Internal;
using Microsoft.Win32;

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

        private static readonly Lazy<string[]> _defaultPaths = new Lazy<string[]>(() =>
        {
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            return new[]
            {
                Path.Combine(programFilesFolder, @"Frontier\Products\elite-dangerous-64"),
                Path.Combine(programFilesFolder, @"Frontier\Products\elite-dangerous-odyssey-64"),
                Path.Combine(programFilesFolder, @"Epic Games\EliteDangerous\Products\elite-dangerous-64"),
                Path.Combine(programFilesFolder, @"Epic Games\EliteDangerous\Products\elite-dangerous-odyssey-64"),
                Path.Combine(programFilesFolder, @"Steam\steamapps\common\Elite Dangerous\Products\elite-dangerous-64"),
                Path.Combine(programFilesFolder, @"Steam\steamapps\common\Elite Dangerous\Products\elite-dangerous-odyssey-64"),
                Path.Combine(programFilesFolder, @"Oculus\Software\frontier-developments-plc-elite-dangerous"),
                Path.Combine(localAppDataFolder, @"Frontier_Developments\Products\elite-dangerous-64"),
                Path.Combine(localAppDataFolder, @"Frontier_Developments\Products\elite-dangerous-odyssey-64"),
            };
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
            MainExecutable = new FileInfo(Path.Combine(path, MainExecutableFileName));
            GraphicsConfiguration = new FileInfo(Path.Combine(path, GraphicsConfigMainFileName));
            ControlSchemes = new DirectoryInfo(Path.Combine(path, ControlSchemesFolderName));

            IsValid = _di.Exists
                && MainExecutable.Exists
                && GraphicsConfiguration.Exists
                && ControlSchemes.Exists
                && ControlSchemes.EnumerateFiles("*.binds").Any();
        }

        /// <summary>
        /// Gets the set of default folder paths where Elite:Dangerous may be installed.
        /// </summary>
        /// <remarks>
        /// Reference: <a href="https://support.frontier.co.uk/kb/faq.php?id=108">Frontier Support: Game Installation and File Locations</a>.
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
        /// Gets the set of non-default folder paths where Elite:Dangerous may be installed.
        /// </summary>
        /// <returns>The set of alternate paths.</returns>
        /// <remarks>
        /// This method looks into the Windows Registry to locate a non-default installation location.
        /// Additionally, it will include results from any Steam Libraries found.
        /// </remarks>
        public static IEnumerable<string> GetAlternatePaths()
        {
            // Reference: https://github.com/Bemoliph/Elite-Dangerous-Downloader/blob/master/downloader.py
            string? launcherPath = (string?)Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{696F8871-C91D-4CB1-825D-36BE18065575}_is1",
                "InstallLocation",
                null);

            if (launcherPath != null)
            {
                yield return Path.Combine(launcherPath, @"Products\elite-dangerous-64");
                yield return Path.Combine(launcherPath, @"Products\elite-dangerous-odyssey-64");
            }

            var steamLibraryFolders = SteamLibraryFolders.FromFile(SteamLibraryFolders.DefaultPath);

            if (steamLibraryFolders != null)
            {
                foreach (string folder in steamLibraryFolders)
                {
                    yield return Path.Combine(folder, @"steamapps\common\Elite Dangerous\Products\elite-dangerous-64");
                    yield return Path.Combine(folder, @"steamapps\common\Elite Dangerous\Products\elite-dangerous-odyssey-64");
                }
            }
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
            if (gameInstallFolder == null)
            {
                throw new ArgumentNullException(nameof(gameInstallFolder));
            }

            if (!gameInstallFolder.IsValid)
            {
                throw new ArgumentException($"'{gameInstallFolder._di.FullName}' is not a valid Elite:Dangerous game install folder.", nameof(gameInstallFolder));
            }

            return gameInstallFolder;
        }
    }
}
