using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EliteFiles.Internal;

namespace EliteFiles
{
    /// <summary>
    /// Represents an Elite:Dangerous journal folder.
    /// </summary>
    public sealed class JournalFolder
    {
        /// <summary>
        /// Gets the pattern used to match journal files.
        /// </summary>
        public const string JournalFilesFilter = "Journal.*.log";

        /// <summary>«JournalFolder»\Status.json.</summary>
        private const string StatusFileName = "Status.json";

        private static readonly Lazy<string> _defaultPath = new Lazy<string>(() =>
        {
            string userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userProfileFolder, @"Saved Games\Frontier Developments\Elite Dangerous");
        });

        private readonly DirectoryInfo _di;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalFolder"/> class,
        /// with the provided journal folder path.
        /// </summary>
        /// <param name="path">The path to the journal folder.</param>
        public JournalFolder(string path)
        {
            _di = new DirectoryInfo(path);
            Status = _di.GetFile(StatusFileName);

            IsValid = Status.Exists
                && _di.Exists
                && _di.EnumerateFiles(JournalFilesFilter).Any();
        }

        /// <summary>
        /// Gets the default Elite:Dangerous' journal folder path.
        /// </summary>
        public static string DefaultPath => _defaultPath.Value;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="JournalFolder"/>
        /// represents a valid Elite:Dangerous journal folder.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the full path of the journal folder.
        /// </summary>
        public string FullName => _di.FullName;

        /// <summary>
        /// Gets the file information for the <c>status.json</c> file.
        /// </summary>
        public FileInfo Status { get; }

        /// <summary>
        /// Asserts that the provided folder is a valid Elite:Dangerous journal folder.
        /// </summary>
        /// <param name="journalFolder">The journal folder.</param>
        /// <returns>The provided folder.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="journalFolder"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="journalFolder"/> is not a valid journal folder.</exception>
        public static JournalFolder AssertValid(JournalFolder journalFolder)
        {
            if (journalFolder == null)
            {
                throw new ArgumentNullException(nameof(journalFolder));
            }

            if (!journalFolder.IsValid)
            {
                throw new ArgumentException($"'{journalFolder.FullName}' is not a valid Elite:Dangerous journal folder.", nameof(journalFolder));
            }

            return journalFolder;
        }

        /// <summary>
        /// Returns an enumerable collection of file information that matches the given search pattern.
        /// </summary>
        /// <param name="searchPattern">The pattern to match against the names of files.</param>
        /// <returns>The enumerable collection of files that match <paramref name="searchPattern"/>.</returns>
        public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
        {
            return _di.EnumerateFiles(searchPattern);
        }
    }
}
