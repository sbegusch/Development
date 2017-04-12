using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myAdminTool.OTContentServer.Controls
{
    public delegate void UpdateGroupHandler();
    public delegate void DeleteGroupHandler();

    public partial class GroupInfoControl : UserControl
    {

        // published events

        public event UpdateGroupHandler OnUpdateGroup;
        public event DeleteGroupHandler OnDeleteGroup;

        private OTCSMemberService.Group _group;
        private int _index;

        public GroupInfoControl()
        {
            InitializeComponent();

            _group = null;
            _index = -1;
        }

        /// <summary>
        /// Get the text from a text box. If there is an empty string
        /// then return null. This keeps the database behavior in tact.
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>

        public String GetText(TextBox textBox)
        {
            String text = null;

            if (textBox.Text != null && textBox.Text != String.Empty)
            {
                text = textBox.Text;
            }

            return text;
        }

        /// <summary>
        /// Set the text in a text box
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        /// <returns></returns>

        public void SetText(TextBox textBox, String text)
        {

            if (text != null && text != String.Empty)
            {
                textBox.Text = text;
            }
            else
            {
                textBox.Text = "";
            }

        }

        /// <summary>
        /// Update the group from the entry fields
        /// </summary>
        /// <returns></returns>

        private bool GetGroupInfo()
        {
            // get group information

            String groupName = GroupName;

            if (groupName == null || groupName == String.Empty)
            {
                MessageBox.Show("Please enter a valid group name", OTCS.Constants.Application);
                return false;
            }

            // test group

            if (_group == null)
            {
                MessageBox.Show("Unable to get group", OTCS.Constants.Application);
                return false;
            }

            _group.Name = groupName;

            return true;
        }

        /// <summary>
        /// Get or set the group. When setting put the groups info
        /// into the entry fields and when getting update the group
        /// from the entry fields
        /// </summary>

        public OTCSMemberService.Group GroupInfo
        {
            get
            {
                OTCSMemberService.Group group = null;

                if (GetGroupInfo())
                {
                    group = _group;
                }

                return _group;
            }
            set
            {
                if (value != null)
                {
                    OTCSMemberService.Group groupIn = value as OTCSMemberService.Group;

                    if (groupIn != null)
                    {
                        _group = groupIn;

                        _textBoxGroupName.Text = _group.Name;
                    }
                }
            }
        }

        /// <summary>
        /// Get or set our index
        /// </summary>

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (value >= 0)
                {
                    _index = value;
                }
            }
        }

        /// <summary>
        /// Hide or show the controls buttons
        /// </summary>

        public bool HideButtons
        {
            set
            {
                HideUpdate = value;
                HideDelete = value;
            }
        }

        /// <summary>
        /// Hide or show the update button
        /// </summary>

        public bool HideUpdate
        {
            set
            {
                if (value)
                {
                    _buttonUpdate.Hide();
                }
                else
                {
                    _buttonUpdate.Show();
                }
            }
        }

        /// <summary>
        /// Hide or show the delete button
        /// </summary>

        public bool HideDelete
        {
            set
            {
                if (value)
                {
                    _buttonDelete.Hide();
                }
                else
                {
                    _buttonDelete.Show();
                }
            }
        }


        /// <summary>
        /// Get or set the group name
        /// </summary>

        public String GroupName
        {
            get
            {
                return GetText(_textBoxGroupName);
            }
            set
            {
                SetText(_textBoxGroupName, value);

                if (_group != null)
                {
                    _group.Name = value;
                }
            }
        }



        private void _buttonUpdate_Click(object sender, EventArgs e)
        {
            if (OnUpdateGroup != null)
            {
                OnUpdateGroup();
            }
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteGroup != null)
            {
                OnDeleteGroup();
            }
        }
    }
}
