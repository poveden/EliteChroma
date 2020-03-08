using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EliteChroma.Core.Internal;
using EliteChroma.Elite.Internal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class ProcessListTest
    {
        private static readonly FieldInfo _fiBuf = typeof(ProcessList).GetField("_buf", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _fiN = typeof(ProcessList).GetField("_n", BindingFlags.NonPublic | BindingFlags.Instance);

        [Fact]
        public void RefreshReturnsAnOrderedListOfUniqueProcessIds()
        {
            var pl = new ProcessList(NativeMethods.Instance);
            pl.Refresh();

            var n = (int)_fiN.GetValue(pl);
            Assert.True(n > 1);

            var buf = (int[])_fiBuf.GetValue(pl);

            for (var i = 1; i < n; i++)
            {
                Assert.True(buf[i - 1] < buf[i]);
            }
        }

        [Theory]
        [MemberData(nameof(BuildSequences))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Theory data")]
        public void ExceptReturnsExpectedValues(int[] first, int[] second)
        {
            var pl1 = InitProcessList(first);
            var pl2 = InitProcessList(second);

            var expectedAdded = second.Except(first).ToList();
            var expectedRemoved = first.Except(second).ToList();
            var expectedAll = expectedAdded.Union(expectedRemoved).OrderBy(x => x).ToList();

            var diff = pl2.Diff(pl1).ToList();

            var added = diff.Where(x => x.Added).Select(x => x.ProcessId).ToList();
            var removed = diff.Where(x => !x.Added).Select(x => x.ProcessId).ToList();
            var all = diff.Select(x => x.ProcessId).ToList();

            Assert.Equal<int>(expectedAll, all);
            Assert.Equal(expectedAdded, added);
            Assert.Equal(expectedRemoved, removed);
        }

        [SuppressMessage("OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Theory data")]
        public static IEnumerable<object[]> BuildSequences()
        {
            // 1234
            // 1234
            yield return new object[]
            {
                GetSequence(1, 10).ToArray(),
                GetSequence(1, 10).ToArray(),
            };

            // ----
            // 1234
            yield return new object[]
            {
                Array.Empty<int>(),
                GetSequence(1, 10).ToArray(),
            };

            // 1234
            // ----
            yield return new object[]
            {
                GetSequence(1, 10).ToArray(),
                Array.Empty<int>(),
            };

            // 123-
            // -234
            yield return new object[]
            {
                GetSequence(1, 10).ToArray(),
                GetSequence(6, 15).ToArray(),
            };

            // 12--
            // --34
            yield return new object[]
            {
                GetSequence(1, 10).ToArray(),
                GetSequence(11, 20).ToArray(),
            };

            // -234
            // 123-
            yield return new object[]
            {
                GetSequence(6, 15).ToArray(),
                GetSequence(1, 10).ToArray(),
            };

            // --34
            // 12--
            yield return new object[]
            {
                GetSequence(11, 20).ToArray(),
                GetSequence(1, 10).ToArray(),
            };

            // -23-
            // 1234
            yield return new object[]
            {
                GetSequence(6, 15).ToArray(),
                GetSequence(1, 20).ToArray(),
            };

            // 1234
            // -23-
            yield return new object[]
            {
                GetSequence(1, 20).ToArray(),
                GetSequence(6, 15).ToArray(),
            };

            // 12-456-
            // -234-67
            yield return new object[]
            {
                GetSequence(1, 10).Concat(GetSequence(13, 18)).ToArray(),
                GetSequence(5, 15).Concat(GetSequence(17, 19)).ToArray(),
            };

            // -234-67
            // 12-456-
            yield return new object[]
            {
                GetSequence(5, 15).Concat(GetSequence(17, 19)).ToArray(),
                GetSequence(1, 10).Concat(GetSequence(13, 18)).ToArray(),
            };
        }

        private static ProcessList InitProcessList(IEnumerable<int> values)
        {
            var res = new ProcessList(NativeMethods.Instance);
            var buf = (int[])_fiBuf.GetValue(res);
            var n = 0;

            foreach (var value in values)
            {
                buf[n++] = value;
            }

            Array.Sort(buf, 0, n);
            _fiN.SetValue(res, n);

            return res;
        }

        private static IEnumerable<int> GetSequence(int from, int to)
        {
            for (var i = from; i <= to; i++)
            {
                yield return i;
            }
        }
    }
}
