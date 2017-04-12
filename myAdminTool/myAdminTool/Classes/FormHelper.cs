using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace myAdminTool.Classes
{
    public static class FormHelper
    {

        public static ButtonItem LastCheckedButton { get; set; }

        public static void Click(SideBarPanelItem sideBar, 
            SuperTabControl superTabControl, ButtonItem ClickedButton, SuperTabItem TabItem)
        { 
            Util.WriteMethodInfoToConsole();
            try
            {
                HConsole.WriteLine(LastCheckedButton.Text + " (" + LastCheckedButton.Name + ") --> unchecked");
                LastCheckedButton.Checked = false;
            }
            catch { }
            foreach (ButtonItem bi in sideBar.SubItems)
            {
                if (bi.Name == ClickedButton.Name)
                {
                    bi.Checked = true;
                    HConsole.WriteLine(bi.Text + " (" + bi.Name + ") --> is now LastCheckedButton");
                    LastCheckedButton = bi;
                }
                else
                {
                    bi.Checked = false;
                }
            }
            //System.Windows.Forms.Form MainForm = System.Windows.Forms.Application.OpenForms.Cast<System.Windows.Forms.Form>().Where(x => x.Name == "frmMain").FirstOrDefault();
            //var c = GetAllControlsOfType(MainForm, typeof(SideBar));
            //Console.WriteLine("Total ButtonItem Controls: " + c.Count());
            if (TabItem != null)
            {
                foreach (SuperTabItem sti in superTabControl.Tabs)
                {
                    if (sti.Name == TabItem.Name)
                    {
                        sti.Visible = true;
                        sti.Text = ClickedButton.Text;
                    }
                    else
                    {
                        sti.Visible = false;
                    }
                }
                superTabControl.Visible = true;
            }
            else
            {
                superTabControl.Visible = false;
            }
            
            //if (!superTabControl.Visible)
            //{
            //    superTabControl.Visible = true;
            //}
        }

        private static IEnumerable<System.Windows.Forms.Control> GetAllControlsOfType(System.Windows.Forms.Control control, Type type)
        { 
            Util.WriteMethodInfoToConsole();
            var controls = control.Controls.Cast<System.Windows.Forms.Control>();

            return controls.SelectMany(ctrl => GetAllControlsOfType(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
    }

    public static class BarManager
    {
        public static string xmlFile { get { return string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, Util.GetAssemblyName); } }

        public static void WriteBarSettingsToXML(DevComponents.DotNetBar.DotNetBarManager dotNetBarMainManager)
        { 
            Util.WriteMethodInfoToConsole();

            if (File.Exists(xmlFile))
            {
                File.Delete(xmlFile);
            }
            // Create a new file
            XmlTextWriter textWriter = new XmlTextWriter(xmlFile, null);
            // Opens the document
            textWriter.WriteStartDocument();
            // Write comments
            //textWriter.WriteComment("First Comment XmlTextWriter Sample Example");
            //textWriter.WriteComment("myXmlFile.xml in root dir");
            //Write the root element
            textWriter.WriteStartElement("BarControls");
           
            foreach (DevComponents.DotNetBar.Bar bar in dotNetBarMainManager.Bars)
            {
                HConsole.WriteLine(bar.Text + "(" + bar.Name + "): " + " DOCKED=" + bar.Docked + " POSITION=" + bar.DockedSite.Dock +
                                                                      " STATE:" + bar.BarState + " AUTOHIDESIDE:" + bar.AutoHideSide + " VISIBLE:" + bar.Visible.ToString());

                if (bar.Name.ToUpper().StartsWith("BAR"))
                {
                    #region V1
                    //textWriter.WriteStartElement("Name", bar.Name);
                    //textWriter.WriteStartElement("Docked"); textWriter.WriteString(bar.Docked.ToString()); textWriter.WriteEndElement();
                    //textWriter.WriteStartElement("Position"); textWriter.WriteString(bar.DockedSite.Dock.ToString()); textWriter.WriteEndElement();
                    //textWriter.WriteStartElement("State"); textWriter.WriteString(bar.BarState.ToString()); textWriter.WriteEndElement();
                    //textWriter.WriteStartElement("AutoHide"); textWriter.WriteString(bar.AutoHideSide.ToString()); textWriter.WriteEndElement();
                    //textWriter.WriteStartElement("Visible"); textWriter.WriteString(bar.Visible.ToString()); textWriter.WriteEndElement();
                    //textWriter.WriteEndElement();
                    #endregion
                    
                    #region V2
                    //textWriter.WriteStartElement("DotNetBar");
                    
                    //textWriter.WriteElementString("Name", bar.Name);
                    //textWriter.WriteElementString("Docked", bar.Docked.ToString());
                    //textWriter.WriteElementString("Position", bar.DockedSite.Dock.ToString());
                    //textWriter.WriteElementString("State", bar.BarState.ToString());
                    //textWriter.WriteElementString("AutoHide", bar.AutoHideSide.ToString());
                    //textWriter.WriteElementString("Visible", bar.Visible.ToString());

                    //textWriter.WriteEndElement();
                    #endregion

                    #region V3 (funktioniert)
                    textWriter.WriteStartElement("DotNetBar");

                    textWriter.WriteAttributeString("Name", bar.Name);
                    textWriter.WriteAttributeString("Docked", bar.Docked.ToString());
                    textWriter.WriteAttributeString("Position", bar.DockedSite.Dock.ToString());
                    textWriter.WriteAttributeString("State", bar.BarState.ToString());
                    textWriter.WriteAttributeString("AutoHide", bar.AutoHideSide.ToString());
                    textWriter.WriteAttributeString("Visible", bar.Visible.ToString());

                    textWriter.WriteEndElement();
                    #endregion
                }
               
            }

            // End the root element
            textWriter.WriteEndElement();
            // Ends the document.
            textWriter.WriteEndDocument();

            // close writer
            textWriter.Close();
        }

        public static List<DotNetBarSettings> ReadBarSettingFromXML()
        {
            Util.WriteMethodInfoToConsole();
            List<DotNetBarSettings> retValue = null;
            if (File.Exists(xmlFile))
            {
                retValue = new List<DotNetBarSettings>();
                DotNetBarSettings setting;
                XmlTextReader textReader = new XmlTextReader(xmlFile);
                textReader.Read();
                // If the node has value
                while (textReader.Read())
                {
                    if (textReader.Name.ToUpper().StartsWith("DOTNETBAR"))
                    {
                        if (textReader.HasAttributes)
                        {
                            setting = new DotNetBarSettings();
                            while (textReader.MoveToNextAttribute())
                            {
                                HConsole.WriteLine(string.Format("{0} --> {1}", textReader.Name, textReader.Value));
                                switch (textReader.Name)
                                {
                                    case "Name":
                                        setting.Name = textReader.Value;
                                        break;
                                    case "Docked":
                                        setting.Docked = textReader.Value;
                                        break;
                                    case "Position":
                                        setting.Position = textReader.Value;
                                        break;
                                    case "State":
                                        setting.State = textReader.Value;
                                        break;
                                    case "AutoHide":
                                        setting.AutoHide = textReader.Value;
                                        break;
                                    case "Visible":
                                        setting.Visible = textReader.Value;
                                        break;
                                }
                            }
                            retValue.Add(setting);
                        }
                    }
                }
            }
            return retValue;
        }
    }

    public class DotNetBarSettings
    {
        public string Name { get; set; }
        public string Docked { get; set; }
        public string Position { get; set; }
        public string State { get; set; }
        public string AutoHide { get; set; }
        public string Visible { get; set; }
    }
}
