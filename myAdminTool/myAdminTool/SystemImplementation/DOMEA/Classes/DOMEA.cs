using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WFLOBJ;
using myAdminTool.Classes;
using CCD.Domea.Login;
using CCD.Domea.Fw;
using CCD.Domea.Fw.Base;
using CCD.Domea.Fw.Base.CcdBase;
using CCD.Domea.Fw.Base.Obj;
using System.IO;
namespace myAdminTool.SystemImplementation.DOMEA
{
    public class DOMEA
    {
        private Session session { get; set; }

        public string ConnectionInfo { get; set; }

        public bool Login()
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                CcdLogin Login = new CcdLogin();
                if (Login.DoLogin())
                {
                    session = Login.Session;
                }
                ConnectionInfo = "DOMEA: " + session.GetLoggedOnUser().Name + "@" + session.HostName;
                
                return true;
            }
            catch (Exception ex)
            {
                Error.Show(ex);
                ConnectionInfo = "DOMEA Login: " + ex.Message;
                return false;
            }
        }

        public bool Logout()
        {
            Util.WriteMethodInfoToConsole();
            try
            {

                session.Logout();
                
                ConnectionInfo = "DOMEA: Logout durchgeführt";

                return true;
            }
            catch (Exception ex)
            {
                Error.Show(ex);
                ConnectionInfo = "DOMEA Logout: " + ex.Message;
                return false;
            }
        }

        public string GetDocumentFile(int Einlaufzahl)
        {
            Document doc = new Document(session, Einlaufzahl);
            string lisFile = doc.GetFileObjects().ToList<FileObject>()[doc.GetFileObjects().Count -1].GetDocumentFile();
            string DocFile = (Path.GetFileNameWithoutExtension(lisFile) + doc.Extension).Replace("*", string.Empty);
            File.Move(lisFile, DocFile);
            //string lisFile = doc.GetFileObjects().ToList<FileObject>()[0].GetDocumentFile();
            return DocFile;
        }

        public Folder GetMainFolder()
        {
            Util.WriteMethodInfoToConsole();
            return session.GetCurrentActor().GetMainFolder();
        }

        public ProcessInstance GetProcessInstanceByID(int IGZ)
        {
            Util.WriteMethodInfoToConsole();
            return new ProcessInstance(session, IGZ);
        }

        public List<WorkItem> GetWorkList()
        {
            Util.WriteMethodInfoToConsole();
            Folder mainFolder = session.GetCurrentActor().GetMainFolder();
            return mainFolder.GetWorkList().ToList<WorkItem>();
        }

        public List<WorkItem> GetWorkList(Folder SelectedFolder)
        {
            Util.WriteMethodInfoToConsole();
            return SelectedFolder.GetWorkList().ToList<WorkItem>();
        }

        public Folder GetFolderByID(int FolderID)
        {
            return new Folder(session, session.GetCurrentActor(), FolderID);
        }

        public List<Document> GetDocuments(int IGZ)
        {
            Util.WriteMethodInfoToConsole();
            return GetProcessInstanceByID(IGZ).GetDocuments().ToList<Document>();
        }
    }
}
