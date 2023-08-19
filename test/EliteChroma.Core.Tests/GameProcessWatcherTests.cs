﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Timers;
using EliteChroma.Core.Elite;
using EliteChroma.Core.Elite.Internal;
using EliteChroma.Core.Tests.Internal;
using EliteFiles;
using TestUtils;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class GameProcessWatcherTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private static readonly GameInstallFolder _gif = new GameInstallFolder(_gameRootFolder);

        private static readonly ConstructorInfo _ciElapsedEventArgs = typeof(ElapsedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(DateTime) }, null)!;

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
            var evs = new EventCollector<GameProcessState>(h => gpw.Changed += h, h => gpw.Changed -= h, nameof(WatchesForGameProcessChanges));

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

        [Fact]
        public void OnChangedIsNotReentrant()
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

            int nOnChangedCalls = 0;
            using var mre = new ManualResetEventSlim();

            gpw.Changed += (sender, e) =>
            {
                Interlocked.Increment(ref nOnChangedCalls);
                mre.Wait();
            };

            void TimerElapsed(int pid)
            {
                nm.ForegroundWindow = new IntPtr(pid);
                InvokeTimerElapsed(gpw);
                mre.Set();
            }

            Task.WaitAll(
                Task.Run(() => TimerElapsed(1000)),
                Task.Run(() => TimerElapsed(2000)));

            Assert.Equal(1, nOnChangedCalls);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var gpw = new GameProcessWatcher(_gif, new NativeMethodsMock());

            bool IsRunning()
            {
                return gpw.GetPrivateField<bool>("_running");
            }

            Assert.False(IsRunning());

            gpw.Start();
            Assert.True(IsRunning());

            gpw.Start();
            Assert.True(IsRunning());

            gpw.Stop();
            Assert.False(IsRunning());

            gpw.Stop();
            Assert.False(IsRunning());
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            var pw = new GameProcessWatcher(_gif, new NativeMethodsMock());
            Assert.False(pw.GetPrivateField<bool>("_disposed"));

            pw.Dispose();
            Assert.True(pw.GetPrivateField<bool>("_disposed"));

            pw.Dispose();
            Assert.True(pw.GetPrivateField<bool>("_disposed"));
        }

        private static void InvokeTimerElapsed(GameProcessWatcher instance)
        {
            // HACK: Hopefully will no longer be needed when .NET (Future) arrives (https://github.com/dotnet/runtime/issues/31204)
            var e = (ElapsedEventArgs)_ciElapsedEventArgs.Invoke(new object[] { DateTime.Now });

            instance.InvokePrivateMethod<object>("Timer_Elapsed", instance, e);
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
