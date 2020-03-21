using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Timers;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite;
using EliteChroma.Elite.Internal;
using EliteFiles;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class GameProcessWatcherTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private static readonly GameInstallFolder _gif = new GameInstallFolder(_gameRootFolder);

        private static readonly ConstructorInfo _ciElapsedEventArgs = typeof(ElapsedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(long) }, null);
        private static readonly MethodInfo _miTimerElapsed = typeof(GameProcessWatcher).GetMethod("Timer_Elapsed", BindingFlags.NonPublic | BindingFlags.Instance);

        [Fact]
        public void WatchesForGameProcessChanges()
        {
            var nm = new NativeMethodsMock()
            {
                Processes =
                {
                    [0] = "System",
                    [1000] = "Process 1000",
                    [2000] = _gif.MainExecutable.FullName,
                },
            };

            using var gpw = new GameProcessWatcher(_gif, nm);
            var evs = new EventCollector<GameProcessState>(h => gpw.Changed += h, h => gpw.Changed -= h);

            var pss = evs.Wait(3, () =>
            {
                InvokeTimerElapsed(gpw);

                nm.ForegroundWindow = new IntPtr(2000);
                InvokeTimerElapsed(gpw);

                nm.ForegroundWindow = new IntPtr(1000);
                InvokeTimerElapsed(gpw);
            });

            Assert.Equal(GameProcessState.NotRunning, pss[0]);
            Assert.Equal(GameProcessState.InForeground, pss[1]);
            Assert.Equal(GameProcessState.InBackground, pss[2]);
        }

        private static void InvokeTimerElapsed(GameProcessWatcher instance)
        {
            // HACK: Hopefully will no longer be needed when .NET 5.0 arrives (https://github.com/dotnet/runtime/issues/31204)
            var e = (ElapsedEventArgs)_ciElapsedEventArgs.Invoke(new object[] { DateTime.Now.ToFileTime() });

            _miTimerElapsed.Invoke(instance, new object[] { instance, e });
        }

        private sealed class NativeMethodsMock : NativeMethodsProcessMock
        {
            public IntPtr ForegroundWindow { get; set; }

            public override IntPtr GetForegroundWindow()
            {
                return ForegroundWindow;
            }

            public override int GetWindowThreadProcessId(IntPtr hWnd, out int processId)
            {
                int id = hWnd.ToInt32();
                processId = id;
                return id;
            }
        }
    }
}
