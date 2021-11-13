using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChromaWrapper.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite;
using EliteFiles.Status;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ChromaControllerTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ChecksIfChromaSdkIsAvailable(bool sdkAvailable)
        {
            var nm = new NativeMethodsMock(sdkAvailable);

            bool available = ChromaController.IsChromaSdkAvailable(nm);

            Assert.Equal(sdkAvailable, available);
        }

        [Fact]
        public void RazerChromaApiGetsCalledOnGameEvents()
        {
            const string statusFile = "Status.json";
            const string journalFile = "Journal.190101020000.01.log";

            var factory = new ChromaMockFactory();
            var mockCKEA = factory.Mock.Setup(x => x.CreateEffect(It.IsAny<IKeyboardEffect>()));

            using TestFolder
                dirRoot = new TestFolder(_gameRootFolder),
                dirOpts = new TestFolder(_gameOptionsFolder),
                dirJournal = new TestFolder();

            dirJournal.WriteText(statusFile, EventSequence.BuildEvent("Status", new { Flags = 0 }));
#pragma warning disable IDE0050
            dirJournal.WriteText(journalFile, EventSequence.BuildEvent("Fileheader", new { part = 1, language = @"English\UK", gameversion = "3.5.0.200 EDH", build = "r210198/r0 " }));
#pragma warning restore IDE0050

            using var cc = new ChromaController(dirRoot.Name, dirOpts.Name, dirJournal.Name)
            {
                ChromaFactory = factory,
                AnimationFrameRate = 0,
                DetectGameInForeground = false,
            };

            Assert.False(cc.DetectGameInForeground);

            using var ceCKEA = new CountdownEvent(1);
            mockCKEA.Callback(() => ceCKEA.Signal());

            cc.Start();

            Assert.True(ceCKEA.Wait(1000));

            var seq = BuildEventSequence();
            ceCKEA.Reset(seq.Count(x => x.ChangesGameState));

            seq.Play(dirJournal, journalFile, statusFile);

            Assert.True(ceCKEA.Wait(200 * seq.Count));

            cc.Stop();
        }

        [Fact]
        public void DefaultAnimationFrameRateIsSetTo30Fps()
        {
            using var cc = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            Assert.Equal(30, cc.AnimationFrameRate);
        }

        [Fact]
        public void CannotSetANegativeAnimationFrameRate()
        {
            using var cc = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            Assert.Throws<ArgumentOutOfRangeException>("AnimationFrameRate", () => cc.AnimationFrameRate = -1);
        }

        [Fact]
        public void EnUSOverrideIsAppliedInTheGameState()
        {
            using var cc = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            var watcher = cc.GetPrivateField<GameStateWatcher>("_watcher")!;

            Assert.False(cc.ForceEnUSKeyboardLayout);
            Assert.False(watcher.GetGameStateSnapshot().ForceEnUSKeyboardLayout);

            cc.ForceEnUSKeyboardLayout = true;
            Assert.True(cc.ForceEnUSKeyboardLayout);
            Assert.True(watcher.GetGameStateSnapshot().ForceEnUSKeyboardLayout);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);

            bool IsRunning()
            {
                return watcher.GetPrivateField<bool>("_running");
            }

            Assert.False(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            var watcher = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }

        [Fact]
        public void RenderEffectIsNotReentrant()
        {
            using var cc = new ChromaController(_gameRootFolder, _gameOptionsFolder, _journalFolder)
            {
                ChromaFactory = new ChromaMockFactory(),
            };

            var game = cc.GetPrivateField<GameStateWatcher>("_watcher")!
                .GetPrivateField<GameState>("_gameState")!;

            var effect = cc.GetPrivateField<LayeredEffect>("_effect")!;

            int nRenderCalls = 0;
            using var mre = new ManualResetEventSlim();

            var layer = new Mock<EffectLayer>();

            layer.Protected()
                .Setup("OnRender", ItExpr.IsAny<ChromaCanvas>(), ItExpr.IsAny<object>())
                .Callback(() =>
                {
                    Interlocked.Increment(ref nRenderCalls);
                    mre.Wait();
                });

            effect.Clear();
            effect.Add(layer.Object);

            game.ProcessState = GameProcessState.InForeground;

            async Task RenderEffect()
            {
                await cc.InvokePrivateMethod<Task>("RenderEffect")!.ConfigureAwait(false);
                mre.Set();
            }

            Task.WaitAll(new[]
            {
                Task.Run(RenderEffect),
                Task.Run(RenderEffect),
            });

            Assert.Equal(1, nRenderCalls);
        }

        private static EventSequence BuildEventSequence()
        {
            return new EventSequence
            {
                { "Music", new { MusicTrack = "MainMenu" }, true },
                { Flags.Docked | Flags.FsdMassLocked | Flags.InMainShip | Flags.LandingGearDeployed | Flags.ShieldsUp },
                { "Undocked", new { StationType = "Coriolis" }, false },
                { Flags.InMainShip | Flags.ShieldsUp },
                { Flags.InMainShip | Flags.ShieldsUp, GuiFocus.GalaxyMap },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.HudInAnalysisMode },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.ScoopingFuel },
                { Flags.InMainShip | Flags.ShieldsUp },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.IsInDanger },
                { "UnderAttack", new { Target = "You" }, true },
                { Flags.InMainShip | Flags.ShieldsUp },
                { Flags.FsdCharging | Flags.InMainShip | Flags.ShieldsUp },
#pragma warning disable IDE0050
                { "StartJump", new { JumpType = "Hyperspace", StarClass = "G" }, true },
#pragma warning restore IDE0050
                { Flags.FsdJump | Flags.InMainShip | Flags.ShieldsUp },
                { "FSDJump", new { StarSystem = "Wolf 1301" }, true },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.FsdCooldown | Flags.Supercruise },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.FsdCooldown | Flags.Supercruise, GuiFocus.FssMode },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.FsdCooldown | Flags.Supercruise },
                { "SupercruiseExit", new { BodyType = "Station" }, false },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.LightsOn | Flags.NightVision | Flags.CargoScoopDeployed | Flags.HardpointsDeployed | Flags.LandingGearDeployed },
                { Flags.FsdCharging | Flags.InMainShip | Flags.ShieldsUp },
                { "StartJump", new { JumpType = "Supercruise" }, true },
                { Flags.FsdJump | Flags.InMainShip | Flags.ShieldsUp },
                { "SupercruiseExit", new { BodyType = "Planet" }, false },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.LandingGearDeployed },
                { "Touchdown", new { PlayerControlled = true }, false },
                { Flags.InMainShip | Flags.ShieldsUp | Flags.LandingGearDeployed | Flags.Landed },
                { Flags.InSrv | Flags.ShieldsUp | Flags.SrvTurretRetracted | Flags.LightsOn },
                { Flags.InSrv | Flags.ShieldsUp | Flags.HudInAnalysisMode | Flags.SrvUsingTurretView | Flags.LightsOn | Flags.SrvHighBeam },
                { Flags.InSrv | Flags.ShieldsUp | Flags.NightVision | Flags.CargoScoopDeployed },
            };
        }

        private sealed class EventSequence : IReadOnlyCollection<Event>
        {
            private readonly List<Event> _events = new List<Event>();

            public int Count => _events.Count;

            public static string BuildEvent(string eventName, object data)
            {
#pragma warning disable IDE0050
                string tsJson = JsonConvert.SerializeObject(new
                {
                    timestamp = DateTimeOffset.UtcNow,
                    @event = eventName,
                });
#pragma warning restore IDE0050

                string entryJson = JsonConvert.SerializeObject(data);

                string json = $"{tsJson[0..^1]},{entryJson[1..]}";

                return $"{json}\r\n";
            }

            public void Add(string eventName, object data, bool changesGameState)
            {
                string json = BuildEvent(eventName, data);
                _events.Add(new Event(eventName == "Status", json, changesGameState));
            }

            [SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "Used implicitly in BuidEventSequence")]
            public void Add(Flags flags, GuiFocus guiFocus = GuiFocus.None)
            {
#pragma warning disable IDE0050
                var data = new
                {
                    Flags = flags,
                    Pips = new[] { 4, 4, 4 },
                    FireGroup = 0,
                    GuiFocus = guiFocus,
                };
#pragma warning restore IDE0050

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

        private sealed class Event
        {
            public Event(bool isStatus, string json, bool changesGameState)
            {
                IsStatus = isStatus;
                Json = json;
                ChangesGameState = changesGameState;
            }

            public bool IsStatus { get; }

            public string Json { get; }

            public bool ChangesGameState { get; }
        }

        private sealed class NativeMethodsMock : NativeMethodsStub
        {
            private readonly bool _sdkAvailable;

            public NativeMethodsMock(bool sdkAvailable)
            {
                _sdkAvailable = sdkAvailable;
            }

            public override IntPtr LoadLibrary(string lpFileName)
            {
                Assert.Contains(lpFileName, new[] { "RzChromaSDK64.dll", "RzChromaSDK.dll" });
                return _sdkAvailable ? new IntPtr(1234) : IntPtr.Zero;
            }

            public override bool FreeLibrary(IntPtr hModule)
            {
                return true;
            }
        }
    }
}
