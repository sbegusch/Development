using System;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

namespace SampleClientServerCode
{
    public partial class MainForm : System.Web.UI.Page
    {
        private static MyICPService service;
        protected void Page_Load(object sender, EventArgs e)
        {
            service = new MyICPService();
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            btnTest.Enabled = false;
            byte[] pdfFile = File.ReadAllBytes(@"C:\HelpAbtFreischalten.pdf");

            ServiceResult<int> result = service.WatermarkFile(pdfFile, "stefan", string.Empty);
            threadID = result.Data;
            lblInfo.Text = "Thread working...";
            lblThreadID.Text = "ThreadID: " + threadID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "windowclose", string.Format("self.startTimer({0});", threadID), true);
        }

        public int threadID { get; set; }

        [ScriptMethod, WebMethod]
        public static bool isThreadFinished(int threadID)
        {
            if (service != null)
            {
                ServiceResult<bool> result = service.PreperationFinished(threadID);
                return result.Data;
            }
            else
            {
                return true;
            }
        }

        [ScriptMethod, WebMethod]
        public static string GetFile()
        {
            string outputFile = @"C:\outputFileFromService.pdf";
            if (service != null)
            {
                ServiceResult<MemoryStream> result = service.GetFile();
                using (FileStream file = new FileStream(outputFile, FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] bytes = new byte[result.Data.Length];
                    result.Data.Read(bytes, 0, (int)result.Data.Length);
                    file.Write(bytes, 0, bytes.Length);
                    result.Data.Close();
                }
            }
            return outputFile;
        }
    }
}