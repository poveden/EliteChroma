using System;
using System.IO;
using EliteFiles.Internal;

namespace EliteFiles
{
    /// <summary>
    /// Represents an Elite:Dangerous game options folder.
    /// </summary>
    public sealed class GameOptionsFolder
    {
        /// <summary>«GameOptionsFolder»\«SubFolder»\StartPreset.start.</summary>
        private const string StartPresetFileName = "StartPreset.start";

        /// <summary>«GameOptionsFolder»\Audio.</summary>
        private const string AudioFolderName = "Audio";

        /// <summary>«GameOptionsFolder»\Bindings.</summary>
        private const string BindingsFolderName = "Bindings";

        /// <summary>«GameOptionsFolder»\Development.</summary>
        private const string DevelopmentFolderName = "Development";

        /// <summary>«GameOptionsFolder»\Graphics.</summary>
        private const string GraphicsFolderName = "Graphics";

        /// <summary>«GameOptionsFolder»\Graphics\GraphicsConfigurationOverride.xml.</summary>
        private const string GraphicsConfigOverrideFileName = "GraphicsConfigurationOverride.xml";

        /// <summary>«GameOptionsFolder»\Player.</summary>
        private const string PlayerFolderName = "Player";

        /// <summary>«GameOptionsFolder»\Startup.</summary>
        private const string StartupFolderName = "Startup";

        /// <summary>«GameOptionsFolder»\Startup\Startup.xml.</summary>
        private const string StartupSettingsFileName = "Settings.xml";

        private static readonly Lazy<string> _defaultPath = new Lazy<string>(() =>
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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

            Audio = _di.GetDirectory(AudioFolderName);
            AudioStartPreset = Audio.GetFile(StartPresetFileName);

            Bindings = _di.GetDirectory(BindingsFolderName);
            BindingsStartPreset = Bindings.GetFile(StartPresetFileName);

            Development = _di.GetDirectory(DevelopmentFolderName);
            DevelopmentStartPreset = Development.GetFile(StartPresetFileName);

            Graphics = _di.GetDirectory(GraphicsFolderName);
            GraphicsConfigurationOverride = Graphics.GetFile(GraphicsConfigOverrideFileName);
            GraphicsStartPreset = Graphics.GetFile(StartPresetFileName);

            Player = _di.GetDirectory(PlayerFolderName);
            PlayerStartPreset = Player.GetFile(StartPresetFileName);

            Startup = _di.GetDirectory(StartupFolderName);
            StartupSettings = Startup.GetFile(StartupSettingsFileName);

            IsValid = _di.Exists
                && AudioStartPreset.Exists
                && DevelopmentStartPreset.Exists
                && GraphicsStartPreset.Exists
                && PlayerStartPreset.Exists
                && StartupSettings.Exists;
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
        /// Gets the directory information for the <c>Audio</c> folder.
        /// </summary>
        public DirectoryInfo Audio { get; }

        /// <summary>
        /// Gets the file information for the <c>Audio\StartPreset.start</c> file.
        /// </summary>
        public FileInfo AudioStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Bindings</c> folder.
        /// </summary>
        public DirectoryInfo Bindings { get; }

        /// <summary>
        /// Gets the file information for the <c>Bindings\StartPreset.start</c> file.
        /// </summary>
        public FileInfo BindingsStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Development</c> folder.
        /// </summary>
        public DirectoryInfo Development { get; }

        /// <summary>
        /// Gets the file information for the <c>Development\StartPreset.start</c> file.
        /// </summary>
        public FileInfo DevelopmentStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Graphics</c> folder.
        /// </summary>
        public DirectoryInfo Graphics { get; }

        /// <summary>
        /// Gets the file information for the <c>Graphics\GraphicsConfigurationOverride.xml</c> file.
        /// </summary>
        public FileInfo GraphicsConfigurationOverride { get; }

        /// <summary>
        /// Gets the file information for the <c>Graphics\StartPreset.start</c> file.
        /// </summary>
        public FileInfo GraphicsStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Player</c> folder.
        /// </summary>
        public DirectoryInfo Player { get; }

        /// <summary>
        /// Gets the file information for the <c>Player\StartPreset.start</c> file.
        /// </summary>
        public FileInfo PlayerStartPreset { get; }

        /// <summary>
        /// Gets the directory information for the <c>Startup</c> folder.
        /// </summary>
        public DirectoryInfo Startup { get; }

        /// <summary>
        /// Gets the file information for the <c>Startup\Settings.xml</c> file.
        /// </summary>
        public FileInfo StartupSettings { get; }

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
