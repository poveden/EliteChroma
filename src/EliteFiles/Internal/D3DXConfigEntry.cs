using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EliteFiles.Internal
{
    [DebuggerDisplay("{Name} = {Value} (Conditions = {Conditions.Count})")]
    internal sealed class D3DXConfigEntry
    {
        private static readonly Regex _rxEntry = new Regex(@"^(?:(pre\s+)|(post\s+)|(local\s+)|(global\s+)|(persist\s+))*(\$?[\w-]+)\s*=\s*(.*)$");

        public D3DXConfigEntry(string name, IEnumerable<string> conditions)
        {
            Name = name;
            Conditions = new List<string>(conditions);
        }

        public string Name { get; }

        public string Value { get; set; } = string.Empty;

        public bool Local { get; set; }

        public bool Global { get; set; }

        public bool Persist { get; set; }

        public bool Pre { get; set; }

        public bool Post { get; set; }

        public IList<string> Conditions { get; }

        public double NumericValue => double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) ? value : double.NaN;

        public static D3DXConfigEntry? Parse(string str, IEnumerable<string> conditions)
        {
            Match m = _rxEntry.Match(str);

            if (!m.Success)
            {
                return null;
            }

            return new D3DXConfigEntry(m.Groups[6].Value, conditions)
            {
                Pre = m.Groups[1].Success,
                Post = m.Groups[2].Success,
                Local = m.Groups[3].Success,
                Global = m.Groups[4].Success,
                Persist = m.Groups[5].Success,
                Value = m.Groups[7].Value,
            };
        }
    }
}
