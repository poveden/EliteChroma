using System;
using System.Collections.Generic;
using System.IO;

namespace EliteFiles
{
    /// <summary>
    /// Defines methods related to the various folders used by Elite:Dangerous.
    /// </summary>
    public static class Folders
    {
        /// <summary>«GameInstallFolder»\GraphicsConfiguration.xml.</summary>
        internal const string GraphicsConfigMainFile = "GraphicsConfiguration.xml";

        /// <summary>«GameInstallFolder»\ControlSchemes.</summary>
        internal const string ControlSchemesFolder = "ControlSchemes";

        /// <summary>«GameOptionsFolder»\Bindings.</summary>
        internal const string GameOptionsBindingsFolder = "Bindings";

        /// <summary>«GameOptionsFolder»\Bindings\StartPreset.start.</summary>
        internal const string GameOptionsBindingsStartPresetFile = "StartPreset.start";

        /// <summary>«GameOptionsFolder»\Graphics.</summary>
        internal const string GameOptionsGraphicsFolder = "Graphics";

        /// <summary>«GameOptionsFolder»\Graphics\GraphicsConfigurationOverride.xml.</summary>
        internal const string GraphicsConfigOverrideFile = "GraphicsConfigurationOverride.xml";

        /// <summary>«JournalFolder»\Status.json.</summary>
        internal const string JournalStatusFile = "Status.json";

        /// <summary>«JournalFolder»\Journal.*.log.</summary>
        internal const string JournalFilesFilter = "Journal.*.log";

        /// <summary>
        /// Returns the set of default folders where Elite:Dangerous may be installed.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{String}"/> with the default folders.</returns>
        /// <remarks>
        /// Reference: <a href="https://support.frontier.co.uk/kb/faq.php?id=108">Frontier Support: Game Installation and File Locations</a>.
        /// </remarks>
        public static IEnumerable<string> GetDefaultGameInstallFolders()
        {
            var programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            foreach (var alt in new[]
            {
                @"Frontier\Products\elite-dangerous-64",
                @"Steam\steamapps\common\Elite Dangerous\Products\elite-dangerous-64",
                @"Oculus\Software\frontier-developments-plc-elite-dangerous",
            })
            {
                yield return Path.Combine(programFilesFolder, alt);
            }

            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            yield return Path.Combine(localAppDataFolder, @"Frontier_Developments\Products\elite-dangerous-64");
        }

        /// <summary>
        /// Gets the default Elite:Dangerous' game options folder.
        /// </summary>
        /// <returns>The default game options folder.</returns>
        public static string GetDefaultGameOptionsFolder()
        {
            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppDataFolder, @"Frontier Developments\Elite Dangerous\Options");
        }

        /// <summary>
        /// Gets the default Elite:Dangerous' journal folder.
        /// </summary>
        /// <returns>The default journal folder.</returns>
        public static string GetDefaultJournalFolder()
        {
            var userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userProfileFolder, @"Saved Games\Frontier Developments\Elite Dangerous");
        }

        /// <summary>
        /// Returns a value indicating whether the given folder
        /// is a valid Elite:Dangerous game install folder.
        /// </summary>
        /// <param name="folder">The folder to check.</param>
        /// <returns><c>true</c> if <paramref name="folder"/> is a valid game install folder; otherwise, <c>false</c>.</returns>
        public static bool IsValidGameInstallFolder(string folder)
        {
            if (folder == null || !Directory.Exists(folder))
            {
                return false;
            }

            if (!File.Exists(Path.Combine(folder, GraphicsConfigMainFile)))
            {
                return false;
            }

            var controlSchemesFolder = Path.Combine(folder, ControlSchemesFolder);

            if (!Directory.Exists(controlSchemesFolder))
            {
                return false;
            }

            if (Directory.GetFiles(controlSchemesFolder, "*.binds").Length == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a value indicating whether the given folder
        /// is a valid Elite:Dangerous game options folder.
        /// </summary>
        /// <param name="folder">The folder to check.</param>
        /// <returns><c>true</c> if <paramref name="folder"/> is a valid game options folder; otherwise, <c>false</c>.</returns>
        /// <remarks>The game options folder is located by default at <c>%LOCALAPPDATA%\Frontier Developments\Elite Dangerous\Options</c>.</remarks>
        public static bool IsValidGameOptionsFolder(string folder)
        {
            if (folder == null || !Directory.Exists(folder))
            {
                return false;
            }

            var bindingsFolder = Path.Combine(folder, GameOptionsBindingsFolder);

            if (!Directory.Exists(bindingsFolder))
            {
                return false;
            }

            if (!File.Exists(Path.Combine(bindingsFolder, GameOptionsBindingsStartPresetFile)))
            {
                return false;
            }

            var graphicsFolder = Path.Combine(folder, GameOptionsGraphicsFolder);

            if (!Directory.Exists(graphicsFolder))
            {
                return false;
            }

            if (!File.Exists(Path.Combine(graphicsFolder, GraphicsConfigOverrideFile)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a value indicating whether the given folder
        /// is a valid Elite:Dangerous journal folder.
        /// </summary>
        /// <param name="folder">The folder to check.</param>
        /// <returns><c>true</c> if <paramref name="folder"/> is a valid journal folder; otherwise, <c>false</c>.</returns>
        /// <remarks>The journal folder is located by default at <c>%USERPROFILE%\Saved Games\Frontier Developments\Elite Dangerous</c>.</remarks>
        public static bool IsValidJournalFolder(string folder)
        {
            if (folder == null || !Directory.Exists(folder))
            {
                return false;
            }

            if (!File.Exists(Path.Combine(folder, JournalStatusFile)))
            {
                return false;
            }

            if (Directory.GetFiles(folder, JournalFilesFilter).Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
