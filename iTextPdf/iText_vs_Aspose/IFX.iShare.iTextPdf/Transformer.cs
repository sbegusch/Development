using System;
using System.Text;
using System.IO;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace IFX.iShare.iTextPdf
{
    public class Transformer
    {
        /*
        private void RemoveExistingWatermarks(string PdfDocument)
        {
            PdfReader reader2 = new PdfReader(PdfDocument);

            //NOTE, This will destroy all layers in the document, only use if you don't have additional layers
            //Remove the OCG group completely from the document.
            //reader2.Catalog.Remove(PdfName.OCPROPERTIES);

            //Clean up the reader, optional
            reader2.RemoveUnusedObjects();

            //Placeholder variables
            PRStream stream;
            String content;
            PdfDictionary page;
            PdfArray contentarray;

            //Get the page count
            int pageCount2 = reader2.NumberOfPages;
            //Loop through each page
            for (int i = 1; i <= pageCount2; i++)
            {
                //Get the page
                page = reader2.GetPageN(i);
                //Get the raw content
                contentarray = page.GetAsArray(PdfName.CONTENTS);
                if (contentarray != null)
                {
                    //Loop through content
                    for (int j = 0; j < contentarray.Size; j++)
                    {
                        //Get the raw byte stream
                        stream = (PRStream)contentarray.GetAsStream(j);
                        //Convert to a string. NOTE, you might need a different encoding here
                        content = System.Text.Encoding.ASCII.GetString(PdfReader.GetStreamBytes(stream));
                        //Look for the OCG token in the stream as well as our watermarked text
                        if (content.IndexOf("/OC") >= 0 && content.IndexOf(watermarkText) >= 0)
                        {
                            //Remove it by giving it zero length and zero data
                            stream.Put(PdfName.LENGTH, new PdfNumber(0));
                            stream.SetData(new byte[0]);
                        }
                    }
                }
            }

            //Write the content out
            using (FileStream fs = new FileStream(unwatermarkedFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (PdfStamper stamper = new PdfStamper(reader2, fs))
                {

                }
            }
        }*/

        private void AddWaterMarkText(PdfContentByte pdfData, string watermarkText, float fontSize, float angle, Rectangle realPageSize)
        {
            iTextSharp.text.pdf.BaseFont baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, false);
            BaseColor color = BaseColor.GRAY;

            var gstate = new PdfGState { FillOpacity = 0.35f, StrokeOpacity = 0.3f };
            pdfData.SaveState();
            pdfData.SetGState(gstate);
            pdfData.SetColorFill(color);
            
            pdfData.BeginText();
            pdfData.SetFontAndSize(baseFont, fontSize);
            var x = (realPageSize.Right + realPageSize.Left) / 2;
            var y = (realPageSize.Bottom + realPageSize.Top) / 2;
            pdfData.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, angle);
            
            pdfData.EndText();
            
            pdfData.RestoreState();
        }

        private void AddMultiLineWaterMarkText(PdfContentByte pdfData, DateTime dtCurrentDate, float fontSize, float angle, Rectangle realPageSize)
        {
            iTextSharp.text.pdf.BaseFont baseFont;
            BaseColor color = BaseColor.RED;
            var x = (realPageSize.Right + realPageSize.Left) / 3;
            var y = (realPageSize.Bottom + realPageSize.Top) / 2;
            string sTextDate = dtCurrentDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            var gstate = new PdfGState { FillOpacity = 0.35f, StrokeOpacity = 0.3f };
            pdfData.SaveState();
            pdfData.SetGState(gstate);
            pdfData.SetColorFill(color);
            
            pdfData.BeginText();
            baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, iTextSharp.text.pdf.BaseFont.WINANSI, false);
            //adding some lines to the left
            ColumnText.ShowTextAligned(pdfData, Element.ALIGN_CENTER,
                new Phrase("Confidential", new Font(baseFont, fontSize)),
                x, y, angle);
            baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.WINANSI, false);
            ColumnText.ShowTextAligned(pdfData, Element.ALIGN_CENTER,
                new Phrase("Downloaded by", new Font(baseFont, fontSize)),
                x + 70, y - 10, angle);
            pdfData.EndText();
            ColumnText.ShowTextAligned(pdfData, Element.ALIGN_CENTER,
                new Phrase("beguschs", new Font(baseFont, fontSize)),
                x + 140, y - 20, angle);
            ColumnText.ShowTextAligned(pdfData, Element.ALIGN_CENTER,
                new Phrase(sTextDate, new Font(baseFont, fontSize)),
                x + 210, y - 30, angle);

            pdfData.EndText();

            pdfData.RestoreState();
        }
        public MemoryStream WatermarkFile(MemoryStream s, string sUserName, DateTime dtCurrentDate, string sCompany)
        {
            using (var ms = new MemoryStream(10 * 1024))
            {
                using (var reader = new PdfReader(s.ToArray()))
                using (var stamper = new PdfStamper(reader, ms))
                {
                    
                    #region SINGLE LINE TEXT
                    
                    var pages = reader.NumberOfPages;
                    for (var i = 1; i <= pages; i++)
                    {
                        var dc = stamper.GetOverContent(i);
                        AddWaterMarkText(dc, "Entwurf", 50, 45, reader.GetPageSizeWithRotation(i));
                    }
                    
                    #endregion
               
                    #region  MULTILINE TEXT
                    /*
                    var pages = reader.NumberOfPages;
                    for (var i = 1; i <= pages; i++)
                    {
                        var dc = stamper.GetOverContent(i);
                        AddMultiLineWaterMarkText(dc, dtCurrentDate, 50, 45, reader.GetPageSizeWithRotation(i));
                    }
                    */
                    #endregion

                    stamper.Close();
                }
                return ms;
            }
        }
    }
}
