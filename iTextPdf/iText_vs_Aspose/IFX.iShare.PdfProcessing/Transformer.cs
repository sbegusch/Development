using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Pdf;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Globalization;

namespace IFX.iShare.PdfProcessing
{
    public class Transformer
    {

        public Transformer()
        {
            PDFTools.ApplyLicense();
        }

        public bool isFileProtected(MemoryStream file)
        {
            PdfFileInfo info = new PdfFileInfo(file);
            return info.IsEncrypted;
        }

        public MemoryStream WatermarkFile(MemoryStream s, string sUserName, DateTime dtCurrentDate, string sCompany)
        {
            Aspose.Pdf.License l2 = new Aspose.Pdf.License();
            l2.SetLicense("ASPOSE-DE-CCO-121128--140416092055-DE-SCO-157407.txt");
            l2.Embedded = true;
            Document pdfDocument = new Document(s);

            Document pdfDocumentNoStamps = pdfDocument; //RemoveExistingWatermarks(pdfDocument);
            PdfFileStamp pdfStamp = new PdfFileStamp(pdfDocumentNoStamps);
            return AddWatermarkToFile(pdfDocumentNoStamps, ref pdfStamp, sUserName, dtCurrentDate, sCompany);
        }

        private MemoryStream AddFooterToFile(Document pdfDocument, ref PdfFileStamp pdfStamp, string sUserName, DateTime dtCurrentDate, string sCompany)
        {
            //create footer
            List<TextStamp> ltFooterStamp = new List<TextStamp>();
            TextStamp footerStamp = null;


            //footerStamp = new TextStamp("Document ID: ");
            ////set properties of the stamp
            //footerStamp.BottomMargin = 10;
            //footerStamp.XIndent = 20;
            ////footerStamp.HorizontalAlignment = HorizontalAlignment.Left;
            //footerStamp.VerticalAlignment = VerticalAlignment.Bottom;
            //footerStamp.TextState.FontStyle = FontStyles.Bold;

            //ltFooterStamp.Add(footerStamp);


            //footerStamp = new TextStamp(documentGuid.ToString());
            ////set properties of the stamp
            //footerStamp.BottomMargin = 10;
            //footerStamp.XIndent = 130;
            ////footerStamp.HorizontalAlignment = HorizontalAlignment.Left;
            //footerStamp.VerticalAlignment = VerticalAlignment.Bottom;
            //footerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red);

            //ltFooterStamp.Add(footerStamp);


            string sTextDate = dtCurrentDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            FormattedText wtext = new FormattedText("Confidential", new FontColor(220, 20, 60), new FontColor(255, 255, 255),
                                                        Aspose.Pdf.Facades.FontStyle.HelveticaBold,
                                                        EncodingType.Winansi, true, 17);
            wtext.AddNewLineText(sUserName, 5);
            wtext.AddNewLineText(sTextDate, 5);
            footerStamp = new TextStamp(wtext);
            ltFooterStamp.Add(footerStamp);


            float fWidth = wtext.TextWidth;


            Dictionary<Tuple<double, double>, List<int>> dictMeasurements = new Dictionary<Tuple<double, double>, List<int>>();
            int nPageCount = 1;
            foreach (Aspose.Pdf.Page pg in pdfDocument.Pages)
            {

                foreach (TextStamp ts in ltFooterStamp)
                {
                    pg.AddStamp(ts);
                }



                nPageCount++;

            }


            MemoryStream outStream = new MemoryStream();
            //save updated PDF file
            pdfStamp.Save(outStream);

            return outStream;

        }


        #region watermarking
        private Document RemoveExistingWatermarks(Document pdfDocument)
        {
            PdfContentEditor ped = new PdfContentEditor(pdfDocument);

            for (int i = 1; i <= pdfDocument.Pages.Count; i++)
            {
                double pHeight = pdfDocument.Pages[i].Rect.Height;
                double pWidth = pdfDocument.Pages[i].Rect.Width;

                StampInfo[] vstamp = ped.GetStamps(i);
                List<int> ltToDelete = new List<int>();
                for (int j = 0; j < vstamp.Length; j++)
                {
                    if (StampType.Form == vstamp[j].StampType)
                    {
                        if ((vstamp[j].Rectangle.Height * 100) / pHeight >= 30 || (vstamp[j].Rectangle.Width * 100) / pWidth >= 30)
                        {
                            ltToDelete.Add(j);
                        }
                    }
                }

                int[] vToDelete = new int[ltToDelete.Count];
                for (int j = 0; j < ltToDelete.Count; j++)
                {
                    vToDelete[j] = ltToDelete[j];
                }

                ped.DeleteStamp(i, vToDelete);
            }


            MemoryStream outStream = new MemoryStream();
            ped.Save(outStream);
            Document pdfDocumentNoStamps = new Document(outStream);
            return pdfDocumentNoStamps;
        }

        private MemoryStream AddWatermarkToFile(Document pdfDocument, ref PdfFileStamp pdfStamp, string sUserName, DateTime dtCurrentDate, string sCompany)
        {
            string sTextDate = dtCurrentDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            FormattedText wtext = new FormattedText("Confidential", new FontColor(220, 20, 60), new FontColor(255, 255, 255),
                                                        Aspose.Pdf.Facades.FontStyle.HelveticaBold,
                                                        EncodingType.Winansi, true, 35);
            wtext.AddNewLineText(sUserName, 5);
            wtext.AddNewLineText(sTextDate, 5);

            int nPageCount = 1;
            foreach (Aspose.Pdf.Page pg in pdfDocument.Pages)
            {
                //get the page size position for each page
                Aspose.Pdf.Rectangle rtPage = pg.TrimBox;
                AddWatermarkToPage(rtPage.Width, rtPage.Height, pg, sUserName, sTextDate);

                nPageCount++;

            }

            MemoryStream outStream = new MemoryStream();
            //save updated PDF file
            DocumentPrivilege pv = null;

            pv = DocumentPrivilege.AllowAll;

            pv.ChangeAllowLevel = 0;
            pv.CopyAllowLevel = 1;
            pv.PrintAllowLevel = 2;

            pdfStamp.Document.Encrypt("", new Guid().ToString(), pv, CryptoAlgorithm.RC4x128, false);

            pdfStamp.Save(outStream);

            return outStream;
        }


        private void AddWatermarkToPage(double pWidth, double pHeight, Aspose.Pdf.Page pg, string sUserName, string sTextDate)
        {
            //try{
            //    DeleteExistingWatermarks(pg);
            //}catch { 


            //}

            double alfaR = 30;

            double c = pWidth / pHeight;
            double alfa = Math.Atan(c);
            alfaR = (alfa * 180) / Math.PI;

            FormattedText wtext = new FormattedText("Confidential", new FontColor(220, 20, 60), new FontColor(255, 255, 255),
                                                        Aspose.Pdf.Facades.FontStyle.HelveticaBold,
                                                        EncodingType.Winansi, true, 35);
            wtext.AddNewLineText("Downloaded by");
            wtext.AddNewLineText(sUserName.Length > 55 ? sUserName.Substring(0, 55) : sUserName);
            wtext.AddNewLineText(sTextDate.Length > 19 ? sTextDate.Substring(0, 19) : sTextDate);

            TextStamp wStamp = new TextStamp(wtext);
            wStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red);
            wStamp.TextState.FontStyle = FontStyles.Bold;
            wStamp.TextState.FontSize = 35;

            wStamp.HorizontalAlignment = HorizontalAlignment.Center;
            wStamp.VerticalAlignment = VerticalAlignment.Center;
            wStamp.TextAlignment = HorizontalAlignment.Center;
            wStamp.Opacity = .15F;
            wStamp.RotateAngle = (float)(alfaR - 90);

            pg.AddStamp(wStamp);
        }
        #endregion

        //private void DeleteExistingWatermarks(Page pg)
        //{
        //    if (null == pg.Artifacts) { return; }

        //    List<int> ltToDel = new List<int>();
        //    for (int i = 1; i <= pg.Artifacts.Count; i++)
        //    {
        //        if (Artifact.ArtifactSubtype.Watermark == pg.Artifacts[i].Subtype)
        //        {
        //            ltToDel.Add(i);
        //        }
        //    }

        //    foreach (int i in ltToDel)
        //    {
        //        pg.Artifacts.Delete(i);
        //    }


        //}


    }
}
