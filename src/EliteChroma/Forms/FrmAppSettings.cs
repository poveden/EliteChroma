using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EliteFiles;

namespace EliteChroma.Forms
{
    public partial class FrmAppSettings : Form
    {
        public FrmAppSettings()
        {
            InitializeComponent();
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
            folderBrowser.Description = "Select the folder where Elite:Dangerous stores user-related game options.\r\nxxxxx";
            folderBrowser.SelectedPath = txtGameOptions.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtGameOptions.Text = folderBrowser.SelectedPath;
                ValidateChildren();
            }
        }

        private void BtnJournal_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = "Select the folder where Elite:Dangerous stores the player journal.\r\nxxxxx";
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
            if (!new GameInstallFolder(txtGameOptions.Text).IsValid)
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
    }
}
