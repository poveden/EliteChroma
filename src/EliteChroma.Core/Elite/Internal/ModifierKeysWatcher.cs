using System.Diagnostics.CodeAnalysis;
using System.Timers;
using EliteChroma.Core.Internal;
using EliteFiles.Bindings;
using EliteFiles.Bindings.Devices;
using static EliteChroma.Core.Internal.NativeMethods;
using Timer = System.Timers.Timer;

namespace EliteChroma.Core.Elite.Internal
{
    internal sealed class ModifierKeysWatcher : NativeMethodsAccessor, IDisposable
    {
        private readonly Dictionary<VirtualKey, DeviceKey> _watch;
        private readonly Timer _timer;

        private DeviceKeySet? _currPressed;
        private bool _running;
        private bool _disposed;

        public ModifierKeysWatcher(INativeMethods nativeMethods)
            : base(nativeMethods)
        {
            _watch = new Dictionary<VirtualKey, DeviceKey>();

            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = false,
            };
            _timer.Elapsed += Timer_Elapsed;
        }

        public event EventHandler<DeviceKeySet>? Changed;

        public void Watch(IEnumerable<DeviceKey> modifiers, string? keyboardLayout, bool enUSOverride)
        {
            _watch.Clear();

            foreach (DeviceKey m in modifiers.Where(x => x.Device == Device.Keyboard && x.Key != null))
            {
                if (KeyMappings.TryGetKey(m.Key!, keyboardLayout, enUSOverride, out VirtualKey key, NativeMethods))
                {
                    _watch[key] = m;
                }
            }
        }

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _timer.Start();
            _running = true;
        }

        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _timer.Stop();
            _running = false;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _timer?.Dispose();
            _disposed = true;
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Will rethrow exceptions into calling thread")]
        private async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                var newPressed = new DeviceKeySet(GetAllPressedModifiers());

                if (!newPressed.Equals(_currPressed))
                {
                    _currPressed = newPressed;

                    Changed?.Invoke(this, _currPressed);
                }
            }
            catch (Exception ex)
            {
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer?view=netcore-5.0#remarks
                await Task.FromException(ex).ConfigureAwait(false);
            }
        }

        private IEnumerable<DeviceKey> GetAllPressedModifiers()
        {
            foreach (KeyValuePair<VirtualKey, DeviceKey> kv in _watch)
            {
                if ((NativeMethods.GetAsyncKeyState(kv.Key) & 0x8000) != 0)
                {
                    yield return kv.Value;
                }
            }
        }
    }
}
