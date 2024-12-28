using System.Diagnostics.CodeAnalysis;
using TestUtils;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class MetaTests
    {
        [Theory]
        [MemberData(nameof(GetAllEventHandlers))]
        public void EventHandlersDeclareSenderParameterAsNullable(Type type, string eventHandlerName)
        {
            MetaTestsCommon.AssertSenderParameterIsNullable(type, eventHandlerName);
        }

        [SuppressMessage("OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Theory data")]
        public static TheoryData<Type, string> GetAllEventHandlers()
        {
            return MetaTestsCommon.GetAllEventHandlers(typeof(ChromaController).Assembly);
        }
    }
}
