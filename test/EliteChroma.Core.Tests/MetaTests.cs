using System.Reflection;
using TestUtils;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class MetaTests
    {
        [Theory]
        [MemberData(nameof(GetAllEventHandlers))]
        public void EventHandlersDeclareSenderParameterAsNullable(MethodInfo eventHandler)
        {
            MetaTestsCommon.AssertSenderParameterIsNullable(eventHandler);
        }

        private static IEnumerable<object[]> GetAllEventHandlers()
        {
            return MetaTestsCommon.GetAllEventHandlers(typeof(ChromaController).Assembly);
        }
    }
}
