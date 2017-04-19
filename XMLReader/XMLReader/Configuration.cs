using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLReader
{
    public class ConfigurationReader
    {
        public static string ConfigFile
        {
            get
            {
                return string.Format("{0}Configuration.xml", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public static Configuration Read()
        {
            Configuration conf = new Configuration();
            XmlTextReader reader = new XmlTextReader(ConfigFile);

            string[] Attributes = null;
            string[] Value = null;
            bool isListContent = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.AttributeCount > 0)
                        {
                            Attributes = new string[reader.AttributeCount];
                            Value = new string[reader.AttributeCount];

                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                Attributes[i] = reader.Name;
                                Value[i] = reader.Value;
                            }
                            if (reader.AttributeCount > 0)
                            {
                                for (int i = 0; i < Attributes.Length; i++)
                                {
                                    if (Attributes[i].ToUpper() == "NAME")
                                    { //ModifyListContent/Sharepoint/List/Name
                                        conf.SharepointLists.Add(new SharepointList(Value[i]));
                                        isListContent = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (reader.Name.ToUpper() == "URL")
                            { //ModifyListContent/Sharepoint/General/Url
                                conf.Urls.Add(reader.ReadString());
                                isListContent = false;
                            }
                            else if (reader.Name.ToUpper() == "COLNAME")
                            { //ModifyListContent/Sharepoint/List/Columns
                                conf.SharepointLists.Last<SharepointList>().Columns.Add(reader.ReadString());
                                isListContent = false;
                            }
                            else if (reader.Name.ToUpper() == "CONTENTS")
                            { //ModifyListContent/Sharepoint/List/Content
                                isListContent = true;
                            }
                            else
                            {
                                if (isListContent)
                                {
                                    foreach (string col in conf.SharepointLists.Last<SharepointList>().Columns)
                                    {
                                        if (reader.Name.ToUpper() == col.ToUpper().Replace(" ", "_"))
                                        {
                                            conf.SharepointLists.Last<SharepointList>().Content.Add(new KeyValuePair<string, string>(col, reader.ReadString()));
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            reader.Close();
            //**************************************************
            return conf;
        }
    }

    public class Configuration
    {
        public Configuration()
        {
            Urls = new List<string>();
            SharepointLists = new List<SharepointList>();
        }
        public List<string> Urls { get; set; }

        public List<SharepointList> SharepointLists { get; set; }
    }

    public class SharepointList
    {
        public SharepointList()
        {
            Columns = new List<string>();
            Content = new List<KeyValuePair<string, string>>();
        }
        public SharepointList(string name)
        {
            Columns = new List<string>();
            Content = new List<KeyValuePair<string, string>>();
            this.Name = name;
        }
        public string Name { get; set; }
        public List<string> Columns { get; set; }
        public List<KeyValuePair<string, string>> Content { get; set; }
    }
}
