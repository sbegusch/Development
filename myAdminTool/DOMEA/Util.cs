using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DOMEA
{
    public static class Util
    {
        public static void WriteMethodInfoToConsole([System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            ConsoleColor ccDefault = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("AssemblyName: " + GetAssemblyName);
            Console.ForegroundColor = ccDefault;

            Console.WriteLine("MemberName: " + memberName);
            Console.WriteLine("SourceFilePath: " + sourceFilePath);
            Console.WriteLine("SourceLineNumber: " + sourceLineNumber);
        }

        private static string GetAssemblyName
        {
            get
            {
                return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) ;
            }
        }
    }
}
