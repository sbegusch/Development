﻿using System;
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
using System.Diagnostics;
namespace myAdminTool.SystemImplementation.DOMEA
{
    public class DOMEA
    {
        private const string APP_CODE = "BIG";

        private const int c_StandardProfileID = 4;
        
        public List<Profile> DOMEAProfiles { get; set; }

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

        public bool Login(bool CreateSysSession = false)
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                CcdLogin Login = new CcdLogin();
                if (Login.DoLogin())
                {
                    if (!CreateSysSession)
                    {
                        session = Login.Session;
                    }
                    else
                    {
                        //session = new Session(Login.Session.GetApiServer());
                        //Password pwd = new Password(APP_CODE);
                        //session.Login(0, pwd);
                        CCD.Domea.Fw.Base.Api.ApiServer apiServer = new CCD.Domea.Fw.Base.Api.ApiServer(Login.Session.HostName, Login.Session.PortNo);
                        Session sysSession = new Session(apiServer);
                        sysSession.Login(1, new Password(APP_CODE));
                        session = sysSession;
                    }
                    DOMEAProfiles = new List<Profile>();
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
        public bool OrganisationExists(string OEBEZ, out int OENr)
        {
            Util.WriteMethodInfoToConsole();
            OENr = -1;
            try
            {
                Organization oe = session.GetOrganizations().First(o => o.Name == OEBEZ);
                if (oe == null)
                {
                    return false;
                }
                else
                {
                    OENr = oe.Id;
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

        public void assignUserFromOeToOe(Organization fromOE, Organization toOE)
        {
            try
            {
                foreach(User user in fromOE.GetUsers())
                {
                    try
                    {
                        User u = toOE.GetUsers().First(f => f.Id == user.Id);
                        HConsole.WriteLine(u.Name + " already assigned to Organisation " + toOE.Name);
                    }
                    catch
                    {
                        user.SetOrganization(toOE);
                        user.Update();
                    }
                }
            }
            catch(Exception ex)
            {
                HConsole.WriteError(ex);
            }
        }


        #endregion

        #region WorkGroup
        public bool WorkGroupExists(string WGBEZ, out int WGNr)
        {
            Util.WriteMethodInfoToConsole();
            WGNr = -1;
            try
            {
                WorkGroup wg = session.GetWorkGroups().First(w => w.Name == WGBEZ);
                if (wg == null)
                {
                    return false;
                }
                else
                {
                    WGNr = wg.Id;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        private int getProfileID(string subString)
        {
            int retValue = -1;
            try
            {
                string stmt = "select profile_id from PROFILE_REP where upper(PROFILE_NAME) like '%" + subString.ToUpper() + "'";
                SQLTransaction transaction = new SQLTransaction(this.session, APP_CODE, stmt);
                foreach (SQLRow row in transaction.GetResultRows())
                {
                    retValue = Convert.ToInt32(row.GetColumn(0));
                    break;
                }
                return retValue;
            }
            catch(Exception ex)
            {
                HConsole.WriteError(ex);
                return retValue;
            }
        }

        public int createWorkGroup(int OEID, string WGBEZ)
        {
            DOMEAProfiles.Clear();
            Organization oe = session.GetOrganizations().First(o => o.Id == OEID);

            WorkGroup wg = new WorkGroup(session);
            wg.Name = WGBEZ;
            wg.SetOrganization(oe);
            wg.Create();
            DOMEAProfiles.Add(new Profile(session, c_StandardProfileID));
            
            if (WGBEZ.ToUpper().EndsWith("_FOM") ||
                WGBEZ.ToUpper().EndsWith("_MV") ||
                WGBEZ.ToUpper().EndsWith("_ZDA"))
            {
                DOMEAProfiles.Add(new Profile(session, getProfileID(WGBEZ.Substring(WGBEZ.IndexOf("_") + 1))));
            }
            wg.AssignProfile(DOMEAProfiles.ToArray());

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
                    user.Update();
                }
            }
        }

        public bool isUserAssignedToWorkGroup(int UserID, int WorkGroupID)
        {
            try
            {
                User user = new User(session, UserID);
                if (user != null)
                {
                    WorkGroup wg = user.GetWorkGroups().First(w => w.Id == WorkGroupID);
                    if (wg == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public WorkGroup getWorkGroupByID(int ID)
        {
            return new WorkGroup(session, ID);
        }

        public Organization getOrganizationByID(int ID)
        {
            return new Organization(session, ID);
        }

        public int WorkItemCount(WorkGroup wg)
        {
            if (wg.GetWorkItems() == null || wg.GetWorkItems().Count < 0)
            {
                return 0;
            }
            else
            {
                return wg.GetWorkItems().Count;
            }
        }
        int counter = 0;
        Stopwatch sWatchMovePI;
        public void MoveWorkItem(WorkGroup fromWorkGroup, WorkGroup toWorkGroup, Organization oe)
        {
            Util.WriteMethodInfoToConsole();
            ProcessInstance PI = null;
            
            List<AccessRule> Rules = new List<AccessRule>();
            AccessRule arRead = new AccessRule(session, CCD.Domea.Fw.Base.AccessType.Read, RuleType.SpecificOrganization, oe);
            AccessRule arWrite = new AccessRule(session, CCD.Domea.Fw.Base.AccessType.Write, RuleType.SpecificOrganization, oe);

            //Rules.Add(new AccessRule(session, CCD.Domea.Fw.Base.AccessType.Read, RuleType.SpecificOrganization, oe));
            //Rules.Add(new AccessRule(session, CCD.Domea.Fw.Base.AccessType.Write, RuleType.SpecificOrganization, oe));
            counter = 0;
            sWatchMovePI = new Stopwatch();
            foreach (WorkItem wi in fromWorkGroup.GetWorkItems())
            {
                try
                {
                    sWatchMovePI.Reset();
                    sWatchMovePI.Start();
                    counter = counter + 1;
                    PI = wi.GetProcessInstance();
                    HConsole.WriteLine(">> Verschieben des Aktes " + PI.Name + " beginnt...");

                    Rules.Clear();
                    Rules.AddRange(PI.GetAccessRules(CCD.Domea.Fw.Base.AccessType.Read));
                    Rules.Add(arRead);
                    Rules.AddRange(PI.GetAccessRules(CCD.Domea.Fw.Base.AccessType.Write));
                    Rules.Add(arWrite);

                    PI.SetActiveWorkItem(wi);
                    PI.SetLock(ProcessInstanceLockLevel.Whole);
                    wi.WriteHistoryLogEvent(30, "Start BIG Changes 2.0", new HistoryLogEventType(session, 99));
                    PI.SetCustomAttribute("CSVERANTWOE", oe.Id.ToString());
                    PI.AssignAccessRules(Rules.ToArray(), true);
                    PI.Update();

                    wi.SetLock(WorkItemLockLevel.WorkItem);
                    wi.DelegateTo(toWorkGroup);
                    wi.ReleaseLock(WorkItemLockLevel.WorkItem);
                    
                    wi.WriteHistoryLogEvent(30, "BIG Changes 2.0 erfolgreich durchgeführt", new HistoryLogEventType(session, 99));
                    PI.ReleaseLock(ProcessInstanceLockLevel.Whole);
                    sWatchMovePI.Stop();
                    HConsole.WriteLine(">> Verschieben des Aktes " + PI.Name + " abgeschlossen... (Zähler: "+ counter + " / Stopwatch: " + sWatchMovePI.Elapsed.ToString("ss\\.ff") + ")");
                }
                catch(Exception ex)
                {
                    HConsole.WriteError(ex);
                }
            }
        }

        #endregion
    }
}
