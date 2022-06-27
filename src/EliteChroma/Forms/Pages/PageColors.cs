using EliteChroma.Core;
using EliteChroma.Internal;
using EliteChroma.Internal.UI;

namespace EliteChroma.Forms.Pages
{
    internal partial class PageColors : UserControl
    {
        static PageColors()
        {
            ChromaColorsMetadata.InitTypeDescriptionProvider();
        }

        public PageColors()
        {
            InitializeComponent();
        }

        public ChromaColors Colors
        {
            get => (ChromaColors)pgColors.SelectedObject;
            set => pgColors.SelectedObject = value;
        }

        private void PageColors_Load(object? sender, EventArgs e)
        {
            // pgColors.PropertySort is set to CategorizedAlphabetical by default,
            // but the initially selected grid item is not the first sorted item in the first sorted category,
            // but the first sorted item overall. Here we select the item that's at the top of the list.
            GridItemCollection allItems = pgColors.GetGridItems();
            pgColors.SelectedGridItem = allItems[0];
        }
    }
}
