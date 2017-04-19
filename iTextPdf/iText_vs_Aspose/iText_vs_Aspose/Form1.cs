using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace iText_vs_Aspose
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

        private enum PdfType
        { 
            iText,
            Aspose
        }

        public Form1()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
        }

        private void btnLoadPdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FileOpenFilter;
            ofd.ShowDialog();
            txtPdfFilePath.Text = ofd.FileName;

            if (txtPdfFilePath.Text != "")
            {
                SourceFile = System.IO.File.ReadAllBytes(txtPdfFilePath.Text);
                btnITextWatermark.Enabled = true;
                //btnAsposeWatermark.Enabled = true;
            }
            else
            {
                btnITextWatermark.Enabled = false;
                btnAsposeWatermark.Enabled = false;
            }
        }

        private void btnITextWatermark_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IFX.iShare.iTextPdf.Transformer transform = new IFX.iShare.iTextPdf.Transformer();

            MemoryStream ms = new MemoryStream(SourceFile);
            stopWatch.Reset();
            stopWatch.Start();
            
            byte[] retValue = transform.WatermarkFile(ms, "begusch", DateTime.Now, "addIT").ToArray();
            
            stopWatch.Stop();
            lblMSiText.Text = "Milliseconds: " + stopWatch.Elapsed.Milliseconds;
            string tmpFile = DestinationFile(PdfType.iText);
            File.WriteAllBytes(tmpFile, retValue);
            Cursor.Current = Cursors.Default;
        }

        private void btnAsposeWatermark_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //IFX.iShare.PdfProcessing.Transformer transform = new IFX.iShare.PdfProcessing.Transformer();

            //MemoryStream ms = new MemoryStream(SourceFile);
            //stopWatch.Reset();
            //stopWatch.Start();
            
            //byte[] retValue = transform.WatermarkFile(ms, "begusch", DateTime.Now, "addIT").ToArray();
            
            //stopWatch.Stop();
            //lblMSAspose.Text = "Milliseconds: " + stopWatch.Elapsed.Milliseconds;
            //string tmpFile = DestinationFile(PdfType.Aspose);
            //File.WriteAllBytes(tmpFile, retValue);
            //Cursor.Current = Cursors.Default;
        }

        #region Helper
        private string DestinationFile(PdfType type)
        {
            string retValue = "";
            switch (type)
            {
                case PdfType.Aspose:
                    retValue = string.Format("{0}\\aspose_{1}{2}", Path.GetDirectoryName(txtPdfFilePath.Text),
                                                                    DateTime.Now.ToString("yyyyMMddhhmmss"),
                                                                    Path.GetExtension(txtPdfFilePath.Text));
                    break;
                case PdfType.iText:
                    retValue = string.Format("{0}\\itext_{1}{2}", Path.GetDirectoryName(txtPdfFilePath.Text),
                                                                    DateTime.Now.ToString("yyyyMMddhhmmss"),
                                                                    Path.GetExtension(txtPdfFilePath.Text));
                    break;
            }
            return retValue;
        }
        #endregion
    }
}
