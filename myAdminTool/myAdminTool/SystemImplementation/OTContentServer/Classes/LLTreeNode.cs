using System;
using System.Collections.Generic;
using System.Text;

using myAdminTool.OTCSDocumentManagement;

namespace myAdminTool.OTCS
{
    class LLTreeNode : System.Windows.Forms.TreeNode
    {
		public Node _node;
		public bool _childNodesAdded;

        public LLTreeNode () : base()
        {
        }

        /// <summary>
        /// Get the livelink node
        /// </summary>
        public Node Data
        {
            get
            {
                return _node;
            }
            set
            {
                if ( value != null )
                {
			        _node = value;
                    this.Text = value.Name;
                }
            }
        }

       /// <summary>
       /// Get or set the name of the livelink node
       /// </summary>
        public String DisplayName
        {
            get
            {
                String name = null;

                if ( _node != null )
                {
                    name = _node.Name;
                }
                else if ( this.Text != null || this.Text != String.Empty )
                {
                    name = this.Text;
                }

                return name;
            }
            set
            {
                if ( value != null && value != String.Empty )
                {
                    this.Text = value;
                }
            }
        }

        /// <summary>
        /// Get the child count for a node
        /// </summary>
        public int ChildCount
        {
            get
            {
                int count = 0;

                if ( _node != null && _node.IsContainer )
                {
                    count = _node.ContainerInfo.ChildCount;
                }

                return count;
            }
            set
            {
                if ( _node != null && _node.IsContainer )
                {
                    if ( value > 0 )
                    {
                        _node.ContainerInfo.ChildCount = value;
                    }
                }
            }
        }

        /// <summary>
        /// Get the node id 
        /// </summary>
        public int ID
        {
            get
            {
                int id = 0;

                if ( _node != null )
                {
                    id = Convert.ToInt32(_node.ID);
                }

                return id;
            }
        }

        /// <summary>
        /// Get or set if child nodes were added
        /// </summary>
        public bool ChildNodesAdded 
        {
            get
            {
                return _childNodesAdded;
            }
            set
            {
                _childNodesAdded = value;
            }
        }
	}
}
