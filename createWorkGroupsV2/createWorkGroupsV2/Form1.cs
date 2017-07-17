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
using oraClient = Oracle.ManagedDataAccess.Client;
using oraType = Oracle.ManagedDataAccess.Types;

namespace createWorkGroupsV2
{
    public partial class frmMain : Form
    {
        const string USER_CODE = "BIG";

        public SCBWflSession Session { get; set; }
        public string ConnectionString { get; set; }
        public oraClient.OracleConnection oracleConnection { get; set; }
        public frmMain()
        {
            InitializeComponent();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select 49 OENR, 'OM Team Wien 1' OEBEZ, 'OFM W1' OE_KURZBEZ, ");
                sb.AppendLine("'OFM W1' || substr(username, instr(username, '_')) ARBEITSGRUPPE ");
                sb.AppendLine("from mitarbeiter where username like 'W1%' and ist_rolle = 1 and status = 0 ");
                sb.AppendLine("order by 1");
                txtWGStatement.Text = sb.ToString();
                
                txtUserStatement.Text = "select USERNR, USERNAME from mitarbeiter where usernr = 11";

                CCDGUI.ccdLogin login = new CCDGUI.ccdLogin();
                if (login.DoLogin())
                {
                    Session = login.Session;
                    SCBWflRows rows = Session.SQLCommand(USER_CODE, "select value from BIG_CONNECTION_STRING");
                    foreach (SCBWflRow row in rows)
                    {
                        ConnectionString = row.ToString();
                        oracleConnection = new oraClient.OracleConnection(ConnectionString);
                        oracleConnection.Open();
                    }
                }
                else
                {
                    this.Close();
                }
                rbSelectUser.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in frmMain()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                dgvPreview.Columns.Clear();

                oraClient.OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.CommandText = txtWGStatement.Text;
                using (oraClient.OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvPreview.DataSource = dataTable;
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in btnLoad_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in frmMain_FormClosing()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbSelectUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSelectUser.Checked)
            {
                txtUserStatement.Enabled = true;
                dgvUser.Enabled = true;
                btnLoadUser.Enabled = true;
            }
            else
            {
                txtUserStatement.Enabled = false;
                btnLoadUser.Enabled = false;
                dgvUser.Columns.Clear();
                dgvUser.Enabled = false;
            }
        }

        private void btnLoadUser_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUser.Columns.Clear();

                oraClient.OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.CommandText = txtUserStatement.Text;
                using (oraClient.OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvUser.DataSource = dataTable;
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in btnLoadUser_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateWorkGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbSelectUser.Checked && dgvUser.Rows.Count > 0)
                { //user über select statement
                    createWorkGroup();
                }

                if (rbPrimaryUser.Checked)
                { // user über die primary OE finden

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in btnCreateWorkGroup_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void createWorkGroup()
        {
            try
            {
                int OENR = 0;
                int userID = 0;
                SCBWflOrganization oe;
                SCBWflUser user;
                SCBWflWorkGroup wg;
                bool checkFlag = false;
                foreach (DataGridViewRow row in dgvPreview.Rows)
                {
                    try
                    {
                        checkFlag = true;
                        OENR = Convert.ToInt32(row.Cells["OENR"].Value.ToString());
                        oe = Session.System.GetOrganizationByID(Session.System.NewIDByLocalKey(OENR));
                        wg = Session.System.NewWorkGroup();
                        wg.SetOrganization(oe);
                        wg.Name = row.Cells["ARBEITSGRUPPE"].Value.ToString();

                        wg.Create();
                    }
                    catch (Exception ex)
                    {
                        checkFlag = false;
                        MessageBox.Show(ex.Message, "Error in SCBWflWorkGroup.Create()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        try
                        {
                            wg = Session.System.FindWorkGroups("Name like '" + row.Cells["ARBEITSGRUPPE"].Value.ToString() + "'").Item(1);
                            checkFlag = true;
                        }
                        catch
                        {
                            checkFlag = false;
                        }
                    }
                    try
                    {
                        if (checkFlag)
                        {
                            foreach (DataGridViewRow userRow in dgvUser.Rows)
                            {
                                userID = Convert.ToInt32(userRow.Cells["USERNR"].Value.ToString());
                                user = Session.System.GetUserByID(Session.System.NewIDByLocalKey(userID));
                                user.AssignWorkGroup(wg);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error in SCBWflUser.AssignWorkGroup()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in createWorkGroup()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
