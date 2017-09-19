using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myAdminTool.Classes;
using System.IO;

using System.Data.SqlClient;

namespace myAdminTool
{
    public partial class frmMain : Form
    {
        #region Public Member / Properties (findMember)
        private const String CONTAINS = "Contains";
        private const String STARTS_WITH = "Starts With";
        private const String ENDS_WITH = "Ends With";
        private const String SOUNDS_LIKE = "Sounds Like";

        private const String USER_LOGIN = "Login";
        private const String USER_FIRSTNAME = "First Name";
        private const String USER_LASTNAME = "Last Name";
        private const String USER_EMAIL = "Email";
        private const String GROUP_NAME = "Group Name";

        private const int PAGE_SIZE = 100;

        List<OTCSMemberService.Member> fMembers;
        OTCS.CWSClient fCWSClient;
        public static int category = -999;
        #endregion

        #region Private Member / Propterties
        private List<DevComponents.DotNetBar.Bar> AllBars { get; set; }
        private SystemImplementation.DOMEA.DOMEA session;

        private SystemImplementation.DOMEA.frmUploadToOTCS frmUpload;

        private SqlConnection connSQLServer;
        
        private DataTable dataTable;
        private int rowCount;
        #endregion

        #region General Form Events

        public frmMain()
        {
            Util.WriteMethodInfoToConsole();
            InitializeComponent();

            #region Create MenuItems
            AddSubMenuItems();
            AddSubMenuItemsVisibleBars();
            #endregion
            lblStatusInfoOracle.Text = "";
            lblStatusInfoContentServer.Text = "";
            lblStatusInfoDOMEA.Text = "";
            lblStatusInfoSQLServer.Text = "";
            labelGeneralInfo.Text = "";

            this.Text = string.Format("{0} [{1}]", this.Text, Util.GetAssemblyVersion);
            Console.Title = this.Text;
            
            #region XML File lesen und Bars anhand des Files ausblenden
            List<DotNetBarSettings> settings = BarManager.ReadBarSettingFromXML();
            if (settings != null)
            {
                DevComponents.DotNetBar.Bar bar;
                DevComponents.DotNetBar.ButtonItem subItem;
                foreach (DotNetBarSettings setting in settings)
                {
                    bar = dotNetBarMainManager.Bars[setting.Name];
                    subItem = (DevComponents.DotNetBar.ButtonItem)mnuVisibleBars.SubItems[setting.Name];
                    if (!Convert.ToBoolean(setting.Visible))
                    {
                        subItem.Checked = false;
                        bar.Hide();
                    }
                    else
                    {
                        //todo: position / docken usw.... setzen
                    }
                }
            }
            else
            {
                #region Bars ausblenden wenn kein XML vorhanden
                foreach (DevComponents.DotNetBar.ButtonItem buttonItem in mnuVisibleBars.SubItems)
                {
                    #region ContentServer
                    if (buttonItem.Name == barOTCS.Name)
                    {
                        buttonItem.Checked = false;
                        barOTCS.Hide();
                    }
                    #endregion
                    #region DOMEA
                    if (buttonItem.Name == barDOMEA.Name)
                    {
                        buttonItem.Checked = false;
                        barDOMEA.Hide();
                    }
                    #endregion
                    #region ORACLE
                    if (buttonItem.Name == barOracle.Name)
                    {
                        buttonItem.Checked = false;
                        barOracle.Hide();
                    }
                    #endregion
                    #region SQLServer
                    if (buttonItem.Name == barSQLServer.Name)
                    {
                        buttonItem.Checked = false;
                        barSQLServer.Hide();
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region ContentServerBar einklappen
            //barOTCS.AutoHide = true;
            #endregion

            //contextMenuFilter.ImageList = imgListEditTable;
            //contextMenuFilter.Items["txtFilter"].ImageIndex = 2;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            //Write Positions to XML
            BarManager.WriteBarSettingsToXML(dotNetBarMainManager);
        }

        private void dotNetBarMainManager_BarStateChanged(object sender, DevComponents.DotNetBar.BarStateChangedEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            HConsole.WriteLine(e.Bar.Text + "(" + e.Bar.Name + ") -- > " + e.Bar.BarState);
        }

        private void mnuShowConsole_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            mnuShowConsole.Checked = ConsoleHelper.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            //StringBuilder sb = new StringBuilder();
            //sb.Append("Autor: Stefan Begusch" + Environment.NewLine);
            //sb.Append("Datum: 2016-04-19" + Environment.NewLine);
            //sb.Append("Beschreibung: Template für einen Windows Client");
            //MessageBox.Show(sb.ToString(), string.Format("Über {0}", this.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Forms.frmAboutBox frmAbout = new Forms.frmAboutBox();
            frmAbout.ShowDialog();
        }


        //private void LoadAllBars()
        //{
        //    Util.WriteMethodInfoToConsole();
        //    AllBars = new List<DevComponents.DotNetBar.Bar>();
        //    DevComponents.DotNetBar.Bar newBar;
        //    foreach(DevComponents.DotNetBar.Bar bar in dotNetBarMainManager.Bars)
        //    {
        //        newBar = bar;
        //        AllBars.Add(newBar);
        //    }
        //}

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            this.Close();
        }

        #endregion

        #region Form Helper

        private void AddSubMenuItems()
        {
            Util.WriteMethodInfoToConsole();
            DevComponents.DotNetBar.ButtonItem buttonItem;
            foreach (DevComponents.DotNetBar.eGrabHandleStyle val in Enum.GetValues(typeof(DevComponents.DotNetBar.eGrabHandleStyle)))
            {
                buttonItem = new DevComponents.DotNetBar.ButtonItem();
                buttonItem.Name = val.ToString();
                buttonItem.Text = val.ToString();
                if (buttonItem.Name == "Caption")
                {
                    buttonItem.Checked = true;
                }
                else
                {
                    buttonItem.Checked = false;
                }
                buttonItem.Click += new System.EventHandler(this.clickSubButtonItem);
                mnuShowBarStyle.SubItems.Add((DevComponents.DotNetBar.BaseItem)buttonItem);
                HConsole.WriteLine(val.ToString());
            }
        }

        private void clickSubButtonItem(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            foreach (DevComponents.DotNetBar.ButtonItem sub in mnuShowBarStyle.SubItems)
            {
                sub.Checked = false;
            }

            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            btn.Checked = true;
            foreach (DevComponents.DotNetBar.eGrabHandleStyle val in Enum.GetValues(typeof(DevComponents.DotNetBar.eGrabHandleStyle)))
            {
                if (val.ToString() == btn.Name)
                {
                    foreach (DevComponents.DotNetBar.Bar bar in dotNetBarMainManager.Bars)
                    {
                        if (bar != mainMenu)
                        {
                            bar.GrabHandleStyle = val;
                        }
                    }
                    break;
                }
            }
        }

        private void AddSubMenuItemsVisibleBars()
        {
            Util.WriteMethodInfoToConsole();
            DevComponents.DotNetBar.ButtonItem buttonItem;
            foreach (DevComponents.DotNetBar.Bar bar in dotNetBarMainManager.Bars)
            {
                if (!bar.MenuBar)
                {
                    buttonItem = new DevComponents.DotNetBar.ButtonItem();
                    buttonItem.Name = bar.Name;
                    buttonItem.Text = bar.Text;
                    buttonItem.Checked = true;
                    buttonItem.Click += new System.EventHandler(this.clickBarVisible);
                    mnuVisibleBars.SubItems.Add((DevComponents.DotNetBar.BaseItem)buttonItem);
                    HConsole.WriteLine("--> add ButtonItem: " + buttonItem.Name);
                }
                else
                {
                    HConsole.WriteLine("--> Bar: " + bar.Name + " wird nicht als Button hinzugefügt weil es sich um die Menübar handelt!");
                }
            }
        }

        private void clickBarVisible(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            DevComponents.DotNetBar.ButtonItem buttonItem = (DevComponents.DotNetBar.ButtonItem)sender;
            foreach (DevComponents.DotNetBar.Bar bar in dotNetBarMainManager.Bars)
            {
                if (bar.Name == buttonItem.Name)
                {
                    if (buttonItem.Checked)
                    {
                        HConsole.WriteLine("--> " + bar.Name + ".Hide()");
                        bar.Hide();
                        buttonItem.Checked = false;
                        break;
                    }
                    else
                    {
                        HConsole.WriteLine("--> " + bar.Name + ".Show()");
                        bar.Show();
                        buttonItem.Checked = true;
                        break;
                    }
                }
            }
        }

        #endregion

        #region DOMEA

        private void btnItemDOMEAShowWorkSpace_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem2,
                superTabControl1, btnItemDOMEAShowWorkSpace, tiDOMEA);
        }

        private void dgvDOMEAWorkList_SelectionChanged(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            if (dgvDOMEAWorkList.Rows.Count > 0)
            {
                try
                {
                    int IGZ = Convert.ToInt32(dgvDOMEAWorkList.SelectedRows[0].Cells["colIGZ"].Value);
                    dgvDOMEADocumentList.Rows.Clear();
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (CCD.Domea.Fw.Base.Obj.Document doc in session.GetDocuments(IGZ))
                    {
                        dgvDOMEADocumentList.Rows.Add(new string[] { "0", doc.Id.ToString(), doc.Name, doc.Comment, doc.GetTemplate().Name });
                    }
                    Cursor.Current = Cursors.Default;
                }
                catch { }
            }
        }

        private void tvDOMEAMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //List<CCD.Domea.Fw.Base.Obj.WorkItem> WorkItems = session.GetWorkList(session.GetFolderByID(Convert.ToInt32(tvDOMEAMain.SelectedNode.Name)));
            //foreach (CCD.Domea.Fw.Base.Obj.WorkItem wi in WorkItems)
            //{
            //    dgvDOMEAWorkList.Rows.Add(new string[] { "0", wi.GetProcessInstance().Id.ToString(), wi.GetProcessInstance().Name, wi.GetProcessInstance().Comment,
            //                                                 wi.GetProcessInstance().CountOfDocuments.ToString(), wi.GetProcessInstance().GetProcess().Name,
            //                                                 wi.GetProcessInstance().GetProcessClass().Name, wi.GetCurrentProcessObject().Name });
            //}
        }

        private void tvDOMEAMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            dgvDOMEAWorkList.Rows.Clear();
            if (e.Node.Parent == null)
            {//dann ist es der Arbeitskorb
                List<CCD.Domea.Fw.Base.Obj.WorkItem> WorkItems = session.GetWorkList();
                Console.WriteLine("Arbeitskorb --> WorkItems.Count: " + WorkItems.Count);
                foreach (CCD.Domea.Fw.Base.Obj.WorkItem wi in WorkItems)
                {
                    dgvDOMEAWorkList.Rows.Add(new string[] { "0", wi.GetProcessInstance().Id.ToString(), wi.GetProcessInstance().Name, wi.GetProcessInstance().Comment,
                                                             wi.GetProcessInstance().CountOfDocuments.ToString(), wi.GetProcessInstance().GetProcess().Name,
                                                             wi.GetProcessInstance().GetProcessClass().Name, wi.GetCurrentProcessObject().Name });
                }
            }
            else
            {//dann ist es ein Folder
                CCD.Domea.Fw.Base.Obj.Folder folder = session.GetFolderByID(Convert.ToInt32(e.Node.Name));
                Console.WriteLine("Folder: " + folder.Name + " --> folder.NoOfWorkItems: " + folder.NoOfWorkItems);
                List<CCD.Domea.Fw.Base.Obj.WorkItem> WorkItems = session.GetWorkList(folder);
                foreach (CCD.Domea.Fw.Base.Obj.WorkItem wi in WorkItems)
                {
                    dgvDOMEAWorkList.Rows.Add(new string[] { "0", wi.GetProcessInstance().Id.ToString(), wi.GetProcessInstance().Name, wi.GetProcessInstance().Comment,
                                                             wi.GetProcessInstance().CountOfDocuments.ToString(), wi.GetProcessInstance().GetProcess().Name,
                                                             wi.GetProcessInstance().GetProcessClass().Name, wi.GetCurrentProcessObject().Name });
                }
            }
        }

        private void btnAddProcessInstanceToOTCS_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDOMEAWorkList.Rows)
            {
                if (row.Cells["colPIChecked"].Value.ToString() == "1")
                {
                    //MessageBox.Show("yes");
                    ShowUploadForm();
                    frmUpload.AddItems(row, SystemImplementation.DOMEA.frmUploadToOTCS.ItemType.ProcessInstance);
                    row.Cells["colPIChecked"].Value = 0;
                }
            }
        }

        private void btnAddDocumentsToOTCS_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDOMEADocumentList.Rows)
            {
                if (row.Cells["colDocChecked"].Value.ToString() == "1")
                {
                    //MessageBox.Show("yes");
                    ShowUploadForm();
                    frmUpload.AddItems(row, SystemImplementation.DOMEA.frmUploadToOTCS.ItemType.Document);
                    row.Cells["colDocChecked"].Value = 0;
                }
            }
        }
        private void ShowUploadForm()
        {
            if (!SystemImplementation.DOMEA.frmUploadToOTCS.IsVisible)
            {
                frmUpload = new SystemImplementation.DOMEA.frmUploadToOTCS(this, this.Left + this.Width, this.Top, this.Height);
                frmUpload.Show();
            }
        }

        private void btnDownloadFromDOMEA_Click(object sender, EventArgs e)
        {
            frmUpload.SetFolderProperties(Convert.ToInt32(txtID.Text), txtName.Text);
        }

        public void UploadProcessInstanceToOTCS(int ParentNodeID, string GZ, string Comment)
        {
            try
            {
                // Get a new Folder template with a unique name for creation
                OTCSDocumentManagement.Node newFolder = fCWSClient.GetNodeTemplate(ParentNodeID, "Folder");

                // Create the new Folder
                newFolder = fCWSClient.CreateNode(newFolder);
                newFolder.Name = GZ;
                newFolder.Comment = Comment;
                fCWSClient.UpdateNode(newFolder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, OTCS.Constants.Application);
            }
        }

        public void UploadDocumentToOTCS(int ParentNodeID, string ELZ, int Einlaufzahl)
        {
            try
            {
                // Get a new FileAtts object populated with info from a file on disk
                string FilePath = session.GetDocumentFile(Einlaufzahl);
                FileInfo fileInfo = new FileInfo(FilePath);

                if (fileInfo != null)
                {
                    // Get a new Document template for the node based in the currently selected node
                    OTCSDocumentManagement.Node newDoc = fCWSClient.GetNodeTemplate(ParentNodeID, "Document");
                    newDoc.Name = ELZ;
                    newDoc = fCWSClient.CreateNodeAndVersion(newDoc, fileInfo);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, OTCS.Constants.Application);
            }
        }

        private void btnItemDOMEALogout_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            btnItemDOMEALogin.Enabled = true;
            btnItemDOMEALogout.Enabled = false;
            btnItemDOMEACR17DOMEA004.Enabled = false;
            btnItemDOMEAShowWorkSpace.Enabled = false;
            tvDOMEAMain.Nodes.Clear();
            dgvDOMEAWorkList.Rows.Clear();
            dgvDOMEADocumentList.Rows.Clear();
            if (session.Logout())
            {
                FormHelper.Click(sideBarPanelItem2,
                    superTabControl1, btnItemDOMEALogout, null);
            }
        }
        #endregion
        
        #region ORACLE


        private void btnItemOracleLogin_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem3,
                superTabControl1, btnItemOracleLogin, null);
            frmConnectOracle conOracle = new frmConnectOracle();
            conOracle.ShowDialog();
            if (OracleHelper.IsConnected)
            {
                lblStatusInfoOracle.Text = OracleHelper.ConnectionInfo;
                btnItemVSVMError.Enabled = true;
                btnItemEditTable.Enabled = true;
                btnItemOracleLogout.Enabled = true;
                btnItemOracleLogin.Enabled = false;
            }
            else
            {
                btnItemVSVMError.Enabled = false;
                btnItemEditTable.Enabled = false;
                btnItemOracleLogout.Enabled = false;
                btnItemOracleLogin.Enabled = true;
            }
        }

        private void btnItemVSVMError_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem3,
                superTabControl1, btnItemVSVMError, tiVSVMError);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            Cursor.Current = Cursors.WaitCursor;
            dgvSST.Rows.Clear();
            dgvAdressaten.Rows.Clear();
            dgvGBF.Rows.Clear();
            List<SST> listSST = OracleHelper.SearchDocument(txtELZ.Text);
            foreach (SST sst in listSST)
            {
                dgvSST.Rows.Add(new string[] { "", sst.Einlaufzahl.ToString(), sst.ELZ, sst.Subject });
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgvSST_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                //MessageBox.Show("Einlaufzahl: " + dgvSST.Rows[e.RowIndex].Cells["colEinlaufzahl"].Value.ToString());

                Cursor.Current = Cursors.WaitCursor;
                txtAddressID.Text = "";
                List<GBF> listGBF = OracleHelper.SearchGBF(Convert.ToInt32(dgvSST.Rows[e.RowIndex].Cells["colEinlaufzahl"].Value.ToString()));
                dgvGBF.Rows.Clear();
                foreach (GBF gbf in listGBF)
                {
                    dgvGBF.Rows.Add(new string[] { gbf.IGZ.ToString(), gbf.ADDRID.ToString(), gbf.Einlaufzahl.ToString(), gbf.Bezeichnung, gbf.NameTeil1, gbf.Nummer.ToString(), gbf.Jahr.ToString() });
                }

                List<ADDRESS> listADDR = OracleHelper.SearchAddress(Convert.ToInt32(dgvSST.Rows[e.RowIndex].Cells["colEinlaufzahl"].Value.ToString()));
                dgvAdressaten.Rows.Clear();
                foreach (ADDRESS addr in listADDR)
                {
                    dgvAdressaten.Rows.Add(new string[] { addr.Einlaufzahl.ToString(), addr.ADDRID.ToString(), addr.Vorname, addr.Nachname, addr.Firma1, addr.Firma2 });
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnUpdateAddressID_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            if (txtAddressID.Text != "" && dgvGBF.Rows.Count > 0)
            {
                int Einlaufzahl = Convert.ToInt32(dgvGBF.SelectedRows[0].Cells["colGBFEinlaufzahl"].Value.ToString());
                int Jahr = Convert.ToInt32(dgvGBF.SelectedRows[0].Cells["colGBFJahr"].Value.ToString());
                int Nummer = Convert.ToInt32(dgvGBF.SelectedRows[0].Cells["colGBFNummer"].Value.ToString());

                string update = "update fb_gebahrungsfaelle set adressid = '" + txtAddressID.Text + "' where einlaufzahl = " + Einlaufzahl + " and nummer = " + Nummer + " and jahr = " + Jahr;
                HConsole.WriteLine("--> " + update);
                DialogResult dr = MessageBox.Show(update, "Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    if (OracleHelper.UpdateAddressID(Einlaufzahl, Jahr, Nummer, txtAddressID.Text))
                    {
                        MessageBox.Show("Update durchgeführt!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HConsole.WriteLine("--> Update durchgeführt!");
                    }
                }
                else
                {
                    HConsole.WriteLine("--> update durch den Benutzer abgebrochen...");
                }
            }
            else
            {
                MessageBox.Show("Wählen Sie zuerst einen Adressaten und einen Gebarungsfall aus!", "Adressat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HConsole.WriteLine("--> kein Adressat ausgewählt...");
            }
        }

        private void dgvAdressaten_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            txtAddressID.Text = dgvAdressaten.Rows[e.RowIndex].Cells["colAdrAdressID"].Value.ToString();
            HConsole.WriteLine("--> Adressat " + txtAddressID.Text + " durch den Benutzer ausgewählt...");
        }

        private void txtELZ_KeyDown(object sender, KeyEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnItemOracleLogout_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem3,
                superTabControl1, btnItemOracleLogout, null);
            OracleHelper.ConnectionClose();
            btnItemVSVMError.Enabled = false;
            btnItemEditTable.Enabled = false;
            btnItemOracleLogout.Enabled = false;
            btnItemOracleLogin.Enabled = true;
            dgvDBObject.DataSource = null;
            tvDatabaseObjects.Nodes.Clear();
            lblStatusInfoOracle.Text = "";
        }


        private void tvDatabaseObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            Cursor.Current = Cursors.WaitCursor;
            dgvDBObject.DataSource = null;
            TreeNode node = e.Node;
            labelGeneralInfo.Text = "";
            if (node.Parent != null)
            {
                string tableName = node.Text;
                dataTable = OracleHelper.GetValuesFromTable(tableName);
                if (dataTable != null)
                {
                    dgvDBObject.DataSource = dataTable;
                    rowCount = dgvDBObject.Rows.Count - 1;
                    labelGeneralInfo.Text = string.Format("RowCount: {0}", rowCount.ToString());
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataView dv = dataTable.DefaultView;
                    dv.RowFilter = GetFilter(txtFilter.Text);
                    dgvDBObject.DataSource = dv;
                    contextMenuFilter.Hide();
                    rowCount = dgvDBObject.Rows.Count - 1;
                    labelGeneralInfo.Text = string.Format("RowCount: {0}", rowCount.ToString());
                }
            }
            catch (Exception ex)
            {
                Error.Show(ex);
            }
        }

        private string GetFilter(string Text)
        {
            Util.WriteMethodInfoToConsole();
            string rowFilter = "";
            try
            {
                foreach (DataGridViewColumn col in dgvDBObject.Columns)
                {
                    if (col.ValueType.Name == "String")
                    {
                        if (rowFilter != "") { rowFilter += "OR "; }
                        rowFilter += string.Format("{0} LIKE '%{1}%' ", col.Name, Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Show(ex);
            }
            return rowFilter;
        }


        #endregion

        #region OTContentServer

        public enum IconTypes
        {
            CATEGORY_ICON = 0,
            DISCUSSION_ICON,
            DOCUMENT_ICON,
            FOLDER_ICON,
            POLL_ICON,
            PROJECT_ICON,
            REPORT_ICON,
            SHORTCUT_ICON,
            TASK_ICON,
            TASKGROUP_ICON,
            TASKLIST_ICON,
            MILESTONE_ICON,
            URL_ICON,
            TOPIC_ICON,
            REPLY_ICON,
            UNKNOWN_ICON
        }

        /// <summary>
        /// Authentication Mode
        /// </summary>
        private OTCS.AuthenticationMode AuthMode
        {
            get
            {
                OTCS.AuthenticationMode authMode = OTCS.AuthenticationMode.CWSAuthentication;

                try
                {
                    authMode = (OTCS.AuthenticationMode)Enum.Parse(typeof(OTCS.AuthenticationMode), Properties.Settings.Default.AuthenticationMethod);
                }
                catch (ArgumentException) { }

                return authMode;
            }
        }

        /// <summary>
        /// Login user name.
        /// </summary>
        private string UserName
        {
            get
            {
                return Properties.Settings.Default.UserName;
            }
        }

        /// <summary>
        /// Login user password.
        /// </summary>
        private string Password
        {
            get
            {
                return OTCS.Util.Decrypt(Properties.Settings.Default.Password);
            }
        }

        /// <summary>
        /// Service root url, e.g. http://localhost/les-services/
        /// </summary>
        private string ServiceRoot
        {
            get
            {
                String root = Properties.Settings.Default.URL;

                int index = root.LastIndexOf("/");
                root = root.Substring(0, index + 1);

                return root;
            }
        }

        /// <summary>
        /// Service url suffix, e.g. .svc
        /// </summary>
        private string ServiceSuffix
        {
            get
            {
                return Properties.Settings.Default.URL.EndsWith(".svc") ? ".svc" : "";
            }
        }

        /// <summary>
        /// RCS Authentication Service URL, e.g. http://localhost:8080/ot-authws/services/Authentication
        /// </summary>
        private string RCSAuthServiceURL
        {
            get
            {
                return Properties.Settings.Default.RCSAuthURL;
            }
        }

        private void btnOTCSLogin_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();

            bool loggedIn = false;
            // enable SSL certificates that are not from a known Certificate Authority (CA), for this sample it accepts any cert
            PermissiveCertificatePolicy.Enable();

            while (!loggedIn)
            {
                // Display a login dialog.
                OTCS.frmOTLoginForm dlg = new OTCS.frmOTLoginForm();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fCWSClient = new OTCS.CWSClient(UserName, Password, ServiceRoot, ServiceSuffix, RCSAuthServiceURL, AuthMode);
                        Cursor.Current = Cursors.WaitCursor;
                        //*************************************************************************************
                        try
                        {
                            string Server = ServiceRoot.Replace("http://", string.Empty).Replace("https://", string.Empty);
                            Server = Server.Substring(0, Server.IndexOf('/'));
                            lblStatusInfoContentServer.Text = "ContentServer: " + UserName + "@" + Server;
                        }
                        catch { lblStatusInfoContentServer.Text = "ContentServer: " + UserName; }

                        FormHelper.Click(sideBarPanelItem1,superTabControl1, btnOTCSLogin, tiOTContentServer);

                        btnOTCSLogin.Enabled = false;
                        btnOTCSShowWorkSpace.Enabled = true;
                        btnOTCSCreateUser.Enabled = true;
                        btnOTCSCreateGroup.Enabled = true;
                        btnOTCSFindMember.Enabled = true;
                        //*************************************************************************************
                        // Set the ImageList to the TreeIcons resource
                        treeViewLivelink.ImageList = TreeIcons;

                        InitWorkspace();

                        loggedIn = true;
                        Cursor.Current = Cursors.Default;
                    }
                    catch (System.ServiceModel.FaultException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, OTCS.Constants.Application + " - " + ex.Code.Name);
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, OTCS.Constants.Application);
                    }
                }
                else
                {
                    break;
                }
            }

            // Set the ImageList to the TreeIcons resource
            treeViewLivelink.ImageList = TreeIcons;
        }

        private void btnOTCSLogout_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            //FormHelper.Click(sideBarPanelItem1,
            //    superTabControl1, btnOTCSLogin, null);

            //if (connOTCS.Close())
            //{
            //    btnOTCSLogin.Enabled = true;
            //    btnOTCSLogout.Enabled = false;
            //}
        }

        private void btnOTCSCreateUser_Click(object sender, EventArgs e)
        {

        }

        private void btnOTCSCreateGroup_Click(object sender, EventArgs e)
        {

        }

        private void btnOTCSFindMember_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem1, superTabControl1, btnOTCSFindMember, tiOTfindMember);

            InitFindMember();
        }
        private void btnOTCSLoadCategories_Click(object sender, EventArgs e)
        {
            OTCSDocumentManagement.Node selectedNode = fCWSClient.GetNode(Convert.ToInt32(txtID.Text));
            Util.WriteMethodInfoToConsole();
            SystemImplementation.OTContentServer.Forms.frmCategories frmCat = new SystemImplementation.OTContentServer.Forms.frmCategories(fCWSClient);
            frmCat.ShowDialog();

            if (category != -999)
            {
                OTCSDocumentManagement.Node categoryObject = fCWSClient.GetNode(category);
                if (categoryObject.Type == "Category")
                {
                    try
                    {
                        fCWSClient.AddCategory(selectedNode, categoryObject);
                        fCWSClient.UpdateNode(selectedNode);
                        getCategoriesForNode(selectedNode);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR - btn_Kategorie_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Es muss ein Objekt vom Typ Kategorie ausgewählt werden");
                }
            }

            //OTCSDocumentManagement.Node catNode = fCWSClient.GetRootNode("CategoriesWS");
            //HConsole.WriteLine("Level=0: " + catNode.Name);
            //ReturnSubNodes(Convert.ToInt32(catNode.ID), 1);
        }

        private void ReturnSubNodes(int parentID, int Level)
        {
            foreach (OTCSDocumentManagement.Node child in fCWSClient.ListNodes(parentID))
            {
                HConsole.WriteLine("Level=" + Level + ": " + child.Name);
                if (fCWSClient.ListNodes(Convert.ToInt32(child.ID)) != null)
                {
                    ReturnSubNodes(Convert.ToInt32(child.ID), Level + 1);
                }
            }
        }
        private void btnOTCSShowWorkSpace_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem1, superTabControl1, btnOTCSLogin, tiOTContentServer);
        }

        private void getCategoriesForNode(OTCSDocumentManagement.Node node)
        {
            try
            {
                if (tcCategories.Controls.Count > 0)
                {
                    tcCategories.Controls.Clear();
                }

                if (node.Metadata.AttributeGroups != null)
                {
                    int top;
                    foreach (OTCSDocumentManagement.AttributeGroup g in node.Metadata.AttributeGroups)
                    {
                        top = 10;
                        TabPage tp = new TabPage();
                        tp.Name = "tp_" + g.Key;
                        tp.Text = g.DisplayName;
                        tp.BackColor = System.Drawing.SystemColors.Control;

                        tcCategories.TabPages.Add(tp);

                        Panel p = new Panel();
                        p.Name = "p_" + g.Key;
                        p.AutoScroll = true;
                        p.Dock = DockStyle.Fill;
                        p.BackColor = System.Drawing.SystemColors.Control;
                        tcCategories.TabPages["tp_" + g.Key].Controls.Add(p);

                        for (int i = 0; i < g.Values.Length; i++)
                        {
                            if (g.Values[i].GetType() == typeof(OTCSDocumentManagement.StringValue))
                            {
                                OTCSDocumentManagement.StringValue val = g.Values[i] as OTCSDocumentManagement.StringValue;
                                TextBox tb = new TextBox();
                                tb.Width = 220;
                                tb.Top = top;
                                tb.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                                tb.ReadOnly = true;
                                tb.Name = "tb_" + i.ToString() + "_" + g.Key;
                                tb.Text = g.Values[i].Description + ": ";
                                if (val.Values != null)
                                {
                                    tb.Text += val.Values[0];
                                }
                                p.Controls.Add(tb);
                                top += 30;

                            }
                            else if (g.Values[i].GetType() == typeof(OTCSDocumentManagement.DateValue))
                            {
                                OTCSDocumentManagement.DateValue val = g.Values[i] as OTCSDocumentManagement.DateValue;
                                TextBox tb = new TextBox();
                                tb.Width = 220;
                                tb.Top = top;
                                tb.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                                tb.ReadOnly = true;
                                tb.Name = "tb_" + i.ToString() + "_" + g.Key;
                                tb.Text = g.Values[i].Description + ": ";
                                if (val.Values != null)
                                {
                                    tb.Text += val.Values[0];
                                }
                                p.Controls.Add(tb);
                                top += 30;
                            }
                            else
                            {
                                //MessageBox.Show("");
                            }

                        }
                    }
                }
                if (tcCategories.TabPages.Count == 0)
                {
                    //tcCategories.Visible = false;
                    groupBox3.Visible = false;
                }
                else
                {
                    //tcCategories.Visible = true;
                    groupBox3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR-getCategoriesForNode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
            }
        }

        private void btnOTCSShowDocVersion_Click(object sender, EventArgs e)
        {
            OTCSDocumentManagement.Node selectedNode = fCWSClient.GetNode(Convert.ToInt32(txtID.Text));
            if (selectedNode.DisplayType == "Dokument")
            {
                SystemImplementation.OTContentServer.Forms.frmVersions frmVer = new SystemImplementation.OTContentServer.Forms.frmVersions(fCWSClient, selectedNode);
                frmVer.ShowDialog();
            }
            else
            {
                MessageBox.Show("Versionen können nur auf Dokumente angezeigt werden!", "Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void treeViewLivelink_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //vielleicht brauch ma es jo 
        }

        private OTCSWorkflowService.ProcessInstance selectedPI;

        private void treeViewLivelink_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            selectedPI = null;
            if (ProcessInstances != null)
            {
                selectedPI = ProcessInstances.Find(a => a.Title == e.Node.Text);
                if (selectedPI != null)
                {
                    string LinkTermTemplate = "<a href=\"TextBoxMoreInfo\">Hier klicken um die ProzessInstanz zu ändern</a>";

                    superTooltip1.ShowTooltip(sender, Cursor.Position);
                    superTooltip1.SetSuperTooltip((System.Windows.Forms.TreeView)sender,
                            new DevComponents.DotNetBar.SuperTooltipInfo(selectedPI.Title, LinkTermTemplate,
                            "ProcessInstance Status: " + selectedPI.Status,
                            null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));

                }
                else
                {
                    superTooltip1.HideTooltip();
                }
            }
        }

        private void treeViewLivelink_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void superTooltip1_MarkupLinkClick(object sender, DevComponents.DotNetBar.MarkupLinkClickEventArgs e)
        {
            if (selectedPI != null)
            {
                for (int i = 0; i < selectedPI.Activities.Length; i++)
                {
                    if (selectedPI.Activities[i].Status.ToString().ToUpper() == "READY")
                    {
                        OTCSWorkflowService.ApplicationData[] appData = fCWSClient.GetWorkItemData(selectedPI, selectedPI.Activities[i].ID);
                        fCWSClient.UpdateWorkItem(selectedPI, selectedPI.Activities[i].ID, appData);
                        //break;
                    }
                }
            }
        }

        private void btnItemEditTable_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            FormHelper.Click(sideBarPanelItem3,
                superTabControl1, btnItemEditTable, tiEditTable);

            tvDatabaseObjects.Nodes.Clear();
            List<DBObject> allDBObjects = OracleHelper.GetAllTables();

            List<DBObject> allTABLE = allDBObjects.FindAll(o => o.Type == "TABLE");
            List<DBObject> allVIEW = allDBObjects.FindAll(o => o.Type == "VIEW");
            //List<DBObject> allSYNONYM = allDBObjects.FindAll(o => o.Type == "SYNONYM");
            //List<DBObject> allSEQUENCE = allDBObjects.FindAll(o => o.Type == "SEQUENCE");

            TreeNode tn = tvDatabaseObjects.Nodes.Add("Tables", "Tables", 0);
            foreach (DBObject obj in allTABLE)
            {
                if (!obj.Name.Contains("$"))
                {
                    tn.Nodes.Add(new TreeNode(obj.Name, 0, 0));
                }
            }
            tn = tvDatabaseObjects.Nodes.Add("Views", "Views", 1);
            foreach (DBObject obj in allVIEW)
            {
                tn.Nodes.Add(new TreeNode(obj.Name, 1, 1));
            }
            //tn = tvDatabaseObjects.Nodes.Add("Synonyms");
            //foreach (DBObject obj in allSYNONYM)
            //{
            //    tn.Nodes.Add(new TreeNode(obj.Name));
            //}
            //tn = tvDatabaseObjects.Nodes.Add("Sequences");
            //foreach (DBObject obj in allSEQUENCE)
            //{
            //    tn.Nodes.Add(new TreeNode(obj.Name));
            //}

            tvDatabaseObjects.Sort();
        }


        // initialize the enterprise work space
        private void InitWorkspace()
        {
            Util.WriteMethodInfoToConsole();
            myAdminTool.OTCSDocumentManagement.Node enterprise = fCWSClient.GetRootNode("EnterpriseWS");

            if (enterprise != null)
            {
                OTCS.LLTreeNode enterpriseNode = new OTCS.LLTreeNode();

                if (enterpriseNode != null)
                {
                    enterpriseNode.Data = enterprise;
                    enterpriseNode.ImageIndex = (int)IconTypes.FOLDER_ICON;
                    enterpriseNode.SelectedImageIndex = (int)IconTypes.FOLDER_ICON;

                    // Add the root level node

                    treeViewLivelink.Nodes.Add(enterpriseNode);
                    treeViewLivelink.SelectedNode = enterpriseNode;

                    // Add the first level of children

                    addLLNodes(enterpriseNode);
                }
            }
        }

        /// <summary>
        /// Add the children of a given node to the TreeView
        /// </summary>
        private void addLLNodes(TreeNode node)
        {
            //Util.WriteMethodInfoToConsole();
            OTCS.LLTreeNode theNode = node as OTCS.LLTreeNode;

            if (theNode != null)
            {
                try
                {
                    int id = theNode.ID;
                    bool useVolume = false;

                    // Use the volume node to get child nodes for these container types.
                    if (theNode.Data.Type == "Discussion" ||
                        theNode.Data.Type == "TaskList" ||
                        theNode.Data.Type == "Project")
                    {
                        id = -id;
                        useVolume = true;
                    }

                    // If the child count is zero OR we are forcing a refresh then get the browse data.
                    if (theNode.ChildCount > 0 || useVolume)
                    {
                        // request the children of this node
                        OTCSDocumentManagement.Node[] children = fCWSClient.ListNodes(id);
                        
                        if (children != null)
                        {
                            // Add each child node as a new LLTreeNode
                            foreach (OTCSDocumentManagement.Node child in children)
                            {
                                addChild(theNode, child);
                            }

                            // ensure childcount matches what we received back
                            theNode.ChildCount = children.Length;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, OTCS.Constants.Application);
                }
            }
        }

        /// <summary>
        /// For a given node, add the sub-directories for node's children in the llnodeTree.
        /// </summary>
        private void addSubLLNodes(OTCS.LLTreeNode theNode)
        {
            //Util.WriteMethodInfoToConsole();
            if (theNode != null)
            {
                if (theNode.ChildCount > 0)
                {
                    for (int i = 0; i < theNode.ChildCount; i++)
                    {
                        addLLNodes(theNode.Nodes[i]);
                    }

                    theNode.ChildNodesAdded = true;
                }
                else
                {
                    theNode.Nodes.Clear();
                }
            }
        }


        /// <summary>
        /// Add the given node as a new child of the parent LLTreeNode.
        /// </summary>
        private void addChild(OTCS.LLTreeNode parentNode, OTCSDocumentManagement.Node child)
        {
            //Util.WriteMethodInfoToConsole();
            OTCS.LLTreeNode childNode = new OTCS.LLTreeNode();

            if (parentNode != null && child != null && childNode != null)
            {
                childNode.Data = child;

                // set the folder icon or document.  Default to the Unknown icon for all others

                int imageIndex = (int)IconTypes.UNKNOWN_ICON;

                if (child.Type == "Category")
                {
                    imageIndex = (int)IconTypes.CATEGORY_ICON;
                }

                else if (child.Type == "Discussion")
                {
                    imageIndex = (int)IconTypes.DISCUSSION_ICON;
                }

                else if (child.Type == "Document")
                {
                    imageIndex = (int)IconTypes.DOCUMENT_ICON;
                }

                else if (child.Type == "Folder")
                {
                    imageIndex = (int)IconTypes.FOLDER_ICON;
                }

                else if (child.Type == "Milestone")
                {
                    imageIndex = (int)IconTypes.MILESTONE_ICON;
                }

                else if (child.Type == "Poll")
                {
                    imageIndex = (int)IconTypes.POLL_ICON;
                }

                else if (child.Type == "Project")
                {
                    imageIndex = (int)IconTypes.PROJECT_ICON;
                }

                else if (child.Type == "Report")
                {
                    imageIndex = (int)IconTypes.REPORT_ICON;
                }

                else if (child.Type == "Task")
                {
                    imageIndex = (int)IconTypes.TASK_ICON;
                }

                else if (child.Type == "TaskGroup")
                {
                    imageIndex = (int)IconTypes.TASKGROUP_ICON;
                }

                else if (child.Type == "TaskList")
                {
                    imageIndex = (int)IconTypes.TASKLIST_ICON;
                }
                else if (child.Type == "Topic")
                {
                    imageIndex = (int)IconTypes.TOPIC_ICON;
                }
                else if (child.Type == "Reply")
                {
                    imageIndex = (int)IconTypes.REPLY_ICON;
                }

                childNode.ImageIndex = imageIndex;
                childNode.SelectedImageIndex = imageIndex;

                parentNode.Nodes.Add(childNode);
            }
        }

        private void treeViewLivelink_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Util.WriteMethodInfoToConsole();
            OTCS.LLTreeNode theNode = e.Node as OTCS.LLTreeNode;

            // If the node has never been loaded before, call addSubLLNodes to populate the sub tree

            if (theNode != null && !theNode.ChildNodesAdded)
            {
                addSubLLNodes(theNode);
            }
        }

        private void treeViewLivelink_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Util.WriteMethodInfoToConsole();
            OTCS.LLTreeNode theNode = e.Node as OTCS.LLTreeNode;

            if (theNode != null)
            {

                int nodeID = theNode.ID;

                try
                {
                    // Retrieve the node from the doc service

                    OTCSDocumentManagement.Node node = theNode.Data;

                    // Get the node if the cached one does not exist.
                    if (null == node)
                    {
                        node = fCWSClient.GetNode(nodeID);
                    }

                    UpdateNodeInfo(node);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, OTCS.Constants.Application);
                }
            }
        }

        private void UpdateNodeInfo(OTCSDocumentManagement.Node node)
        {
            Util.WriteMethodInfoToConsole();
            // Assign various data to the fields on the form

            txtName.Text = node.Name;
            txtModifyDate.Text = node.ModifyDate.ToString();
            txtCreateDate.Text = node.CreateDate.ToString();
            txtID.Text = node.ID.ToString();

            // If there's a comment, assign the comment value to the form field.
            // otherwise clear the form field.

            if (node.Comment != null)
            {
                if (node.Comment.IndexOf("\n") == 0)
                {
                    txtComment.Text = node.Comment;
                }
                else
                {
                    txtComment.Lines = node.Comment.Split('\n');
                }
            }
            else
            {
                txtComment.Text = "";
            }

            // Retrieve the member for the creator and assign the user name to the creator field on the form
            if (node.CreatedBy != null)
            {
                int createdBy = (int)node.CreatedBy;
                txtCreator.Text = fCWSClient.GetMemberDisplayName(createdBy);
            }
            else
            {
                txtCreator.Text = "";
            }

            getCategoriesForNode(node);
            
            // Set the button enablemment
            //addVersionBtn.Enabled = (node.Type == "Document") ? true : false;
            //fetchBtn.Enabled = (node.Type == "Document") ? true : false;

            //// Note: You can't use node.Container because it is set to true for any node that can contain another node
            //// including channels, categories, etc.
            //addFolderBtn.Enabled = addDocBtn.Enabled = ((node.Type == "Folder") || (node.Type == "EnterpriseWS"));
            //deleteBtn.Enabled = (node.Type != "EnterpriseWS");
        }

        private void btnCSRefresh_Click(object sender, EventArgs e)
        {
            Refresh(treeViewLivelink.TopNode);
        }

        private void btnCSAddFolder_Click(object sender, EventArgs e)
        {
            // get the parent node

            OTCS.LLTreeNode currNode = treeViewLivelink.SelectedNode as OTCS.LLTreeNode;

            if (currNode == null)
            {
                MessageBox.Show("You must select a parent node in the tree view", OTCS.Constants.Application + " - Parent Node", MessageBoxButtons.OK);
                return;
            }

            try
            {
                // Get a new Folder template with a unique name for creation
                OTCSDocumentManagement.Node newFolder = fCWSClient.GetNodeTemplate(currNode.ID, "Folder");

                // Create the new Folder
                newFolder = fCWSClient.CreateNode(newFolder);

                // inc child count and refresh current node
                refreshAddChild(currNode);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, OTCS.Constants.Application);
            }
        }

        private void refreshAddChild(OTCS.LLTreeNode currNode)
        {
            if (currNode != null)
            {
                // inc the child count

                currNode.ChildCount += 1;

                // Refresh the current node

                Refresh(currNode);
                currNode.Expand();

            }

        }

        private void btnCSAddDocument_Click(object sender, EventArgs e)
        {
            // get the parent node
            OTCS.LLTreeNode currNode = treeViewLivelink.SelectedNode as OTCS.LLTreeNode;

            if (currNode == null)
            {
                MessageBox.Show("You must select a parent node in the tree view", OTCS.Constants.Application + " - Parent Node", MessageBoxButtons.OK);
                return;
            }

            try
            {
                // Get a new FileAtts object populated with info from a file on disk
                FileInfo fileInfo = SelectUploadFile("Add Document");

                if (fileInfo != null)
                {
                    // Get a new Document template for the node based in the currently selected node
                    OTCSDocumentManagement.Node newDoc = fCWSClient.GetNodeTemplate(currNode.ID, "Document");
                    newDoc.Name = fileInfo.Name;
                    newDoc = fCWSClient.CreateNodeAndVersion(newDoc, fileInfo);

                    // inc child count and refresh current node
                    refreshAddChild(currNode);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, OTCS.Constants.Application);
            }
        }

        /// <summary>
        /// Prompt the user to select a file from desktop
        /// Return an instance of FileInfo with the info for the selected file.
        /// </summary>
        private FileInfo SelectUploadFile(string message)
        {
            FileInfo fileInfo = null;

            // create open file dialog
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog != null)
            {
                openDialog.Filter = "";
                openDialog.Title = message;
                openDialog.ShowDialog();

                if (openDialog.FileName != null && openDialog.FileName.Length > 0)
                {
                    fileInfo = new FileInfo(openDialog.FileName);
                }
            }

            return fileInfo;
        }

        /// <summary>
        /// Refresh the tree starting with the specified TreeNode.
        /// </summary>
        private void Refresh(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                if (node.IsExpanded)
                {
                    // Save all expanded nodes rooted at node, even those that are
                    // indirectly rooted.
                    string[] tooBigExpandedNodes = new string[node.GetNodeCount(true)];
                    int iExpandedNodes = Refresh_GetExpanded(node,
                        tooBigExpandedNodes,
                        0);

                    if (iExpandedNodes > 0)
                    {
                        string[] expandedNodes = new string[iExpandedNodes];
                        Array.Copy(tooBigExpandedNodes, 0, expandedNodes, 0,
                            iExpandedNodes);

                        node.Nodes.Clear();
                        addLLNodes(node);

                        // Below added so children with sub nodes show up with 
                        // expand/collapse button.
                        addSubLLNodes((OTCS.LLTreeNode)node);
                        node.Expand();


                        // check all children. Some might have had sub-directories added
                        // from an external application so previous childless nodes
                        // might now have children.
                        for (int j = 0; j < node.Nodes.Count; j++)
                        {
                            if (node.Nodes[j].Nodes.Count > 0)
                            {
                                // If the child has subdirectories. If it was expanded
                                // before the refresh, then expand after the refresh.
                                Refresh_Expand(node.Nodes[j], expandedNodes);
                            }
                        }
                    }
                    else
                    {
                        // If the node is expanded and there are no children expanded, 
                        // we should update the tree by reading the node in case an 
                        // external application add/removed any nodes.

                        node.Nodes.Clear();
                        addLLNodes(node);

                        // Below added so children with sub nodes show up with 
                        // expand/collapse button.
                        addSubLLNodes((OTCS.LLTreeNode)node);
                        node.Expand();
                    }
                }
                else
                {
                    // If the node is not expanded, then there is no need to check
                    // if any of the children were expanded. However, we should
                    // update the tree by reading the nodes in case an external
                    // application add/removed any nodes.
                    node.Nodes.Clear();
                    addLLNodes(node);
                }
            }
            else
            {
                // Again, if there are no children, then there is no need to
                // worry about expanded nodes but if an external application
                // add/removed any nodes we should reflect that.
                node.Nodes.Clear();
                addLLNodes(node);
            }
        }

        /// <summary>
        /// Refresh helper function to expand all nodes whose paths are in parameter ExpandedNodes.
        /// </summary>
        /// <param term='node'>
        ///        Node from which to start expanding.
        /// </param>
        /// <param term='expandedNodes'>
        ///        Array of strings with the path names of all nodes to expand.
        /// </param>
        private void Refresh_Expand(TreeNode Node, string[] ExpandedNodes)
        {
            for (int i = ExpandedNodes.Length - 1; i >= 0; i--)
            {
                if (ExpandedNodes[i] == Node.Text)
                {
                    // For the expand button to show properly, one level of
                    // invisible children have to be added to the tree.
                    addSubLLNodes((OTCS.LLTreeNode)Node);
                    Node.Expand();

                    // If the node is expanded, expand any children that were
                    // expanded before the refresh.
                    for (int j = 0; j < Node.Nodes.Count; j++)
                    {
                        Refresh_Expand(Node.Nodes[j], ExpandedNodes);
                    }

                    return;
                }
            }
        }
        /// <summary>
        /// Refresh helper functions to get all expanded nodes under the given node.
        /// </summary>
        /// <param term='expandedNodes'>
        ///        Reference to an array of paths containing all nodes which were in the
        ///        expanded state when Refresh was requested.
        /// </param>
        /// <param term='startIndex'>
        ///        Array index of ExpandedNodes to start adding entries to.
        /// </param>
        /// <retvalue>
        ///        New StartIndex, i.e. given value of StartIndex + number of entries
        ///        added to ExpandedNodes.
        /// </retvalue>
        private int Refresh_GetExpanded(TreeNode Node, string[] ExpandedNodes, int StartIndex)
        {
            if (StartIndex < ExpandedNodes.Length)
            {
                if (Node.IsExpanded)
                {
                    ExpandedNodes[StartIndex] = Node.Text;
                    StartIndex++;
                    for (int i = 0; i < Node.Nodes.Count; i++)
                    {
                        StartIndex = Refresh_GetExpanded(Node.Nodes[i],
                            ExpandedNodes,
                            StartIndex);
                    }
                }
                return StartIndex;
            }
            return -1;
        }

        private void btnCSSave_Click(object sender, EventArgs e)
        {
            OTCS.LLTreeNode currNode = (OTCS.LLTreeNode)treeViewLivelink.SelectedNode;

            if (currNode != null)
            {
                bool anyChanges = false;

                // Fetch the original node from the service so we can use it as a template for the update call
                OTCSDocumentManagement.Node updateNode = fCWSClient.GetNode(currNode.ID);

                // Replace any updated fields in the updateNode with changed values from the form

                if (txtName.Text != updateNode.Name)
                {
                    updateNode.Name = txtName.Text;
                    anyChanges = true;
                }

                // Goofy bit of logic to try and determine of the comments have been changed.
                // Not entirely reliable due to CRLF vs CR issues but it seems to err on the side
                // of updating to frequently, not the other way around.

                if ((updateNode.Comment == null && txtComment.Text != "") ||
                     (updateNode.Comment != null && txtComment.Text == "") ||
                     (updateNode.Comment != null && updateNode.Comment != txtComment.Text))
                {
                    updateNode.Comment = txtComment.Text;
                    anyChanges = true;
                }

                if (anyChanges)
                {
                    try
                    {
                        // Update the node passing in the updateNode with changes
                        fCWSClient.UpdateNode(updateNode);

                        // Update the name in the tree view in case we renamed the node
                        currNode.Text = txtName.Text;

                        currNode.Data = fCWSClient.GetNode(Convert.ToInt32(updateNode.ID));
                        UpdateNodeInfo(currNode.Data);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, OTCS.Constants.Application);
                    }
                }
            }
        }

        private List<OTCSWorkflowService.ProcessInstance> ProcessInstances { get; set; }

        private void btnLoadProcessInstances_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //OTCSDocumentManagement.Node selectedNode = fCWSClient.GetNode(Convert.ToInt32(txtID.Text));
                OTCSWorkflowService.ProcessResult processResult;
                OTCSWorkflowService.PageHandle pageHandle;

                // Get the initial page of processes
                processResult = fCWSClient.ListProcesses();

                do
                {
                    pageHandle = processResult.PageHandle;

                    if (processResult.Processes != null)
                    {
                        ProcessInstances = new List<OTCSWorkflowService.ProcessInstance>();
                        foreach (OTCSWorkflowService.ProcessInstance process in processResult.Processes)
                        {
                            ProcessInstances.Add(process);
                        }
                    }

                    // Get the next page if we're not done.
                    if (!pageHandle.FinalPage)
                    {
                        processResult = fCWSClient.ListProcesses(pageHandle);
                    }
                }
                while (!pageHandle.FinalPage);
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.Show(ex);
            }
        }

        #endregion
        
        #region Find Member

        private void InitFindMember()
        {
            fMembers = null;
            
            _comboBoxSearchColumn.Items.Add(USER_LOGIN);
            _comboBoxSearchColumn.Items.Add(USER_FIRSTNAME);
            _comboBoxSearchColumn.Items.Add(USER_LASTNAME);
            _comboBoxSearchColumn.Items.Add(USER_EMAIL);
            _comboBoxSearchColumn.Items.Add(GROUP_NAME);
            _comboBoxSearchColumn.SelectedIndex = 0;

            _comboBoxSearchMatching.Items.Add(CONTAINS);
            _comboBoxSearchMatching.Items.Add(STARTS_WITH);
            _comboBoxSearchMatching.Items.Add(ENDS_WITH);
            _comboBoxSearchMatching.Items.Add(SOUNDS_LIKE);
            _comboBoxSearchMatching.SelectedIndex = 0;
        }

        private void ClearSearchResults()
        {
            ClearMemberInfo();
            _listViewMembers.Items.Clear();
        }

        private void ClearMemberInfo()
        {
            // If there's any content in the member info view clear it
            if (_splitContainerSearchResults.Panel2.Controls.Count > 0)
            {
                _splitContainerSearchResults.Panel2.Controls.Clear();
            }
        }

        private void ShowMember(OTCSMemberService.Member member, int index)
        {
            ClearMemberInfo();

            OTCSMemberService.User user = member as OTCSMemberService.User;
            OTCSMemberService.Group group = member as OTCSMemberService.Group;

            if (user != null)
            {
                OTContentServer.Controls.UserInfoControl userInfoCtrl = new OTContentServer.Controls.UserInfoControl(fCWSClient);

                // Setup and display user info control

                userInfoCtrl.SetGroupNames();
                // For now, don't allow changes to the GroupName
                userInfoCtrl.DisableGroupSelection();
                userInfoCtrl.UserInfo = user;
                userInfoCtrl.SelectGroup(userInfoCtrl.GetGroupByGroupID(Convert.ToInt32(user.DepartmentGroupID)));
                userInfoCtrl.TimeZone = (user.TimeZone != null ? user.TimeZone.Value : 0);

                userInfoCtrl.Index = index;
                userInfoCtrl.HideButtons = false;
                userInfoCtrl.OnUpdateUser += new OTContentServer.Controls.UpdateUserHandler(userInfoCtrl_OnUpdateUser);
                userInfoCtrl.OnDeleteUser += new OTContentServer.Controls.DeleteUserHandler(userInfoCtrl_OnDeleteUser);

                _splitContainerSearchResults.Panel2.Controls.Add(userInfoCtrl);
            }
            else if (group != null)
            {
                OTContentServer.Controls.GroupInfoControl groupInfoCtrl = new OTContentServer.Controls.GroupInfoControl();

                groupInfoCtrl.GroupInfo = group;
                groupInfoCtrl.Index = index;
                groupInfoCtrl.HideButtons = false;
                groupInfoCtrl.OnUpdateGroup += new OTContentServer.Controls.UpdateGroupHandler(groupInfoCtrl_OnUpdateGroup);
                groupInfoCtrl.OnDeleteGroup += new OTContentServer.Controls.DeleteGroupHandler(groupInfoCtrl_OnDeleteGroup);

                _splitContainerSearchResults.Panel2.Controls.Add(groupInfoCtrl);
            }
        }

        public void UpdateSelectedMember()
        {
            // if no connection or no items in search stop

            if (fCWSClient == null || _listViewMembers.Items.Count == 0)
            {
                return;
            }

            // get user control

            Control userCtrl = _splitContainerSearchResults.Panel2.Controls[0];

            if (userCtrl == null)
            {
                return;
            }

            // get the displayed member

            OTCSMemberService.Member member = null;
            int index = -1;
            bool isUser = true;
            OTContentServer.Controls.UserInfoControl userInfoCtrl = userCtrl as OTContentServer.Controls.UserInfoControl;
            OTContentServer.Controls.GroupInfoControl groupInfoCtrl = userCtrl as OTContentServer.Controls.GroupInfoControl;

            if (userInfoCtrl != null)
            {
                member = (OTCSMemberService.Member)userInfoCtrl.UserInfo;
                index = userInfoCtrl.Index;
            }
            else if (groupInfoCtrl != null)
            {
                isUser = false;
                member = (OTCSMemberService.Member)groupInfoCtrl.GroupInfo;
                index = groupInfoCtrl.Index;
            }

            if (member == null || index != _listViewMembers.SelectedIndices[0])
            {
                return;
            }

            // update the member

            try
            {
                member = fCWSClient.UpdateMember(member);

                // update name and group

                _listViewMembers.Items[index].Text = member.DisplayName;

                if (isUser)
                {
                    userInfoCtrl.UserInfo = (OTCSMemberService.User)member;
                }
                else
                {
                    groupInfoCtrl.GroupInfo = (OTCSMemberService.Group)member;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, OTCS.Constants.Application);
            }





        }

        public void DeleteSelectedMember()
        {
            // if no connection or no items in search stop

            if (fCWSClient == null || _listViewMembers.Items.Count == 0)
            {
                return;
            }

            // get user control

            Control userCtrl = _splitContainerSearchResults.Panel2.Controls[0];

            if (userCtrl == null)
            {
                return;
            }

            // get the displayed user 

            OTCSMemberService.Member member = null;
            int index = -1;
            OTContentServer.Controls.UserInfoControl userInfoCtrl = userCtrl as OTContentServer.Controls.UserInfoControl;
            OTContentServer.Controls.GroupInfoControl groupInfoCtrl = userCtrl as OTContentServer.Controls.GroupInfoControl;

            if (userInfoCtrl != null)
            {
                member = (OTCSMemberService.Member)userInfoCtrl.UserInfo;
                index = userInfoCtrl.Index;

            }
            else if (groupInfoCtrl != null)
            {

                member = groupInfoCtrl.GroupInfo;
                index = groupInfoCtrl.Index;

            }

            // check member valid

            if (member == null || index != _listViewMembers.SelectedIndices[0])
            {
                return;
            }

            // delete the member

            try
            {
                fCWSClient.DeleteMember(Convert.ToInt32(member.ID));

                if (_listViewMembers.Items.Count > 1)
                {
                    // remove member from view and cache

                    _listViewMembers.Items.Remove(_listViewMembers.Items[index]);
                    fMembers.Remove(fMembers[index]);

                    // select new member to display

                    if (index > 0)
                    {
                        index--;
                    }

                    _listViewMembers.Items[index].Selected = true;
                }
                else
                {
                    ClearSearchResults();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, OTCS.Constants.Application);
            }
        }

        private OTCSMemberService.MemberSearchOptions SetMemberSearchOptions(String searchString, String searchColumn, String searchMatching)
        {
            OTCSMemberService.MemberSearchOptions searchOptions = new OTCSMemberService.MemberSearchOptions();
            bool groupSearch = false;

            // Set the search options
            // - scope of search
            searchOptions.Scope = OTCSMemberService.SearchScope.SYSTEM;

            // - max number of results per page
            searchOptions.PageSize = PAGE_SIZE;

            // - search string
            if (searchString == null)
            {
                searchOptions.Search = "";
            }
            else
            {
                searchOptions.Search = searchString;
            }

            // - column to search
            switch (searchColumn)
            {
                case USER_LOGIN:
                    searchOptions.Column = OTCSMemberService.SearchColumn.NAME;
                    break;

                case USER_FIRSTNAME:
                    searchOptions.Column = OTCSMemberService.SearchColumn.FIRSTNAME;
                    break;

                case USER_LASTNAME:
                    searchOptions.Column = OTCSMemberService.SearchColumn.LASTNAME;
                    break;

                case USER_EMAIL:
                    searchOptions.Column = OTCSMemberService.SearchColumn.MAILADDRESS;
                    break;

                case GROUP_NAME:
                    searchOptions.Column = OTCSMemberService.SearchColumn.NAME;
                    groupSearch = true;
                    break;

                default:
                    searchOptions.Column = OTCSMemberService.SearchColumn.NAME;
                    break;
            }

            // - matching method to use
            switch (searchMatching)
            {
                case CONTAINS:
                    searchOptions.Matching = OTCSMemberService.SearchMatching.CONTAINS;
                    break;

                case STARTS_WITH:
                    searchOptions.Matching = OTCSMemberService.SearchMatching.STARTSWITH;
                    break;

                case ENDS_WITH:
                    searchOptions.Matching = OTCSMemberService.SearchMatching.ENDSWITH;
                    break;

                case SOUNDS_LIKE:
                    searchOptions.Matching = OTCSMemberService.SearchMatching.SOUNDSLIKE;
                    break;

                default:
                    searchOptions.Matching = OTCSMemberService.SearchMatching.CONTAINS;
                    break;
            }

            // - member types to return
            if (groupSearch)
            {
                searchOptions.Filter = OTCSMemberService.SearchFilter.GROUP;
            }
            else
            {
                searchOptions.Filter = OTCSMemberService.SearchFilter.USER;
            }

            return searchOptions;
        }


        private void _buttonSearch_Click(object sender, EventArgs e)
        {
            OTCSMemberService.PageHandle pageHandle;
            OTCSMemberService.MemberSearchResults searchResults = new OTCSMemberService.MemberSearchResults();
            OTCSMemberService.MemberSearchOptions searchOptions =
                SetMemberSearchOptions(_textBoxSearchString.Text, _comboBoxSearchColumn.Text, _comboBoxSearchMatching.Text);

            try
            {
                searchResults = fCWSClient.SearchForMembers(searchOptions);
                pageHandle = searchResults.PageHandle;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, OTCS.Constants.Application);
                return;
            }

            // If there were no members that matched the criteria
            if (searchResults.Members == null || searchResults.Members.Length == 0)
            {
                MessageBox.Show("There were no members that matched your search criteria.", OTCS.Constants.Application);
                return;
            }

            ClearSearchResults();

            bool first = true;
            fMembers = new List<OTCSMemberService.Member>();

            do
            {
                foreach (OTCSMemberService.Member member in searchResults.Members)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = member.DisplayName;
                    _listViewMembers.Items.Add(listViewItem);
                    fMembers.Add(member);

                    if (first)
                    {
                        ShowMember(member, _listViewMembers.Items.Count);
                        first = false;
                        listViewItem.Selected = true;
                    }
                }

                try
                {
                    searchResults = fCWSClient.SearchForMembers(pageHandle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, OTCS.Constants.Application);
                }
            }
            while (!searchResults.PageHandle.FinalPage);

            _listViewMembers.Focus();
        }

        private void _listViewMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get first select index and show member if valid
            if (_listViewMembers.SelectedIndices.Count > 0)
            {
                int index = _listViewMembers.SelectedIndices[0];

                if (index >= 0 && fMembers != null && fMembers.Count > index)
                {
                    OTCSMemberService.Member member = fMembers[index];

                    if (member != null)
                    {
                        ShowMember(member, index);
                    }
                }
            }
            else
            {
                ClearMemberInfo();
            }
        }

        private void userInfoCtrl_OnUpdateUser()
        {
            UpdateSelectedMember();
        }

        private void userInfoCtrl_OnDeleteUser()
        {
            DeleteSelectedMember();
        }

        private void groupInfoCtrl_OnUpdateGroup()
        {
            UpdateSelectedMember();
        }

        private void groupInfoCtrl_OnDeleteGroup()
        {
            DeleteSelectedMember();
        }

        #endregion

        #region SQL Server
        private void btnItemSQLServerLogin_Click(object sender, EventArgs e)
        { //TEST FÜR LUKAS
            Util.WriteMethodInfoToConsole();
            try
            {
                //string DBServer = "10.11.53.68,1450";
                //string DBCatalog = "OTCSdb_e";
                //string DBUser = "OTCSdbuser_e";
                //string DBUserPwd = "1DB2User3";
                string DBServer = "IMDEVPC";
                string DBCatalog = "DOMEA";
                string DBUser = "DOMEA";
                string DBUserPwd = "DOMEA";

                connSQLServer = new SqlConnection();
                connSQLServer.ConnectionString = string.Format("Data Source={0};" + //=> cms_t_listener,1450" +
                                                 "Initial Catalog={1};" +
                                                 "User id={2};" +
                                                 "Password={3};", DBServer, DBCatalog, DBUser, DBUserPwd);
                connSQLServer.Open();
                btnItemSQLServerLogout.Enabled = true;
                btnItemSQLServerLogin.Enabled = false;
                lblStatusInfoSQLServer.Text = string.Format("SQLServer: {0}@{1}", DBUser, DBServer);
            }
            catch (Exception ex)
            {
                Error.Show(ex);
            }
        }

        private void btnItemSQLServerLogout_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            try
            {
                connSQLServer.Close();
                btnItemSQLServerLogout.Enabled = false;
                btnItemSQLServerLogin.Enabled = true;
                lblStatusInfoSQLServer.Text = "SQLServer: logout";
            }
            catch (Exception ex)
            {
                Error.Show(ex);
            }
        }
        #endregion

        #region CR17DOMEA004
        //Organisationszusammenlegung

        #region Control Events

        const int imgIdxEmpty = 1;
        const int imgIdxFinished = 2;
        const int imgIdxWorking = 3;
        int rowID = -1;

        private void btnItemDOMEALogin_Click(object sender, EventArgs e)
        {
            Util.WriteMethodInfoToConsole();
            #region old Version WorkList
            //FormHelper.Click(sideBarPanelItem2, 
            //    superTabControl1, btnItemDOMEALogin, tiDOMEA);
            #endregion

            session = new SystemImplementation.DOMEA.DOMEA();
            if (session.Login())
            {
                btnItemDOMEALogin.Enabled = false;
                btnItemDOMEACR17DOMEA004.Enabled = true;
                btnItemDOMEALogout.Enabled = true;

                #region old Version WorkList
                //lblStatusInfoDOMEA.Text = session.ConnectionInfo;
                //btnItemDOMEAShowWorkSpace.Enabled = true;
                //btnItemDOMEALogin.Enabled = false;
                //btnItemDOMEALogout.Enabled = true;
                //CCD.Domea.Fw.Base.Obj.Folder mainFolder = session.GetMainFolder();
                //tvDOMEAMain.ImageList = imgListDOMEA;
                //tvDOMEAMain.Nodes.Add(mainFolder.Id.ToString(), mainFolder.GetSession().GetLoggedOnUser().Name, 0);
                //foreach(CCD.Domea.Fw.Base.Obj.Folder subFolder in mainFolder.GetSubFolders())
                //{
                //    tvDOMEAMain.Nodes[0].Nodes.Add(subFolder.Id.ToString(), subFolder.Name);
                //}
                //tvDOMEAMain.SelectedNode = tvDOMEAMain.Nodes[0];

                //List<CCD.Domea.Fw.Base.Obj.WorkItem> WorkItems = session.GetWorkList();
                //foreach (CCD.Domea.Fw.Base.Obj.WorkItem wi in WorkItems)
                //{
                //    dgvDOMEAWorkList.Rows.Add(new string[] { "0", wi.GetProcessInstance().Id.ToString(), wi.GetProcessInstance().Name, wi.GetProcessInstance().Comment, 
                //                                             wi.GetProcessInstance().CountOfDocuments.ToString(), wi.GetProcessInstance().GetProcess().Name, 
                //                                             wi.GetProcessInstance().GetProcessClass().Name, wi.GetCurrentProcessObject().Name });
                //}
                #endregion
            }
            #region Old Version
            //if (DOMEA.DOMEA.LibDOMEAImplementationExist)
            //{
            //    DOMEA.DOMEA domea = new DOMEA.DOMEA();
            //    domea.Init();
            //}
            //else
            //{
            //    MessageBox.Show("DOMEA Implementation (DOMEA.dll) nicht gefunden!", "DOMEA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            #endregion
        }

        private void btnItemDOMEACR17DOMEA004_Click(object sender, EventArgs e)
        {
            FormHelper.Click(sideBarPanelItem2,
                superTabControl1, btnItemDOMEACR17DOMEA004, tiCR17DOMEA004);

            //Default Image
            DataGridViewImageColumn cell = (DataGridViewImageColumn)dgvCR17DOMEA004Configuration.Columns["colStatus"];
            cell.Image = imgListDOMEA.Images[imgIdxEmpty];

            //Testdaten
            for (int i = 0; i < 100; i++)
            {
                rowID = dgvCR17DOMEA004Configuration.Rows.Add(i.ToString(), i.ToString(), "From_" + i.ToString(), "", i.ToString(), "To_" + i.ToString(), "");
            }

            dgvCR17DOMEA004Configuration.FirstDisplayedScrollingRowIndex = 0;
        }


        private void btnMovePI_Click(object sender, EventArgs e)
        {
            bwMoveProcessInstances.RunWorkerAsync();
        }

        #endregion

        #region BackGroundWorker move ProcessInstances
        private void bwMoveProcessInstances_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int idx = 0; idx < dgvCR17DOMEA004Configuration.Rows.Count; idx++)
            {
                bwMoveProcessInstances.ReportProgress(idx, idx);

                #region moving ProcessInstances
                ///<hier wird dann das wirkliche Verschieben durchgeführt>
                System.Threading.Thread.Sleep(500);
                #endregion
            }
        }

        private void bwMoveProcessInstances_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ChangeImage(Convert.ToInt32(e.UserState));

            SetFirstVisibleRow(Convert.ToInt32(e.UserState));
        }

        private void bwMoveProcessInstances_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeImage(dgvCR17DOMEA004Configuration.Rows.Count, true);
        }

        private void ChangeImage(int workingRowID, bool isLastRow = false)
        {
            #region finished Rows
            List<DataGridViewRow> finishedRows = new List<DataGridViewRow>
                                            (from DataGridViewRow r in dgvCR17DOMEA004Configuration.Rows
                                             where Convert.ToInt32(r.Cells["colRowID"].Value) < workingRowID
                                             select r);

            DataGridViewImageCell cell = null;
            foreach (DataGridViewRow row in finishedRows)
            {
                cell = (DataGridViewImageCell)row.Cells["colStatus"];
                cell.Value = imgListDOMEA.Images[imgIdxFinished];
            }
            #endregion

            #region working Row
            if (!isLastRow)
            {
                cell = (DataGridViewImageCell)dgvCR17DOMEA004Configuration.Rows[workingRowID].Cells["colStatus"];
                cell.Value = imgListDOMEA.Images[imgIdxWorking];
            }
            #endregion
        }

        private void SetFirstVisibleRow(int workingRowID)
        {
            if (dgvCR17DOMEA004Configuration.FirstDisplayedScrollingRowIndex + dgvCR17DOMEA004Configuration.DisplayedRowCount(false) <= workingRowID)
            {
                dgvCR17DOMEA004Configuration.FirstDisplayedScrollingRowIndex =
                    workingRowID - dgvCR17DOMEA004Configuration.DisplayedRowCount(false) + 1;
            }
        }

        #endregion

        #endregion

    }

    #region Certificate Policy

    // Derived from the WCF TransportSecurity sample. This class registers a callback for certificate validation and
    // simply returns true for all certificates. Production code should check the attributes of the certificate to
    // verify that it is acceptable.
    class PermissiveCertificatePolicy
    {
        static PermissiveCertificatePolicy sCurrentPolicy;

        PermissiveCertificatePolicy()
        {
            Util.WriteMethodInfoToConsole();
            // register our callback
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertValidate);
        }

        public static void Enable()
        {
            Util.WriteMethodInfoToConsole();
            // create a new policy that will register our callback
            sCurrentPolicy = new PermissiveCertificatePolicy();
        }

        // called by .NET when an unknown certificate is detected
        bool RemoteCertValidate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            Util.WriteMethodInfoToConsole();
            // we accept all certificates
            return true;
        }
    }

    #endregion
}
