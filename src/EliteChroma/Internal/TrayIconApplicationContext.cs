using System.Diagnostics.CodeAnalysis;

namespace EliteChroma.Internal
{
    [ExcludeFromCodeCoverage]
    internal abstract class TrayIconApplicationContext : ApplicationContext
    {
        private bool _disposed;

        protected TrayIconApplicationContext()
        {
            ContextMenu = new ContextMenuStrip();

            Application.ApplicationExit += Application_ApplicationExit;

            TrayIcon = new NotifyIcon
            {
                Text = Application.ProductName,
                Visible = true,
                ContextMenuStrip = ContextMenu,
            };

            TrayIcon.MouseClick += TrayIcon_MouseClick;
            TrayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
        }

        protected ContextMenuStrip ContextMenu { get; }

        protected NotifyIcon TrayIcon { get; }

        protected virtual void OnApplicationExit(EventArgs e)
        {
            if (_disposed)
            {
                return;
            }

            ContextMenu.Dispose();
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
        }

        protected virtual void OnTrayIconClick(MouseEventArgs e)
        {
        }

        protected virtual void OnTrayIconDoubleClick(MouseEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            base.Dispose(disposing);

            if (disposing)
            {
                ContextMenu.Dispose();
                TrayIcon.Dispose();
            }

            _disposed = true;
        }

        private void Application_ApplicationExit(object? sender, EventArgs e)
        {
            OnApplicationExit(e);
        }

        private void TrayIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            OnTrayIconClick(e);
        }

        private void TrayIcon_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            OnTrayIconDoubleClick(e);
        }
    }
}
