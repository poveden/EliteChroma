using System;
using System.Collections.Generic;
using System.Threading;
using EliteChroma.Core.Internal;
using EliteChroma.Elite.Internal;
using EliteFiles;
using EliteFiles.Bindings;
using EliteFiles.Graphics;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Elite
{
    public sealed class GameStateWatcher : IDisposable
    {
        private readonly JournalWatcher _journalWatcher;
        private readonly StatusWatcher _statusWatcher;
        private readonly BindingsWatcher _bindingsWatcher;
        private readonly GraphicsConfigWatcher _graphicsConfig;
        private readonly GameProcessWatcher _gameProcessWatcher;

        private readonly ModifierKeysWatcher _modifierKeysWatcher;

        private readonly GameState _gameState;

        private bool _running;
        private int _dispatching;

        private bool _disposed;

        public GameStateWatcher(string gameInstallFolder, string gameOptionsFolder, string journalFolder)
            : this(gameInstallFolder, gameOptionsFolder, journalFolder, NativeMethods.Instance)
        {
        }

        internal GameStateWatcher(string gameInstallFolder, string gameOptionsFolder, string journalFolder, INativeMethods nativeMethods)
        {
            var gif = new GameInstallFolder(gameInstallFolder);
            var gof = new GameOptionsFolder(gameOptionsFolder);
            var jf = new JournalFolder(journalFolder);

            _journalWatcher = new JournalWatcher(jf);
            _journalWatcher.Started += JournalWatcher_Started;
            _journalWatcher.EntryAdded += JournalWatcher_EntryAdded;

            _statusWatcher = new StatusWatcher(jf);
            _statusWatcher.Changed += StatusWatcher_Changed;

            _bindingsWatcher = new BindingsWatcher(gif, gof);
            _bindingsWatcher.Changed += BindingsWatcher_Changed;

            _graphicsConfig = new GraphicsConfigWatcher(gif, gof);
            _graphicsConfig.Changed += GraphicsConfig_Changed;

            _modifierKeysWatcher = new ModifierKeysWatcher(nativeMethods);
            _modifierKeysWatcher.Changed += ModifierKeysWatcher_Changed;

            _gameProcessWatcher = new GameProcessWatcher(gif, nativeMethods);
            _gameProcessWatcher.Changed += GameProcessWatcher_Changed;

            _gameState = new GameState();
        }

        public event EventHandler<EventArgs> Changed;

        public bool DetectForegroundProcess { get; set; } = true;

        public bool RaisePreStartupEvents { get; set; }

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _running = true;

            _graphicsConfig.Start();
            _bindingsWatcher.Start();
            _statusWatcher.Start();
            _modifierKeysWatcher.Start();
            _journalWatcher.Start();

            if (DetectForegroundProcess)
            {
                _gameProcessWatcher.Start();
            }
        }

        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _running = false;

            _modifierKeysWatcher.Stop();
            _journalWatcher.Stop();
            _statusWatcher.Stop();
            _bindingsWatcher.Stop();
            _graphicsConfig.Stop();
            _gameProcessWatcher.Stop();
        }

        public GameState GetGameStateSnapshot()
        {
            lock (_gameState)
            {
                return _gameState.Copy();
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Stop();

            _journalWatcher.Dispose();
            _statusWatcher.Dispose();
            _bindingsWatcher.Dispose();
            _graphicsConfig.Dispose();
            _modifierKeysWatcher.Dispose();
            _gameProcessWatcher.Dispose();

            _disposed = true;
        }

        private static IEnumerable<DeviceKey> GetAllModifiers(IEnumerable<Binding> bindings)
        {
            var res = new HashSet<DeviceKey>();

            foreach (var binding in bindings)
            {
                foreach (var modifier in binding.Primary.Modifiers)
                {
                    res.Add(modifier);
                }

                foreach (var modifier in binding.Secondary.Modifiers)
                {
                    res.Add(modifier);
                }
            }

            return res;
        }

        private void JournalWatcher_Started(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void JournalWatcher_EntryAdded(object sender, JournalEntry e)
        {
            switch (e)
            {
                case FileHeader _ when !DetectForegroundProcess:
                    _gameState.ProcessState = GameProcessState.InForeground;
                    break;

                case Shutdown _ when !DetectForegroundProcess:
                    _gameState.ProcessState = GameProcessState.NotRunning;
                    break;

                case StartJump fsdJump:
                    _gameState.FsdJumpType = fsdJump.JumpType;
                    _gameState.FsdJumpStarClass = fsdJump.StarClass;
                    _gameState.FsdJumpChange = DateTimeOffset.UtcNow;
                    break;

                case Music music:
                    _gameState.MusicTrack = music.MusicTrack;
                    break;

                default:
                    switch (e.Event)
                    {
                        case "FSDJump": // Happens when entering a new system from hyperspace.
                        case "SupercruiseEntry": // Happens when entering supercruise
                            _gameState.FsdJumpType = StartJump.FsdJumpType.None;
                            _gameState.FsdJumpStarClass = null;
                            _gameState.FsdJumpChange = DateTimeOffset.UtcNow;
                            break;

                        default:
                            // Event won't affect gamestate.
                            return;
                    }

                    break;
            }

            OnChanged();
        }

        private void StatusWatcher_Changed(object sender, StatusEntry e)
        {
            _gameState.Status = e;
            OnChanged();
        }

        private void BindingsWatcher_Changed(object sender, BindingPreset e)
        {
            _modifierKeysWatcher.Watch(GetAllModifiers(e.Bindings.Values));
            _gameState.Bindings = e.Bindings;
            OnChanged();
        }

        private void GraphicsConfig_Changed(object sender, GraphicsConfig e)
        {
            _gameState.GuiColour = e.GuiColour.Default;
            OnChanged();
        }

        private void ModifierKeysWatcher_Changed(object sender, DeviceKeySet e)
        {
            _gameState.PressedModifiers = e;
            OnChanged();
        }

        private void GameProcessWatcher_Changed(object sender, GameProcessState e)
        {
            _gameState.ProcessState = e;
            OnChanged();
        }

        private void OnChanged()
        {
            if (!_journalWatcher.IsWatching && !RaisePreStartupEvents)
            {
                return;
            }

            if (Interlocked.Exchange(ref _dispatching, 1) == 1)
            {
                return;
            }

            try
            {
                Changed?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                Interlocked.Exchange(ref _dispatching, 0);
            }
        }
    }
}
