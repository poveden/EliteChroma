using System;
using System.Linq;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public class EliteFileSystemWatcherTests
    {
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            using var tf = new TestFolder();

            var ti = typeof(JournalFolder).Assembly.DefinedTypes
                .Single(x => x.IsNotPublic && x.Name == "EliteFileSystemWatcher");

            var ci = ti.GetConstructor(new[] { typeof(string) })!;

            var fsw = (IDisposable)ci.Invoke(new object[] { tf.Name });

#pragma warning disable IDISP016, IDISP017
            fsw.Dispose();
            fsw.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
