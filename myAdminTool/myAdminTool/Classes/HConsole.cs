using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace myAdminTool
{
    public static class HConsole
    {
        public static bool DoWrite { get; set; }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static void WriteLine(string line, object ObjectDump = null)
        {
            if (DoWrite)
            {
                Console.WriteLine(line);
                if (ObjectDump != null) { Dump(ObjectDump, true); }
                Logger.WriteFile(line);
            }
        }

        public static void WriteError(Exception ex)
        {
            string MethodName = GetLastCalledMethod(ex);
            if (MethodName != "")
            {
                Console.WriteLine(string.Format("{0}\n{1}\n{2}", DateTime.Now.ToString(), MethodName, ex.Message));
                Logger.WriteFile(string.Format("{0} {1} {2}", DateTime.Now.ToString(), MethodName, ex.Message));
            }
            else
            {
                Console.WriteLine(string.Format("{0}\n{1}", DateTime.Now.ToString(), ex.Message));
                Logger.WriteFile(string.Format("{0} {1}", DateTime.Now.ToString(), ex.Message));
            }
        }

        private static string GetLastCalledMethod(Exception ex)
        {
            try
            {
                var stackTrace = new System.Diagnostics.StackTrace(ex);
                return stackTrace.GetFrame(1).GetMethod().Name;
            }
            catch
            {
                return "";
            }
        }

        private static void Dump(object o, bool LogToFile = true)
        {
            if (o == null)
            {
                Console.WriteLine("null");
                return;
            }

            var properties =
                from prop in o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where prop.CanRead
                && !prop.GetIndexParameters().Any() // exclude indexed properties to keep things simple
                select new
                {
                    prop.Name,
                    Value = prop.GetValue(o, null)
                };

            Console.WriteLine(o.ToString());
            if (LogToFile) { Logger.WriteFile(o.ToString()); }
            foreach (var prop in properties)
            {
                Console.WriteLine(
                    "\t{0}: {1}",
                    prop.Name,
                    (prop.Value ?? "null").ToString());
                if (LogToFile)
                {
                    Logger.WriteFile(string.Format("\t{0}: {1}",
                                        prop.Name,
                                        (prop.Value ?? "null").ToString()));
                }
            }
        }
    }

    #region FileLogger

    public static class Logger
    {
        private static string Filename
        {
            get
            {
                return string.Format("{0}{1}.log", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
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
