using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using EliteFiles.Bindings.Binds;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents an Elite:Dangerous binding presets file.
    /// </summary>
    public sealed class BindingPreset
    {
        private static readonly int _numBindingCategories = Enum.GetValues(typeof(BindingCategory)).Length;
        private static readonly IReadOnlyDictionary<string, BindingCategory> _bindNameCategories = BuildBindNameCategoryMap();

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

            var res = new BindingPreset
            {
                PresetName = xml.Root.Attribute("PresetName")?.Value,
                KeyboardLayout = xml.Root.Element("KeyboardLayout")?.Value,
            };

            string majorVersion = xml.Root.Attribute("MajorVersion")?.Value;
            string minorVersion = xml.Root.Attribute("MinorVersion")?.Value;

            if (Version.TryParse($"{majorVersion}.{minorVersion}", out Version version))
            {
                res.Version = version;
            }

            foreach (XElement xSetting in xml.Root.Elements())
            {
                var binding = Binding.FromXml(xSetting);

                if (binding != null)
                {
                    res._dict[binding.Name] = binding;
                }
            }

            return res;
        }

        /// <summary>
        /// Merges <see cref="BindingPreset"/> objects from different binding categories into a single object.
        /// </summary>
        /// <param name="categoryBindingPresets">The collection of binding presets per category.</param>
        /// <returns>The merged bindings preset.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="categoryBindingPresets"/> is <c>null</c>.</exception>
        /// <remarks>
        /// Starting from Odyssey, bindings have been splitted into categories. Each <see cref="BindingPreset"/> object
        /// contains a full set of bindings. However, since each category may be assigned in game a different preset,
        /// (via the <c>StartPreset.start</c> file, only a section of the preset will apply.
        /// This method picks all relevant bindings from the corresponding presets, and puts them together into a single preset.
        /// </remarks>
        public static BindingPreset MergeFromCategories(IReadOnlyDictionary<BindingCategory, BindingPreset> categoryBindingPresets)
        {
            _ = categoryBindingPresets ?? throw new ArgumentNullException(nameof(categoryBindingPresets));

            T UniqueOrDefault<T>(Func<BindingPreset, T> selector, IEqualityComparer<T> comparer = null)
            {
                var distinct = new HashSet<T>(categoryBindingPresets.Values.Select(selector), comparer);
                return distinct.Count == 1 ? distinct.Single() : default;
            }

            var res = new BindingPreset
            {
                PresetName = UniqueOrDefault(x => x.PresetName, StringComparer.Ordinal),
                Version = UniqueOrDefault(x => x.Version),
                KeyboardLayout = UniqueOrDefault(x => x.KeyboardLayout, StringComparer.OrdinalIgnoreCase),
            };

            foreach (KeyValuePair<string, BindingCategory> kv in _bindNameCategories)
            {
                if (!categoryBindingPresets.TryGetValue(kv.Value, out BindingPreset bindingPreset) || bindingPreset == null)
                {
                    continue;
                }

                if (!bindingPreset.Bindings.TryGetValue(kv.Key, out Binding binding))
                {
                    continue;
                }

                res._dict.Add(kv.Key, binding);
            }

            return res;
        }

        /// <summary>
        /// Gets the paths of the currently active binding preset files per category.
        /// </summary>
        /// <param name="gameInstallFolder">The path to the game installation folder.</param>
        /// <param name="gameOptionsFolder">The path to the game options folder.</param>
        /// <returns>The path to the file, or <c>null</c> if no active preset could be found.</returns>
        public static IReadOnlyDictionary<BindingCategory, string> FindActivePresetFiles(GameInstallFolder gameInstallFolder, GameOptionsFolder gameOptionsFolder)
        {
            _ = GameInstallFolder.AssertValid(gameInstallFolder);
            _ = GameOptionsFolder.AssertValid(gameOptionsFolder);

            var bindsFiles = new Dictionary<BindingCategory, string>(_numBindingCategories);

            using (FileStream fs = gameOptionsFolder.BindingsStartPreset.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using var sr = new StreamReader(fs);

                string bindsName;
                for (int i = 0; (bindsName = sr.ReadLine()) != null; i++)
                {
                    string bindsFile =
                        TryGetBindingsFilePath(gameOptionsFolder.Bindings, bindsName)
                        ?? TryGetBindingsFilePath(gameInstallFolder.ControlSchemes, bindsName);

                    if (bindsFile == null)
                    {
                        return null;
                    }

                    bindsFiles.Add((BindingCategory)i, bindsFile);
                }
            }

            if (bindsFiles.Count == 1)
            {
                // Pre-Odyssey behaviour.
                for (int i = 1; i < _numBindingCategories; i++)
                {
                    bindsFiles[(BindingCategory)i] = bindsFiles[0];
                }

                return bindsFiles;
            }

            if (bindsFiles.Count >= _numBindingCategories)
            {
                return bindsFiles;
            }

            return null;
        }

        private static string TryGetBindingsFilePath(DirectoryInfo path, string bindsName)
        {
            IEnumerable<string> matches =
                from file in path.EnumerateFiles($"{bindsName}.*")
                let m = Regex.Match(file.Name, @"(?:\.(\d\.\d))?\.binds$")
                where m.Success
                orderby Version.Parse(m.Groups[1].Length != 0 ? m.Groups[1].Value : "1.0") descending
                select file.FullName;

            return matches.FirstOrDefault();
        }

        private static IReadOnlyDictionary<string, BindingCategory> BuildBindNameCategoryMap()
        {
            var res = new Dictionary<string, BindingCategory>(StringComparer.Ordinal);

            IEnumerable<(BindingCategory, IReadOnlyCollection<string>)> allBinds =
                from type in typeof(InterfaceMode).Assembly.GetTypes()
                where type.Namespace == typeof(InterfaceMode).Namespace
                let category = (BindingCategory)type.GetField(nameof(InterfaceMode.Category), BindingFlags.Public | BindingFlags.Static).GetValue(null)
                let allNames = (IReadOnlyCollection<string>)type.GetProperty(nameof(InterfaceMode.All), BindingFlags.Public | BindingFlags.Static).GetValue(null)
                select (category, allNames);

            foreach ((BindingCategory category, IReadOnlyCollection<string> allNames) in allBinds)
            {
                foreach (string name in allNames)
                {
                    res.Add(name, category);
                }
            }

            return res;
        }
    }
}
