
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using myAdminTool.Classes;

namespace myAdminTool
{
    public partial class frmConnectOracle : Form
    {
        private TnsNamesReader tnsNamesReader;
        private List<KeyValuePair<string, string>> TNSNamesEntries;
        public frmConnectOracle()
        {
            InitializeComponent();
            lblStatusInfo.Text = "";

            //tnsNamesReader = new TnsNamesReader(@"C:\oracle\tnsnames\tnsnames.ora");
            tnsNamesReader = new TnsNamesReader();
            tnsNamesReader.Read();
            TNSNamesEntries = tnsNamesReader.Properties;

            foreach (KeyValuePair<string, string> entry in TNSNamesEntries)
            {
                cbTNSNames.Items.Add(entry.Key);
            }
            if (cbTNSNames.Items.Count > 0) { cbTNSNames.SelectedIndex = 0; }
        }

        private void cbTNSNames_SelectedValueChanged(object sender, EventArgs e)
        {
            txtDBHost.Text = TNSNamesEntries.Find(a => a.Key == cbTNSNames.Text).Value;
            Console.WriteLine(cbTNSNames.Text + " --> " + txtDBHost.Text);
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTNSNames.Text == "") { throw new Exception("Bitte tnsnames.ora Eintrag auswählen!"); }
                if (txtUser.Text == "") { throw new Exception("Bitte Benutzer eingeben!"); }
                if (txtPassword.Text == "") { throw new Exception("Bitte Passwort eingeben!"); }

                string ConnectionString = string.Format("Data Source={0};User Id={1};Password={2};", cbTNSNames.Text, txtUser.Text, txtPassword.Text);
                OracleHelper.ConnectionOpen(ConnectionString);

                lblStatusInfo.Text = string.Format("Oracle: {0}@{1}", txtUser.Text, cbTNSNames.Text);
                OracleHelper.ConnectionInfo = lblStatusInfo.Text;
                btnDisconnect.Enabled = true;
                btnConnect.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                lblStatusInfo.Text = "";
                Error.Show(ex);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            OracleHelper.ConnectionClose();
            lblStatusInfo.Text = "";
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConnect_Click(sender, e);
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }
    }
}
