using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Forms;
using EliteChroma.Internal;
using EliteChroma.Properties;

namespace EliteChroma
{
    [ExcludeFromCodeCoverage]
    internal class AppContext : TrayIconApplicationContext
    {
        private readonly string _appSettingsPath;

        private ChromaController? _cc;
        private AppSettings? _currentSettings;

        public AppContext(string? appSettingsPath = null)
        {
            _appSettingsPath = appSettingsPath ?? AppSettings.GetDefaultPath();

            TrayIcon.Icon = Resources.EliteChromaIcon;

#pragma warning disable IDISP004
            _ = ContextMenu.Items.Add("&Settings...", null, Settings_Click);
            _ = ContextMenu.Items.Add("-");
            _ = ContextMenu.Items.Add("&About...", null, About_Click);
            _ = ContextMenu.Items.Add("-");
            _ = ContextMenu.Items.Add("E&xit", null, Exit_Click);
#pragma warning restore IDISP004
            ContextMenu.Opening += ContextMenu_Opening;
        }

        public bool Start()
        {
            if (!ChromaController.IsChromaSdkAvailable())
            {
                _ = MessageBox.Show(
                    Resources.MsgBox_RazerChromaSdkNotFound,
                    new AssemblyInfo().Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return false;
            }

            var settings = AppSettings.Load(_appSettingsPath);

            if (!settings.IsValid())
            {
                _ = MessageBox.Show(
                    Resources.MsgBox_UnableToIdentifyFolders,
                    new AssemblyInfo().Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                if (!EditSettingsDialog(settings))
                {
                    return false;
                }
            }

            settings.Save(_appSettingsPath);
            CycleChromaController(settings);
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _cc?.Dispose();
            }
        }

        protected override void OnTrayIconClick(MouseEventArgs e)
        {
            _ = BringToFrontAnyOpenModalForm();
        }

        protected override void OnTrayIconDoubleClick(MouseEventArgs e)
        {
            _ = BringToFrontAnyOpenModalForm();
        }

        private static bool BringToFrontAnyOpenModalForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Modal)
                {
                    frm.BringToFront();
                    return true;
                }
            }

            return false;
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (BringToFrontAnyOpenModalForm())
            {
                e.Cancel = true;
            }
        }

        private void Settings_Click(object? sender, EventArgs eventArgs)
        {
            var settings = AppSettings.Load(_appSettingsPath);

            if (!EditSettingsDialog(settings))
            {
                return;
            }

            settings.Save(_appSettingsPath);

            CycleChromaController(settings);
        }

        private void About_Click(object? sender, EventArgs eventArgs)
        {
            using var frm = new FrmAboutBox();
            _ = frm.ShowDialog(ContextMenu);
        }

        private void Exit_Click(object? sender, EventArgs eventArgs)
        {
            _cc?.Stop();
            ExitThread();
        }

        private bool EditSettingsDialog(AppSettings settings)
        {
            using var frm = new FrmAppSettings
            {
                GameInstallFolder = settings.GameInstallFolder!,
                GameOptionsFolder = settings.GameOptionsFolder!,
                JournalFolder = settings.JournalFolder!,
                DetectGameInForeground = settings.DetectGameInForeground,
                ForceEnUSKeyboardLayout = settings.ForceEnUSKeyboardLayout,
                Colors = settings.Colors,
            };

            if (frm.ShowDialog(ContextMenu) != DialogResult.OK)
            {
                return false;
            }

            settings.GameInstallFolder = frm.GameInstallFolder;
            settings.GameOptionsFolder = frm.GameOptionsFolder;
            settings.JournalFolder = frm.JournalFolder;
            settings.DetectGameInForeground = frm.DetectGameInForeground;
            settings.ForceEnUSKeyboardLayout = frm.ForceEnUSKeyboardLayout;

            return true;
        }

        private void CycleChromaController(AppSettings settings)
        {
            if (_cc != null && CanSoftCycle(settings))
            {
                _cc.ForceEnUSKeyboardLayout = settings.ForceEnUSKeyboardLayout;
                _cc.Colors = settings.Colors;
                _cc.Refresh();
                _currentSettings = settings;
                return;
            }

            _cc?.Dispose();
            _cc = new ChromaController(settings.GameInstallFolder!, settings.GameOptionsFolder!, settings.JournalFolder!)
            {
                DetectGameInForeground = settings.DetectGameInForeground,
                ForceEnUSKeyboardLayout = settings.ForceEnUSKeyboardLayout,
                Colors = settings.Colors,
            };
            _cc.Start();
            _currentSettings = settings;
        }

        private bool CanSoftCycle(AppSettings newSettings)
        {
            if (_currentSettings == null)
            {
                return false;
            }

            return string.Equals(_currentSettings.GameInstallFolder, newSettings.GameInstallFolder, StringComparison.OrdinalIgnoreCase)
                && string.Equals(_currentSettings.GameOptionsFolder, newSettings.GameOptionsFolder, StringComparison.OrdinalIgnoreCase)
                && string.Equals(_currentSettings.JournalFolder, newSettings.JournalFolder, StringComparison.OrdinalIgnoreCase)
                && _currentSettings.DetectGameInForeground == newSettings.DetectGameInForeground;
        }
    }
}
