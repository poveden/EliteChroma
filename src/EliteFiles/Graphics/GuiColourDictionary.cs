using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents a collection of <see cref="GuiColourMatrix"/> entries.
    /// </summary>
    [Serializable]
    public sealed class GuiColourDictionary : Dictionary<string, GuiColourMatrix>
    {
        internal GuiColourDictionary()
            : base(StringComparer.Ordinal)
        {
        }

        [ExcludeFromCodeCoverage]
        private GuiColourDictionary(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        /// <summary>
        /// Gets the default <see cref="GuiColourMatrix"/> instance.
        /// </summary>
        public GuiColourMatrix? Default => TryGetValue("Default", out GuiColourMatrix? gcm) ? gcm : null;

        internal static GuiColourDictionary FromXml(XElement? xml)
        {
            var res = new GuiColourDictionary();

            if (xml == null)
            {
                return res;
            }

            foreach (XElement elem in xml.Elements())
            {
                var matrix = GuiColourMatrix.FromXml(elem);

                if (matrix != null)
                {
                    res[elem.Name.LocalName] = matrix;
                }
            }

            return res;
        }

        internal void OverrideWith(GuiColourDictionary? other)
        {
            if (other == null)
            {
                return;
            }

            foreach (KeyValuePair<string, GuiColourMatrix> elem in other)
            {
                this[elem.Key] = elem.Value;
            }
        }
    }
}
