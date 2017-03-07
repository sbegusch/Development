// -----------------------------------------------------------------------
// <copyright file="PrepareFileThread.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SampleClientServerCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public delegate void DelWatermarkedFile (MemoryStream msFile);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PrepareFileThread
    {
        private MemoryStream _msFile;
        private DelWatermarkedFile _delWatermarkedFile;
        private string _sUsername;
        private string _sCompany;

        public PrepareFileThread(MemoryStream msFile, DelWatermarkedFile delWatermarkedFile, string sUsername, string sCompany)
        {
            _msFile = msFile;
            _delWatermarkedFile = delWatermarkedFile;
            _sUsername = sUsername;
            _sCompany = sCompany;
        }

        public void Start()
        {
            //IFX.iShare.PdfProcessing.Transformer transform = new IFX.iShare.PdfProcessing.Transformer();
            //_delWatermarkedFile(transform.WatermarkFile(_msFile, _sUsername, DateTime.Now, _sCompany));

            // * * * TEST * * * 
            System.Threading.Thread.Sleep(15000);
            _delWatermarkedFile(_msFile);
        }
    }
}
