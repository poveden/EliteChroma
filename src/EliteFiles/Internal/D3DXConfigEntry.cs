using System.Globalization;
using System.Text.RegularExpressions;

namespace EliteFiles.Internal
{
    internal sealed class D3DXConfigEntry
    {
        private static readonly Regex _rxEntry = new Regex(@"^(?:(pre\s+)|(post\s+)|(global\s+)|(persist\s+))*(\$?[\w-]+)\s*=\s*(.*)$");

        public D3DXConfigEntry(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Value { get; set; } = string.Empty;

        public bool Global { get; set; }

        public bool Persist { get; set; }

        public bool Pre { get; set; }

        public bool Post { get; set; }

        public double NumericValue => double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) ? value : double.NaN;

        public static D3DXConfigEntry? Parse(string str)
        {
            Match m = _rxEntry.Match(str);

            if (!m.Success)
            {
                return null;
            }

            return new D3DXConfigEntry(m.Groups[5].Value)
            {
                Pre = m.Groups[1].Success,
                Post = m.Groups[2].Success,
                Global = m.Groups[3].Success,
                Persist = m.Groups[4].Success,
                Value = m.Groups[6].Value,
            };
        }
    }
}
