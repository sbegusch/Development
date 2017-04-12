using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using myAdminTool.Classes;
using System.Data;

namespace myAdminTool
{
    public class OracleHelper
    {
        public static bool IsConnected { get; set; }
        public static string ConnectionInfo { get; set; }

        private static OracleConnection Connection { get; set; }
        public static void ConnectionOpen(string ConnectionString)
        {
            Util.WriteMethodInfoToConsole();
            Connection = new OracleConnection(ConnectionString);
            Connection.Open();
            IsConnected = true;
        }
        public static void ConnectionClose()
        {
            Util.WriteMethodInfoToConsole();
            if (Connection != null)
            {
                try
                {
                    Connection.Close();
                    IsConnected = false;
                    ConnectionInfo = "";
                }
                catch { }
            }
        }

        public static List<SST> SearchDocument(string Text)
        {
            Util.WriteMethodInfoToConsole();
            List<SST> retValue = new List<SST>();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = string.Format("select einlaufzahl, elz, anmerkungen from sst where elz like '{0}'", Text);
            OracleDataReader reader = cmd.ExecuteReader();
            SST sst; 
            while (reader.Read())
            {
                sst = new SST();
                sst.Einlaufzahl = Convert.ToInt32(reader["einlaufzahl"]);
                sst.ELZ = reader["elz"].ToString();
                sst.Subject = reader["anmerkungen"].ToString();
                retValue.Add(sst);
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            return retValue;
        }

        public static List<GBF> SearchGBF(int Einlaufzahl)
        {
            Util.WriteMethodInfoToConsole();
            List<GBF> retValue = new List<GBF>();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = string.Format("select igz, adressid, einlaufzahl, bezeichnung, nameteil1, nummer, jahr from fb_gebahrungsfaelle where einlaufzahl = {0}", Einlaufzahl);
            OracleDataReader reader = cmd.ExecuteReader();
            GBF gbf;
            string addrID = "";
            while (reader.Read())
            {
                gbf = new GBF();
                gbf.IGZ = Convert.ToInt32(reader["igz"]);
                addrID = reader["adressid"].ToString();
                if (addrID.Trim() != "")
                {
                    gbf.ADDRID = Convert.ToInt32(reader["adressid"]);
                }
                gbf.Einlaufzahl = Convert.ToInt32(reader["einlaufzahl"]);
                gbf.Bezeichnung = reader["bezeichnung"].ToString();
                gbf.NameTeil1 = reader["nameteil1"].ToString();
                gbf.Nummer = Convert.ToInt32(reader["nummer"]);
                gbf.Jahr = Convert.ToInt32(reader["jahr"]);
                
                retValue.Add(gbf);
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            return retValue;
        }

        public static List<ADDRESS> SearchAddress(int Einlaufzahl)
        {
            Util.WriteMethodInfoToConsole();
            List<ADDRESS> retValue = new List<ADDRESS>();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = string.Format("select einlaufzahl, addrid, firstname, lastname, company1, company2 from dom_address_sst where einlaufzahl = {0}", Einlaufzahl);
            OracleDataReader reader = cmd.ExecuteReader();
            ADDRESS addr;
            while (reader.Read())
            {
                addr = new ADDRESS();
                addr.ADDRID = Convert.ToInt32(reader["addrid"]);
                addr.Einlaufzahl = Convert.ToInt32(reader["einlaufzahl"]);
                addr.Vorname = reader["firstname"].ToString();
                addr.Nachname = reader["lastname"].ToString();
                addr.Firma1 = reader["company1"].ToString();
                addr.Firma2 = reader["company2"].ToString();
                retValue.Add(addr);
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            return retValue;
        }

        public static bool UpdateAddressID(int Einlaufzahl, int Jahr, int Nummer, string AddressID)
        {
            Util.WriteMethodInfoToConsole();
            OracleCommand cmd = Connection.CreateCommand();
            try
            {
                cmd.CommandText = string.Format("update fb_gebahrungsfaelle set adressid = '{0}' where einlaufzahl = {1} and nummer = {2} and jahr = {3}", AddressID, Einlaufzahl, Nummer, Jahr);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Error.Show(ex);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        public static List<DBObject> GetAllTables()
        {
            Util.WriteMethodInfoToConsole();
            List<DBObject> allTables = new List<DBObject>();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "select TABLE_NAME, TABLE_TYPE from cat";
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allTables.Add(new DBObject(reader["TABLE_NAME"].ToString(), reader["TABLE_TYPE"].ToString()));
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            return allTables;
        }

        public static DataTable GetValuesFromTable(string TableName)
        {
            Util.WriteMethodInfoToConsole();
            DataTable dt = new DataTable();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = string.Format("select * from {0}", TableName);
            
            OracleDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            return dt;
        }
    }

    public class DBObject
    {
        public DBObject()
        {}
        public DBObject(string _Name, string _Type)
        {
            this.Name = _Name;
            this.Type = _Type;
        }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class SST
    {
        public int Einlaufzahl { get; set; }
        public string ELZ { get; set; }
        public string Subject { get; set; }
    }

    public class GBF
    {
        public int IGZ { get; set; }
        public int ADDRID { get; set; }
        public int Einlaufzahl { get; set; }
        public string Bezeichnung { get; set; }
        public string NameTeil1 { get; set; }
        public int Jahr { get; set; }
        public int Nummer { get; set; }
    }

    public class ADDRESS
    {
        public int ADDRID { get; set; }
        public int Einlaufzahl { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Firma1 { get; set; }
        public string Firma2 { get; set; }
    }
    public class TnsNamesReader
    {
        private string _tnsNamesFile;
        private string _tnsNamesFileContent;

        public List<KeyValuePair<string, string>> Properties { get; private set; }

        public TnsNamesReader(string tnsNamesFile = null)
        {
            Util.WriteMethodInfoToConsole();
            Properties = new List<KeyValuePair<string, string>>();
            _tnsNamesFile = tnsNamesFile;
        }

        public void Read()
        {
            Util.WriteMethodInfoToConsole();
            if (string.IsNullOrWhiteSpace(_tnsNamesFile))
                _tnsNamesFile = FindTnsNamesFile();

            _tnsNamesFileContent = ReadAllText(_tnsNamesFile);

            Extract();
        }

        private void Extract()
        {
            Util.WriteMethodInfoToConsole();
            Properties.Clear();

            var tnsNamesEntries = RetrieveTNSNamesEntries();
            var tnsHostEntries = RetrieveTNSHostEntries();

            try
            {
                if (tnsNamesEntries.Count != tnsHostEntries.Count)
                    throw new Exception(string.Format("Found {0} tns names for {1} tns hosts on tnsnames.ora file.",
                                                      tnsNamesEntries.Count,
                                                      tnsHostEntries.Count));
            }
            catch { }

            try
            {
                for (var i = 0; i < tnsNamesEntries.Count; i++)
                    Properties.Add(new KeyValuePair<string, string>(tnsNamesEntries[i], tnsHostEntries[i]));
            }
            catch { }
        }

        private List<string> RetrieveTNSHostEntries()
        {
            Util.WriteMethodInfoToConsole();
            //(?<=\(HOST = ).*(?=\)\()
            var tnsHostEntryRegex = new Regex("(?<=\\(HOST = ).*(?=\\)\\()", RegexOptions.Compiled);
            var tnsHostEntryMatches = tnsHostEntryRegex.Matches(_tnsNamesFileContent);
            var tnsHostEntries = (from Match match in tnsHostEntryMatches select match.ToString().Trim()).ToList();

            return tnsHostEntries;
        }

        private List<string> RetrieveTNSNamesEntries()
        {
            Util.WriteMethodInfoToConsole();
            var tnsNamesEntryRegex = new Regex(@"[\n][\s]*[^\(][a-zA-Z0-9_.]+[\s]*", RegexOptions.Compiled);
            var tnsNameEntryMatches = tnsNamesEntryRegex.Matches(_tnsNamesFileContent);
            var tnsNamesEntries = (from Match match in tnsNameEntryMatches select match.ToString().Trim()).ToList();

            return tnsNamesEntries;
        }

        private static string ReadAllText(string fileName)
        {
            Util.WriteMethodInfoToConsole();
            var sb = new StringBuilder();
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (!line.StartsWith("#"))
                        sb.AppendLine(line);
                    line = reader.ReadLine();
                }

                reader.Close();
            }

            return sb.ToString();
        }

        private static string FindTnsNamesFile()
        {
            Util.WriteMethodInfoToConsole();
            const string TNS_ADMIN = "TNS_ADMIN";
            const string SUB_PATH = @"network\ADMIN\tnsnames.ora";

            var environmentPathRegex =
                new Regex(@"[a-zA-Z]:\\[a-zA-Z0-9\\]*(oracle|app)[a-zA-Z0-9_.\\]*(?=bin)",
                          RegexOptions.Compiled);

            var path = Environment.GetEnvironmentVariable("Path");
            if (path != null)
            {
                var matches = environmentPathRegex.Matches(path);

                foreach (Match match in matches)
                {
                    if (File.Exists(Path.Combine(match.ToString(), SUB_PATH)))
                        return Path.Combine(match.ToString(), SUB_PATH);
                }
            }
            var environmentVariable = Environment.GetEnvironmentVariable(TNS_ADMIN);
            if (!string.IsNullOrEmpty(environmentVariable))
            {
                return Path.Combine(environmentVariable, "tnsnames.ora");
            }
            throw new FileNotFoundException("File not found.", "tnsnames.ora");
        }
    }
}