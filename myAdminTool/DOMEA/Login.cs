using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CCDGUI;
using WFLOBJ;

namespace DOMEA
{
    public class Login
    {
        private SCBWflSession LoginSession { get; set; }
        public void DoLogin()
        {
            Util.WriteMethodInfoToConsole();
            ccdLogin Login = new ccdLogin();
            if (Login.DoLogin())
            {
                Console.WriteLine("--> Connected to DOMEA Server: " + Login.Session.HostName);
                LoginSession = Login.Session;
            }
        }

        public int LoginSessionWorkItemCount 
        { 
            get
            {
                if (LoginSession == null || !LoginSession.IsConnected || !LoginSession.IsLoggedOn) { return -1; }
                return LoginSession.WorkList.GetWorkItems().Count;
            }
        }
    }
}
