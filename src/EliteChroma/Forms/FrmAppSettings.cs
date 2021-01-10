using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Internal;
using EliteChroma.Internal.UI;
using EliteChroma.Properties;
using EliteFiles;

namespace EliteChroma.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class FrmAppSettings : Form
    {
        private const string _gameFoldersSection = "GameFolders";
        private const string _keyboardSection = "Keyboard";
        private const string _colorsSection = "Colors";

        private readonly HashSet<string> _sectionErrors = new HashSet<string>(StringComparer.Ordinal);

        static FrmAppSettings()
        {
            ChromaColorsMetadata.InitTypeDescriptionProvider();
        }

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

        public string GameInstallFolder
        {
            get => txtGameInstall.Text;
            set => txtGameInstall.Text = value;
        }

        public string GameOptionsFolder
        {
            get => txtGameOptions.Text;
            set => txtGameOptions.Text = value;
        }

        public string JournalFolder
        {
            get => txtJournal.Text;
            set => txtJournal.Text = value;
        }

        public bool ForceEnUSKeyboardLayout
        {
            get => chEnUSOverride.Checked;
            set => chEnUSOverride.Checked = value;
        }

        public ChromaColors Colors
        {
            get => (ChromaColors)pgColors.SelectedObject;
            set => pgColors.SelectedObject = value;
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
            tvSections.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvSections.Nodes[_gameFoldersSection].Tag = grpEDFolders;
            tvSections.Nodes[_keyboardSection].Tag = pnlKeyboard;
            tvSections.Nodes[_colorsSection].Tag = pnlColors;

            tvSections.SelectedNode = tvSections.Nodes[_gameFoldersSection];

            pgColors.SelectedGridItem = pgColors.GetGridItems()[0];

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
                SetError(_gameFoldersSection, txtGameInstall, "Invalid game install folder");
            }
        }

        private void TxtGameInstall_Validated(object sender, EventArgs e)
        {
            SetError(_gameFoldersSection, txtGameInstall, string.Empty);
        }

        private void TxtGameOptions_Validating(object sender, CancelEventArgs e)
        {
            if (!new GameOptionsFolder(txtGameOptions.Text).IsValid)
            {
                e.Cancel = true;
                SetError(_gameFoldersSection, txtGameOptions, "Invalid game options folder");
            }
        }

        private void TxtGameOptions_Validated(object sender, EventArgs e)
        {
            SetError(_gameFoldersSection, txtGameOptions, string.Empty);
        }

        private void TxtJournal_Validating(object sender, CancelEventArgs e)
        {
            if (!new JournalFolder(txtJournal.Text).IsValid)
            {
                e.Cancel = true;
                SetError(_gameFoldersSection, txtJournal, "Invalid journal folder");
            }
        }

        private void TxtJournal_Validated(object sender, EventArgs e)
        {
            SetError(_gameFoldersSection, txtJournal, string.Empty);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LinkGameFolders_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Reference: https://stackoverflow.com/a/53245993/400347
            var ps = new ProcessStartInfo((string)e.Link.LinkData)
            {
                UseShellExecute = true,
                Verb = "open",
            };

            Process.Start(ps);
        }

        private void TvSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ((Control)e.Node.Tag).Visible = true;

            foreach (TreeNode node in tvSections.Nodes)
            {
                if (node == e.Node)
                {
                    continue;
                }

                ((Control)node.Tag).Visible = false;
            }
        }

        private void TvSections_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var b = e.State.HasFlag(TreeNodeStates.Focused) ? SystemBrushes.HighlightText : SystemBrushes.ControlText;
            e.Graphics.DrawString(e.Node.Text, tvSections.Font, b, e.Bounds);

            if (!_sectionErrors.Contains(e.Node.Name))
            {
                return;
            }

            var x = e.Bounds.Right - 4;
            var y = e.Bounds.Top + (e.Bounds.Height - Resources.RedDot.Height) / 2;
            var r = new Rectangle(new Point(x, y), Resources.RedDot.Size);
            e.Graphics.DrawImageUnscaledAndClipped(Resources.RedDot, r);
        }

        public override bool ValidateChildren()
        {
            var res = base.ValidateChildren();
            tvSections.Refresh();
            return res;
        }

        private void SetError(string sectionKey, Control control, string value)
        {
            errorProvider.SetError(control, value);

            if (string.IsNullOrEmpty(value))
            {
                _sectionErrors.Remove(sectionKey);
            }
            else
            {
                _sectionErrors.Add(sectionKey);
            }
        }

        private void PgColors_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var v = e.ChangedItem.Value;
        }
    }
}
