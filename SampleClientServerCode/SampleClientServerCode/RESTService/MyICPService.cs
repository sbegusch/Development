using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Threading;
using System.IO;

namespace SampleClientServerCode
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MyICPService : IMyICPService
    {
        private List<Thread> ThreadList { get; set; }
        private MemoryStream resMemoryStream { get; set; }

        public void delReturnFile(MemoryStream msFile)
        {
            resMemoryStream = msFile;
        }

        public ServiceResult<int> WatermarkFile(byte[] msFile, string sUsername, string sCompany)
        {
            var result = new ServiceResult<int>() { Success = true };

            try
            {
                DelWatermarkedFile delWatermarkedFile = new DelWatermarkedFile(delReturnFile);
                PrepareFileThread pft = new PrepareFileThread(new MemoryStream(msFile), delWatermarkedFile, sUsername, sCompany);

                Thread thread = new Thread(new ThreadStart(pft.Start));
                thread.Start();
                
                if (ThreadList == null) { ThreadList = new List<Thread>(); }
                result.Data = thread.GetHashCode();
                ThreadList.Add(thread);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public ServiceResult<bool> PreperationFinished(int threadID)
        {
            var result = new ServiceResult<bool>() { Success = true };

            try
            {
                Thread thread = ThreadList.Find(t => t.GetHashCode() == threadID);

                if (thread != null)
                {
                    result.Data = !thread.IsAlive;
                }
                else
                {
                    throw new Exception("Thread with ID " + threadID + " not found!");
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        
        public ServiceResult<MemoryStream> GetFile()
        {
            var result = new ServiceResult<MemoryStream>() { Success = true };

            try
            {
                result.Data = resMemoryStream;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
