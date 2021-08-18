using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents a collection of <see cref="GuiColourMatrix"/> entries.
    /// </summary>
    public sealed class GuiColourDictionary : Dictionary<string, GuiColourMatrix>
    {
        internal GuiColourDictionary()
            : base(StringComparer.Ordinal)
        {
        }

        /// <summary>
        /// Gets the default <see cref="GuiColourMatrix"/> instance.
        /// </summary>
        public GuiColourMatrix? Default => TryGetValue("Default", out GuiColourMatrix gcm) ? gcm : null;

        internal static GuiColourDictionary FromXml(XElement? xml)
        {
            var res = new GuiColourDictionary();

            if (xml == null)
            {
                return res;
            }

            foreach (XElement elem in xml.Elements())
            {
                res[elem.Name.LocalName] = GuiColourMatrix.FromXml(elem);
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
