using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EliteFiles.Status;
using Newtonsoft.Json;
using TestUtils;

namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class EventSequence : IReadOnlyCollection<Event>
    {
        private readonly List<Event> _events = new List<Event>();

        public int Count => _events.Count;

        public static string BuildEvent(string eventName, object data)
        {
            string tsJson = JsonConvert.SerializeObject(new
            {
                timestamp = DateTimeOffset.UtcNow,
                @event = eventName,
            });

            string entryJson = JsonConvert.SerializeObject(data);

            string json = $"{tsJson[0..^1]},{entryJson[1..]}";

            return $"{json}\r\n";
        }

        public void Add(string eventName, object data, bool changesGameState)
        {
            string json = BuildEvent(eventName, data);
            _events.Add(new Event(eventName == "Status", json, changesGameState));
        }

        public void Add(Flags flags, GuiFocus guiFocus = GuiFocus.None)
        {
            var data = new
            {
                Flags = flags,
                Pips = new[] { 4, 4, 4 },
                FireGroup = 0,
                GuiFocus = guiFocus,
            };

            Add("Status", data, true);
        }

        public void Play(TestFolder journalFolder, string journalFile, string statusFile)
        {
            var journalBuf = new StringBuilder();

            foreach (var e in this)
            {
                if (!e.IsStatus)
                {
                    journalBuf.Append(e.Json);
                    continue;
                }

                if (journalBuf.Length != 0)
                {
                    journalFolder.WriteText(journalFile, journalBuf.ToString(), true);
                    journalBuf.Clear();
                    Thread.Sleep(100);
                }

                journalFolder.WriteText(statusFile, e.Json, false);
                Thread.Sleep(100);
            }

            if (journalBuf.Length != 0)
            {
                journalFolder.WriteText(journalFile, journalBuf.ToString(), true);
                journalBuf.Clear();
                Thread.Sleep(100);
            }
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
