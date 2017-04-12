using myAdminTool.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.SystemImplementation.DOMEA
{
    public partial class frmUploadToOTCS : Form
    {
        private Object MainForm;

        public enum ItemType
        {
            Document,
            ProcessInstance
        }

        public static bool IsVisible { get; set; }
        public frmUploadToOTCS(Object frmMain, int Left, int Top, int Height = 0)
        {
            InitializeComponent();
            MainForm = frmMain;
            this.Left = Left;
            this.Top = Top;
            if (Height > 0)
            {
                this.Height = Height;
            }
            IsVisible = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                int Counter = 0;
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewRow row in dgvUpload.Rows)
                {
                    if (row.Cells["colChecked"].Value.ToString() == "1")
                    {
                        if (Convert.ToInt32(row.Cells["colIGZ"].Value) > 0)
                        {//dann handelt es sich um einen Akt
                            ((frmMain)MainForm).UploadProcessInstanceToOTCS(Convert.ToInt32(txtFolderID.Text), row.Cells["colGZ"].Value.ToString(), row.Cells["colPIComment"].Value.ToString());
                            row.Cells["colChecked"].Value = "0";
                            Counter += 1;
                        }
                        if (Convert.ToInt32(row.Cells["colEinlaufzahl"].Value) > 0)
                        {//dann handelt es sich um ein Dokument
                            ((frmMain)MainForm).UploadDocumentToOTCS(Convert.ToInt32(txtFolderID.Text), row.Cells["colELZ"].Value.ToString(), Convert.ToInt32(row.Cells["colEinlaufzahl"].Value.ToString()));
                            row.Cells["colChecked"].Value = "0";
                            Counter += 1;
                        }
                    }
                }
                Cursor.Current = Cursors.WaitCursor;
                if (Counter == 1)
                {
                    MessageBox.Show("Upload von " + Counter + " Objekt durchgeführt", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (Counter > 1)
                {
                    MessageBox.Show("Upload von " + Counter + " Objekten durchgeführt", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.WaitCursor;
                Error.Show(ex);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUploadToOTCS_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsVisible = false;
        }

        public void AddItems(DataGridViewRow Row, ItemType Type)
        {
            switch (Type)
            {
                case ItemType.Document:
                    dgvUpload.Rows.Add(new string[] { "0", "-1", Row.Cells["colDOMEinlaufzahl"].Value.ToString(), "", "", Row.Cells["colDOMELZ"].Value.ToString(), Row.Cells["colDOMAnmerkung"].Value.ToString() });
                    break;
                case ItemType.ProcessInstance:
                    dgvUpload.Rows.Add(new string[] { "0", Row.Cells["colIGZ"].Value.ToString(), "-2", Row.Cells["colGZ"].Value.ToString(), Row.Cells["colComment"].Value.ToString(), "", "" });
                    break;
            }
        }

        private void txtFolderID_TextChanged(object sender, EventArgs e)
        {
            btnUpload.Enabled = true;
        }
        public void SetFolderProperties(int ID, string Name)
        {
            txtFolderID.Text = ID.ToString();
            txtFolderName.Text = Name;
        }
    }
}
