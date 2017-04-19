// -----------------------------------------------------------------------
// <copyright file="PDFTools.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace IFX.iShare.PdfProcessing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using Aspose.Pdf.Facades;
    using Aspose.Pdf;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class PDFTools
    {
        public static void ApplyLicense()
        {
            Aspose.Pdf.License l2 = new Aspose.Pdf.License();
            l2.SetLicense("ASPOSE-DE-CCO-121128--140416092055-DE-SCO-157407.txt");
            l2.Embedded = true;
        }

        public static bool isFileProtected(MemoryStream file)
        {
            ApplyLicense();
            PdfFileInfo info = new PdfFileInfo(file);

            return info.IsEncrypted || info.HasOpenPassword || info.HasEditPassword;
        }

        public static bool hasFileWatermarks(MemoryStream file)
        {
            ApplyLicense();
            bool ret = false;

            Aspose.Pdf.Document doc = new Aspose.Pdf.Document(file);

            XImageCollection col = doc.Pages[1].Resources.Images;

            if (col.Count != 0)
                ret = true;

            return ret;
        }

    }
}
