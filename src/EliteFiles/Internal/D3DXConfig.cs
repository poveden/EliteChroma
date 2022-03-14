using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

namespace EliteFiles.Internal
{
    internal sealed class D3DXConfig
    {
        private static readonly Regex _rxSection = new Regex(@"^\[(.+?)\]\s*$");
        private static readonly Regex _rxIf = new Regex(@"^if\s+[^=]");
        private static readonly Regex _rxEndIf = new Regex(@"^endif\s*$");

        public D3DXConfig()
        {
            Files = new List<string>();
            Sections = new Dictionary<string, D3DXConfigSection>(StringComparer.OrdinalIgnoreCase);
        }

        public IList<string> Files { get; }

        public IDictionary<string, D3DXConfigSection> Sections { get; }

        public static D3DXConfig? FromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var res = new D3DXConfig();

            var pendingFiles = new List<string>
            {
                Path.GetFullPath(path),
            };

            while (pendingFiles.Count > 0)
            {
                string file = pendingFiles[0];
                pendingFiles.RemoveAt(0);
                Dictionary<string, List<D3DXConfigEntry>> iniFile;

                try
                {
                    iniFile = ParseIniFile(file);
                }
                catch (InvalidDataException)
                {
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }

                if (iniFile.Count == 0)
                {
                    continue;
                }

                res.Files.Add(file);

                foreach ((string sectionName, List<D3DXConfigEntry> entries) in iniFile)
                {
                    if (sectionName.Equals("Include", StringComparison.OrdinalIgnoreCase))
                    {
                        ParseIncludeSection(file, entries, pendingFiles, res.Files);
                        continue;
                    }

                    if (!res.Sections.TryGetValue(sectionName, out D3DXConfigSection section))
                    {
                        section = new D3DXConfigSection();
                        res.Sections.Add(sectionName, section);
                    }

                    foreach (D3DXConfigEntry entry in entries)
                    {
                        // We mimic what 3dmigoto does (see https://github.com/bo3b/3Dmigoto/blob/master/DirectX11/IniHandler.cpp)
                        if (!section.Contains(entry.Name))
                        {
                            section.Add(entry);
                        }
                    }
                }
            }

            return res.Files.Count != 0 ? res : null;
        }

        private static Dictionary<string, List<D3DXConfigEntry>> ParseIniFile(string path)
        {
            var res = new Dictionary<string, List<D3DXConfigEntry>>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(path))
            {
                throw new InvalidDataException();
            }

            using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fs);

            List<D3DXConfigEntry>? section = null;

            int ifLevel = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.Length == 0 || line.StartsWith(";", StringComparison.Ordinal))
                {
                    continue;
                }

                Match m = _rxSection.Match(line);
                if (m.Success)
                {
                    ifLevel = 0;
                    section = new List<D3DXConfigEntry>();
                    res.Add(m.Groups[1].Value, section);
                    continue;
                }

                if (_rxIf.IsMatch(line))
                {
                    ifLevel++;
                    continue;
                }

                if (_rxEndIf.IsMatch(line))
                {
                    if (ifLevel > 0)
                    {
                        ifLevel--;
                    }

                    continue;
                }

                if (ifLevel != 0)
                {
                    continue;
                }

                var entry = D3DXConfigEntry.Parse(line);

                if (entry != null)
                {
                    section?.Add(entry);
                    continue;
                }

                throw new InvalidDataException();
            }

            return res;
        }

        private static void ParseIncludeSection(string currentFile, List<D3DXConfigEntry> entries, List<string> pendingFiles, IList<string> processedFiles)
        {
            foreach (D3DXConfigEntry entry in entries)
            {
                if (entry.Name.Equals("include", StringComparison.OrdinalIgnoreCase))
                {
                    string file = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(currentFile), entry.Value));

                    if (pendingFiles.Any(x => file.Equals(x, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    if (processedFiles.Any(x => file.Equals(x, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    pendingFiles.Add(file);
                    continue;
                }

                if (entry.Name.Equals("include_recursive", StringComparison.OrdinalIgnoreCase))
                {
                    string basePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(currentFile), entry.Value));

                    if (!Directory.Exists(basePath))
                    {
                        continue;
                    }

                    foreach (string file in Directory.EnumerateFiles(basePath, "*.ini", SearchOption.AllDirectories))
                    {
                        if (pendingFiles.Any(x => file.Equals(x, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        if (processedFiles.Any(x => file.Equals(x, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        pendingFiles.Add(file);
                    }

                    continue;
                }

                if (entry.Name.Equals("exclude_recursive", StringComparison.OrdinalIgnoreCase))
                {
                    Matcher m = new Matcher().AddInclude(entry.Value);

                    for (int i = pendingFiles.Count - 1; i >= 0; i--)
                    {
                        PatternMatchingResult ms = m.Match(Path.GetFileName(pendingFiles[i]));

                        if (ms.HasMatches)
                        {
                            pendingFiles.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
