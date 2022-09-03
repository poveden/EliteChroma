using System.Diagnostics.CodeAnalysis;
using EliteChroma.Internal;
using EliteChroma.Properties;

namespace EliteChroma
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using var mutex = new Mutex(true, $"{nameof(EliteChroma)}-SingleInstance-Mutex");

#pragma warning disable S2222 // False positive
            bool isSingleInstance = mutex.WaitOne(0, true);
#pragma warning restore S2222

            try
            {
                if (!isSingleInstance)
                {
                    _ = MessageBox.Show(
                        Resources.MsgBox_AppAlreadyRunning,
                        new AssemblyInfo().Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    return;
                }

                ApplicationConfiguration.Initialize();

                using var appContext = new AppContext();

                if (appContext.Start())
                {
                    Application.Run(appContext);
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
