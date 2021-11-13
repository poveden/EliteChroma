using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32;

namespace EliteFiles.Status.Internal
{
    [ExcludeFromCodeCoverage]
    internal sealed class WindowsRegistry : IWindowsRegistry
    {
        public static readonly IWindowsRegistry Instance = new WindowsRegistry();

        private WindowsRegistry()
        {
        }

        public object? GetValue(string keyName, string? valueName, object? defaultValue)
        {
            return Registry.GetValue(keyName, valueName, defaultValue);
        }
    }
}
