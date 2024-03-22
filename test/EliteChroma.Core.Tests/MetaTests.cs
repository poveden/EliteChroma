using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Theory data")]
        public static TheoryData<MethodInfo> GetAllEventHandlers()
        {
            return MetaTestsCommon.GetAllEventHandlers(typeof(ChromaController).Assembly);
        }
    }
}
