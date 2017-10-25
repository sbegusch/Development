using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WFLOBJ;
using DataGridViewAutoFilter;
using System.Reflection;

namespace moveToFolder
{
    public partial class frmMain : Form
    {
        const string APP_CODE = "BIG";
        const int PRODUCT_ID = 0;

        #region private/public Member
        public SCBWflSession sysSession { get; set; }

        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        private int DataCount = 0;
        private bool Tab2Loaded;

        private OracleHelper ora;
        private Assembly assem;
        private Logger logger;
        #endregion

        #region Main Form

        public frmMain()
        {
            InitializeComponent();
            assem = typeof(frmMain).Assembly;
            Console.Title = assem.FullName;
            CCDGUI.ccdLogin login = new CCDGUI.ccdLogin();
            if (login.DoLogin())
            {
                try
                {
                    Console.WriteLine("\n...KONFIGURATION WIRD GELADEN...");
                    logger = new Logger();
                    logger.Filename = string.Format("{0}{1}_{2}.csv", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().ProcessName, DateTime.Now.ToString("yyyyMMddhhmmss"));
                    Cursor.Current = Cursors.WaitCursor;
                    sysSession = new SCBWflSession();
                    sysSession.ConnectServer(login.Session.HostName, login.Session.PortNo);
                    sysSession.SystemLogin(APP_CODE, PRODUCT_ID);

                    lblDOMEAConnection.Text = sysSession.HostName + ";" + sysSession.PortNo;

                    ora = new OracleHelper(DomeaConfiguration.GetOracleConnectionString(sysSession));
                    ora.OpenConnection();

                    dgvBIG_FOLDER_RESTORE_TMP.DataSource = bindingSource1;
                    /*
                    string stmt = "select distinct f.tmp_ID ID, g.igz, g.gz, f.wgnr, f.wgname, f.PFAD_GESAMT, f.folderbez, f.FOLDERNR, f.VATER_FOLDERBEZ, f.PATH STATUS " +
                                "from BIG_FOLDER_RESTORE_TMP f, gst g " +
                                "where f.foldernr >= 0 " +
                                  "and g.gz = f.gz " +
                                  "and f.wgnr = f.usernr " +
                                //"order by f.pfad_gesamt";
                                "order by f.tmp_ID";
                     
                    string stmt = "select distinct f.tmp_ID ID, g.igz, g.gz, f.wgnr, f.wgname, f.PFAD_GESAMT, f.PFAD, f.folderbez, f.FOLDERNR, f.VATER_FOLDERBEZ, f.PATH STATUS, " +
                                                   "m.DESTINATION_ID, m.DESTINATION_NAME, m.DESTINATION_NAME || '\' || f.PFAD PFAD_NEU " +
                                    "from BIG_FOLDER_RESTORE_TMP f, gst g, V_BIG_WORKGROUP_OLD_NEW m  " +
                                    "where f.foldernr >= 0  " +
                                    "and g.gz = f.gz  " +
                                    "and f.wgnr = f.usernr  " +
                                    "and m.source_id = f.wgnr " +
                                    "order by f.tmp_ID";
                    */
                    string stmt = "select distinct f.tmp_ID ID, g.igz, g.gz, f.wgnr, f.wgname, f.PFAD_GESAMT, f.PFAD, f.folderbez, f.FOLDERNR, f.VATER_FOLDERBEZ, f.PATH STATUS, " +
                                                   "m.DESTINATION_ID, m.DESTINATION_NAME, m.DESTINATION_NAME || '\\' || f.PFAD PFAD_NEU " +
                                    "from BIG_FOLDER_RESTORE_TMP f, gst g, V_BIG_WORKGROUP_OLD_NEW m  " +
                                    "where trim(f.path) is null " +  // nur solche die noch nicht verarbeitet wurden
                                    "and f.foldernr >= 0  " +
                                    "and g.gz = f.gz  " +
                                    "and f.wgnr = f.usernr  " +
                                    "and m.source_id = f.wgnr " +
                                    "order by f.tmp_ID";

                    //bindingSource1.DataSource = ora.GetData(stmt);

                    //dgvBIG_FOLDER_RESTORE_TMP.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvBIG_FOLDER_RESTORE_TMP_DataBindingComplete);
                    
                    dgvBIG_FOLDER_RESTORE_TMP.DataSource = ora.GetData(stmt);

                    DataCount = dgvBIG_FOLDER_RESTORE_TMP.Rows.Count;
                    Tab2Loaded = false;
                    Cursor.Current = Cursors.Default;
                }
                catch(Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("LADEN DER KONFIGURATION ABGESCHLOSSEN....");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFolder4Workgroup.Text = "";
            if (!Tab2Loaded)
            {
                Console.WriteLine("KONFIGURATION WIRD GELADEN....");

                string stmt = "select distinct f.wgnr usernr, m.username " +
                                "from BIG_FOLDER_RESTORE_TMP f, mitarbeiter m " +
                                 "where m.usernr = f.wgnr " +
                                 "and m.ist_rolle = 1 " +
                                 "order by 2";

                lblWorkGroups.Text = "";

                dgvWorkGroups.DataSource = bindingSource2;
                bindingSource2.DataSource = ora.GetData(stmt);
                lblWorkGroups.Text = "WorkGroups " + dgvWorkGroups.Rows.Count;
                Tab2Loaded = true;
            }
        }
        
        #endregion

        #region 1. Tab

        private void dgvBIG_FOLDER_RESTORE_TMP_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dgvBIG_FOLDER_RESTORE_TMP.DataSource == null) return;

                //foreach (DataGridViewColumn col in dgvBIG_FOLDER_RESTORE_TMP.Columns)
                //{
                //    if (col.Name == "WGNAME")
                //    {
                //        col.HeaderCell = new
                //                DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                //    }
                //}
                //dgvBIG_FOLDER_RESTORE_TMP.AutoResizeColumns();
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvBIG_FOLDER_RESTORE_TMP_DataBindingComplete(object sender, EventArgs e)
        {
            //lblRowCount.Text = dgvBIG_FOLDER_RESTORE_TMP.Rows.Count + " Datensätze von " + DataCount + " geladen...";
        }

        private void btnMovePI_Click(object sender, EventArgs e)
        {
            bwMoveProcessInstance.RunWorkerAsync();
        }

        #endregion

        #region 2. Tab

        private void dgvWorkGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvCreateFolder.Rows.Clear();

            int WorkGroupID = Convert.ToInt32(dgvWorkGroups.Rows[e.RowIndex].Cells[0].Value);
            string WorkGroup = dgvWorkGroups.Rows[e.RowIndex].Cells[1].Value.ToString();

            Console.WriteLine("\nKONFIGURATION FÜR " + WorkGroup + " WIRD GELADEN....");
            
            string stmt = "SELECT distinct usernr,pfad_gesamt, pfad,vater_folderbez, folderbez,foldernr,vater_foldernr,FOLDER_LEVEL " +
                            "FROM( " +
                            "SELECT " +
                               "USERNR, " +
                               "USERNAME || SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\') PFAD_GESAMT, " +
                               "SUBSTR(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\'), 3) PFAD, " +
                               "NVL(substr(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\'), 2, LENGTH(SYS_CONNECT_BY_PATH(FOLDERBEZ, '\\') )- LENGTH(FOLDERBEZ) - 4), ' ')  VATER_FOLDERBEZ, " +
                               "FOLDERBEZ, " +
                               "FOLDERNR, " +
                               "VATER_FOLDERNR, " +
                               "LEVEL FOLDER_LEVEL " +
                            "FROM " +
                               "(SELECT f.USERNR, m.USERNAME, f.FOLDERNR, f.VATER_FOLDERNR, f.FOLDERBEZ " +
                                "FROM FOLDER f, MITARBEITER m " +
                                "WHERE f.USERNR = " + WorkGroupID + " " +  // --> USERNUMMER
                                "AND m.USERNR = f.USERNR " +
                                  "AND m.IST_ROLLE = 1) f " +
                            "START WITH f.VATER_FOLDERNR = 0 " +
                            "CONNECT BY PRIOR f.FOLDERNR=f.VATER_FOLDERNR " +
                            ")";
            
            dgvCreateFolder.DataSource = ora.GetData(stmt);
            lblFolder4Workgroup.Text = dgvCreateFolder.Rows.Count + " Ordner zum Erstellen...";
        }
        
        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvCreateFolder.Rows)
                {
                    Console.WriteLine(row.Cells["PFAD_GESAMT"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("btnCreateFolder_Click(): " + ex.Message);
            }
        }
        
        #endregion

        #region Background Worker - MoveProcessInstance

        private void bwMoveProcessInstance_DoWork(object sender, DoWorkEventArgs e)
        {
            int maxRows = dgvBIG_FOLDER_RESTORE_TMP.Rows.Count;
            int counter = 0;
            int percent = 0;
            int IGZ = 0;
            int workGroupID = 0;
            int folderID = 0;
            string GZ = "";
            string DESTINATION_NAME = "";
            string PFAD_NEU = "";
            string message = "";
            logger.WriteFile(string.Format("IGZ;GZ;DESTINATION_ID;DESTINATION_NAME;PFAD_NEU"));
            DomeaHelper domea = new DomeaHelper(sysSession);
            foreach(DataGridViewRow row in dgvBIG_FOLDER_RESTORE_TMP.Rows)
            {
                if (row.Cells["STATUS"].Value.ToString().Trim() == "")
                {
                    GZ = "";
                    DESTINATION_NAME = "";
                    PFAD_NEU = "";
                    IGZ = 0;
                    workGroupID = 0;
                    folderID = 0;
                    message = "";
                    IGZ = Convert.ToInt32(row.Cells["IGZ"].Value);

                    try
                    {
                        //workGroupID = Convert.ToInt32(row.Cells["WGNR"].Value); // => alte WorkGroup
                        workGroupID = Convert.ToInt32(row.Cells["DESTINATION_ID"].Value); // => neue WorkGroup
                        folderID = Convert.ToInt32(row.Cells["FOLDERNR"].Value);
                        GZ = row.Cells["GZ"].Value.ToString();
                        DESTINATION_NAME = row.Cells["DESTINATION_NAME"].Value.ToString();
                        PFAD_NEU = row.Cells["PFAD_NEU"].Value.ToString();
                    
                        //move WorkItem
                        if (domea.moveWorkItem(IGZ, workGroupID, folderID, out message))
                        {
                            row.Cells["STATUS"].Value = "finished";
                            logger.WriteFile(string.Format("{0};{1};{2};{3};{4}",IGZ,GZ,workGroupID,DESTINATION_NAME,PFAD_NEU));
                            InfoLogger.WriteFile(string.Format("Akt {0} mit der IGZ {1} nach {2} verschoben...", GZ, IGZ, PFAD_NEU));
                        }
                        else
                        {
                            ErrorLogger.WriteFile(string.Format("{0};{1};{2};{3};{4}", IGZ, GZ, workGroupID, DESTINATION_NAME, PFAD_NEU));
                            if (message != "")
                            {
                                ErrorLogger.WriteFile(GZ + ": " + message);
                                Console.WriteLine(GZ + ": " + message);
                                row.Cells["STATUS"].Value = message;
                            }
                            else
                            {
                                row.Cells["STATUS"].Value = "Warning";
                            }
                        }
                        counter = counter + 1;
                        percent = (counter * 100) / maxRows;
                        bwMoveProcessInstance.ReportProgress(percent, row);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("IGZ: " + IGZ + " - " + ex.Message);
                    }
                }
            }
            e.Result = counter;
        }

        private int rowID;
        private string updateStmt;

        private void bwMoveProcessInstance_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)e.UserState;
            try
            {
                //UPDATE STATUS = Feld PATH to "finished"
                rowID = Convert.ToInt32(row.Cells["ID"].Value);
                if (rowID > 0)
                {
                    //updateStmt = "update BIG_FOLDER_RESTORE_TMP set path = ' ' where tmp_id = " + rowID;
                    //updateStmt = "update BIG_FOLDER_RESTORE_TMP set path = 'finished' where tmp_id = " + rowID;
                    updateStmt = "update BIG_FOLDER_RESTORE_TMP set path = '" + row.Cells["STATUS"].Value.ToString() + "' where tmp_id = " + rowID;
                    
                    if (ora.Execute(updateStmt))
                    {
                        //row.Cells["STATUS"].Value = "finished";
                        Console.Title = e.ProgressPercentage + "% finished";
                    }
                    else
                    {
                        Console.WriteLine("Error on: " + updateStmt);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(row.Cells["GZ"].Value.ToString() + Environment.NewLine + ex.Message);
            }
        }

        private void bwMoveProcessInstance_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.Title = assem.FullName;
            Console.WriteLine("move ProcessInstance finished, Count: " + e.Result.ToString() + " --> " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
        }

        #endregion

    }
}
