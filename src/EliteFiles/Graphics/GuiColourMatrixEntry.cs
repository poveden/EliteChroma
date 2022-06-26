using System.Globalization;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents the single-color component of a GUI colour transformation matrix.
    /// </summary>
    public sealed class GuiColourMatrixEntry
    {
        private readonly double[] _v = new double[3];

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiColourMatrixEntry"/> class.
        /// </summary>
        public GuiColourMatrixEntry()
        {
        }

        internal GuiColourMatrixEntry(double r, double g, double b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        /// <summary>
        /// Gets the red component value.
        /// </summary>
        public double Red
        {
            get => _v[0];
            init => _v[0] = Math.Clamp(value, -1, 1);
        }

        /// <summary>
        /// Gets the green component value.
        /// </summary>
        public double Green
        {
            get => _v[1];
            init => _v[1] = Math.Clamp(value, -1, 1);
        }

        /// <summary>
        /// Gets the blue component value.
        /// </summary>
        public double Blue
        {
            get => _v[2];
            init => _v[2] = Math.Clamp(value, -1, 1);
        }

        internal double this[int index] => _v[index];

        internal static GuiColourMatrixEntry? FromXml(XElement? xml)
        {
            string[]? values = xml?.Value.Split(',');

            if (values == null || values.Length != 3)
            {
                return null;
            }

            if (!double.TryParse(values[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double r)
                || !double.TryParse(values[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double g)
                || !double.TryParse(values[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double b))
            {
                return null;
            }

            return new GuiColourMatrixEntry
            {
                Red = r,
                Green = g,
                Blue = b,
            };
        }
    }
}
