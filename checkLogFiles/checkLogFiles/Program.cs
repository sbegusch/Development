using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkLogFiles
{
    class Program
    {
        const Int32 BufferSize = 128;
        
        static void Main(string[] args)
        {
            DOMEA domea = new DOMEA();
            domea.OpenConnection();
            string outFileName = string.Format("{0}logFiles\\out.csv", AppDomain.CurrentDomain.BaseDirectory);

            logToFile(outFileName, "WGNr;WGName;FolderName;GZ");

            string[] allFiles = Directory.GetFiles(string.Format("{0}logFiles", AppDomain.CurrentDomain.BaseDirectory));
            foreach(string file in allFiles)
            {
                if (file.EndsWith(".log"))
                {
                    Console.WriteLine(file);
                    readFile(file, outFileName, domea);
                }
            }

            //string fileName = string.Format("{0}logFiles\\myAdminTool_sub1.log", AppDomain.CurrentDomain.BaseDirectory);
            ////string outFileName = fileName.Replace(".log", ".csv");
            //readFile(fileName, outFileName, domea);

            //fileName = string.Format("{0}logFiles\\myAdminTool_sub2.log", AppDomain.CurrentDomain.BaseDirectory);
            ////outFileName = fileName.Replace(".log", ".csv");
            //readFile(fileName, outFileName, domea);

            //fileName = string.Format("{0}logFiles\\myAdminTool_sub3.log", AppDomain.CurrentDomain.BaseDirectory);
            ////outFileName = fileName.Replace(".log", ".csv");
            //readFile(fileName, outFileName, domea);

            //fileName = string.Format("{0}logFiles\\myAdminTool_sub4.log", AppDomain.CurrentDomain.BaseDirectory);
            ////outFileName = fileName.Replace(".log", ".csv");
            //readFile(fileName, outFileName, domea);
        }

        private const string c_Foldername = ">> Foldername: ";
        private const string c_VerschiebenDesAktes = ">> Verschieben des Aktes ";
        private const string c_Beginnt = " beginnt...";
        
        static List<string> allFoldersFromUser;

        private static void readFile(string inFileName, string outFileName, DOMEA domea)
        {
            using (var fileStream = File.OpenRead(inFileName))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    bool isWorkgroup = false;
                    string folderName = "";
                    string tmpText = "";
                    int WGNR = -1;
                    string WGName = "";
                    string tmpLine = "";
                    allFoldersFromUser = new List<string>();
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        //if (line.Contains("MemberName: MoveWorkItem"))
                        if (line.Contains("SourceLineNumber: 397"))
                        { //Dann ist die nächste Zeile ein Arbeitsgruppennname
                            isWorkgroup = true;
                            folderName = "";
                        }
                        else if (line.Contains(c_Foldername))
                        {
                            tmpText = line.Substring(line.IndexOf(c_Foldername) + c_Foldername.Length).Trim();
                            if (isWorkgroup)
                            {
                                allFoldersFromUser.Clear();
                                WGName = tmpText;
                                WGNR = domea.GetWorkGroupID(tmpText);
                                //logToFile(outFileName, "WorkGroupName: " + tmpText);
                                //logToFile(outFileName, "WorkGroupID: " + WGNR);
                                isWorkgroup = false;
                            }
                            else
                            {
                                folderName = tmpText;
                                allFoldersFromUser.Add(folderName);
                                //logToFile(outFileName, folderName);
                            }
                        }
                        else if (line.Contains(c_VerschiebenDesAktes) && line.Contains(c_Beginnt))
                        {
                            tmpText = line.Substring(line.IndexOf(c_VerschiebenDesAktes) + c_VerschiebenDesAktes.Length).Replace(c_Beginnt, string.Empty).Trim();
                            tmpLine = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"", WGNR, WGName, folderName, tmpText);
                            //Console.WriteLine(tmpLine);
                            //logToFile(outFileName, tmpLine);
                            domea.LogToTable(WGNR, WGName, folderName, tmpText, allFoldersFromUser);
                        }
                        
                    }
                }
            }
        }

        private static void logToFile(string logFile, string line)
        {
            //using (StreamWriter sw = File.AppendText(logFile)) 
            using (StreamWriter writer = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                writer.WriteLine(line);
                //sw.WriteLine(line.Substring(line.IndexOf(">> ")));
            }	
        }


    }
}
