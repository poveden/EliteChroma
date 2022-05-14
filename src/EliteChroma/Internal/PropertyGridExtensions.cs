using System.Diagnostics.CodeAnalysis;

namespace EliteChroma.Internal
{
    internal static class PropertyGridExtensions
    {
        // Reference: https://stackoverflow.com/questions/5169179/how-to-enumerate-propertygrid-items/51670577#51670577
        [ExcludeFromCodeCoverage]
        public static GridItemCollection GetGridItems(this PropertyGrid propertyGrid)
        {
            GridItem gi = propertyGrid.SelectedGridItem;

            while (gi.GridItemType != GridItemType.Root)
            {
                gi = gi.Parent!;
            }

            return gi.GridItems;
        }
    }
}
