using EliteChroma.Core.Elite.Internal;
using EliteChroma.Core.Tests.Internal;
using EliteFiles;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class GameProcessTrackerTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private static readonly string _mainExePath = new GameInstallFolder(_gameRootFolder).MainExecutable.FullName;

        [Fact]
        public void IsGameProcessMethodReturnsExpectedValues()
        {
            var nmMock = new NativeMethodsProcessMock
            {
                Processes =
                {
                    [0] = "System",
                    [1000] = "Process 1000",
                    [2000] = _mainExePath,
                },
            };

            var gpt = new GameProcessTracker(_mainExePath, nmMock);

            Assert.False(gpt.IsGameProcess(0));
            Assert.False(gpt.IsGameProcess(1000));
            Assert.True(gpt.IsGameProcess(2000));
            Assert.False(gpt.IsGameProcess(1000));
            Assert.True(gpt.IsGameProcess(2000));
        }

        [Fact]
        public void IsGameRunningMethodWithoutFullScanWontCheckForNewProcesses()
        {
            var nmMock = new NativeMethodsProcessMock
            {
                Processes =
                {
                    [0] = "System",
                    [1000] = "Process 1000",
                    [2000] = _mainExePath,
                },
            };

            var gpt = new GameProcessTracker(_mainExePath, nmMock);

            Assert.False(gpt.IsGameRunning(false));

            gpt.IsGameProcess(2000);
            Assert.True(gpt.IsGameRunning(false));

            nmMock.Processes.Remove(2000);
            Assert.False(gpt.IsGameRunning(false));

            nmMock.Processes.Remove(1000);
            nmMock.Processes.Add(2000, _mainExePath);
            Assert.False(gpt.IsGameRunning(false));
        }

        [Fact]
        public void IsGameRunningMethodWithFullScanWillCheckForNewProcesses()
        {
            var nmMock = new NativeMethodsProcessMock
            {
                Processes =
                {
                    [0] = "System",
                    [1000] = "Process 1000",
                    [2000] = _mainExePath,
                },
            };

            var gpt = new GameProcessTracker(_mainExePath, nmMock);

            Assert.True(gpt.IsGameRunning(true));

            nmMock.Processes.Remove(2000);
            Assert.False(gpt.IsGameRunning(true));

            nmMock.Processes.Remove(1000);
            nmMock.Processes.Add(2000, _mainExePath);
            Assert.True(gpt.IsGameRunning(true));
        }

        [Fact]
        public void TryGetProcessFileNameMethodHandlesPInvokeFailures()
        {
            var nmMock = new NativeMethodsProcessMock
            {
                Processes =
                {
                    [0] = "System",
                    [1000] = "Process 1000",
                    [2000] = _mainExePath,
                },
            };

            var gpt = new GameProcessTracker(_mainExePath, nmMock);

            Assert.True(gpt.IsGameProcess(2000));

            nmMock.OpenProcessFailure = true;
            Assert.False(gpt.IsGameRunning(false));
            Assert.False(gpt.IsGameProcess(2000));
            nmMock.OpenProcessFailure = false;

            Assert.True(gpt.IsGameProcess(2000));

            nmMock.EnumProcessModulesFailure = true;
            Assert.False(gpt.IsGameRunning(false));
            Assert.False(gpt.IsGameProcess(2000));
            nmMock.EnumProcessModulesFailure = false;

            Assert.True(gpt.IsGameProcess(2000));

            nmMock.GetModuleFileNameExFailure = true;
            Assert.False(gpt.IsGameRunning(false));
            Assert.False(gpt.IsGameProcess(2000));
            nmMock.GetModuleFileNameExFailure = false;

            Assert.True(gpt.IsGameProcess(2000));
        }
    }
}
