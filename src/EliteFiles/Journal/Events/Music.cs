using System.Text.Json.Serialization;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording a change on the game music 'mood'.
    /// </summary>
    [JournalEntry("Music")]
    public sealed class Music : JournalEntry
    {
        /// <summary>
        /// Gets or sets the name of the music track.
        /// </summary>
        [JsonPropertyName("MusicTrack")]
        public string? MusicTrack { get; set; }
    }
}
