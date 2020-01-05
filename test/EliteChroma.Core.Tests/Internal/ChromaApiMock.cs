using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Colore.Api;
using Colore.Data;
using Colore.Effects.ChromaLink;
using Colore.Effects.Generic;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;

namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class ChromaApiMock : IChromaApi
    {
        public event EventHandler<MockCall> Called;

        public Task<Guid> CreateChromaLinkEffectAsync(ChromaLinkEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateChromaLinkEffectAsync<T>(ChromaLinkEffect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateDeviceEffectAsync(Guid deviceId, Effect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateDeviceEffectAsync<T>(Guid deviceId, Effect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateHeadsetEffectAsync(HeadsetEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateHeadsetEffectAsync<T>(HeadsetEffect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateKeyboardEffectAsync(KeyboardEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateKeyboardEffectAsync<T>(KeyboardEffect effect, T data)
            where T : struct
        {
            Called?.Invoke(this, new MockCall(nameof(CreateKeyboardEffectAsync), data));
            return Task.FromResult(Guid.NewGuid());
        }

        public Task<Guid> CreateKeypadEffectAsync(KeypadEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateKeypadEffectAsync<T>(KeypadEffect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateMouseEffectAsync(MouseEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateMouseEffectAsync<T>(MouseEffect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateMousepadEffectAsync(MousepadEffect effect)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateMousepadEffectAsync<T>(MousepadEffect effect, T data)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public Task DeleteEffectAsync(Guid effectId) => Task.CompletedTask;

        public Task InitializeAsync(AppInfo info)
        {
            Called?.Invoke(this, new MockCall(nameof(InitializeAsync), info));
            return Task.CompletedTask;
        }

        public Task<SdkDeviceInfo> QueryDeviceAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public void RegisterEventNotifications(IntPtr windowHandle)
        {
            throw new NotImplementedException();
        }

        public Task SetEffectAsync(Guid effectId) => Task.CompletedTask;

        public Task UninitializeAsync()
        {
            Called?.Invoke(this, new MockCall(nameof(UninitializeAsync)));
            return Task.CompletedTask;
        }

        public void UnregisterEventNotifications()
        {
            throw new NotImplementedException();
        }

        public sealed class MockCall
        {
            internal MockCall(string method, params object[] args)
            {
                Method = method;
                Args = args;
            }

            public string Method { get; }

            public IList<object> Args { get; }
        }
    }
}
