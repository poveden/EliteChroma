using EliteChroma.Core.Elite.Internal;
using EliteChroma.Core.Internal;
using EliteFiles;
using EliteFiles.Bindings;
using EliteFiles.Graphics;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Core.Elite
{
    public sealed class GameStateWatcher : IDisposable
    {
        private readonly JournalWatcher _journalWatcher;
        private readonly StatusWatcher _statusWatcher;
        private readonly BindingsWatcher _bindingsWatcher;
        private readonly GraphicsConfigWatcher _graphicsConfig;
        private readonly GameProcessWatcher _gameProcessWatcher;
        private readonly EdhmConfigWatcher _edhmConfigWatcher;

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

            _edhmConfigWatcher = new EdhmConfigWatcher(gif);
            _edhmConfigWatcher.Changed += EdhmConfigWatcher_Changed;

            _gameState = new GameState();
        }

        public event EventHandler<ChangeType>? Changed;

        public enum ChangeType
        {
            JournalDrain,
            JournalEntry,
            StatusEntry,
            BindingPreset,
            GraphicsConfig,
            DeviceKeySet,
            GameProcessState,
            EdhmConfig,
        }

        public bool DetectForegroundProcess { get; set; } = true;

        public bool ForceEnUSKeyboardLayout
        {
            get => _gameState.ForceEnUSKeyboardLayout;
            set => _gameState.ForceEnUSKeyboardLayout = value;
        }

        public bool RaisePreStartupEvents { get; set; }

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _running = true;

            _graphicsConfig.Start();
            _edhmConfigWatcher.Start();
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
            _edhmConfigWatcher.Stop();
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
            _edhmConfigWatcher.Dispose();

            _disposed = true;
        }

        private void JournalWatcher_Started(object? sender, EventArgs e)
        {
            OnChanged(ChangeType.JournalDrain);
        }

        private void JournalWatcher_EntryAdded(object? sender, JournalEntry e)
        {
            switch (e)
            {
                case FileHeader when !DetectForegroundProcess:
                    _gameState.ProcessState = GameProcessState.InForeground;
                    _gameState.GameMode = LoadGame.PlayMode.None;
                    break;

                case Shutdown when !DetectForegroundProcess:
                    _gameState.ProcessState = GameProcessState.NotRunning;
                    _gameState.GameMode = LoadGame.PlayMode.None;
                    break;

                case StartJump fsdJump:
                    _gameState.FsdJumpType = fsdJump.JumpType;
                    _gameState.FsdJumpStarClass = fsdJump.StarClass;
                    _gameState.FsdJumpChange = DateTimeOffset.UtcNow;
                    break;

                case Music music:
                    _gameState.MusicTrack = music.MusicTrack;

                    if (_gameState.MusicTrack == Music.MainMenu)
                    {
                        _gameState.GameMode = LoadGame.PlayMode.None;
                    }

                    break;

                case UnderAttack underAttack:
                    _gameState.AttackTarget = underAttack.Target;
                    _gameState.AttackTargetChange = DateTimeOffset.UtcNow;
                    break;

                case LoadGame loadGame:
                    _gameState.Horizons = loadGame.Horizons;
                    _gameState.Odyssey = loadGame.Odyssey;
                    _gameState.GameMode = loadGame.GameMode;
                    break;

                default:
                    switch (e.Event)
                    {
                        case "FSDJump": // Happens when a new system has been entered from hyperspace.
                        case "SupercruiseEntry": // Happens when supercruise has been entered.
                        case "SupercruiseExit": // Happens when supercruise hass been exited.
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

            OnChanged(ChangeType.JournalEntry);
        }

        private void StatusWatcher_Changed(object? sender, StatusEntry e)
        {
            _gameState.Status = e;
            OnChanged(ChangeType.StatusEntry);
        }

        private void BindingsWatcher_Changed(object? sender, BindingPreset e)
        {
            var gb = new GameBindings(e);
            _gameState.Bindings = gb;
            _modifierKeysWatcher.Watch(gb.Modifiers, gb.KeyboardLayout, _gameState.ForceEnUSKeyboardLayout);
            OnChanged(ChangeType.BindingPreset);
        }

        private void GraphicsConfig_Changed(object? sender, GraphicsConfig e)
        {
            _gameState.GuiColour = e.GuiColour?.Default ?? GuiColourMatrix.Default;
            OnChanged(ChangeType.GraphicsConfig);
        }

        private void ModifierKeysWatcher_Changed(object? sender, DeviceKeySet e)
        {
            _gameState.PressedModifiers = e;
            OnChanged(ChangeType.DeviceKeySet);
        }

        private void GameProcessWatcher_Changed(object? sender, GameProcessState e)
        {
            _gameState.ProcessState = e;
            OnChanged(ChangeType.GameProcessState);
        }

        private void EdhmConfigWatcher_Changed(object? sender, EdhmConfig e)
        {
            _gameState.EdhmConfig = e;
            OnChanged(ChangeType.EdhmConfig);
        }

        private void OnChanged(ChangeType changeType)
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
                Changed?.Invoke(this, changeType);
            }
            finally
            {
                _ = Interlocked.Exchange(ref _dispatching, 0);
            }
        }
    }
}
