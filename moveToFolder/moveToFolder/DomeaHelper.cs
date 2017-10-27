using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLOBJ;

namespace moveToFolder
{
    public static class DomeaConfiguration
    {
        public static string GetOracleConnectionString(SCBWflSession sysSession)
        {
            SCBWflRows rows = sysSession.System.SQLCommand("select value from BIG_CONNECTION_STRING");
            foreach(SCBWflRow row in rows)
            {
                return row.GetColumns().Item(1).ToString();
            }
            return "";
        }
    }
    public class DomeaHelper
    {
        private SCBWflSession sysSession;
        private SCBWflSession workGroupSession;

        public SCBWflFolder SubFolder { get; set; }

        public DomeaHelper(SCBWflSession _sysSession)
        {
            sysSession = _sysSession;
            allFolders = new List<SCBWflFolder>();
        }

        public SCBWflSession getWorkGroupSession(int workGroupID)
        {
            try
            {
                SCBWflWorkGroup wg = sysSession.System.GetWorkGroupByID(sysSession.System.NewIDByLocalKey(workGroupID));
                return sysSession.StartWorkGroupSession(wg);
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public void startWorkGroupSession(int workGroupID)
        {
            try
            {
                SCBWflWorkGroup wg = sysSession.System.GetWorkGroupByID(sysSession.System.NewIDByLocalKey(workGroupID));
                workGroupSession = sysSession.StartWorkGroupSession(wg);
            }
            catch (Exception ex)
            {
                workGroupSession = null;
            }

        }

        public void stopWorkGroupSession()
        {
            sysSession.StopWorkGroup();
        }

        public bool moveWorkItem(int igz, int workGroupID, int destFolderID, out string message)
        {
            message = "";
            try
            {
                if (igz > 0)
                {
                    SCBWflFolder folder = null;
                    if (allFolders.Count > 0)
                    {
                        folder = allFolders.Find(f => f.ID.ToLong(IDType.wflLocalKey) == destFolderID);
                    }
                    if (folder == null)
                    {
                        workGroupSession = getWorkGroupSession(workGroupID);
                        if (workGroupSession != null)
                        {
                            folder = FindFolder(workGroupSession.WorkList, destFolderID);
                            stopWorkGroupSession();
                            if (folder != null) { allFolders.Add(folder); }
                        }
                        else
                        {
                            message = "WorkGroupSession is null: " + workGroupID;
                        }
                    }
                    if (folder != null)
                    {
                        //System.Windows.Forms.MessageBox.Show(folder.Name + " (" + folder.ID.ToLong(IDType.wflLocalKey) + ")" + " " + destFolderID);
                        Console.WriteLine(folder.Name + " (" + folder.ID.ToLong(IDType.wflLocalKey) + ")" + " " + destFolderID);

                        SCBWflProcessInstance pi = sysSession.System.GetProcessInstanceByID(sysSession.System.NewIDByLocalKey(igz));
                        SCBWflWorkItem wi = pi.GetWorkItems().Item(1);
                        if (wi.GetCurrentActor().ID.ToLong(IDType.wflLocalKey) == workGroupID)
                        {
                            pi.SetLock(LockTypeOfProcInst.wflProcInstWhole);
                            wi.MoveToFolder(folder);
                            pi.ReleaseLock(LockTypeOfProcInst.wflProcInstWhole);
                            return true;
                        }
                        else
                        {
                            message = "Aktueller Benutzer stimmt nicht mit Arbeitsgruppe überein! CurrentActor: " + wi.GetCurrentActor().ID.ToLong(IDType.wflLocalKey) + " WorkGroupID: " + workGroupID;
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("IGZ: " + igz + ": " + ex.Message);
                message = "IGZ: " + igz + ": " + ex.Message;
                return false;
            }
        }

        private List<SCBWflFolder> allFolders { get; set; }

        private SCBWflFolder FindFolder(SCBWflWorkList WorkList, int destFolderID)
        {
            foreach (SCBWflFolder sub in WorkList.GetSubFolders())
            {
                if (sub.ID.ToLong(IDType.wflLocalKey) == destFolderID)
                {
                    return sub;
                }

                if (sub.GetSubFolders().Count > 0)
                {
                    SCBWflFolder found = FindFolder(sub, destFolderID);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        private SCBWflFolder FindFolder(SCBWflFolder folder,int destFolderID)
        {
            foreach (SCBWflFolder sub in folder.GetSubFolders())
            {
                if (sub.ID.ToLong(IDType.wflLocalKey) == destFolderID)
                {
                    return sub;
                }

                if (sub.GetSubFolders().Count > 0)
                {
                    SCBWflFolder found = FindFolder(sub, destFolderID);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        public SCBWflFolder createFolderInWorkList(int newWorkGroupID, string OrdnerName)
        {
            SCBWflFolder folder = null;
            try
            {
                if (workGroupSession != null)
                {
                    foreach(SCBWflFolder subFolder in workGroupSession.WorkList.GetSubFolders())
                    {
                        if (subFolder.Name == OrdnerName)
                        {
                            Console.WriteLine("Folder '" + OrdnerName + "' existiert bereits!");
                            return subFolder;
                        }
                    }
                    folder = workGroupSession.WorkList.CreateSubFolder(OrdnerName);
                }
                return folder;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return folder;
            }
        }

        public SCBWflFolder createFolderInFolder(SCBWflFolder folder, string OrdnerName)
        {
            try
            {
                foreach (SCBWflFolder subFolder in folder.GetSubFolders())
                {
                    if (subFolder.Name == OrdnerName)
                    {
                        Console.WriteLine("Folder '" + OrdnerName + "' existiert bereits!");
                        return subFolder;
                    }
                }
                if (folder != null)
                {
                    return folder.CreateSubFolder(OrdnerName);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
