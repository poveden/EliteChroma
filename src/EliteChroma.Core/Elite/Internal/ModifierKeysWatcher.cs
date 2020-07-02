using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using EliteChroma.Core.Internal;
using EliteFiles.Bindings;
using EliteFiles.Bindings.Devices;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Elite.Internal
{
    internal sealed class ModifierKeysWatcher : NativeMethodsAccessor, IDisposable
    {
        private readonly Dictionary<VirtualKey, DeviceKey> _watch;
        private readonly Timer _timer;

        private DeviceKeySet _currPressed;

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

        public event EventHandler<DeviceKeySet> Changed;

        public void Watch(IEnumerable<DeviceKey> modifiers)
        {
            _watch.Clear();

            foreach (var m in modifiers.Where(x => x.Device == Device.Keyboard))
            {
                var key = KeyMappings.EliteKeys[m.Key];
                _watch[key] = m;
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Will rethrow exceptions into calling thread")]
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
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
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer?view=netcore-3.1#remarks
                await Task.FromException(ex).ConfigureAwait(false);
            }
        }

        private IEnumerable<DeviceKey> GetAllPressedModifiers()
        {
            foreach (var kv in _watch)
            {
                if ((NativeMethods.GetAsyncKeyState(kv.Key) & 0x8000) != 0)
                {
                    yield return kv.Value;
                }
            }
        }
    }
}
