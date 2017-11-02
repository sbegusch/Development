using System;
using System.Windows.Forms;
using System.Drawing;

namespace myCountdown
{
//*****************************************************************************
    static class Program
    {
        private static NotifyIcon notico;
        private static DateTime endDate;
        //==========================================================================
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] astrArg)
        {
            ContextMenu cm;
            MenuItem miCurr;
            int iIndex = 0;

            ///<Kontextmenü erzeugen>
            cm = new ContextMenu();

            ///<Kontextmenüeinträge erzeugen>
            miCurr = new MenuItem();
            miCurr.Index = iIndex++;
            miCurr.Text = "&Beenden";
            miCurr.Click += new System.EventHandler(ExitClick);
            cm.MenuItems.Add(miCurr);

            ///<NotifyIcon selbst erzeugen>
            notico = new NotifyIcon();
            notico.Icon = new Icon("timer.ico"); 
            notico.Text = "";
            notico.Visible = true;
            notico.ContextMenu = cm;
            notico.MouseMove += new MouseEventHandler(NotifyIconMouseMove);
            notico.MouseClick += new MouseEventHandler(NotifyIconMouseClick);

            endDate = new DateTime(2017, 11, 15, 23, 59, 59);
            // Ohne Appplication.Run geht es nicht
            Application.Run();
        }

        //==========================================================================
        private static void ExitClick(Object sender, EventArgs e)
        {
            notico.Dispose();
            Application.Exit();
        }
        
        private static void NotifyIconMouseMove(Object sender, EventArgs e)
        {
            notico.Text = "nur noch " + CountWeekdays(DateTime.Now, endDate) + " Wochentage";
        }

        private static void NotifyIconMouseClick(Object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left)
            {
                frmMain main = new frmMain();
                main.ShowDialog();
            }
        }

        private static int CountWeekdays(DateTime startTime, DateTime endTime)
        {
            TimeSpan timeSpan = endTime - startTime;
            DateTime dateTime;
            int weekdays = 0;
            for (int i = 0; i < timeSpan.Days; i++)
            {
                dateTime = startTime.AddDays(i);
                if (IsWeekDay(dateTime))
                    weekdays++;
            }
            return weekdays;
        }

        private static bool IsWeekDay(DateTime dateTime)
        {
            return ((dateTime.DayOfWeek != DayOfWeek.Saturday) && (dateTime.DayOfWeek != DayOfWeek.Sunday));
        }
    }
}