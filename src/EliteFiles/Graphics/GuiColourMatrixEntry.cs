using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents the single-color component of a GUI colour transformation matrix.
    /// </summary>
    public sealed class GuiColourMatrixEntry
    {
        private readonly double[] _v = new double[3];

        private GuiColourMatrixEntry(double r, double g, double b)
        {
            _v[0] = r;
            _v[1] = g;
            _v[2] = b;
        }

        /// <summary>
        /// Gets the red component value.
        /// </summary>
        public double Red => _v[0];

        /// <summary>
        /// Gets the green component value.
        /// </summary>
        public double Green => _v[1];

        /// <summary>
        /// Gets the blue component value.
        /// </summary>
        public double Blue => _v[2];

        internal double this[int index] => _v[index];

        internal static GuiColourMatrixEntry FromXml(XElement xml)
        {
            double[] values = xml.Value.Split(',')
                .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                .ToArray();

            return new GuiColourMatrixEntry(values[0], values[1], values[2]);
        }
    }
}
