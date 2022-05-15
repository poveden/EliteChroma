using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;
using System.Text;
using EliteChroma.Properties;
using EliteFiles;

namespace EliteChroma.Forms.Pages
{
    internal partial class PageGameFolders : UserControl
    {
        private readonly List<ToolStripMenuItem> _gameInstallFolders;

        public PageGameFolders()
        {
            InitializeComponent();

            _gameInstallFolders = CreateGameInstallFolderMenuItems();

            string[] urls = new[]
            {
                Resources.Url_GameInstallFoldersHelp,
                Resources.Url_GameOptionsFolderHelp,
                Resources.Url_JournalFolderHelp,
            };

            ApplyLinks(linkGameFolders, urls);
            ApplyIcon(pbInformation, SystemIcons.Information);
            ApplyIcon(pbWarning, SystemIcons.Warning);
        }

        public event EventHandler<string>? Error;

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

        private static void ApplyIcon(PictureBox pictureBox, Icon icon)
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);

            using var g = Graphics.FromImage(pictureBox.Image);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(Bitmap.FromHicon(icon.Handle), pictureBox.DisplayRectangle);
        }

        private List<ToolStripMenuItem> CreateGameInstallFolderMenuItems()
        {
            var res = new List<ToolStripMenuItem>();

            IEnumerable<string> allPossibleFolders = EliteFiles.GameInstallFolder.DefaultPaths
                .Concat(EliteFiles.GameInstallFolder.GetAlternatePaths())
                .Where(Directory.Exists);

            foreach (string folder in allPossibleFolders)
            {
                ToolStripMenuItem mi = CreateGameInstallFolderMenuItem(folder);
                res.Add(mi);
            }

            return res;
        }

        private ToolStripMenuItem CreateGameInstallFolderMenuItem(string folder)
        {
            var mi = new ToolStripMenuItem
            {
                Text = folder.Replace("&", "&&", StringComparison.Ordinal),
                Tag = folder,
            };

            mi.Click += TsmiGameInstallFolder_Click;

            return mi;
        }

        private void PageGameFolders_Load(object? sender, EventArgs e)
        {
            for (int i = 0; i < _gameInstallFolders.Count; i++)
            {
                ctxGameInstall.Items.Insert(i, _gameInstallFolders[i]);
            }
        }

        private void CtxGameInstall_Opening(object? sender, CancelEventArgs e)
        {
            foreach (ToolStripMenuItem item in _gameInstallFolders)
            {
                item.Checked = txtGameInstall.Text == (string)item.Tag;
            }
        }

        private void TsmiGameInstallFolder_Click(object? sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender!;
            txtGameInstall.Text = (string)item.Tag;
            _ = ValidateChildren();
        }

        [ExcludeFromCodeCoverage]
        private void TsmiGameInstallBrowse_Click(object? sender, EventArgs e)
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

        [ExcludeFromCodeCoverage]
        private void BtnGameOptions_Click(object? sender, EventArgs e)
        {
            folderBrowser.Description = Resources.FolderDialogDescription_GameOptionsFolder;
            folderBrowser.SelectedPath = txtGameOptions.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtGameOptions.Text = folderBrowser.SelectedPath;
                _ = ValidateChildren();
            }
        }

        [ExcludeFromCodeCoverage]
        private void BtnJournal_Click(object? sender, EventArgs e)
        {
            folderBrowser.Description = Resources.FolderDialogDescription_JournalFolder;
            folderBrowser.SelectedPath = txtJournal.Text;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtJournal.Text = folderBrowser.SelectedPath;
                _ = ValidateChildren();
            }
        }

        private void TxtGameInstall_Validating(object? sender, CancelEventArgs e)
        {
            if (!new GameInstallFolder(txtGameInstall.Text).IsValid)
            {
                e.Cancel = true;
                SetError(txtGameInstall, "Invalid game install folder");
            }
        }

        private void TxtGameInstall_Validated(object? sender, EventArgs e)
        {
            SetError(txtGameInstall, string.Empty);
        }

        private void TxtGameOptions_Validating(object? sender, CancelEventArgs e)
        {
            if (!new GameOptionsFolder(txtGameOptions.Text).IsValid)
            {
                e.Cancel = true;
                SetError(txtGameOptions, "Invalid game options folder");
            }
        }

        private void TxtGameOptions_Validated(object? sender, EventArgs e)
        {
            SetError(txtGameOptions, string.Empty);
        }

        private void TxtJournal_Validating(object? sender, CancelEventArgs e)
        {
            if (!new JournalFolder(txtJournal.Text).IsValid)
            {
                e.Cancel = true;
                SetError(txtJournal, "Invalid journal folder");
            }
        }

        private void TxtJournal_Validated(object? sender, EventArgs e)
        {
            SetError(txtJournal, string.Empty);
        }

        [ExcludeFromCodeCoverage]
        private void LinkGameFolders_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            // Reference: https://stackoverflow.com/a/53245993/400347
            var ps = new ProcessStartInfo((string)e.Link.LinkData)
            {
                UseShellExecute = true,
                Verb = "open",
            };

            Process.Start(ps)?.Dispose();
        }

        private void SetError(Control control, string value)
        {
            Error?.Invoke(control, value);
        }
    }
}
