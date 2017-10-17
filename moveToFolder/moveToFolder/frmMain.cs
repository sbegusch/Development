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

namespace moveToFolder
{
    public partial class frmMain : Form
    {
        const string APP_CODE = "BIG";
        const int PRODUCT_ID = 0;
        public SCBWflSession sysSession { get; set; }

        private BindingSource bindingSource1 = new BindingSource();

        public frmMain()
        {
            InitializeComponent();
            CCDGUI.ccdLogin login = new CCDGUI.ccdLogin();
            if (login.DoLogin())
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    sysSession = new SCBWflSession();
                    sysSession.ConnectServer(login.Session.HostName, login.Session.PortNo);
                    sysSession.SystemLogin(APP_CODE, PRODUCT_ID);

                    lblDOMEAConnection.Text = sysSession.HostName + ";" + sysSession.PortNo;

                    OracleHelper ora = new OracleHelper(DomeaConfiguration.GetOracleConnectionString(sysSession));
                    ora.OpenConnection();

                    dgView.DataSource = bindingSource1;
                    bindingSource1.DataSource = ora.GetData();

                    lblRowCount.Text = dgView.Rows.Count + " Datensätze geladen...";
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

        private void dgView_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgView.DataSource == null) return;

                foreach (DataGridViewColumn col in dgView.Columns)
                {
                    col.HeaderCell = new
                            DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                }
                dgView.AutoResizeColumns();
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgView.Rows)
            {
                Console.WriteLine(row.Cells["PFAD_GESAMT"].Value.ToString());
            }
        }
    }
}
