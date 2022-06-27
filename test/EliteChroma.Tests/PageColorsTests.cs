using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Forms.Pages;
using EliteChroma.Internal;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class PageColorsTests
    {
        [Fact]
        public void PageColorsHasTheFirstSortedCategorySelectedOnLoad()
        {
            using var page = new PageColors
            {
                Colors = new ChromaColors(),
            };

            var pgColors = page.GetPrivateField<PropertyGrid>("pgColors")!;
            Assert.Equal(PropertySort.CategorizedAlphabetical, pgColors.PropertySort);
            Assert.Equal(GridItemType.Property, pgColors.SelectedGridItem.GridItemType);

            page.CreateControl();

            var sortedCat0 = pgColors.GetGridItems().Cast<GridItem>()
                .OrderBy(x => x.Label, StringComparer.CurrentCulture)
                .First();

            Assert.Equal(GridItemType.Category, pgColors.SelectedGridItem.GridItemType);
            Assert.Equal(sortedCat0, pgColors.SelectedGridItem);
        }
    }
}
