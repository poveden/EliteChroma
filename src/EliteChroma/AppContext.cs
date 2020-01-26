using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Forms;
using EliteChroma.Internal;
using EliteChroma.Properties;
using EliteFiles;
using Microsoft.Win32;

namespace EliteChroma
{
    internal class AppContext : TrayIconApplicationContext
    {
        private ChromaController _cc;

        public AppContext()
        {
            if (!ValidateFolders())
            {
                return;
            }

            var settings = AppSettings.Default;
            _cc = new ChromaController(settings.GameInstallFolder, settings.GameOptionsFolder, settings.JournalFolder);
            _cc.Start();

            this.TrayIcon.Icon = Resources.EliteChromaIcon;

            ContextMenu.Items.Add("&Settings...", null, Settings_Click);
            ContextMenu.Items.Add("-");
            ContextMenu.Items.Add("&About...", null, About_Click);
            ContextMenu.Items.Add("-");
            ContextMenu.Items.Add("E&xit", null, Exit_Click);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _cc.Dispose();
        }

        private void Settings_Click(object sender, EventArgs eventArgs)
        {
            using var frm = new FrmAppSettings();
            
            var settings = AppSettings.Default;

            frm.txtGameInstall.Text = settings.GameInstallFolder;
            frm.txtGameOptions.Text = settings.GameOptionsFolder;
            frm.txtJournal.Text = settings.JournalFolder;

            if (frm.ShowDialog(ContextMenu) == DialogResult.OK)
            {
                _cc.Dispose();

                settings.GameInstallFolder = frm.txtGameInstall.Text;
                settings.GameOptionsFolder = frm.txtGameOptions.Text;
                settings.JournalFolder = frm.txtJournal.Text;

                settings.Save();

                _cc = new ChromaController(settings.GameInstallFolder, settings.GameOptionsFolder, settings.JournalFolder);
                _cc.Start();
            }
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

        private bool ValidateFolders()
        {
            var settings = AppSettings.Default;

            var gameInstall = settings.GameInstallFolder;
            var gameOptions = settings.GameOptionsFolder;
            var journal = settings.JournalFolder;

            var firstTimeRun = string.IsNullOrEmpty(gameInstall)
                && string.IsNullOrEmpty(gameOptions)
                && string.IsNullOrEmpty(journal);

            if (firstTimeRun)
            {
                gameInstall = GetPossibleGameInstallFolders().FirstOrDefault(Directory.Exists);
                gameOptions = GameOptionsFolder.DefaultPath;
                journal = JournalFolder.DefaultPath;
            }

            var allValid = new GameInstallFolder(gameInstall).IsValid
                && new GameOptionsFolder(gameOptions).IsValid
                && new JournalFolder(journal).IsValid;

            if (!allValid)
            {
                MessageBox.Show(
                    Resources.MsgBox_UnableToIdentifyFolders,
                    FrmAboutBox.AssemblyTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                using var frm = new FrmAppSettings();
                
                frm.txtGameInstall.Text = gameInstall;
                frm.txtGameOptions.Text = gameOptions;
                frm.txtJournal.Text = journal;

                if (frm.ShowDialog(ContextMenu) != DialogResult.OK)
                {
                    return false;
                }

                gameInstall = frm.txtGameInstall.Text;
                gameOptions = frm.txtGameOptions.Text;
                journal = frm.txtJournal.Text;
            }

            settings.GameInstallFolder = gameInstall;
            settings.GameOptionsFolder = gameOptions;
            settings.JournalFolder = journal;
            settings.Save();

            return true;
        }

        private static IEnumerable<string> GetPossibleGameInstallFolders()
        {
            foreach (var folder in GameInstallFolder.DefaultPaths)
            {
                yield return folder;
            }

            // Reference: https://github.com/Bemoliph/Elite-Dangerous-Downloader/blob/master/downloader.py
            var launcherPath = (string)Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{696F8871-C91D-4CB1-825D-36BE18065575}_is1",
                "InstallLocation",
                null);

            if (launcherPath != null)
            {
                yield return Path.Combine(launcherPath, @"Products\elite-dangerous-64");
                yield return Path.Combine(launcherPath, @"Products\FORC-FDEV-D-1002");
            }

            yield return Path.GetDirectoryName(Application.ExecutablePath);
        }
    }
}
