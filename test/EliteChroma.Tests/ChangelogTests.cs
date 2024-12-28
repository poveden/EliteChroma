using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class ChangelogTests
    {
        private static readonly Regex _rxUrl = new Regex(@"^https\://github.com/poveden/EliteChroma/(?:releases/tag/(?<tag>.+?)|compare/(?<tag1>.+?)\.{3}(?<tag2>.+?))$");
        private static readonly Regex _rxTag = new Regex(@"^v(?<version>[0-9]+\.[0-9]+\.[0-9]+)$");

        private static readonly IReadOnlyList<ChangelogEntry> _entries = GetChangelogEntries();

        [Fact]
        public void MostRecentReleaseMatchesEliteChromaManifestVersion()
        {
            const string EliteChromaProjectPath = @"src\EliteChroma\EliteChroma.csproj";
            const string EliteChromaXPath = "/Project/PropertyGroup/Version";
            const string VersionExpr = @"^\d+\.\d+\.\d+$";

            string solutionDir = MetaTestsCommon.GetSolutionDirectory();

            string eliteChromaPath = Path.Combine(solutionDir, EliteChromaProjectPath);
            var eliteChromaProject = XDocument.Load(eliteChromaPath);
            var ecVersion = eliteChromaProject.XPathSelectElement(EliteChromaXPath);
            Assert.NotNull(ecVersion);
            Assert.Matches(VersionExpr, ecVersion!.Value);

            var entry = _entries.First(x => x.Valid && x.Released);

            Assert.Equal(ecVersion.Value, entry.ReleaseVersion);
        }

        [Fact]
        public void AllEntriesAreValid()
        {
            Assert.All(_entries, x => Assert.True(x.Valid));
        }

        [Fact]
        public void EntriesAreOrderedFromNewestToOldest()
        {
            for (int i = 0; i < _entries.Count - 1; i++)
            {
                Assert.True(_entries[i].ReleaseDate >= _entries[i + 1].ReleaseDate);
            }
        }

        [Fact]
        public void EntriesAreOrderedFromHighestToLowestVersionNumber()
        {
            int newestRelease = _entries[0].Released ? 0 : 1;

            for (int i = newestRelease; i < _entries.Count - 1; i++)
            {
                var currVer = new Version(_entries[i].ReleaseVersion!);
                var prevVer = new Version(_entries[i + 1].ReleaseVersion!);
                Assert.True(currVer > prevVer);
            }
        }

        [SkippableFact]
        public void OnlyASingleUnreleasedEntryMayBePresentAtTheTop()
        {
            var allUnreleased = _entries.Where(x => !x.Released).ToList();
            Skip.If(allUnreleased.Count == 0);

            Assert.Single(allUnreleased);
            Assert.Same(_entries[0], allUnreleased[0]);
        }

        [SkippableFact]
        public void ComparisonRangeLinkInUnreleasedEntryEndsAtHeadReference()
        {
            var unreleased = _entries.FirstOrDefault(x => !x.Released);
            Skip.If(unreleased == null);

            var m = _rxUrl.Match(unreleased.Url!);
            Assert.Matches(_rxTag, m.Groups["tag1"].Value);
            Assert.Equal("HEAD", m.Groups["tag2"].Value);
        }

        [Fact]
        public void OldestEntryIsVersionOneZeroZero()
        {
            Assert.Equal("1.0.0", _entries[^1].ReleaseVersion);
        }

        [Fact]
        public void AllUrlsPointEitherToATagOrToACompareRangeInTheRepository()
        {
            Assert.All(_entries.Select(x => x.Url), x => Assert.Matches(_rxUrl, x));
        }

        [Fact]
        public void OnlyTheOldestEntryLinksToAGitTag()
        {
            var m = _rxUrl.Match(_entries[^1].Url!);
            var g = m.Groups["tag"];
            Assert.True(g.Success);
            Assert.Matches(_rxTag, g.Value);

            int newestRelease = _entries[0].Released ? 0 : 1;
            for (int i = newestRelease; i < _entries.Count - 1; i++)
            {
                m = _rxUrl.Match(_entries[i].Url!);
                Assert.False(m.Groups["tag"].Success);
            }
        }

        [Fact]
        public void AllEntriesButTheOldestLinkToAComparisonRange()
        {
            var m = _rxUrl.Match(_entries[^1].Url!);
            Assert.False(m.Groups["tag1"].Success);
            Assert.False(m.Groups["tag2"].Success);

            int newestRelease = _entries[0].Released ? 0 : 1;
            for (int i = newestRelease; i < _entries.Count - 1; i++)
            {
                m = _rxUrl.Match(_entries[i].Url!);
                var g1 = m.Groups["tag1"];
                var g2 = m.Groups["tag2"];
                Assert.True(g1.Success);
                Assert.Matches(_rxTag, g1.Value);
                Assert.True(g2.Success);
                Assert.Matches(_rxTag, g2.Value);
            }
        }

        [Fact]
        public void ReleaseVersionAndReleaseTagMatchesForAllReleases()
        {
            foreach (var entry in _entries.Where(x => x.Released))
            {
                var m = _rxUrl.Match(entry.Url!);
                string tag = m.Groups["tag"].Success ? m.Groups["tag"].Value : m.Groups["tag2"].Value;

                m = _rxTag.Match(tag);
                string tagVersion = m.Groups["version"].Value;

                Assert.Equal(entry.ReleaseVersion, tagVersion);
            }
        }

        [Fact]
        public void AllEntriesCoverAllGitTags()
        {
            var m = _rxUrl.Match(_entries[0].Url!);
            var g = m.Groups["tag1"];
            Assert.True(g.Success);
            string prevTag = g.Value;

            for (int i = 1; i < _entries.Count; i++)
            {
                m = _rxUrl.Match(_entries[i].Url!);
                bool isSingleTag = m.Groups["tag"].Success;
                g = isSingleTag ? m.Groups["tag"] : m.Groups["tag2"];
                Assert.True(g.Success);
                string currTag = g.Value;

                Assert.Equal(currTag, prevTag);

                if (!isSingleTag)
                {
                    g = m.Groups["tag1"];
                }

                Assert.True(g.Success);
                prevTag = g.Value;
            }
        }

        private static IReadOnlyList<ChangelogEntry> GetChangelogEntries()
        {
            const string ChangelogPath = "CHANGELOG.md";

            string solutionDir = MetaTestsCommon.GetSolutionDirectory();
            string changelogPath = Path.Combine(solutionDir, ChangelogPath);

            using var fs = File.OpenText(changelogPath);
            var res = new List<ChangelogEntry>();

            string? line;
            int i = 0;
            while ((line = fs.ReadLine()) != null)
            {
                var entry = ChangelogEntry.TryParse(line, ++i);

                if (entry == null)
                {
                    continue;
                }

                res.Add(entry);
            }

            return res;
        }

        [DebuggerDisplay("{LineNumber}: {ReleaseVersion} ({Url}) - {ReleaseDate}")]
        private sealed class ChangelogEntry
        {
            private static readonly Regex _rxMarkdownH2 = new Regex(@"^\s*##(?!#+)");

            private static readonly Regex _rxReleased = new Regex(@"^## \[(?<version>[0-9]+\.[0-9]+\.[0-9]+)\]\((?<url>.+?)\) \u2014 (?<date>[0-9]{4}-[0-9]{2}-[0-9]{2})$");
            private static readonly Regex _rxUnreleased = new Regex(@"^## \[Unreleased\]\((?<url>.+?)\)$");

            private ChangelogEntry()
            {
            }

            public bool Valid { get; init; }

            public int LineNumber { get; init; }

            public bool Released { get; init; }

            public string? ReleaseVersion { get; init; }

            public string? Url { get; init; }

            public DateTimeOffset? ReleaseDate { get; init; }

            public static ChangelogEntry? TryParse(string line, int lineNumber)
            {
                if (!_rxMarkdownH2.IsMatch(line))
                {
                    return null;
                }

                var m = _rxReleased.Match(line);
                if (m.Success)
                {
                    return new ChangelogEntry
                    {
                        Valid = true,
                        LineNumber = lineNumber,
                        Released = true,
                        ReleaseVersion = m.Groups["version"].Value,
                        Url = m.Groups["url"].Value,
                        ReleaseDate = DateTimeOffset.ParseExact(m.Groups[3].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                    };
                }

                m = _rxUnreleased.Match(line);
                if (m.Success)
                {
                    return new ChangelogEntry
                    {
                        Valid = true,
                        LineNumber = lineNumber,
                        Url = m.Groups["url"].Value,
                        ReleaseDate = DateTimeOffset.MaxValue,
                    };
                }

                return new ChangelogEntry
                {
                    LineNumber = lineNumber,
                };
            }
        }
    }
}
