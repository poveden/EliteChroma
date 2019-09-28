using System;
using System.IO;
using EliteFiles.Internal;

namespace EliteFiles.Status
{
    /// <summary>
    /// Watches for changes in Elite:Dangerous game status file and raises events accordingly.
    /// </summary>
    public sealed class StatusWatcher : IDisposable
    {
        private readonly string _statusFile;
        private readonly EliteFileSystemWatcher _watcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusWatcher"/> class
        /// with the given player journal folder path.
        /// </summary>
        /// <param name="journalFolder">The path to the player journal folder.</param>
        public StatusWatcher(string journalFolder)
        {
            if (!Folders.IsValidJournalFolder(journalFolder))
            {
                throw new ArgumentException($"'{journalFolder}' is not a valid Elite:Dangerous journal folder.", nameof(journalFolder));
            }

            _statusFile = Path.Combine(journalFolder, Folders.JournalStatusFile);
            _watcher = new EliteFileSystemWatcher(journalFolder, Folders.JournalStatusFile);
            _watcher.Changed += StatusWatcher_Changed;
        }

        /// <summary>
        /// Occurs when the game status has changed.
        /// </summary>
        public event EventHandler<StatusEntry> Changed;

        /// <summary>
        /// Starts watching for changes in the game status file.
        /// </summary>
        public void Start()
        {
            _watcher.Stop();
            Reload();
            _watcher.Start();
        }

        /// <summary>
        /// Stops watching for changes in the game status file.
        /// </summary>
        public void Stop()
        {
            _watcher.Stop();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="StatusWatcher"/>.
        /// </summary>
        public void Dispose()
        {
            _watcher.Dispose();
        }

        private void StatusWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            var status = StatusEntry.FromFile(_statusFile);

            if (status == null)
            {
                return;
            }

            Changed?.Invoke(this, status);
        }
    }
}
