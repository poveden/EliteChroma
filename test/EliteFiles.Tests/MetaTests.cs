using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using EliteFiles.Internal;
using EliteFiles.Journal;
using EliteFiles.Journal.Internal;
using TestUtils;
using Xunit;

namespace EliteFiles.Tests
{
    public class MetaTests
    {
        [Theory]
        [MemberData(nameof(GetAllEventHandlers))]
        public void EventHandlersDeclareSenderParameterAsNullable(MethodInfo eventHandler)
        {
            MetaTestsCommon.AssertSenderParameterIsNullable(eventHandler);
        }

        [Fact]
        public void AllJournalEntryTypesRegisteredInTheJournalEntryConverterAreIncludedInTheSerializerContext()
        {
            var converterTypes = new HashSet<Type>(typeof(JournalEntryConverter).InvokePrivateStaticMethod<Dictionary<string, Type>>("BuildJournayEntryEventMap")!.Values)
            {
                typeof(JournalEntry),
            };

            var serializerContextTypes = new HashSet<Type>(
                from pi in typeof(EliteFilesSerializerContext).GetProperties()
                where pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(JsonTypeInfo<>)
                select pi.PropertyType.GenericTypeArguments[0]);

            Assert.ProperSubset(serializerContextTypes, converterTypes);
        }

        [SuppressMessage("OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Theory data")]
        public static IEnumerable<object[]> GetAllEventHandlers()
        {
            return MetaTestsCommon.GetAllEventHandlers(typeof(JournalFolder).Assembly);
        }
    }
}
