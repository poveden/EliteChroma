using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Windows.Forms;
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

            if (!mutex.WaitOne(0, true))
            {
                _ = MessageBox.Show(
                    Resources.MsgBox_AppAlreadyRunning,
                    new AssemblyInfo().Title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            _ = Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var appContext = new AppContext();

            if (appContext.Start())
            {
                Application.Run(appContext);
            }

            mutex.ReleaseMutex();
        }
    }
}
