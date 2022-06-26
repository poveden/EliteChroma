using System.Diagnostics.CodeAnalysis;
using EliteFiles.Internal;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents an <see href="https://github.com/psychicEgg/EDHM">Elite Dangerous HUD Mod</see> configuration file.
    /// </summary>
    public sealed class EdhmConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdhmConfig"/> class.
        /// </summary>
        public EdhmConfig()
        {
            Constants = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            Files = new List<string>();
        }

        /// <summary>
        /// Gets the constants defined in the configuration.
        /// </summary>
        public IDictionary<string, double> Constants { get; }

        /// <summary>
        /// Gets the list of files that compose this configuration.
        /// </summary>
        public IList<string> Files { get; }

        /// <summary>
        /// Read the EDHM configuration from the given D3DX file.
        /// </summary>
        /// <param name="path">The path to the D3DX file.</param>
        /// <returns>The EDHM configuration, or <c>null</c> if the file couldn't be read (e.g. in the middle of an update).</returns>
        public static EdhmConfig? FromFile(string path)
        {
            var config = D3DXConfig.FromFile(path);

            if (config == null)
            {
                return null;
            }

            var res = new EdhmConfig();

            foreach (string file in config.Files)
            {
                res.Files.Add(file);
            }

            if (!config.Sections.TryGetValue("Constants", out D3DXConfigSection? constants))
            {
                return res;
            }

            foreach (D3DXConfigEntry entry in constants)
            {
                res.Constants.Add(entry.Name, entry.NumericValue);
            }

            return res;
        }

        /// <summary>
        /// Gets the RGB colour matrix.
        /// </summary>
        /// <returns>The RGB colour matrix, or <c>null</c> if at least one of the required values is not defined in the configuration.</returns>
        public IRgbTransformMatrix? GetColourMatrix()
        {
            return ColourMatrix.FromConfig(this);
        }

        [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Complete array")]
        private sealed class ColourMatrix : IRgbTransformMatrix
        {
            private static readonly string[,] _keys =
            {
                { "x150", "y150", "z150" },
                { "x151", "y151", "z151" },
                { "x152", "y152", "z152" },
            };

            private readonly double[,] _values;

            private ColourMatrix(double[,] values)
            {
                _values = values;
            }

            public double this[int row, int col] => _values[row, col];

            public static ColourMatrix? FromConfig(EdhmConfig config)
            {
                double[,] res = new double[3, 3];

                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        string key = _keys[row, col];

                        if (config.Constants.TryGetValue(key, out double value) && !double.IsNaN(value))
                        {
                            res[row, col] = value;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                return new ColourMatrix(res);
            }
        }
    }
}
