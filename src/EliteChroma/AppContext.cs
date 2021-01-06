using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Core.Windows;
using EliteChroma.Forms;
using EliteChroma.Internal;
using EliteChroma.Properties;

namespace EliteChroma
{
    [ExcludeFromCodeCoverage]
    internal class AppContext : TrayIconApplicationContext
    {
        private readonly string _appSettingsPath;
        private readonly WinChromaFactory _chromaFactory;

        private ChromaController _cc;

        public AppContext(string appSettingsPath = null)
        {
            _appSettingsPath = appSettingsPath ?? AppSettings.GetDefaultPath();

            this.TrayIcon.Icon = Resources.EliteChromaIcon;

            ContextMenu.Items.Add("&Settings...", null, Settings_Click);
            ContextMenu.Items.Add("-");
            ContextMenu.Items.Add("&About...", null, About_Click);
            ContextMenu.Items.Add("-");
            ContextMenu.Items.Add("E&xit", null, Exit_Click);

            _chromaFactory = new WinChromaFactory();
        }

        public bool Start()
        {
            if (!ChromaController.IsChromaSdkAvailable())
            {
                MessageBox.Show(
                    Resources.MsgBox_RazerChromaSdkNotFound,
                    new AssemblyInfo().Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return false;
            }

            var settings = AppSettings.Load(_appSettingsPath);

            if (!settings.IsValid())
            {
                MessageBox.Show(
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
            _cc?.Dispose();
            _chromaFactory.Dispose();
        }

        private void Settings_Click(object sender, EventArgs eventArgs)
        {
            var settings = AppSettings.Load(_appSettingsPath);

            if (!EditSettingsDialog(settings))
            {
                return;
            }

            settings.Save(_appSettingsPath);

            CycleChromaController(settings);
        }

        private void About_Click(object sender, EventArgs eventArgs)
        {
            using var frm = new FrmAboutBox();
            frm.ShowDialog(ContextMenu);
        }

        private void Exit_Click(object sender, EventArgs eventArgs)
        {
            _cc.Stop();
            ExitThread();
        }

        private bool EditSettingsDialog(AppSettings settings)
        {
            using var frm = new FrmAppSettings();

            frm.txtGameInstall.Text = settings.GameInstallFolder;
            frm.txtGameOptions.Text = settings.GameOptionsFolder;
            frm.txtJournal.Text = settings.JournalFolder;
            frm.chEsUSOverride.Checked = settings.ForceEnUSKeyboardLayout;

            if (frm.ShowDialog(ContextMenu) != DialogResult.OK)
            {
                return false;
            }

            settings.GameInstallFolder = frm.txtGameInstall.Text;
            settings.GameOptionsFolder = frm.txtGameOptions.Text;
            settings.JournalFolder = frm.txtJournal.Text;
            settings.ForceEnUSKeyboardLayout = frm.chEsUSOverride.Checked;

            return true;
        }

        private void CycleChromaController(AppSettings settings)
        {
            _cc?.Dispose();
            _cc = new ChromaController(settings.GameInstallFolder, settings.GameOptionsFolder, settings.JournalFolder)
            {
                ChromaFactory = _chromaFactory,
                ForceEnUSKeyboardLayout = settings.ForceEnUSKeyboardLayout,
                Colors = settings.Colors,
            };
            _cc.Start();
        }
    }
}
