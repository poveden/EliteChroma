using System.Diagnostics.CodeAnalysis;
using EliteChroma.Core;
using EliteChroma.Properties;

namespace EliteChroma.Forms
{
    internal partial class FrmAppSettings : Form
    {
        private const string _gameFoldersSection = "GameFolders";
        private const string _generalSection = "General";
        private const string _keyboardSection = "Keyboard";
        private const string _colorsSection = "Colors";

        private readonly HashSet<string> _sectionErrors = new HashSet<string>(StringComparer.Ordinal);

        public FrmAppSettings()
        {
            InitializeComponent();
            InitializeComponentInternal();
        }

        public string GameInstallFolder
        {
            get => pgGameFolders.GameInstallFolder;
            set => pgGameFolders.GameInstallFolder = value;
        }

        public string GameOptionsFolder
        {
            get => pgGameFolders.GameOptionsFolder;
            set => pgGameFolders.GameOptionsFolder = value;
        }

        public string JournalFolder
        {
            get => pgGameFolders.JournalFolder;
            set => pgGameFolders.JournalFolder = value;
        }

        public bool DetectGameInForeground
        {
            get => pgGeneral.DetectGameInForeground;
            set => pgGeneral.DetectGameInForeground = value;
        }

        public bool ForceEnUSKeyboardLayout
        {
            get => pgKeyboard.ForceEnUSKeyboardLayout;
            set => pgKeyboard.ForceEnUSKeyboardLayout = value;
        }

        public ChromaColors Colors
        {
            get => pgColors.Colors;
            set => pgColors.Colors = value;
        }

        public override bool ValidateChildren()
        {
            bool res = base.ValidateChildren();
            tvSections.Refresh();
            return res;
        }

        private void InitializeComponentInternal()
        {
            tvSections.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvSections.Nodes[_gameFoldersSection].Tag = pgGameFolders;
            tvSections.Nodes[_generalSection].Tag = pgGeneral;
            tvSections.Nodes[_keyboardSection].Tag = pgKeyboard;
            tvSections.Nodes[_colorsSection].Tag = pgColors;
        }

        [ExcludeFromCodeCoverage]
        private void AppSettings_Load(object? sender, EventArgs e)
        {
            tvSections.SelectedNode = tvSections.Nodes[0];

            _ = ValidateChildren();
        }

        [ExcludeFromCodeCoverage]
        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        [ExcludeFromCodeCoverage]
        private void TvSections_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            ((Control)e.Node!.Tag).Visible = true;

            foreach (TreeNode node in tvSections.Nodes)
            {
                if (node == e.Node)
                {
                    continue;
                }

                ((Control)node.Tag).Visible = false;
            }
        }

        [ExcludeFromCodeCoverage]
        private void TvSections_DrawNode(object? sender, DrawTreeNodeEventArgs e)
        {
            Brush b = e.State.HasFlag(TreeNodeStates.Focused) ? SystemBrushes.HighlightText : SystemBrushes.ControlText;
            e.Graphics.DrawString(e.Node!.Text, tvSections.Font, b, e.Bounds);

            if (!_sectionErrors.Contains(e.Node.Name))
            {
                return;
            }

            int x = e.Bounds.Right - 4;
            int y = e.Bounds.Top + ((e.Bounds.Height - Resources.RedDot.Height) / 2);
            var r = new Rectangle(new Point(x, y), Resources.RedDot.Size);
            e.Graphics.DrawImageUnscaledAndClipped(Resources.RedDot, r);
        }

        private void PgGameFolders_Error(object? sender, string e)
        {
            var control = (Control)sender!;

            if (control is TextBox textBox)
            {
                errorProvider.SetIconPadding(textBox, -20);
            }

            SetError(_gameFoldersSection, control, e);
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

            tvSections.Refresh();
        }
    }
}
