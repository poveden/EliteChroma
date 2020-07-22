using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Colore;

namespace EliteChroma.Core.Windows.Internal
{
    internal sealed class ChromaWindow : NativeWindow, IDisposable
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Win32 handle constant")]
        private static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        private bool _disposed;

        public ChromaWindow()
        {
            CreateHandle(new CreateParams
            {
                Parent = HWND_MESSAGE,
            });
        }

        public IChroma Chroma { get; set; }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            ReleaseHandle();
            _disposed = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (HandleMessage(m))
            {
                return;
            }

            base.WndProc(ref m);
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "IChroma.HandleMessage() should not throw exceptions in a message pump.")]
        private bool HandleMessage(Message m)
        {
            if (Chroma == null || m.Msg != Colore.Data.Constants.WmChromaEvent)
            {
                return false;
            }

            try
            {
                return Chroma.HandleMessage(Handle, m.Msg, m.WParam, m.LParam);
            }
            catch
            {
                return false;
            }
        }
    }
}
