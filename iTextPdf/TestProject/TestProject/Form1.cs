using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        #region MessageBox
        const string MsgText = "Open the new file?";
        const string MsgCaption = "Watermark";
        #endregion

        #region File
        const string FileOpenFilter = "Pdf Files|*.pdf";
        #endregion
        
        #region private Member
        private byte[] SourceFile { get; set; }
        private Stopwatch stopWatch { get; set; }

        #endregion

        public Form1()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
        }

        private void btnLoadPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FileOpenFilter;
            ofd.ShowDialog();
            txtFilePath.Text = ofd.FileName;

            if (txtFilePath.Text != "")
            {
                SourceFile = System.IO.File.ReadAllBytes(txtFilePath.Text);
                btnAddWatermark.Enabled = true;
                btnAddWatermark2.Enabled = true;
            }
            else
            {
                btnAddWatermark.Enabled = false;
                btnAddWatermark2.Enabled = false;
            }
        }

        private void btnAddWatermark_Click(object sender, EventArgs e)
        {
            iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);

            stopWatch.Reset();
            stopWatch.Start();
            byte[] retValue = Watermark.AddWatermark(SourceFile , bf, txtWatermark.Text);
            stopWatch.Stop();
            lblITextSharpAllPages.Text = "Milliseconds: " + stopWatch.Elapsed.Milliseconds;
            string tmpFile = DestinationFile;
            File.WriteAllBytes(tmpFile, retValue);

            DialogResult dr = MessageBox.Show(MsgText, MsgCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                Process.Start(tmpFile);
            }
        }
        
        private void btnAddWatermark2_Click(object sender, EventArgs e)
        {
            string tmpFile = DestinationFile;
            stopWatch.Reset();
            stopWatch.Start();
            Watermark.manipulatePdf(txtFilePath.Text, tmpFile, txtWatermark2.Text);
            stopWatch.Stop();
            lblITextSharpFirstPage.Text = "Milliseconds: " + stopWatch.Elapsed.Milliseconds;
            DialogResult dr = MessageBox.Show(MsgText, MsgCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Process.Start(tmpFile);
            }
        }

        #region Helper
        private string DestinationFile
        {
            get
            {
                string retValue = "";
                retValue = string.Format("{0}\\out_{1}{2}", Path.GetDirectoryName(txtFilePath.Text),
                                                            DateTime.Now.ToString("yyyyMMddhhmmss"),
                                                            Path.GetExtension(txtFilePath.Text));

                return retValue;
            }
        }
        #endregion
    }
}
