using System.Reflection;
using Xunit;

namespace TestUtils
{
    internal static class MetaTestsCommon
    {
        private static readonly NullabilityInfoContext _context = new NullabilityInfoContext();

        public static string GetSolutionDirectory()
        {
            const string SolutionFilename = "EliteChroma.sln";

            var fi = new FileInfo(typeof(MetaTestsCommon).Assembly.Location);

            for (var di = fi.Directory; di != null; di = di.Parent)
            {
                if (di.GetFiles(SolutionFilename, SearchOption.TopDirectoryOnly).Length != 0)
                {
                    return di.FullName;
                }
            }

            throw new InvalidOperationException("Solution directory could not be found.");
        }

        public static void AssertSenderParameterIsNullable(MethodInfo eventHandler)
        {
            ArgumentNullException.ThrowIfNull(eventHandler);

            var ni = _context.Create(eventHandler.GetParameters()[0]);
            Assert.Equal(NullabilityState.Nullable, ni.WriteState);
        }

        public static TheoryData<MethodInfo> GetAllEventHandlers(Assembly assembly)
        {
            return new TheoryData<MethodInfo>(
                from t in assembly.GetTypes()
                where !t.FullName!.StartsWith("Coverlet.", StringComparison.Ordinal) // Reference: https://github.com/coverlet-coverage/coverlet/issues/1191
                from mi in t.GetMethods(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                let ps = mi.GetParameters()
                where mi.ReturnType == typeof(void)
                && ps.Length == 2
                && ps[0].ParameterType == typeof(object)
                && (ps[1].ParameterType.IsAssignableTo(typeof(EventArgs)) || ps[0].Name == "sender")
                select mi);
        }
    }
}
