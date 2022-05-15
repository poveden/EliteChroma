namespace EliteChroma.Forms.Pages
{
    internal partial class PageKeyboard : UserControl
    {
        public PageKeyboard()
        {
            InitializeComponent();
        }

        public bool ForceEnUSKeyboardLayout
        {
            get => chEnUSOverride.Checked;
            set => chEnUSOverride.Checked = value;
        }
    }
}
