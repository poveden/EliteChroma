using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents an Elite:Dangerous binding presets file.
    /// </summary>
    public sealed class BindingPreset
    {
        private readonly Dictionary<string, Binding> _dict = new Dictionary<string, Binding>(StringComparer.Ordinal);

        private BindingPreset()
        {
        }

        /// <summary>
        /// Gets the name of the bindings preset.
        /// </summary>
        public string PresetName { get; private set; }

        /// <summary>
        /// Gets the game version for which this preset is targeted to.
        /// </summary>
        public Version Version { get; private set; }

        /// <summary>
        /// Gets the keyboard layout.
        /// </summary>
        public string KeyboardLayout { get; private set; }

        /// <summary>
        /// Gets the collection of all bindings.
        /// </summary>
        public IReadOnlyDictionary<string, Binding> Bindings => _dict;

        /// <summary>
        /// Read the binding presets from the given file.
        /// </summary>
        /// <param name="path">The path to the binding presets file.</param>
        /// <returns>The bindings preset, or <c>null</c> if the file couldn't be read (e.g. in the middle of an update).</returns>
        public static BindingPreset FromFile(string path)
        {
            XDocument xml;

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fs.Length == 0)
                {
                    return null;
                }

                xml = XDocument.Load(fs);
            }

            var res = new BindingPreset
            {
                PresetName = xml.Root.Attribute("PresetName")?.Value,
                KeyboardLayout = xml.Root.Element("KeyboardLayout")?.Value,
            };

            var majorVersion = xml.Root.Attribute("MajorVersion")?.Value;
            var minorVersion = xml.Root.Attribute("MinorVersion")?.Value;

            if (Version.TryParse($"{majorVersion}.{minorVersion}", out var version))
            {
                res.Version = version;
            }

            foreach (var xSetting in xml.Root.Elements())
            {
                var binding = Binding.FromXml(xSetting);

                if (binding != null)
                {
                    res._dict[binding.Name] = binding;
                }
            }

            return res;
        }
    }
}
