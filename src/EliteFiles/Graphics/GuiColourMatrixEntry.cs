using System;
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
        /// Gets or sets the red component value.
        /// </summary>
        public double Red
        {
            get => _v[0];
            set => _v[0] = Math.Clamp(value, -1, 1);
        }

        /// <summary>
        /// Gets or sets the green component value.
        /// </summary>
        public double Green
        {
            get => _v[1];
            set => _v[1] = Math.Clamp(value, -1, 1);
        }

        /// <summary>
        /// Gets or sets the blue component value.
        /// </summary>
        public double Blue
        {
            get => _v[2];
            set => _v[2] = Math.Clamp(value, -1, 1);
        }

        internal double this[int index]
        {
            get => _v[index];
            set => _v[index] = Math.Clamp(value, -1, 1);
        }

        internal static GuiColourMatrixEntry FromXml(XElement xml)
        {
            string[] values = xml.Value.Split(',');

            return new GuiColourMatrixEntry
            {
                Red = double.Parse(values[0], CultureInfo.InvariantCulture),
                Green = double.Parse(values[1], CultureInfo.InvariantCulture),
                Blue = double.Parse(values[2], CultureInfo.InvariantCulture),
            };
        }
    }
}
