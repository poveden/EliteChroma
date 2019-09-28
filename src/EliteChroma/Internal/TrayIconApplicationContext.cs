using System;
using System.Windows.Forms;

namespace EliteChroma.Internal
{
    internal abstract class TrayIconApplicationContext : ApplicationContext
    {
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

            this.TrayIcon.MouseClick += TrayIcon_MouseClick;
            this.TrayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
        }

        protected ContextMenuStrip ContextMenu { get; }

        protected NotifyIcon TrayIcon { get; }

        protected virtual void OnApplicationExit(EventArgs e)
        {
            ContextMenu?.Dispose();

            if (TrayIcon != null)
            {
                TrayIcon.Visible = false;
                TrayIcon.Dispose();
            }
        }

        protected virtual void OnTrayIconClick(MouseEventArgs e)
        {
        }

        protected virtual void OnTrayIconDoubleClick(MouseEventArgs e)
        {
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.OnApplicationExit(e);
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            OnTrayIconClick(e);
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnTrayIconDoubleClick(e);
        }
    }
}
