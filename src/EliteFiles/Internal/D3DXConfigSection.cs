namespace EliteFiles.Internal
{
    internal sealed class D3DXConfigSection
    {
        private readonly List<D3DXConfigEntry> _entries = new List<D3DXConfigEntry>();

        public void Add(D3DXConfigEntry entry)
        {
            _entries.Add(entry);
        }

        public IEnumerable<D3DXConfigEntry> GetEntries(Func<string, bool> conditionEvaluator)
        {
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (D3DXConfigEntry entry in _entries)
            {
                // We mimic what 3dmigoto does (see https://github.com/bo3b/3Dmigoto/blob/master/DirectX11/IniHandler.cpp)
                if (!names.Add(entry.Name))
                {
                    continue;
                }

                if (entry.Conditions.All(conditionEvaluator))
                {
                    yield return entry;
                }
            }
        }
    }
}
