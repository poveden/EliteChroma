using Newtonsoft.Json;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording a change on the game music 'mood'.
    /// </summary>
    [JournalEntry("Music")]
    public sealed class Music : JournalEntry
    {
        private Music()
        {
        }

        /// <summary>
        /// Gets the name of the music track.
        /// </summary>
        [JsonProperty("MusicTrack")]
        public string MusicTrack { get; private set; }
    }
}
