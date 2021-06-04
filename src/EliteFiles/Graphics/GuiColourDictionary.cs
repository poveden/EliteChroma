using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents a collection of <see cref="GuiColourMatrix"/> entries.
    /// </summary>
    public sealed class GuiColourDictionary : ReadOnlyDictionary<string, GuiColourMatrix>
    {
        private GuiColourDictionary()
            : base(new Dictionary<string, GuiColourMatrix>(StringComparer.Ordinal))
        {
        }

        /// <summary>
        /// Gets the default <see cref="GuiColourMatrix"/> instance.
        /// </summary>
        public GuiColourMatrix Default => Dictionary.TryGetValue("Default", out GuiColourMatrix gcm) ? gcm : null;

        internal static GuiColourDictionary FromXml(XElement xml)
        {
            if (xml == null)
            {
                return null;
            }

            var res = new GuiColourDictionary();

            foreach (XElement elem in xml.Elements())
            {
                res.Dictionary[elem.Name.LocalName] = GuiColourMatrix.FromXml(elem);
            }

            return res;
        }

        internal void OverrideWith(GuiColourDictionary other)
        {
            if (other == null)
            {
                return;
            }

            foreach (KeyValuePair<string, GuiColourMatrix> elem in other)
            {
                Dictionary[elem.Key] = elem.Value;
            }
        }
    }
}
