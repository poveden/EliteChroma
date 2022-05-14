using System.Collections;
using System.Text;
using EliteFiles.Status;
using Newtonsoft.Json;
using TestUtils;
using Xunit.Abstractions;

namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class EventSequence : IReadOnlyCollection<Event>
    {
        private readonly List<Event> _events = new List<Event>();
        private readonly ITestOutputHelper _output;

        public EventSequence(ITestOutputHelper output)
        {
            _output = output;
        }

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

        public void Add(Flags flags, Flags2 flags2 = Flags2.None, string? selectedWeapon = null, GuiFocus guiFocus = GuiFocus.None)
        {
            var data = new
            {
                Flags = flags,
                Flags2 = flags2,
                Pips = new[] { 4, 4, 4 },
                FireGroup = 0,
                GuiFocus = guiFocus,
                SelectedWeapon = selectedWeapon,
            };

            Add("Status", data, true);
        }

        public void Play(TestFolder journalFolder, string journalFile, string statusFile)
        {
            var journalBuf = new StringBuilder();
            int gameStateChanges = 0;

            foreach (var e in this)
            {
                if (!e.IsStatus)
                {
                    gameStateChanges += e.ChangesGameState ? 1 : 0;
                    journalBuf.Append(e.Json);
                    continue;
                }

                if (journalBuf.Length != 0)
                {
                    _output.WriteLine("\r\n[Journal] {0}", journalBuf.ToString()[..^2]);
                    _output.WriteLine("({0} game state changes should follow)", gameStateChanges);
                    journalFolder.WriteText(journalFile, journalBuf.ToString(), true);
                    journalBuf.Clear();
                    gameStateChanges = 0;
                    Thread.Sleep(100);
                }

                _output.WriteLine("\r\n[Status]  {0}", e.Json[..^2]);
                _output.WriteLine("({0} game state changes should follow)", e.ChangesGameState ? 1 : 0);
                journalFolder.WriteText(statusFile, e.Json, false);
                Thread.Sleep(100);
            }

            if (journalBuf.Length != 0)
            {
                _output.WriteLine("\r\n[Journal] {0}", journalBuf.ToString()[..^2]);
                _output.WriteLine("({0} game state changes should follow)", gameStateChanges);
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
