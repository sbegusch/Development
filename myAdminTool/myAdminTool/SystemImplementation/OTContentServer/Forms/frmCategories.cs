using myAdminTool.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.SystemImplementation.OTContentServer.Forms
{
    public partial class frmCategories : Form
    {
        OTCS.CWSClient fCWSClient;

        private bool setRetvalNull = true;

        public frmCategories(OTCS.CWSClient _fCWSClient)
        {
            InitializeComponent();
            fCWSClient = _fCWSClient;
            TreeViewCategories.ImageList = TreeIcons;
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            try
            {
                Util.WriteMethodInfoToConsole();
                myAdminTool.OTCSDocumentManagement.Node categoriesNode = fCWSClient.GetRootNode("CategoriesWS");// fCWSClient.GetNode(2006);

                if (categoriesNode != null)
                {
                    OTCS.LLTreeNode categNode = new OTCS.LLTreeNode();

                    if (categNode != null)
                    {
                        categNode.Data = categoriesNode;
                        categNode.ImageIndex = (int)frmMain.IconTypes.FOLDER_ICON;
                        categNode.SelectedImageIndex = (int)frmMain.IconTypes.FOLDER_ICON;

                        // Add the root level node

                        TreeViewCategories.Nodes.Add(categNode);
                        TreeViewCategories.SelectedNode = categNode;

                        // Add the first level of children

                        addLLNodes(categNode);
                    }
                    TreeViewCategories.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                frmMain.category = -999;
                MessageBox.Show(string.Format("FEHLER Frm Categories\n{0}", ex.Message), "FEHLER - frmCategories Categories_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }



        #region Befüllen der Treeview
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

                int imageIndex = (int)frmMain.IconTypes.UNKNOWN_ICON;

                if (child.Type == "Category")
                {
                    imageIndex = (int)frmMain.IconTypes.CATEGORY_ICON;
                }

                else if (child.Type == "Discussion")
                {
                    imageIndex = (int)frmMain.IconTypes.DISCUSSION_ICON;
                }

                else if (child.Type == "Document")
                {
                    imageIndex = (int)frmMain.IconTypes.DOCUMENT_ICON;
                }

                else if (child.Type == "Folder")
                {
                    imageIndex = (int)frmMain.IconTypes.FOLDER_ICON;
                }

                else if (child.Type == "Milestone")
                {
                    imageIndex = (int)frmMain.IconTypes.MILESTONE_ICON;
                }

                else if (child.Type == "Poll")
                {
                    imageIndex = (int)frmMain.IconTypes.POLL_ICON;
                }

                else if (child.Type == "Project")
                {
                    imageIndex = (int)frmMain.IconTypes.PROJECT_ICON;
                }

                else if (child.Type == "Report")
                {
                    imageIndex = (int)frmMain.IconTypes.REPORT_ICON;
                }

                else if (child.Type == "Task")
                {
                    imageIndex = (int)frmMain.IconTypes.TASK_ICON;
                }

                else if (child.Type == "TaskGroup")
                {
                    imageIndex = (int)frmMain.IconTypes.TASKGROUP_ICON;
                }

                else if (child.Type == "TaskList")
                {
                    imageIndex = (int)frmMain.IconTypes.TASKLIST_ICON;
                }
                else if (child.Type == "Topic")
                {
                    imageIndex = (int)frmMain.IconTypes.TOPIC_ICON;
                }
                else if (child.Type == "Reply")
                {
                    imageIndex = (int)frmMain.IconTypes.REPLY_ICON;
                }

                childNode.ImageIndex = imageIndex;
                childNode.SelectedImageIndex = imageIndex;

                parentNode.Nodes.Add(childNode);
            }
        }
        #endregion

        private void TreeViewCategories_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Util.WriteMethodInfoToConsole();
            OTCS.LLTreeNode theNode = e.Node as OTCS.LLTreeNode;

            // If the node has never been loaded before, call addSubLLNodes to populate the sub tree

            if (theNode != null && !theNode.ChildNodesAdded)
            {
                addSubLLNodes(theNode);
            }
        }



        private void btn_Uebernehmen_Click(object sender, EventArgs e)
        {
            OTCS.LLTreeNode currNode = (OTCS.LLTreeNode)TreeViewCategories.SelectedNode;
            frmMain.category = -999;
            if (currNode != null)
            {
                try
                {
                    frmMain.category = currNode.ID;
                    setRetvalNull = false;
                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, OTCS.Constants.Application);
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

        private void btn_abbrechen_Click(object sender, EventArgs e)

        {
            frmMain.category = -999;
            this.Close();
        }

        private void frmCategories_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(setRetvalNull)
            {
                frmMain.category = -999;
            }
        }
    }
}
