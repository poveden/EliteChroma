using System.Xml;
using System.Xml.Linq;

namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents an Elite:Dangerous graphics configuration file.
    /// </summary>
    /// <remarks>
    /// Currently, only <c>GUIColour</c> settings are extracted from the configuration file.
    /// </remarks>
    public sealed class GraphicsConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsConfig"/> class.
        /// </summary>
        public GraphicsConfig()
        {
            GuiColour = new GuiColourDictionary();
        }

        private GraphicsConfig(GuiColourDictionary guiColour)
        {
            GuiColour = guiColour;
        }

        /// <summary>
        /// Gets the GUI colour configuration.
        /// </summary>
        public GuiColourDictionary GuiColour { get; }

        /// <summary>
        /// Read the graphics configuration from the given file.
        /// </summary>
        /// <param name="path">The path to the graphics configuration file.</param>
        /// <returns>The graphics configuration, or <c>null</c> if the file couldn't be read (e.g. in the middle of an update).</returns>
        public static GraphicsConfig? FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            XDocument xml;

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fs.Length == 0)
                {
                    return null;
                }

                try
                {
                    xml = XDocument.Load(fs);
                }
                catch (XmlException)
                {
                    return null;
                }
            }

            var guiColour = GuiColourDictionary.FromXml(xml.Root!.Element("GUIColour"));

            return new GraphicsConfig(guiColour);
        }

        /// <summary>
        /// Updates this instance with values from another graphics configuration instance.
        /// </summary>
        /// <param name="other">The graphics configuration to override this instance with.</param>
        /// <remarks>
        /// Currently, only <c>GUIColour</c> settings are overriden.
        /// </remarks>
        public void OverrideWith(GraphicsConfig? other)
        {
            GuiColour?.OverrideWith(other?.GuiColour);
        }
    }
}
