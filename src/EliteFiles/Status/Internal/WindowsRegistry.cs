using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;
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

        [SupportedOSPlatform("windows")]
        public object? GetValue(string keyName, string? valueName, object? defaultValue)
        {
            return Registry.GetValue(keyName, valueName, defaultValue);
        }
    }
}
