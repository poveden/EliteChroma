using System.Diagnostics.Tracing;

namespace EliteFiles.Internal
{
    // Reference: https://learn.microsoft.com/en-us/dotnet/core/diagnostics/eventsource-instrumentation
    [EventSource(Name = "EliteFiles")]
    internal sealed class LogEventSource : EventSource
    {
        public static LogEventSource Log { get; } = new LogEventSource();

        [Event(1, Message = "Journal: Opening \"{0}\"", Level = EventLevel.Informational, Keywords = Keywords.Journal)]
        public void JournalOpening(string path)
        {
            WriteEvent(1, path);
        }

        [Event(2, Message = "Journal: Closing \"{0}\"", Level = EventLevel.Informational, Keywords = Keywords.Journal)]
        public void JournalClosing(string? path)
        {
            WriteEvent(2, path);
        }

        [Event(3, Message = "Journal: Dispatching entry ({0})", Level = EventLevel.Verbose, Keywords = Keywords.Journal)]
        public void JournaDispatchingEntry(string path)
        {
            WriteEvent(3, path);
        }

        [Event(4, Message = "Bindings: Could not resolve bindings preset file name after {0} retries", Level = EventLevel.Warning, Keywords = Keywords.Bindings)]
        public void BindingsPresetFileResolutionFailed(int retries)
        {
            WriteEvent(4, retries);
        }

        [Event(5, Message = "Bindings: Could not read file \"{0}\" after {1} retries", Level = EventLevel.Warning, Keywords = Keywords.Bindings)]
        public void BindingsFileCannotBeRead(string path, int retries)
        {
            WriteEvent(5, path, retries);
        }

        [Event(6, Message = "Bindings: Raising Changed event", Level = EventLevel.Verbose, Keywords = Keywords.Bindings)]
        public void BindingsRaisingChangedEvent()
        {
            WriteEvent(6);
        }

        [Event(7, Message = "Graphics: Could not read file \"{0}\" after {1} retries", Level = EventLevel.Warning, Keywords = Keywords.Graphics)]
        public void GraphicsFileCannotBeRead(string path, int retries)
        {
            WriteEvent(7, path, retries);
        }

        [Event(8, Message = "Graphics: Raising Changed event", Level = EventLevel.Verbose, Keywords = Keywords.Graphics)]
        public void GraphicsRaisingChangedEvent()
        {
            WriteEvent(8);
        }

        [Event(9, Message = "Status: Could not read file \"{0}\" after {1} retries", Level = EventLevel.Warning, Keywords = Keywords.Status)]
        public void StatusFileCannotBeRead(string path, int retries)
        {
            WriteEvent(9, path, retries);
        }

        [Event(10, Message = "Status: Raising Changed event", Level = EventLevel.Verbose, Keywords = Keywords.Status)]
        public void StatusRaisingChangedEvent()
        {
            WriteEvent(10);
        }

        [Event(11, Message = "EDHM: Could not read file \"{0}\" after {1} retries", Level = EventLevel.Warning, Keywords = Keywords.Edhm)]
        public void EdhmFileCannotBeRead(string path, int retries)
        {
            WriteEvent(11, path, retries);
        }

        [Event(12, Message = "EDHM: Raising Changed event", Level = EventLevel.Verbose, Keywords = Keywords.Edhm)]
        public void EdhmRaisingChangedEvent()
        {
            WriteEvent(12);
        }

        [Event(13, Message = "EDHM: D3DX.ini file changed: \"{0}\"", Level = EventLevel.Verbose, Keywords = Keywords.Edhm)]
        public void EdhmD3DXIniChanged(string path)
        {
            WriteEvent(13, path);
        }

        [Event(14, Message = "EDHM: Includes file ignored: \"{0}\"", Level = EventLevel.Verbose, Keywords = Keywords.Edhm)]
        public void EdhmIncludesIgnored(string path)
        {
            WriteEvent(14, path);
        }

        [Event(15, Message = "EDHM: Includes file changed: \"{0}\"", Level = EventLevel.Verbose, Keywords = Keywords.Edhm)]
        public void EdhmIncludesChanged(string path)
        {
            WriteEvent(15, path);
        }

        private static class Keywords
        {
            public const EventKeywords Journal = (EventKeywords)0b__00001;

            public const EventKeywords Bindings = (EventKeywords)0b_00010;

            public const EventKeywords Graphics = (EventKeywords)0b_00100;

            public const EventKeywords Status = (EventKeywords)0b___01000;

            public const EventKeywords Edhm = (EventKeywords)0b_____10000;
        }
    }
}
