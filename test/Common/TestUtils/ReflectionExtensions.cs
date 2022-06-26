using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TestUtils
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Shared test code.")]
    internal static class ReflectionExtensions
    {
        public static T? GetPrivateField<T>(this object obj, string name)
        {
            return (T?)obj.GetType()
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(obj);
        }

        public static void SetPrivateField<T>(this object obj, string name, T value)
        {
            obj.GetType()
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(obj, value);
        }

        public static T? GetPrivateStaticField<T>(this Type type, string name)
        {
            return (T?)type
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Static)!
                .GetValue(null);
        }

        public static T? InvokePrivateMethod<T>(this object obj, string name, params object[] parameters)
        {
            return (T?)obj.GetType()
                .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(obj, parameters);
        }

        public static T? InvokePrivateStaticMethod<T>(this Type type, string name, params object[] parameters)
        {
            return (T?)type
                .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)!
                .Invoke(null, parameters);
        }
    }
}
