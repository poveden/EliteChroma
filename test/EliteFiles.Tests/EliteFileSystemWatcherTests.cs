using System.Diagnostics.CodeAnalysis;
using TestUtils;
using Xunit;

namespace EliteFiles.Tests
{
    public class EliteFileSystemWatcherTests
    {
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            using var tf = new TestFolder();

            var ti = typeof(JournalFolder).Assembly.DefinedTypes
                .Single(x => x.IsNotPublic && x.Name == "EliteFileSystemWatcher");

            var ci = ti.GetConstructor(new[] { typeof(string) })!;

            var fsw = (IDisposable)ci.Invoke(new object[] { tf.Name });
            Assert.False(fsw.GetPrivateField<bool>("_disposed"));

            fsw.Dispose();
            Assert.True(fsw.GetPrivateField<bool>("_disposed"));

            fsw.Dispose();
            Assert.True(fsw.GetPrivateField<bool>("_disposed"));
        }
    }
}
