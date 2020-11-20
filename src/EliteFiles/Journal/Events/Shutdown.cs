namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording a clean shutdown of the game.
    /// </summary>
    [JournalEntry("Shutdown")]
    public sealed class Shutdown : JournalEntry
    {
    }
}
