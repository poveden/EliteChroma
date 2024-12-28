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

        public static void AssertSenderParameterIsNullable(Type type, string eventHandlerName)
        {
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(eventHandlerName);

            var eventHandler = type.GetMethod(eventHandlerName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            Assert.NotNull(eventHandler);

            var ni = _context.Create(eventHandler.GetParameters()[0]);
            Assert.Equal(NullabilityState.Nullable, ni.WriteState);
        }

        public static TheoryData<Type, string> GetAllEventHandlers(Assembly assembly)
        {
            var res = new TheoryData<Type, string>();

            var methods =
                from t in assembly.GetTypes()
                where !t.FullName!.StartsWith("Coverlet.", StringComparison.Ordinal) // Reference: https://github.com/coverlet-coverage/coverlet/issues/1191
                from mi in t.GetMethods(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                let ps = mi.GetParameters()
                where mi.ReturnType == typeof(void)
                && ps.Length == 2
                && ps[0].ParameterType == typeof(object)
                && (ps[1].ParameterType.IsAssignableTo(typeof(EventArgs)) || ps[0].Name == "sender")
                select (t, mi.Name);

            foreach (var (type, name) in methods)
            {
                res.Add(type, name);
            }

            return res;
        }
    }
}
