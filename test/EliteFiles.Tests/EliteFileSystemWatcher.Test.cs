using System.Diagnostics.CodeAnalysis;
using EliteFiles.Internal;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class EliteFileSystemWatcherTest
    {
        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            using var tf = new TestFolder();
            var fsw = new EliteFileSystemWatcher(tf.Name);
#pragma warning disable IDISP016, IDISP017
            fsw.Dispose();
            fsw.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
