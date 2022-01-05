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
        private readonly bool _readOnly;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiColourMatrixEntry"/> class.
        /// </summary>
        public GuiColourMatrixEntry()
        {
        }

        internal GuiColourMatrixEntry(double r, double g, double b, bool readOnly)
        {
            Red = r;
            Green = g;
            Blue = b;
            _readOnly = readOnly;
        }

        /// <summary>
        /// Gets or sets the red component value.
        /// </summary>
        public double Red
        {
            get => _v[0];
            set
            {
                ThrowIfReadOnly();
                _v[0] = Math.Clamp(value, -1, 1);
            }
        }

        /// <summary>
        /// Gets or sets the green component value.
        /// </summary>
        public double Green
        {
            get => _v[1];
            set
            {
                ThrowIfReadOnly();
                _v[1] = Math.Clamp(value, -1, 1);
            }
        }

        /// <summary>
        /// Gets or sets the blue component value.
        /// </summary>
        public double Blue
        {
            get => _v[2];
            set
            {
                ThrowIfReadOnly();
                _v[2] = Math.Clamp(value, -1, 1);
            }
        }

        internal double this[int index]
        {
            get => _v[index];
            set
            {
                ThrowIfReadOnly();
                _v[index] = Math.Clamp(value, -1, 1);
            }
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

        private void ThrowIfReadOnly()
        {
            if (_readOnly)
            {
                throw new InvalidOperationException("Property is read-only.");
            }
        }
    }
}
