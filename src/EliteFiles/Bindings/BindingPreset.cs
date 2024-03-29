﻿using System.Collections.ObjectModel;
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
        private const string _defaultBindingPresetFileName = "KeyboardMouseOnly.binds";

        private static readonly int _numBindingCategories = Enum.GetValues(typeof(BindingCategory)).Length;
        private static readonly IReadOnlyDictionary<string, BindingCategory> _bindNameCategories = BuildBindNameCategoryMap();
        private static readonly IReadOnlyDictionary<BindingCategory, string> _noPresetFiles = new ReadOnlyDictionary<BindingCategory, string>(new Dictionary<BindingCategory, string>());

        private readonly Dictionary<string, Binding> _dict = new Dictionary<string, Binding>(StringComparer.Ordinal);

        /// <summary>
        /// Gets or sets the name of the bindings preset.
        /// </summary>
        public string? PresetName { get; set; }

        /// <summary>
        /// Gets or sets the game version for which this preset is targeted to.
        /// </summary>
        public Version? Version { get; set; }

        /// <summary>
        /// Gets or sets the keyboard layout.
        /// </summary>
        public string? KeyboardLayout { get; set; }

        /// <summary>
        /// Gets the collection of all bindings.
        /// </summary>
        public IDictionary<string, Binding> Bindings => _dict;

        /// <summary>
        /// Read the binding presets from the given file.
        /// </summary>
        /// <param name="path">The path to the binding presets file.</param>
        /// <returns>The bindings preset, or <c>null</c> if the file couldn't be read (e.g. in the middle of an update).</returns>
        public static BindingPreset? FromFile(string path)
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
                PresetName = xml.Root!.Attribute("PresetName")?.Value,
                KeyboardLayout = xml.Root.Element("KeyboardLayout")?.Value,
            };

            string? majorVersion = xml.Root.Attribute("MajorVersion")?.Value;
            string? minorVersion = xml.Root.Attribute("MinorVersion")?.Value;

            if (Version.TryParse($"{majorVersion}.{minorVersion}", out Version? version))
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
            ArgumentNullException.ThrowIfNull(categoryBindingPresets);

            T UniqueOrDefault<T>(Func<BindingPreset, T> selector, IEqualityComparer<T>? comparer = null)
            {
                var distinct = new HashSet<T>(categoryBindingPresets.Values.Select(selector), comparer);
                return distinct.Count == 1 ? distinct.Single() : default!;
            }

            var res = new BindingPreset
            {
                PresetName = UniqueOrDefault(x => x.PresetName, StringComparer.Ordinal),
                Version = UniqueOrDefault(x => x.Version),
                KeyboardLayout = UniqueOrDefault(x => x.KeyboardLayout, StringComparer.OrdinalIgnoreCase),
            };

            foreach (KeyValuePair<string, BindingCategory> kv in _bindNameCategories)
            {
                if (!categoryBindingPresets.TryGetValue(kv.Value, out BindingPreset? bindingPreset) || bindingPreset == null)
                {
                    continue;
                }

                if (!bindingPreset.Bindings.TryGetValue(kv.Key, out Binding? binding))
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
        /// <returns>The paths to the files, or an empty set if no active preset could be found.</returns>
        public static IReadOnlyDictionary<BindingCategory, string> FindActivePresetFiles(GameInstallFolder gameInstallFolder, GameOptionsFolder gameOptionsFolder)
        {
            _ = GameInstallFolder.AssertValid(gameInstallFolder);
            _ = GameOptionsFolder.AssertValid(gameOptionsFolder);

            if (!gameOptionsFolder.BindingsStartPreset.Exists)
            {
                return GetDefaultPresetFiles(gameInstallFolder);
            }

            var bindsFiles = new Dictionary<BindingCategory, string>(_numBindingCategories);

            using (FileStream fs = gameOptionsFolder.BindingsStartPreset.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using var sr = new StreamReader(fs);

                string? bindsName;
                int i = 0;
                while ((bindsName = sr.ReadLine()) != null)
                {
                    string? bindsFile =
                        TryGetBindingsFilePath(gameOptionsFolder.Bindings, bindsName)
                        ?? TryGetBindingsFilePath(gameInstallFolder.ControlSchemes, bindsName);

                    if (bindsFile == null)
                    {
                        return _noPresetFiles;
                    }

                    bindsFiles.Add((BindingCategory)i, bindsFile);
                    i++;
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

            return _noPresetFiles;
        }

        private static string? TryGetBindingsFilePath(DirectoryInfo path, string bindsName)
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
                let category = (BindingCategory)type.GetField(nameof(InterfaceMode.Category), BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!
                let allNames = (IReadOnlyCollection<string>)type.GetProperty(nameof(InterfaceMode.All), BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!
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

        private static IReadOnlyDictionary<BindingCategory, string> GetDefaultPresetFiles(GameInstallFolder gameInstallFolder)
        {
            FileInfo? bindsFile = gameInstallFolder.ControlSchemes.EnumerateFiles(_defaultBindingPresetFileName, SearchOption.TopDirectoryOnly).FirstOrDefault();

            if (bindsFile == null || !bindsFile.Exists)
            {
                return _noPresetFiles;
            }

            var res = new Dictionary<BindingCategory, string>(_numBindingCategories);

            foreach (BindingCategory bindingCategory in Enum.GetValues(typeof(BindingCategory)))
            {
                res[bindingCategory] = bindsFile.FullName;
            }

            return res;
        }
    }
}
