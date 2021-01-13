using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite.Internal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ProcessListTests
    {
        private static readonly FieldInfo _fiBuf = typeof(ProcessList).GetField("_buf", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _fiN = typeof(ProcessList).GetField("_n", BindingFlags.NonPublic | BindingFlags.Instance);

        [Fact]
        public void RefreshReturnsAnOrderedListOfUniqueProcessIds()
        {
            var nm = new NativeMethodsMock { ProcessIds = new[] { 4, 3, 2, 1 } };
            var pl = new ProcessList(nm);

            pl.Refresh();

            var n = (int)_fiN.GetValue(pl);
            Assert.Equal(4, n);

            var buf = ((int[])_fiBuf.GetValue(pl)).Take(n).ToArray();
            Assert.Equal(new[] { 1, 2, 3, 4 }, buf);
        }

        [Theory]
        [MemberData(nameof(BuildSequences))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Theory data")]
        public void ExceptReturnsExpectedValues(int[] first, int[] second)
        {
            var nm1 = new NativeMethodsMock { ProcessIds = first };
            var pl1 = new ProcessList(nm1);
            pl1.Refresh();

            var nm2 = new NativeMethodsMock { ProcessIds = second };
            var pl2 = new ProcessList(nm2);
            pl2.Refresh();

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

        [Fact]
        public void RefreshWillNotChangeTheInternalCollectionWhenEnumProcessesFails()
        {
            var ids = new[] { 1, 2, 3, 4 };
            var nm = new NativeMethodsMock { ProcessIds = ids };

            var pl = new ProcessList(nm);
            pl.Refresh();

            var n = (int)_fiN.GetValue(pl);
            Assert.Equal(ids.Length, n);

            nm.ProcessIds = null;
            pl.Refresh();

            var buf = ((int[])_fiBuf.GetValue(pl)).Take(n).ToArray();
            Assert.Equal(ids, buf);
        }

        [Fact]
        public void CanRemoveEntries()
        {
            var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var nm = new NativeMethodsMock { ProcessIds = ids };

            var pl = new ProcessList(nm);
            pl.Refresh();

            Assert.False(pl.Remove(11));

            Assert.True(pl.Remove(3));

            var n = (int)_fiN.GetValue(pl);
            Assert.Equal(8, n);

            var buf = ((int[])_fiBuf.GetValue(pl)).Take(n).ToArray();
            Assert.Equal(new[] { 1, 2, 4, 5, 6, 7, 8, 9 }, buf);

            nm.ProcessIds = new[] { 1 };
            pl.Refresh();

            Assert.True(pl.Remove(1));
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

        private static IEnumerable<int> GetSequence(int from, int to)
        {
            for (var i = from; i <= to; i++)
            {
                yield return i;
            }
        }

        private sealed class NativeMethodsMock : NativeMethodsStub
        {
            public IList<int> ProcessIds { get; set; }

            public override bool EnumProcesses(int[] lpidProcess, int cb, out int lpcbNeeded)
            {
                if (ProcessIds == null)
                {
                    lpcbNeeded = 0;
                    return false;
                }

                ProcessIds.CopyTo(lpidProcess, 0);
                lpcbNeeded = ProcessIds.Count * Marshal.SizeOf<int>();
                return true;
            }
        }
    }
}
