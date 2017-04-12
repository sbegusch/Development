using System;
using System.Windows.Forms;
using myAdminTool.Classes;

namespace myAdminTool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Util.WriteMethodInfoToConsole();
            ConsoleHelper.IsVisible = true;
            ConsoleHelper.Show();
            HConsole.DoWrite = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
