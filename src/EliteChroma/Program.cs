using System;
using System.Windows.Forms;

namespace EliteChroma
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var appContext = new AppContext();

            if (appContext.Ready)
            {
                Application.Run(appContext);
            }
        }
    }
}
