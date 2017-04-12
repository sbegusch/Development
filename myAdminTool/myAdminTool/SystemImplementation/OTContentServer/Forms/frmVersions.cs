using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.SystemImplementation.OTContentServer.Forms
{
    public partial class frmVersions : Form
    {
        OTCS.CWSClient fCWSClient;
        OTCSDocumentManagement.Node selectedNode;
        List<string> svFilter;

        public frmVersions(OTCS.CWSClient _fCWSClient, OTCSDocumentManagement.Node _selectedNode)
        {
            InitializeComponent();
            fCWSClient = _fCWSClient;
            selectedNode = _selectedNode; 
        }

        private void frmVersions_Load(object sender, EventArgs e)
        {
            this.Text += selectedNode.Name;

            OTCSDocumentManagement.NodeVersionInfo ni = selectedNode.VersionInfo;
            int idx;
            svFilter = new List<string>();
            if (ni == null)
            {
                this.Close();
            }
            else
            {
                for (int i = 0; i < ni.Versions.Length; i++)
                {
                    idx = dgv_Versions.Rows.Add();
                    dgv_Versions.Rows[idx].Cells["colVersion"].Value = ni.Versions[i].VerMajor.ToString() + "." + ni.Versions[i].VerMinor.ToString();
                    dgv_Versions.Rows[idx].Cells["colDatum"].Value = ni.Versions[i].CreateDate.ToString();
                    dgv_Versions.Rows[idx].Cells["colErsteller"].Value = fCWSClient.GetMemberDisplayName(Convert.ToInt32(ni.Versions[i].Owner));
                    dgv_Versions.Rows[idx].Cells["colDateiname"].Value = ni.Versions[i].Filename;
                    dgv_Versions.Rows[idx].Cells["colDocID"].Value = ni.Versions[i].ID.ToString();
                    dgv_Versions.Rows[idx].Cells["colVersionNum"].Value = ni.Versions[i].Number.ToString();
                    dgv_Versions.Rows[idx].Cells["colDateityp"].Value = ni.Versions[i].FileType.ToString();

                }

                checkIfSelected();
                //MessageBox.Show(ni.Versions[ni.VersionNum - 1].VerMajor.ToString());
                //MessageBox.Show(ni.Versions[ni.Versions.Length - 1].VerMajor.ToString());
            }
        }

        private void dgv_Versions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            checkIfSelected();
        }

        private void checkIfSelected()
        {
            if (dgv_Versions.SelectedRows.Count == 1)
            {
                btn_DownloadDoc.Enabled = true;
            }
            else
            {
                btn_DownloadDoc.Enabled = false;
            }
        }

        private void btn_DownloadDoc_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Title = "Downloadverzeichnis wählen";
            sv.InitialDirectory = "C:\\";
            //MessageBox.Show(dgv_Versions.SelectedRows[0].Cells["colDateityp"].Value.ToString() + "|*." + dgv_Versions.SelectedRows[0].Cells["colDateityp"].Value.ToString());
            sv.Filter = dgv_Versions.SelectedRows[0].Cells["colDateityp"].Value.ToString() + "|*." + dgv_Versions.SelectedRows[0].Cells["colDateityp"].Value.ToString();
            if (dgv_Versions.SelectedRows[0].Cells["colDateiname"].Value.ToString().Contains("."))
            {
                sv.FileName = dgv_Versions.SelectedRows[0].Cells["colDateiname"].Value.ToString();
            }
            else
            {
                sv.FileName = dgv_Versions.SelectedRows[0].Cells["colDateiname"].Value.ToString() + dgv_Versions.SelectedRows[0].Cells["colDateityp"].Value.ToString();
            }

            if(sv.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fCWSClient.DownloadVersionContents(Convert.ToInt32(dgv_Versions.SelectedRows[0].Cells["colDocID"].Value.ToString()), Convert.ToInt32(dgv_Versions.SelectedRows[0].Cells["colVersionNum"].Value.ToString()), Path.GetFullPath(sv.FileName));

                    MessageBox.Show("Das Dokument wurde erfolgreich gespeichert", "Speichern erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR - btn_DownloadDoc_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            //MessageBox.Show(dgv_Versions.SelectedRows[0].Cells["colDocID"].Value.ToString());
        }

        private void btnOTCSCloseVersion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
