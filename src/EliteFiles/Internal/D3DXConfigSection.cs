using System.Collections.ObjectModel;

namespace EliteFiles.Internal
{
    internal sealed class D3DXConfigSection : KeyedCollection<string, D3DXConfigEntry>
    {
        public D3DXConfigSection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        protected override string GetKeyForItem(D3DXConfigEntry item)
        {
            return item.Name;
        }
    }
}
