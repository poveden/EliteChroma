using System.Reflection;

namespace EliteFiles.Tests.Internal
{
    internal static class ReflectionExtensions
    {
        public static T GetPrivateField<T>(this object obj, string name)
        {
            return (T)obj.GetType()
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(obj);
        }

        public static T InvokePrivateMethod<T>(this object obj, string name, params object[] parameters)
        {
            return (T)obj.GetType()
                .GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(obj, parameters);
        }
    }
}
