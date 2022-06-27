namespace EliteChroma.Forms.Pages
{
    internal partial class PageGeneral : UserControl
    {
        public PageGeneral()
        {
            InitializeComponent();
        }

        public bool DetectGameInForeground
        {
            get => chDetectGameProcess.Checked;
            set => chDetectGameProcess.Checked = value;
        }
    }
}
