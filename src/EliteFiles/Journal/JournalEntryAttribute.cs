namespace EliteFiles.Journal
{
    /// <summary>
    /// Used to mark a <see cref="JournalEntry"/> subclass as a journal entry of the given event name.
    /// Instances of <see cref="JournalReader"/> will try to deserialize journal entries to types matching the event name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class JournalEntryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalEntryAttribute"/> class
        /// with the given event name.
        /// </summary>
        /// <param name="eventName">The name of the journal event.</param>
        public JournalEntryAttribute(string eventName)
        {
            EventName = eventName;
        }

        /// <summary>
        /// Gets the name of the journal event.
        /// </summary>
        public string EventName { get; }
    }
}
