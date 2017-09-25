using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.Forms
{
    public partial class frmParentOrganisation : Form
    {
        public int OENr { get; set; }

        public frmParentOrganisation()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOENR.Text != "" && CheckStringIsNummeric(txtOENR.Text))
            {
                OENr = Convert.ToInt32(txtOENR.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Eine OENr kann nur aus Zahlen bestehen!", "OENr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckStringIsNummeric(string Text)
        {
            bool retValue = true;
            foreach(char c in Text)
            {
                if (!char.IsNumber(c))
                {
                    retValue = false;
                    break;
                }
            }
            return retValue;
        }
    }
}
