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
        public SCBWflSession sysSession { get; set; }

        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        private int DataCount = 0;
        private bool Tab2Loaded;

        private OracleHelper ora;

        public frmMain()
        {
            InitializeComponent();
            Assembly assem = typeof(frmMain).Assembly;
            Console.Title = assem.FullName;
            CCDGUI.ccdLogin login = new CCDGUI.ccdLogin();
            if (login.DoLogin())
            {
                try
                {
                    Console.WriteLine("\n...KONFIGURATION WIRD GELADEN...");

                    Cursor.Current = Cursors.WaitCursor;
                    sysSession = new SCBWflSession();
                    sysSession.ConnectServer(login.Session.HostName, login.Session.PortNo);
                    sysSession.SystemLogin(APP_CODE, PRODUCT_ID);

                    lblDOMEAConnection.Text = sysSession.HostName + ";" + sysSession.PortNo;

                    ora = new OracleHelper(DomeaConfiguration.GetOracleConnectionString(sysSession));
                    ora.OpenConnection();

                    dgvBIG_FOLDER_RESTORE_TMP.DataSource = bindingSource1;

                    string stmt = "select distinct g.igz, g.gz, f.wgnr, f.wgname, f.PFAD_GESAMT, f.folderbez, f.FOLDERNR " +
                                "from BIG_FOLDER_RESTORE_TMP f, gst g " +
                                "where f.foldernr >= 0 " +
                                  "and g.gz = f.gz " +
                                  "and f.wgnr = f.usernr " +
                                "order by f.pfad_gesamt";
                    bindingSource1.DataSource = ora.GetData(stmt);

                    dgvBIG_FOLDER_RESTORE_TMP.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgvBIG_FOLDER_RESTORE_TMP_DataBindingComplete);

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

        private void dgvBIG_FOLDER_RESTORE_TMP_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvBIG_FOLDER_RESTORE_TMP.DataSource == null) return;

                foreach (DataGridViewColumn col in dgvBIG_FOLDER_RESTORE_TMP.Columns)
                {
                    if (col.Name == "WGNAME")
                    {
                        col.HeaderCell = new
                                DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                    }
                }
                dgvBIG_FOLDER_RESTORE_TMP.AutoResizeColumns();
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvBIG_FOLDER_RESTORE_TMP_DataBindingComplete(object sender, EventArgs e)
        {
            lblRowCount.Text = dgvBIG_FOLDER_RESTORE_TMP.Rows.Count + " Datensätze von " + DataCount + " geladen...";
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
            catch(Exception ex)
            {
                Console.WriteLine("btnCreateFolder_Click(): " + ex.Message);
            }
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

        //private void dgvCreateFolder_BindingContextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dgvCreateFolder.DataSource == null) return;

        //        foreach (DataGridViewColumn col in dgvCreateFolder.Columns)
        //        {
        //            col.HeaderCell = new
        //                    DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
        //        }
        //        dgvCreateFolder.AutoResizeColumns();
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void frmMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("LADEN DER KONFIGURATION ABGESCHLOSSEN....");
        }

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

    }
}
