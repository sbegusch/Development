using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moveToFolder
{
    public class Logger
    {
        public string Filename { set; get; }
        
        /// <summary>
        /// Append a LogMessage to the LogFile
        /// </summary>
        public void WriteFile(String sLines)
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename).Close();
            }
            using (FileStream fs = new FileStream(Filename, FileMode.Open, System.Security.AccessControl.FileSystemRights.AppendData, FileShare.Write, 4096, FileOptions.None))
            {
                using (StreamWriter myFile = new StreamWriter(fs))
                {
                    myFile.AutoFlush = true;
                    myFile.WriteLine(sLines);
                    myFile.Close();
                }
            }
        }
    }

    #region ErrorLogger

    public static class ErrorLogger
    {
        private static string Filename
        {
            get
            {
                return string.Format("{0}error.log", AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        /// <summary>
        /// Append a LogMessage to the LogFile
        /// </summary>
        public static void WriteFile(String sLines)
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename).Close();
            }
            using (FileStream fs = new FileStream(Filename, FileMode.Open, System.Security.AccessControl.FileSystemRights.AppendData, FileShare.Write, 4096, FileOptions.None))
            {
                using (StreamWriter myFile = new StreamWriter(fs))
                {
                    myFile.AutoFlush = true;
                    myFile.WriteLine(string.Format("{0} - [{1}]: {2}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), ProcessID, sLines.TrimEnd()));
                    myFile.Close();
                }
            }
        }

        /// <summary>
        /// retourniert die ProzessID für das LogFile
        /// </summary>
        private static int ProcessID
        {
            get
            {
                System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                return currentProcess.Id;
            }
        }

    }

    #endregion

    #region InfoLogger

    public static class InfoLogger
    {
        private static string Filename
        {
            get
            {
                return string.Format("{0}info.log", AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        /// <summary>
        /// Append a LogMessage to the LogFile
        /// </summary>
        public static void WriteFile(String sLines)
        {
            if (!File.Exists(Filename))
            {
                File.Create(Filename).Close();
            }
            using (FileStream fs = new FileStream(Filename, FileMode.Open, System.Security.AccessControl.FileSystemRights.AppendData, FileShare.Write, 4096, FileOptions.None))
            {
                using (StreamWriter myFile = new StreamWriter(fs))
                {
                    myFile.AutoFlush = true;
                    myFile.WriteLine(string.Format("{0} - [{1}]: {2}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), ProcessID, sLines.TrimEnd()));
                    myFile.Close();
                }
            }
        }

        /// <summary>
        /// retourniert die ProzessID für das LogFile
        /// </summary>
        private static int ProcessID
        {
            get
            {
                System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                return currentProcess.Id;
            }
        }

    }

    #endregion
}
