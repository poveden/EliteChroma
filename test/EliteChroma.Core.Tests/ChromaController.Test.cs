using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EliteChroma.Core.Tests.Internal;
using EliteFiles.Status;
using Newtonsoft.Json;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class ChromaControllerTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        [Fact]
        public void ChecksIfChromaSdkIsAvailable()
        {
            var available = ChromaController.IsChromaSdkAvailable();

            Assert.True(available);
        }

        [Fact]
        public async Task RazerChromaApiGetsCalledOnGameEvents()
        {
            const string statusFile = "Status.json";
            const string journalFile = "Journal.190101020000.01.log";

            var chromaApi = new ChromaApiMock();
            var evs = new EventCollector<ChromaApiMock.MockCall>(h => chromaApi.Called += h, h => chromaApi.Called -= h);

            using TestFolder
                dirRoot = new TestFolder(_gameRootFolder),
                dirOpts = new TestFolder(_gameOptionsFolder),
                dirJournal = new TestFolder();

            dirJournal.WriteText(statusFile, EventSequence.BuildEvent("Status", new { Flags = 0 }));
            dirJournal.WriteText(journalFile, EventSequence.BuildEvent("Fileheader", new { part = 1, language = @"English\UK", gameversion = "3.5.0.200 EDH", build = "r210198/r0 " }));

            using var cc = new ChromaController(dirRoot.Name, dirOpts.Name, dirJournal.Name)
            {
                ChromaApi = chromaApi,
                AnimationFrameRate = 0,
            };

            var mcs = await evs.WaitAsync(5, cc.Start, 1000).ConfigureAwait(false);
            Assert.Equal("InitializeAsync", mcs[0].Method);
            Assert.Equal("CreateKeyboardEffectAsync", mcs[1].Method);

            var seq = BuildEventSequence();
            mcs = await evs.WaitAsync(seq.Count, () => seq.Play(dirJournal, journalFile, statusFile), 200 * seq.Count).ConfigureAwait(false);
            Assert.Equal(seq.Count, mcs.Count);

            var mc = await evs.WaitAsync(cc.Stop, 1000).ConfigureAwait(false);
            Assert.Equal("UninitializeAsync", mc.Method);
        }

        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            var watcher = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }

        private static EventSequence BuildEventSequence()
        {
            return new EventSequence
            {
                { "Music", new { MusicTrack = "MainMenu" } },
                { Flags.Docked | Flags.FsdMassLocked | Flags.InMainShip | Flags.LandingGearDeployed | Flags.ShieldsUp },
                { "Undocked", new { StationType = "Coriolis" } },
                { Flags.InMainShip | Flags.ShieldsUp },
                { Flags.InMainShip | Flags.ShieldsUp, GuiFocus.GalaxyMap },
                { Flags.InMainShip | Flags.ShieldsUp },
                { Flags.FsdCharging | Flags.InMainShip | Flags.ShieldsUp },
                { "StartJump", new { JumpType = "Hyperspace", StarClass = "G" } },
                { Flags.FsdJump | Flags.InMainShip | Flags.ShieldsUp },
                { "FSDJump", new { StarSystem = "Wolf 1301" } },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.FsdCooldown | Flags.Supercruise },
                { "SupercruiseExit", new { BodyType = "Station" } },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.LightsOn | Flags.NightVision | Flags.CargoScoopDeployed | Flags.HardpointsDeployed | Flags.LandingGearDeployed },
            };
        }

        private sealed class EventSequence : IReadOnlyCollection<(bool isStatus, string json)>
        {
            private readonly List<(bool IsStatus, string Json)> _events = new List<(bool IsStatus, string Json)>();

            public int Count => _events.Count;

            public static string BuildEvent(string eventName, object data)
            {
                var tsJson = JsonConvert.SerializeObject(new
                {
                    timestamp = DateTimeOffset.UtcNow,
                    @event = eventName,
                });

                var entryJson = JsonConvert.SerializeObject(data);

                var json = $"{tsJson[0..^1]},{entryJson.Substring(1)}";

                return $"{json}\r\n";
            }

            public void Add(string eventName, object data)
            {
                var json = BuildEvent(eventName, data);
                _events.Add((eventName == "Status", json));
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

                Add("Status", data);
            }

            public void Play(TestFolder journalFolder, string journalFile, string statusFile)
            {
                var journalBuf = new StringBuilder();

                foreach (var (isStatus, json) in this)
                {
                    if (!isStatus)
                    {
                        journalBuf.Append(json);
                        continue;
                    }

                    if (journalBuf.Length != 0)
                    {
                        journalFolder.WriteText(journalFile, journalBuf.ToString(), true);
                        journalBuf.Clear();
                        Thread.Sleep(100);
                    }

                    journalFolder.WriteText(statusFile, json, false);
                    Thread.Sleep(100);
                }

                if (journalBuf.Length != 0)
                {
                    journalFolder.WriteText(journalFile, journalBuf.ToString(), true);
                    journalBuf.Clear();
                    Thread.Sleep(100);
                }
            }

            public IEnumerator<(bool isStatus, string json)> GetEnumerator() => _events.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
