using System.Reflection;

namespace EliteChroma.Core.Tests.Internal
{
    internal static class ReflectionExtensions
    {
        public static T GetPrivateField<T>(this object obj, string name)
        {
            return (T)obj.GetType()
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(obj);
        }

        public static void SetPrivateField<T>(this object obj, string name, T value)
        {
            obj.GetType()
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(obj, value);
        }

        public static T InvokePrivateMethod<T>(this object obj, string name, params object[] parameters)
        {
            return (T)obj.GetType()
                .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(obj, parameters);
        }
    }
}
