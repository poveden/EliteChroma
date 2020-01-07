using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EliteChroma.Properties;
using EliteFiles;

namespace EliteChroma.Forms
{
    public partial class FrmAppSettings : Form
    {
        public FrmAppSettings()
        {
            InitializeComponent();

            ApplyLinks(linkGameFolders, new[]
            {
                Resources.Url_GameInstallFoldersHelp,
                Resources.Url_GameOptionsFolderHelp,
                Resources.Url_JournalFolderHelp,
            });
        }

        private static void ApplyLinks(LinkLabel linkLabel, IEnumerable<string> urls)
        {
            var template = linkLabel.Text;
            var finalTxt = new StringBuilder(template.Length);
            var j = -1;

            foreach (var url in urls)
            {
                var i = template.IndexOf('{', j + 1);
                finalTxt.Append(template, j + 1, i - j - 1);

                j = template.IndexOf('}', i + 1);
                var l = j - i - 1;
                var linkTxt = template.Substring(i + 1, l);

                linkLabel.Links.Add(new LinkLabel.Link(finalTxt.Length, l, url));
                finalTxt.Append(linkTxt);
            }

            finalTxt.Append(template, j + 1, template.Length - j - 1);

            linkLabel.Text = finalTxt.ToString();
        }

        private void AppSettings_Load(object sender, EventArgs e)
        {
            ValidateChildren();
        }

        private void BtnGameInstall_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = "Select the folder where Elite:Dangerous is installed:";
            folderBrowser.SelectedPath = txtGameInstall.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtGameInstall.Text = folderBrowser.SelectedPath;
                ValidateChildren();
            }
        }

        private void BtnGameOptions_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = "Select the folder where Elite:Dangerous stores user-related game options:";
            folderBrowser.SelectedPath = txtGameOptions.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtGameOptions.Text = folderBrowser.SelectedPath;
                ValidateChildren();
            }
        }

        private void BtnJournal_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = "Select the folder where Elite:Dangerous stores the player journal:";
            folderBrowser.SelectedPath = txtJournal.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtJournal.Text = folderBrowser.SelectedPath;
                ValidateChildren();
            }
        }

        private void TxtGameInstall_Validating(object sender, CancelEventArgs e)
        {
            if (!new GameInstallFolder(txtGameInstall.Text).IsValid)
            {
                e.Cancel = true;
                errorProvider.SetError(txtGameInstall, "Invalid game install folder");
            }
        }

        private void TxtGameInstall_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtGameInstall, string.Empty);
        }

        private void TxtGameOptions_Validating(object sender, CancelEventArgs e)
        {
            if (!new GameOptionsFolder(txtGameOptions.Text).IsValid)
            {
                e.Cancel = true;
                errorProvider.SetError(txtGameOptions, "Invalid game options folder");
            }
        }

        private void TxtGameOptions_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtGameOptions, string.Empty);
        }

        private void TxtJournal_Validating(object sender, CancelEventArgs e)
        {
            if (!new JournalFolder(txtJournal.Text).IsValid)
            {
                e.Cancel = true;
                errorProvider.SetError(txtJournal, "Invalid journal folder");
            }
        }

        private void TxtJournal_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(txtJournal, string.Empty);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void linkGameFolders_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Reference: https://stackoverflow.com/a/53245993/400347
            var ps = new ProcessStartInfo((string)e.Link.LinkData)
            {
                UseShellExecute = true,
                Verb = "open",
            };

            Process.Start(ps);
        }
    }
}
