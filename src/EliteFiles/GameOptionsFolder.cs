using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EliteFiles
{
    /// <summary>
    /// Represents an Elite:Dangerous game options folder.
    /// </summary>
    public sealed class GameOptionsFolder
    {
        /// <summary>«GameOptionsFolder»\Bindings.</summary>
        private const string BindingsFolderName = "Bindings";

        /// <summary>«GameOptionsFolder»\Bindings\StartPreset.start.</summary>
        private const string BindingsStartPresetFileName = "StartPreset.start";

        /// <summary>«GameOptionsFolder»\Graphics.</summary>
        private const string GraphicsFolderName = "Graphics";

        /// <summary>«GameOptionsFolder»\Graphics\GraphicsConfigurationOverride.xml.</summary>
        private const string GraphicsConfigOverrideFileName = "GraphicsConfigurationOverride.xml";

        private static readonly Lazy<string> _defaultPath = new Lazy<string>(() =>
        {
            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppDataFolder, @"Frontier Developments\Elite Dangerous\Options");
        });

        private readonly DirectoryInfo _di;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOptionsFolder"/> class,
        /// with the provided game options folder path.
        /// </summary>
        /// <param name="path">The path to the game options folder.</param>
        public GameOptionsFolder(string path)
        {
            _di = new DirectoryInfo(path);
            Bindings = new DirectoryInfo(Path.Combine(path, BindingsFolderName));
            BindingsStartPreset = new FileInfo(Path.Combine(Bindings.FullName, BindingsStartPresetFileName));
            Graphics = new DirectoryInfo(Path.Combine(path, GraphicsFolderName));
            GraphicsConfigurationOverride = new FileInfo(Path.Combine(Graphics.FullName, GraphicsConfigOverrideFileName));

            IsValid = _di.Exists
                && Bindings.Exists
                && BindingsStartPreset.Exists
                && Graphics.Exists
                && GraphicsConfigurationOverride.Exists;
        }

        /// <summary>
        /// Gets the default Elite:Dangerous' game options folder path.
        /// </summary>
        public static string DefaultPath => _defaultPath.Value;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="GameOptionsFolder"/>
        /// represents a valid Elite:Dangerous game options folder.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the full path of the game options folder.
        /// </summary>
        public string FullName => _di.FullName;

        /// <summary>
        /// Gets the directory information for the <c>Bindings</c> folder.
        /// </summary>
        public DirectoryInfo Bindings { get; }

        /// <summary>
        /// Gets the file information for the <c>Bindings\StartPreset.start</c> file.
        /// </summary>
        public FileInfo BindingsStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Graphics</c> folder.
        /// </summary>
        public DirectoryInfo Graphics { get; }

        /// <summary>
        /// Gets the file information for the <c>Graphics\GraphicsConfigurationOverride.xml</c> file.
        /// </summary>
        public FileInfo GraphicsConfigurationOverride { get; }

        /// <summary>
        /// Asserts that the provided folder is a valid Elite:Dangerous game options folder.
        /// </summary>
        /// <param name="gameOptionsFolder">The game options folder.</param>
        /// <returns>The provided folder.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="gameOptionsFolder"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="gameOptionsFolder"/> is not a valid game options folder.</exception>
        public static GameOptionsFolder AssertValid(GameOptionsFolder gameOptionsFolder)
        {
            if (gameOptionsFolder == null)
            {
                throw new ArgumentNullException(nameof(gameOptionsFolder));
            }

            if (!gameOptionsFolder.IsValid)
            {
                throw new ArgumentException($"'{gameOptionsFolder._di.FullName}' is not a valid Elite:Dangerous game options folder.", nameof(gameOptionsFolder));
            }

            return gameOptionsFolder;
        }
    }
}
