using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace moveToFolder
{
    public class OracleHelper
    {
        private string connString { get; set; }
        private OracleConnection Connection { get; set; }

        public OracleHelper(string ConnectionString)
        {
            this.connString = ConnectionString;
        }
        public void OpenConnection()
        {
            try
            {
                Connection = new OracleConnection(this.connString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string stmt = "select distinct g.igz, g.gz, f.wgnr, f.wgname, f.PFAD_GESAMT, f.folderbez, f.FOLDERNR " +
                                "from BIG_FOLDER_RESTORE_TMP f, gst g " +
                                "where f.foldernr >= 0 " +
                                  "and g.gz = f.gz " +
                                  "and f.wgnr = f.usernr " +
                                "order by f.pfad_gesamt";

                using (OracleCommand cmd = new OracleCommand(stmt, Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
