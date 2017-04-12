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
    public delegate void UpdateUserHandler();
    public delegate void DeleteUserHandler();

    public partial class UserInfoControl : UserControl
    {

        private const int TIMEZONE_INDEX_OFFSET = 7;
        private const int DEFAULT_GROUP_ID = 1001;

        // published events
        public event UpdateUserHandler OnUpdateUser;
        public event DeleteUserHandler OnDeleteUser;

        private OTCS.CWSClient fCWSClient;

        private OTCSMemberService.User _user;
        private int _index;

        public UserInfoControl(OTCS.CWSClient cwsClient)
        {
            InitializeComponent();

            fCWSClient = cwsClient;

            _textBoxUsername.Focus();

            _user = null;
            _index = -1;
        }

        public String GroupName
        {
            get
            {
                String groupName = null;

                if (_comboBoxGroupName.SelectedItem == null)
                {
                    groupName = "";
                }
                else
                {
                    groupName = _comboBoxGroupName.SelectedItem.ToString();
                }

                return groupName;
            }
        }

        public void SetGroupNames()
        {
            OTCSMemberService.MemberSearchResults searchResults = null;

            try
            {
                searchResults = fCWSClient.SearchForGroupsByName("");

                // Set the combo box
                foreach (OTCSMemberService.Member m in searchResults.Members)
                {
                    // Set the Default Group as the default
                    if (m.ID == DEFAULT_GROUP_ID)
                    {
                        OTCSMemberService.Group defaultGroup = (OTCSMemberService.Group)m;
                        int index = _comboBoxGroupName.Items.Add(m.DisplayName);
                        _comboBoxGroupName.SelectedIndex = index;
                    }
                    else
                    {
                        _comboBoxGroupName.Items.Add(m.DisplayName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unable to find any groups.", OTCS.Constants.Application);
            }
        }

        public void SelectGroup(OTCSMemberService.Group group)
        {
            _comboBoxGroupName.SelectedItem = group.DisplayName;
        }

        public void DisableGroupSelection()
        {
            _comboBoxGroupName.Enabled = false;
        }

        public OTCSMemberService.Group GetGroupByGroupID(int groupID)
        {
            OTCSMemberService.Group group = new OTCSMemberService.Group();

            group = (OTCSMemberService.Group)fCWSClient.GetMemberByID(groupID);

            return group;
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

            if (textBox.Text != null)
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
        /// Update the user from the entry fields
        /// </summary>
        /// <returns></returns>
        public bool GetUserInfo()
        {
            // get user information

            String username = this.Username;
            String password = this.Password;
            String firstName = this.FirstName;
            String middleName = this.MiddleName;
            String lastName = this.LastName;
            String title = this.Title;
            String email = this.Email;
            String phone = this.Phone;
            String fax = this.Fax;
            int timeZone = this.TimeZone;
            String officeLocation = this.OfficeLocation;

            bool loginEnabled = this.LoginEnabled;
            bool publicAccessEnabled = this.PublicAccessEnabled;
            bool createUpdateUsers = this.CreateUpdateUsers;
            bool createUpdateGroups = this.CreateUpdateGroups;
            bool canAdministerUsers = this.CanAdministerUsers;
            bool canAdministerSystem = this.CanAdministerSystem;

            // test user

            if (_user == null)
            {
                MessageBox.Show("Unable to get the user", OTCS.Constants.Application);
                return false;
            }

            if (_user.Privileges == null)
            {
                MessageBox.Show("Unable to get user's privileges", OTCS.Constants.Application);
                return false;
            }

            // set user information

            _user.Name = username;
            _user.Password = password;
            _user.FirstName = firstName;
            _user.MiddleName = middleName;
            _user.LastName = lastName;
            _user.Title = title;
            _user.Email = email;
            _user.Phone = phone;
            _user.Fax = fax;
            _user.TimeZone = timeZone;
            _user.OfficeLocation = officeLocation;

            _user.Privileges.LoginEnabled = loginEnabled;
            _user.Privileges.PublicAccessEnabled = publicAccessEnabled;
            _user.Privileges.CreateUpdateUsers = createUpdateUsers;
            _user.Privileges.CreateUpdateGroups = createUpdateGroups;
            _user.Privileges.CanAdministerUsers = canAdministerUsers;
            _user.Privileges.CanAdministerSystem = canAdministerSystem;

            return true;
        }

        /// <summary>
        /// Get or set the user. When setting put the users info
        /// into the entry fields and when getting update the user
        /// from the entry fields
        /// </summary>
        public OTCSMemberService.User UserInfo
        {
            get
            {
                // set returned user if we get valid information

                OTCSMemberService.User user = null;

                if (GetUserInfo())
                {
                    user = _user;
                }

                return user;
            }
            set
            {
                if (value != null)
                {
                    OTCSMemberService.User userIn = value as OTCSMemberService.User;

                    if (userIn != null)
                    {
                        _user = userIn;

                        _textBoxUsername.Text = _user.Name;
                        _textBoxUsername.Enabled = false;
                        _textBoxFirstName.Text = _user.FirstName;
                        _textBoxMiddleName.Text = _user.MiddleName;
                        _textBoxLastName.Text = _user.LastName;
                        _textBoxTitle.Text = _user.Title;
                        _textBoxEmail.Text = _user.Email;
                        _textBoxPhone.Text = _user.Phone;
                        _textBoxFax.Text = _user.Fax;

                        // if password undefined then disable
                        // password 

                        if (_user.Password != null)
                        {
                            _textBoxPassword.Text = _user.Password;
                        }
                        else
                        {
                            _textBoxPassword.Enabled = false;
                        }

                        // set time zone

                        if (_user.TimeZone != null)
                        {
                            _comboBoxTimeZone.SelectedIndex = _user.TimeZone.Value;
                        }

                        _textBoxOfficeLocation.Text = _user.OfficeLocation;

                        if (_user.Privileges != null)
                        {
                            _checkBoxLoginEnabled.Checked = _user.Privileges.LoginEnabled;
                            _checkBoxPublicAccessEnabled.Checked = _user.Privileges.PublicAccessEnabled;
                            _checkBoxCreateUpdateUsers.Checked = _user.Privileges.CreateUpdateUsers;
                            _checkBoxCreateUpdateGroups.Checked = _user.Privileges.CreateUpdateGroups;
                            _checkBoxCanAdministerUsers.Checked = _user.Privileges.CanAdministerUsers;
                            _checkBoxCanAdministerSystem.Checked = _user.Privileges.CanAdministerSystem;
                        }

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
        /// Get or set the password
        /// </summary>
        public String Password
        {
            get
            {
                return GetText(_textBoxPassword);
            }
            set
            {
                SetText(_textBoxPassword, value);

                if (_user != null)
                {
                    _user.Password = value;
                }
            }
        }

        /// <summary>
        /// Get or set the user name
        /// </summary>
        public String Username
        {
            get
            {
                return GetText(_textBoxUsername);
            }
            set
            {
                SetText(_textBoxPassword, value);

                if (_user != null)
                {
                    _user.Password = value;
                }
            }
        }

        /// <summary>
        /// Get or set the first name
        /// </summary>
        public String FirstName
        {
            get
            {
                return GetText(_textBoxFirstName);
            }
            set
            {
                SetText(_textBoxFirstName, value);

                if (_user != null)
                {
                    _user.FirstName = value;
                }
            }
        }

        /// <summary>
        /// Get or set the middle name
        /// </summary>
        public String MiddleName
        {
            get
            {
                return GetText(_textBoxMiddleName);
            }
            set
            {
                SetText(_textBoxMiddleName, value);

                if (_user != null)
                {
                    _user.MiddleName = value;
                }
            }
        }

        /// <summary>
        /// Get or set the last name
        /// </summary>
        public String LastName
        {
            get
            {
                return GetText(_textBoxLastName);
            }
            set
            {
                SetText(_textBoxLastName, value);

                if (_user != null)
                {
                    _user.LastName = value;
                }
            }
        }

        /// <summary>
        /// Get or set the email
        /// </summary>
        public String Email
        {
            get
            {
                return GetText(_textBoxEmail);
            }
            set
            {
                SetText(_textBoxEmail, value);

                if (_user != null)
                {
                    _user.Email = value;
                }
            }
        }

        /// <summary>
        /// Get or set the title
        /// </summary>
        public String Title
        {
            get
            {
                return GetText(_textBoxTitle);
            }
            set
            {
                SetText(_textBoxTitle, value);

                if (_user != null)
                {
                    _user.Title = value;
                }
            }
        }

        /// <summary>
        /// Get or set the phone
        /// </summary>
        public String Phone
        {
            get
            {
                return GetText(_textBoxPhone);
            }
            set
            {
                SetText(_textBoxPhone, value);

                if (_user != null)
                {
                    _user.Phone = value;
                }
            }
        }

        /// <summary>
        /// Get or set the fax
        /// </summary>
        public String Fax
        {
            get
            {
                return GetText(_textBoxFax);
            }
            set
            {
                SetText(_textBoxFax, value);

                if (_user != null)
                {
                    _user.Fax = value;
                }
            }

        }

        /// <summary>
        /// Get or set the time zone
        /// </summary>
        public int TimeZone
        {
            // To sync with time zones in Content Server, use 0 for unset, or +TIMEZONE_INDEX_OFFSET otherwise
            get
            {
                return _comboBoxTimeZone.SelectedIndex > 0 ? _comboBoxTimeZone.SelectedIndex + TIMEZONE_INDEX_OFFSET : 0;
            }
            set
            {
                _comboBoxTimeZone.SelectedIndex = value > TIMEZONE_INDEX_OFFSET ? value - TIMEZONE_INDEX_OFFSET : 0;
            }
        }

        /// <summary>
        /// Get or set the office location
        /// </summary>
        public String OfficeLocation
        {
            get
            {
                return GetText(_textBoxOfficeLocation);
            }
            set
            {
                SetText(_textBoxOfficeLocation, value);

                if (_user != null)
                {
                    _user.OfficeLocation = value;
                }
            }
        }

        public bool LoginEnabled
        {
            get
            {
                return _checkBoxLoginEnabled.Checked;
            }
        }

        public bool PublicAccessEnabled
        {
            get
            {
                return _checkBoxPublicAccessEnabled.Checked;
            }
        }

        public bool CreateUpdateGroups
        {
            get
            {
                return _checkBoxCreateUpdateGroups.Checked;
            }
        }

        public bool CreateUpdateUsers
        {
            get
            {
                return _checkBoxCreateUpdateUsers.Checked;
            }
        }

        public bool CanAdministerUsers
        {
            get
            {
                return _checkBoxCanAdministerUsers.Checked;
            }
        }

        public bool CanAdministerSystem
        {
            get
            {
                return _checkBoxCanAdministerSystem.Checked;
            }
        }

        private void _buttonUpdate_Click(object sender, EventArgs e)
        {
            if (OnUpdateUser != null)
            {
                OnUpdateUser();
            }
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteUser != null)
            {
                OnDeleteUser();
            }
        }

    }
}
