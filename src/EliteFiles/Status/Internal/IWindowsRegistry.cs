namespace EliteFiles.Status.Internal
{
    /// <summary>
    /// Defines methods to access the Windows registry.
    /// </summary>
    internal interface IWindowsRegistry
    {
        /// <summary>
        /// Retrieves the value associated with the specified name, in the specified registry key.
        /// </summary>
        /// <param name="keyName">The full registry path of the key.</param>
        /// <param name="valueName">The name of the name/value pair.</param>
        /// <param name="defaultValue">The value to return if valueName does not exist.</param>
        /// <returns>
        /// <c>null</c> if the subkey specified by keyName does not exist;
        /// otherwise, the value associated with <paramref name="valueName"/>,
        /// or <paramref name="defaultValue"/> if valueName is not found.
        /// </returns>
        /// <seealso cref="Microsoft.Win32.Registry.GetValue(string, string?, object?)"/>
        object? GetValue(string keyName, string? valueName, object? defaultValue);
    }
}
