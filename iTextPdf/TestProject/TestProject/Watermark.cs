using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using System;
using static iTextSharp.text.Font;

namespace TestProject
{
    public class Watermark
    {
        const string DefaultWatermark = "This watermark is added UNDER the existing content";

        private static void AddWaterMarkText(PdfContentByte pdfData, string watermarkText, BaseFont font, float fontSize, float angle, BaseColor color, Rectangle realPageSize)
        {
            var gstate = new PdfGState { FillOpacity = 0.35f, StrokeOpacity = 0.3f };
            pdfData.SaveState();
            pdfData.SetGState(gstate);
            pdfData.SetColorFill(color);
            pdfData.BeginText();
            pdfData.SetFontAndSize(font, fontSize);
            var x = (realPageSize.Right + realPageSize.Left) / 2;
            var y = (realPageSize.Bottom + realPageSize.Top) / 2;
            pdfData.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, angle);
            pdfData.EndText();
            pdfData.RestoreState();
        }

        public static byte[] AddWatermark(byte[] bytes, BaseFont baseFont, string watermarkText)
        {
            using (var ms = new MemoryStream(10 * 1024))
            {
                using (var reader = new PdfReader(bytes))
                using (var stamper = new PdfStamper(reader, ms))
                {
                    var pages = reader.NumberOfPages;
                    for (var i = 1; i <= pages; i++)
                    {
                        var dc = stamper.GetOverContent(i);
                        AddWaterMarkText(dc, watermarkText, baseFont, 50, 45, BaseColor.GRAY, reader.GetPageSizeWithRotation(i));
                    }
                    stamper.Close();
                }
                return ms.ToArray();
            }
        }

        public static void manipulatePdf(String src, String dest, string Text = DefaultWatermark)
        {
            using (var ms = new MemoryStream(10 * 1024))
            {
                using (var reader = new PdfReader(src))
                using (var stamper = new PdfStamper(reader, ms))
                {
                    PdfContentByte under = stamper.GetUnderContent(1);
                    Font f = new Font(FontFamily.HELVETICA, 45);
                    Phrase p = new Phrase(Text, f);
                    var gstate = new PdfGState { FillOpacity = 0.35f, StrokeOpacity = 0.3f };
                    under.SaveState();
                    under.SetGState(gstate);
                    under.SetColorFill(BaseColor.GRAY);
                    Rectangle realPageSize = reader.GetPageSizeWithRotation(1);
                    var x = (realPageSize.Right + realPageSize.Left) / 2;
                    var y = (realPageSize.Bottom + realPageSize.Top) / 2;
                    ColumnText.ShowTextAligned(under, Element.ALIGN_CENTER, p, x, y, 45);
                    under.RestoreState();
                    stamper.Close();
                    reader.Close();
                }
                File.WriteAllBytes(dest, ms.ToArray());
            }
        }
    }
}
