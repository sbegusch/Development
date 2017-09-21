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
        private const string APP_CODE = "BIG";

        private Session session { get; set; }

        public string ConnectionInfo { get; set; }
        
        public string OracleConnectionString 
        {
            get
            {
                string retValue = "";
                string stmtConnectionString = "select value from BIG_CONNECTION_STRING";
                SQLTransaction transaction = new SQLTransaction(this.session, APP_CODE, stmtConnectionString);
                foreach (SQLRow row in transaction.GetResultRows())
                {
                    retValue = row.ToString();
                }
                return retValue;
            }
        }

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
            Util.WriteMethodInfoToConsole();
            Folder mainFolder = session.GetCurrentActor().GetMainFolder();
            return mainFolder.GetSubFolders().First(f => f.Id == FolderID);

            //return new Folder(session, session.GetCurrentActor(), FolderID);
        }

        public List<Document> GetDocuments(int IGZ)
        {
            Util.WriteMethodInfoToConsole();
            return GetProcessInstanceByID(IGZ).GetDocuments().ToList<Document>();
        }

        #region Organisation
        public bool OrganisationExists(string OEBEZ)
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                if (session.GetOrganizations().First(o => o.Name == OEBEZ) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public int createOrganisation(int ParentOEID, string OEBEZ, string OEKURZBEZ)
        {
            Organization parent = session.GetOrganizations().First(o => o.Id == ParentOEID);
            Organization oe = new Organization(session);
            oe.Name = OEBEZ;
            oe.Code = OEKURZBEZ;
            oe.SetParentOrganization(parent);
            oe.Create();
            return oe.Id;
        }
        #endregion

        #region WorkGroup
        public bool WorkGroupExists(string WGBEZ)
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                if (session.GetWorkGroups().First(w => w.Name == WGBEZ) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public int createWorkGroup(int OEID, string WGBEZ)
        {
            Organization oe = session.GetOrganizations().First(o => o.Id == OEID);

            WorkGroup wg = new WorkGroup(session);
            wg.Name = WGBEZ;
            wg.SetOrganization(oe);
            wg.Create();
            wg.AssignProfile(new Profile(session, 4));
            return wg.Id;
        }

        public void assignUserToWorkGroup(int UserID, int WorkGroupID)
        {
            User user = new User(session, UserID);
            if (user != null)
            {
                WorkGroup wg = new WorkGroup(session, WorkGroupID);
                if (wg != null)
                {
                    user.AssignWorkGroup(wg);
                }
            }
        }
        #endregion
    }
}
