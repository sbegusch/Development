using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLOBJ;
using CCDGUI;
using System.IO;

namespace checkLogFiles
{
    public class DOMEA
    {
        private const string c_APP_CODE = "BIG";

        private SCBWflSession Session { get; set; }
        private SCBWflSession SysSession { get; set; }
        
        public void OpenConnection()
        {
            ccdLogin login = new ccdLogin();
            if (login.DoLogin())
            {
                try
                {
                    Session = login.Session;
                    SysSession = new SCBWflSession();
                    SysSession.ConnectServer(Session.HostName, Session.PortNo);
                    SysSession.SystemLogin(c_APP_CODE, 0);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("OpenDOMEA Session: " + ex.Message);
                    System.Environment.Exit(0);
                }
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        public int GetWorkGroupID(string Name)
        {
            SCBWflRows rows = SysSession.System.SQLCommand("select usernr from mitarbeiter where username = '" + Name + "' and ist_rolle = 1");
            foreach(SCBWflRow row in rows)
            {
                return Convert.ToInt32(row.ToString());
            }
            return -1;
        }
        public int Counter { get; set; }
        public void LogToTable(int WGNr, string WGName, string FolderName, string GZ, List<string> allPreviousFolder)
        {
            try
            {
                FolderObject o = getFolderObject(WGNr, FolderName, allPreviousFolder);
                
                //SysSession.System.SQLCommand("insert into BIG_WORKGROUP_FOLDER_RESTORE values(" + WGNr + ", '" + WGName + "', '" + FolderName + "', '" + GZ + "')");
                if (o != null)
                {
                    SysSession.System.SQLCommand("insert into BIG_FOLDER_RESTORE_TMP values(" + WGNr + ", '" + WGName + "', '" + FolderName + "', '" + GZ + "', ' ', " + WGNr + ", '" + o.PFAD_GESAMT + "', '" +
                                                                                            o.PFAD + "', '" + o.VATER_FOLDERBEZ + "', '" + o.FOLDERBEZ + "', " + o.FOLDERNR + ", " + o.VATER_FOLDERNR + ", " + o.FOLDER_LEVEL + ")");
                }
                else
                {
                    SysSession.System.SQLCommand("insert into BIG_FOLDER_RESTORE_TMP values(" + WGNr + ", '" + WGName + "', '" + FolderName + "', '" + GZ + "', ' ', " + WGNr + ", ' ', ' ', ' ', ' ', -200, -200, -200)");
                }

                Console.Title = "Insertierte Datensätze " + Counter++;
            }
            catch(Exception ex)
            {
                Console.WriteLine("LogToTable --> " + ex.Message);
            }
        }
        
        FolderObject fo;
        public FolderObject getFolderObject(int UserNr, string FolderName, List<string> allPreviousFolder)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT usernr,pfad_gesamt, pfad,vater_folderbez, folderbez,foldernr,vater_foldernr,FOLDER_LEVEL FROM( ");
                sb.Append("SELECT ");
                   sb.Append("USERNR,  ");
                   sb.Append("USERNAME || SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\') PFAD_GESAMT,  ");
                   sb.Append("SUBSTR(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\'), 2) PFAD,  ");
                   sb.Append("NVL(substr(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\'), 2, LENGTH(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\') )- LENGTH(FOLDERBEZ) - 2), ' ')  VATER_FOLDERBEZ,  ");
                   sb.Append("FOLDERBEZ,  ");
                   sb.Append("FOLDERNR,  ");
                   sb.Append("VATER_FOLDERNR,  ");
                   sb.Append("LEVEL FOLDER_LEVEL ");
                sb.Append("FROM  ");
                   sb.Append("(SELECT f.USERNR, m.USERNAME, f.FOLDERNR, f.VATER_FOLDERNR, f.FOLDERBEZ  ");
                    sb.Append("FROM FOLDER f, MITARBEITER m  ");
                    sb.Append("WHERE f.USERNR = " + UserNr); //--> USERNUMMER 
                      sb.Append(" AND m.USERNR = f.USERNR ");
                      sb.Append("AND m.IST_ROLLE = 1) f ");
                sb.Append("START WITH f.VATER_FOLDERNR = 0 ");
                sb.Append("CONNECT BY PRIOR f.FOLDERNR=f.VATER_FOLDERNR ");
                sb.Append(") WHERE FOLDERBEZ = '" + FolderName + "'"); //--> FOLDERBEZEICHNUNG 

                SCBWflRows rows = SysSession.System.SQLCommand(sb.ToString());
                string parentFolder = "";
                for (int i = allPreviousFolder.Count - 1; i >= 0; i--)
                {
                    parentFolder = allPreviousFolder[i];
                    foreach (SCBWflRow row in rows)
                    {
                        //Console.WriteLine("4: " + row.GetColumns().Item(4).ToString());
                        //Console.WriteLine("5: " + row.GetColumns().Item(5).ToString());
                        if (parentFolder == row.GetColumns().Item(4).ToString() || rows.Count == 1)
                        {
                            //yes
                            try
                            {
                                fo = new FolderObject();
                                if (row.GetColumns().Item(1) != null) { fo.USERNR = Convert.ToInt32(row.GetColumns().Item(1).ToString()); }
                                if (row.GetColumns().Item(2) != null) { fo.PFAD_GESAMT = row.GetColumns().Item(2).ToString(); }
                                if (row.GetColumns().Item(3) != null) { fo.PFAD = row.GetColumns().Item(3).ToString(); }
                                if (row.GetColumns().Item(4) != null) { fo.VATER_FOLDERBEZ = row.GetColumns().Item(4).ToString(); }
                                if (row.GetColumns().Item(5) != null) { fo.FOLDERBEZ = row.GetColumns().Item(5).ToString(); }
                                if (row.GetColumns().Item(6) != null) { fo.FOLDERNR = Convert.ToInt32(row.GetColumns().Item(6).ToString()); }
                                if (row.GetColumns().Item(7) != null) { fo.VATER_FOLDERNR = Convert.ToInt32(row.GetColumns().Item(7).ToString()); }
                                if (row.GetColumns().Item(8) != null) { fo.FOLDER_LEVEL = Convert.ToInt32(row.GetColumns().Item(8).ToString()); }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("getFolderObject --> " + parentFolder + "  --> " + ex.Message);
                            }
                            return fo;
                        }
                    }
                }
                
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine("getFolderObject --> " + ex.Message);
                return null;
            }
        }
    }

    public class FolderObject
    {
        public FolderObject()
        {
            this.WGNR = -99;
            this.WGNAME = "init";
            this.FOLDERNAME = "init";
            this.GZ = "init";
            this.PATH = "init";
            this.USERNR = -99;
            this.PFAD_GESAMT = "init";
            this.PFAD = "init";
            this.VATER_FOLDERBEZ = "init";
            this.FOLDERBEZ = "init";
            this.FOLDERNR = -99;
            this.VATER_FOLDERNR = -99;
            this.FOLDER_LEVEL = -99; 
        }
        public int WGNR {get; set;}
        public string WGNAME {get; set;}
        public string FOLDERNAME {get; set;}
        public string GZ {get; set;}
        public string PATH {get; set;}
        public int USERNR {get; set;}
        public string PFAD_GESAMT {get; set;}
        public string PFAD {get; set;}
        public string VATER_FOLDERBEZ {get; set;}
        public string FOLDERBEZ {get; set;}
        public int FOLDERNR {get; set;}
        public int VATER_FOLDERNR {get; set;}
        public int FOLDER_LEVEL { get; set; }
    }
}
