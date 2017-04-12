
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.Classes
{
    public static class Util
    {
        public static string GetAssemblyVersion 
        { 
            get 
            { 
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
            } 
        }

        public static string GetAssemblyName
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        /// <summary>
        /// Retourniert den Namen der aktuellen Methode (aus der diese aufgerufen wird ;)
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethodName()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static void WriteMethodInfoToConsole(string MethodName)
        {
            HConsole.WriteLine("Aufruf der Methode: " + MethodName + "()");
        }

        public static void WriteMethodInfoToConsole([System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            HConsole.WriteLine("MemberName: " + memberName);
            HConsole.WriteLine("SourceFilePath: " + sourceFilePath);
            HConsole.WriteLine("SourceLineNumber: " + sourceLineNumber);
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public static class Error
    {
        public static void Show(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HConsole.WriteLine(ex.Message, ex);
        }
    }
}
