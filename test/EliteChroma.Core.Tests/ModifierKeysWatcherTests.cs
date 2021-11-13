using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite.Internal;
using EliteFiles.Bindings;
using Xunit;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Tests
{
    public class ModifierKeysWatcherTests
    {
        [Fact]
        public void GetsAllPressedModifiers()
        {
            var nmm = new NativeMethodsMock()
            {
                PressedKeys =
                {
                    VirtualKey.VK_LCONTROL,
                    (VirtualKey)'A',
                },
            };

            using var mkw = new ModifierKeysWatcher(nmm);

            var key1 = FromXml<DeviceKey>("<Key1 Device='Keyboard' Key='Key_LeftControl' />");
            var key2 = FromXml<DeviceKey>("<Key1 Device='Keyboard' Key='Key_LeftShift' />");
            var key3 = FromXml<DeviceKey>("<Key2 Device='Keyboard' Key='Key_A' />");

            mkw.Watch(new[] { key1, key2, key3 }, "en-US", false);

            var keys = mkw.InvokePrivateMethod<IEnumerable<DeviceKey>>("GetAllPressedModifiers")!.ToList();

            Assert.Contains(key1, keys);
            Assert.DoesNotContain(key2, keys);
            Assert.Contains(key3, keys);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var mkw = new ModifierKeysWatcher(new NativeMethodsMock());

            bool IsRunning()
            {
                return mkw.GetPrivateField<bool>("_running");
            }

            Assert.False(IsRunning());

            mkw.Start();
            Assert.True(IsRunning());

            mkw.Start();
            Assert.True(IsRunning());

            mkw.Stop();
            Assert.False(IsRunning());

            mkw.Stop();
            Assert.False(IsRunning());
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            var mkw = new ModifierKeysWatcher(new NativeMethodsMock());
            Assert.False(mkw.GetPrivateField<bool>("_disposed"));

            mkw.Dispose();
            Assert.True(mkw.GetPrivateField<bool>("_disposed"));

            mkw.Dispose();
            Assert.True(mkw.GetPrivateField<bool>("_disposed"));
        }

        private static T FromXml<T>(string xml)
            where T : DeviceKeyBase
        {
            var xe = XElement.Parse(xml);

            return typeof(T).InvokePrivateStaticMethod<T>("FromXml", xe)!;
        }

        private sealed class NativeMethodsMock : NativeMethodsStub
        {
            private const short _hiBit = unchecked((short)0x8000);

            public HashSet<VirtualKey> PressedKeys { get; } = new HashSet<VirtualKey>();

            public override IntPtr GetKeyboardLayout(int idThread)
            {
                return new IntPtr(NativeMethodsKeyboardMock.EnUS);
            }

            public override short GetAsyncKeyState(VirtualKey vKey)
            {
                return PressedKeys.Contains(vKey) ? _hiBit : (short)0;
            }
        }
    }
}
