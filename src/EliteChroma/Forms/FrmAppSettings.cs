using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
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
    internal partial class FrmAppSettings : Form
    {
        private const string _gameFoldersSection = "GameFolders";
        private const string _keyboardSection = "Keyboard";
        private const string _colorsSection = "Colors";

        private readonly HashSet<string> _sectionErrors = new HashSet<string>(StringComparer.Ordinal);
        private readonly List<ToolStripMenuItem> _gameInstallFolders;

        static FrmAppSettings()
        {
            ChromaColorsMetadata.InitTypeDescriptionProvider();
        }

        public FrmAppSettings()
        {
            InitializeComponent();

            _gameInstallFolders = CreateGameInstallFolderMenuItems();
            tsmiGameInstallBrowse.Click += TsmiGameInstallBrowse_Click;

            string[] urls = new[]
            {
                Resources.Url_GameInstallFoldersHelp,
                Resources.Url_GameOptionsFolderHelp,
                Resources.Url_JournalFolderHelp,
            };

            ApplyLinks(linkGameFolders, urls);
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

        public override bool ValidateChildren()
        {
            bool res = base.ValidateChildren();
            tvSections.Refresh();
            return res;
        }

        private static void ApplyLinks(LinkLabel linkLabel, IEnumerable<string> urls)
        {
            string template = linkLabel.Text;
            var finalTxt = new StringBuilder(template.Length);
            int j = -1;

            foreach (string url in urls)
            {
                int i = template.IndexOf('{', j + 1);
                _ = finalTxt.Append(template, j + 1, i - j - 1);

                j = template.IndexOf('}', i + 1);
                int l = j - i - 1;
                string linkTxt = template.Substring(i + 1, l);

                _ = linkLabel.Links.Add(new LinkLabel.Link(finalTxt.Length, l, url));
                _ = finalTxt.Append(linkTxt);
            }

            _ = finalTxt.Append(template, j + 1, template.Length - j - 1);

            linkLabel.Text = finalTxt.ToString();
        }

        private List<ToolStripMenuItem> CreateGameInstallFolderMenuItems()
        {
            var res = new List<ToolStripMenuItem>();

            IEnumerable<string> allPossibleFolders = EliteFiles.GameInstallFolder.DefaultPaths
                .Concat(EliteFiles.GameInstallFolder.GetAlternatePaths())
                .Where(Directory.Exists);

            foreach (string folder in allPossibleFolders)
            {
                var mi = new ToolStripMenuItem
                {
                    Text = folder.Replace("&", "&&", StringComparison.Ordinal),
                    Tag = folder,
                };

                mi.Click += TsmiGameInstallFolder_Click;

                res.Add(mi);
            }

            return res;
        }

        private void AppSettings_Load(object sender, EventArgs e)
        {
            tvSections.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvSections.Nodes[_gameFoldersSection].Tag = grpEDFolders;
            tvSections.Nodes[_keyboardSection].Tag = pnlKeyboard;
            tvSections.Nodes[_colorsSection].Tag = pnlColors;

            tvSections.SelectedNode = tvSections.Nodes[_gameFoldersSection];

            pgColors.SelectedGridItem = pgColors.GetGridItems()[0];

            for (int i = 0; i < _gameInstallFolders.Count; i++)
            {
                ctxGameInstall.Items.Insert(i, _gameInstallFolders[i]);
            }

            _ = ValidateChildren();
        }

        private void CtxGameInstall_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripMenuItem item in _gameInstallFolders)
            {
                item.Checked = txtGameInstall.Text == (string)item.Tag;
            }
        }

        private void TsmiGameInstallFolder_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            txtGameInstall.Text = (string)item.Tag;
            _ = ValidateChildren();
        }

        private void TsmiGameInstallBrowse_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = Resources.FolderDialogDescription_GameInstallFolder;
            folderBrowser.SelectedPath = txtGameInstall.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowser.SelectedPath;

                if (!new GameInstallFolder(path).IsValid)
                {
                    // Perhaps the user chose the base folder where all E:D variants are installed
                    foreach (string edPath in new[]
                    {
                        @"Products\elite-dangerous-64",
                        @"Products\elite-dangerous-odyssey-64",
                    })
                    {
                        string ed64SubPath = Path.Combine(path, edPath);

                        if (new GameInstallFolder(ed64SubPath).IsValid)
                        {
                            path = ed64SubPath;
                            break;
                        }
                    }
                }

                txtGameInstall.Text = path;
                _ = ValidateChildren();
            }
        }

        private void BtnGameOptions_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = Resources.FolderDialogDescription_GameOptionsFolder;
            folderBrowser.SelectedPath = txtGameOptions.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtGameOptions.Text = folderBrowser.SelectedPath;
                _ = ValidateChildren();
            }
        }

        private void BtnJournal_Click(object sender, EventArgs e)
        {
            folderBrowser.Description = Resources.FolderDialogDescription_JournalFolder;
            folderBrowser.SelectedPath = txtJournal.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtJournal.Text = folderBrowser.SelectedPath;
                _ = ValidateChildren();
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

            _ = Process.Start(ps);
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
            Brush b = e.State.HasFlag(TreeNodeStates.Focused) ? SystemBrushes.HighlightText : SystemBrushes.ControlText;
            e.Graphics.DrawString(e.Node.Text, tvSections.Font, b, e.Bounds);

            if (!_sectionErrors.Contains(e.Node.Name))
            {
                return;
            }

            int x = e.Bounds.Right - 4;
            int y = e.Bounds.Top + ((e.Bounds.Height - Resources.RedDot.Height) / 2);
            var r = new Rectangle(new Point(x, y), Resources.RedDot.Size);
            e.Graphics.DrawImageUnscaledAndClipped(Resources.RedDot, r);
        }

        private void SetError(string sectionKey, Control control, string value)
        {
            errorProvider.SetError(control, value);

            if (string.IsNullOrEmpty(value))
            {
                _ = _sectionErrors.Remove(sectionKey);
            }
            else
            {
                _ = _sectionErrors.Add(sectionKey);
            }
        }
    }
}
