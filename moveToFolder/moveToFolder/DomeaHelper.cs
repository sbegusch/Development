using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLOBJ;

namespace moveToFolder
{
    public static class DomeaConfiguration
    {
        public static string GetOracleConnectionString(SCBWflSession sysSession)
        {
            SCBWflRows rows = sysSession.System.SQLCommand("select value from BIG_CONNECTION_STRING");
            foreach(SCBWflRow row in rows)
            {
                return row.GetColumns().Item(1).ToString();
            }
            return "";
        }
    }
    public class DomeaHelper
    {

    }
}
