using System.Diagnostics.CodeAnalysis;
using ChromaWrapper.Sdk;
using EliteChroma.Chroma;
using EliteChroma.Core.Internal;
using EliteChroma.Elite;
using EliteFiles;

namespace EliteChroma.Core
{
    public sealed class ChromaController : IDisposable
    {
        private const int _defaultFps = 30; // Confirmed at Razer DevCon 2021.

        private readonly GameStateWatcher _watcher;
        private readonly ChromaEffect<LayerRenderState> _effect;
        private readonly System.Timers.Timer _animation;
        private readonly SemaphoreSlim _chromaLock = new SemaphoreSlim(1, 1);

        private ChromaColors _colors = new ChromaColors();

        private IChromaSdk? _chroma;
        private int _rendering;
        private int _fps;
        private DateTimeOffset _chromaWarmupUntil;

        private bool _running;
        private bool _disposed;

        [ExcludeFromCodeCoverage]
        public ChromaController(string gameInstallFolder)
            : this(gameInstallFolder, GameOptionsFolder.DefaultPath, JournalFolder.DefaultPath)
        {
        }

        public ChromaController(string gameInstallFolder, string gameOptionsFolder, string journalFolder)
        {
            _watcher = new GameStateWatcher(gameInstallFolder, gameOptionsFolder, journalFolder);
            _watcher.Changed += GameState_Changed;

            _animation = new System.Timers.Timer
            {
                AutoReset = true,
                Enabled = false,
            };
            _animation.Elapsed += Animation_Elapsed;
            AnimationFrameRate = _defaultFps;

            _effect = InitChromaEffect();
        }

        public IChromaFactory ChromaFactory { get; set; } = new ChromaFactory();

        public int AnimationFrameRate
        {
            get => _fps;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(AnimationFrameRate));
                }

                _fps = value;
                if (_fps > 0)
                {
                    _animation.Interval = 1000.0 / _fps;
                }
            }
        }

        public bool DetectGameInForeground
        {
            get => _watcher.DetectForegroundProcess;
            set => _watcher.DetectForegroundProcess = value;
        }

        public bool ForceEnUSKeyboardLayout
        {
            get => _watcher.ForceEnUSKeyboardLayout;
            set => _watcher.ForceEnUSKeyboardLayout = value;
        }

        public ChromaColors Colors
        {
            get => _colors;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                _colors = value;
            }
        }

        [ExcludeFromCodeCoverage]
        public static bool IsChromaSdkAvailable()
        {
            return IsChromaSdkAvailable(NativeMethods.Instance);
        }

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _watcher.Start();
            _running = true;
        }

        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _watcher.Stop();
            ChromaStop().Wait();
            _running = false;
        }

        public void Refresh()
        {
            if (!_running)
            {
                throw new InvalidOperationException("Controller is currently stopped.");
            }

            _animation.Enabled = true;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Stop();
            _watcher?.Dispose();
            _animation?.Dispose();
            _chromaLock.Dispose();
            _disposed = true;
        }

        internal static bool IsChromaSdkAvailable(INativeMethods nativeMethods)
        {
            bool is64Bit = Environment.Is64BitProcess && Environment.Is64BitOperatingSystem;
            string dllName = is64Bit ? "RzChromaSDK64.dll" : "RzChromaSDK.dll";

            IntPtr hModule = nativeMethods.LoadLibrary(dllName);

            if (hModule != IntPtr.Zero)
            {
                _ = nativeMethods.FreeLibrary(hModule);
                return true;
            }

            return false;
        }

        private static ChromaEffect<LayerRenderState> InitChromaEffect()
        {
            IEnumerable<LayerBase> layers =
                from type in typeof(LayerBase).Assembly.GetTypes()
                where type.IsSubclassOf(typeof(LayerBase)) && !type.IsAbstract
                select (LayerBase)Activator.CreateInstance(type)!;

            var res = new ChromaEffect<LayerRenderState>();

            foreach (LayerBase layer in layers)
            {
                _ = res.Add(layer);
            }

            return res;
        }

        private async Task ChromaStart()
        {
            await _chromaLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_chroma != null)
                {
                    return;
                }

                _chroma = await ChromaFactory.CreateAsync().ConfigureAwait(false);
                _chromaWarmupUntil = DateTimeOffset.UtcNow.Add(ChromaFactory.WarmupDelay);
            }
            finally
            {
                _ = _chromaLock.Release();
            }
        }

        private async Task ChromaStop()
        {
            await _chromaLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_chroma == null)
                {
                    return;
                }

                _chroma.Dispose();
                _chroma = null;
            }
            finally
            {
                _ = _chromaLock.Release();
            }
        }

        private async void GameState_Changed(object? sender, GameStateWatcher.ChangeType e)
        {
            if (_animation.Enabled)
            {
                // No need to add extra frames while animating.
                return;
            }

            await RenderEffect().ConfigureAwait(false);
        }

        private async void Animation_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await RenderEffect().ConfigureAwait(false);
        }

        private async Task RenderEffect()
        {
            if (Interlocked.Exchange(ref _rendering, 1) == 1)
            {
                return;
            }

            try
            {
                GameState game = _watcher.GetGameStateSnapshot();

                if (game.ProcessState == GameProcessState.NotRunning)
                {
                    await ChromaStop().ConfigureAwait(false);
                    return;
                }

                await ChromaStart().ConfigureAwait(false);

                await _chromaLock.WaitAsync().ConfigureAwait(false);
                try
                {
                    game.Now = DateTimeOffset.UtcNow;
                    _effect.Render(_chroma!, new LayerRenderState(game, _colors));
                }
                finally
                {
                    _ = _chromaLock.Release();
                }

                _animation.Enabled = AnimationFrameRate > 0
                    && (_chromaWarmupUntil > DateTimeOffset.UtcNow || _effect.Layers.OfType<LayerBase>().Any(x => x.Animated));
            }
            finally
            {
                _ = Interlocked.Exchange(ref _rendering, 0);
            }
        }
    }
}
