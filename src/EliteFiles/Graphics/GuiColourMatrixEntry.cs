using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiColourMatrixEntry"/> class,
        /// with the provided red, green and blue values.
        /// </summary>
        /// <param name="r">The red component in the -1.0 to 1.0 range.</param>
        /// <param name="g">The green component in the -1.0 to 1.0 range.</param>
        /// <param name="b">The blue component in the -1.0 to 1.0 range.</param>
        public GuiColourMatrixEntry(double r, double g, double b)
        {
            _v[0] = Math.Clamp(-1, r, 1);
            _v[1] = Math.Clamp(-1, g, 1);
            _v[2] = Math.Clamp(-1, b, 1);
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
