namespace myAdminTool.OTContentServer.Controls
{
    partial class UserInfoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._checkBoxCreateUpdateUsers = new System.Windows.Forms.CheckBox();
            this._labelGroupName = new System.Windows.Forms.Label();
            this._comboBoxGroupName = new System.Windows.Forms.ComboBox();
            this._labelUserInfoInstructions = new System.Windows.Forms.Label();
            this._textBoxPassword = new System.Windows.Forms.TextBox();
            this._labelPassword = new System.Windows.Forms.Label();
            this._textBoxUsername = new System.Windows.Forms.TextBox();
            this._labelUserName = new System.Windows.Forms.Label();
            this._checkBoxCanAdministerSystem = new System.Windows.Forms.CheckBox();
            this._checkBoxCanAdministerUsers = new System.Windows.Forms.CheckBox();
            this._checkBoxCreateUpdateGroups = new System.Windows.Forms.CheckBox();
            this._labelFirstName = new System.Windows.Forms.Label();
            this._textBoxTitle = new System.Windows.Forms.TextBox();
            this._labelMiddleName = new System.Windows.Forms.Label();
            this._groupboxUserRights = new System.Windows.Forms.GroupBox();
            this._labelUserRightsInstructions = new System.Windows.Forms.Label();
            this._checkBoxLoginEnabled = new System.Windows.Forms.CheckBox();
            this._checkBoxPublicAccessEnabled = new System.Windows.Forms.CheckBox();
            this._textBoxOfficeLocation = new System.Windows.Forms.TextBox();
            this._labelOfficeLocation = new System.Windows.Forms.Label();
            this._comboBoxTimeZone = new System.Windows.Forms.ComboBox();
            this._labelLastName = new System.Windows.Forms.Label();
            this._textBoxFirstName = new System.Windows.Forms.TextBox();
            this._labelTimeZone = new System.Windows.Forms.Label();
            this._buttonDelete = new System.Windows.Forms.Button();
            this._textBoxMiddleName = new System.Windows.Forms.TextBox();
            this._labelFax = new System.Windows.Forms.Label();
            this._textBoxFax = new System.Windows.Forms.TextBox();
            this._labelEmail = new System.Windows.Forms.Label();
            this._textBoxLastName = new System.Windows.Forms.TextBox();
            this._labelPhone = new System.Windows.Forms.Label();
            this._textBoxPhone = new System.Windows.Forms.TextBox();
            this._labelTitle = new System.Windows.Forms.Label();
            this._buttonUpdate = new System.Windows.Forms.Button();
            this._textBoxEmail = new System.Windows.Forms.TextBox();
            this._groupboxUserInfo = new System.Windows.Forms.GroupBox();
            this._groupboxUserRights.SuspendLayout();
            this._groupboxUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkBoxCreateUpdateUsers
            // 
            this._checkBoxCreateUpdateUsers.AutoSize = true;
            this._checkBoxCreateUpdateUsers.Location = new System.Drawing.Point(24, 105);
            this._checkBoxCreateUpdateUsers.Name = "_checkBoxCreateUpdateUsers";
            this._checkBoxCreateUpdateUsers.Size = new System.Drawing.Size(142, 17);
            this._checkBoxCreateUpdateUsers.TabIndex = 14;
            this._checkBoxCreateUpdateUsers.Text = "Create and Modify Users";
            this._checkBoxCreateUpdateUsers.UseVisualStyleBackColor = true;
            // 
            // _labelGroupName
            // 
            this._labelGroupName.AutoSize = true;
            this._labelGroupName.Location = new System.Drawing.Point(21, 84);
            this._labelGroupName.Name = "_labelGroupName";
            this._labelGroupName.Size = new System.Drawing.Size(62, 13);
            this._labelGroupName.TabIndex = 30;
            this._labelGroupName.Text = "Department";
            // 
            // _comboBoxGroupName
            // 
            this._comboBoxGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxGroupName.Location = new System.Drawing.Point(109, 81);
            this._comboBoxGroupName.Name = "_comboBoxGroupName";
            this._comboBoxGroupName.Size = new System.Drawing.Size(197, 21);
            this._comboBoxGroupName.TabIndex = 1;
            // 
            // _labelUserInfoInstructions
            // 
            this._labelUserInfoInstructions.AutoSize = true;
            this._labelUserInfoInstructions.Location = new System.Drawing.Point(22, 26);
            this._labelUserInfoInstructions.Name = "_labelUserInfoInstructions";
            this._labelUserInfoInstructions.Size = new System.Drawing.Size(226, 13);
            this._labelUserInfoInstructions.TabIndex = 18;
            this._labelUserInfoInstructions.Text = "Enter the personal information for the new user";
            // 
            // _textBoxPassword
            // 
            this._textBoxPassword.Location = new System.Drawing.Point(109, 111);
            this._textBoxPassword.Name = "_textBoxPassword";
            this._textBoxPassword.PasswordChar = '*';
            this._textBoxPassword.Size = new System.Drawing.Size(197, 20);
            this._textBoxPassword.TabIndex = 2;
            // 
            // _labelPassword
            // 
            this._labelPassword.AutoSize = true;
            this._labelPassword.Location = new System.Drawing.Point(21, 114);
            this._labelPassword.Name = "_labelPassword";
            this._labelPassword.Size = new System.Drawing.Size(53, 13);
            this._labelPassword.TabIndex = 19;
            this._labelPassword.Text = "Password";
            // 
            // _textBoxUsername
            // 
            this._textBoxUsername.Location = new System.Drawing.Point(109, 53);
            this._textBoxUsername.Name = "_textBoxUsername";
            this._textBoxUsername.Size = new System.Drawing.Size(197, 20);
            this._textBoxUsername.TabIndex = 0;
            // 
            // _labelUserName
            // 
            this._labelUserName.AutoSize = true;
            this._labelUserName.Location = new System.Drawing.Point(21, 56);
            this._labelUserName.Name = "_labelUserName";
            this._labelUserName.Size = new System.Drawing.Size(64, 13);
            this._labelUserName.TabIndex = 20;
            this._labelUserName.Text = "Login Name";
            // 
            // _checkBoxCanAdministerSystem
            // 
            this._checkBoxCanAdministerSystem.AutoSize = true;
            this._checkBoxCanAdministerSystem.Location = new System.Drawing.Point(24, 143);
            this._checkBoxCanAdministerSystem.Name = "_checkBoxCanAdministerSystem";
            this._checkBoxCanAdministerSystem.Size = new System.Drawing.Size(111, 17);
            this._checkBoxCanAdministerSystem.TabIndex = 16;
            this._checkBoxCanAdministerSystem.Text = "Administer System";
            this._checkBoxCanAdministerSystem.UseVisualStyleBackColor = true;
            // 
            // _checkBoxCanAdministerUsers
            // 
            this._checkBoxCanAdministerUsers.AutoSize = true;
            this._checkBoxCanAdministerUsers.Location = new System.Drawing.Point(24, 124);
            this._checkBoxCanAdministerUsers.Name = "_checkBoxCanAdministerUsers";
            this._checkBoxCanAdministerUsers.Size = new System.Drawing.Size(104, 17);
            this._checkBoxCanAdministerUsers.TabIndex = 15;
            this._checkBoxCanAdministerUsers.Text = "Administer Users";
            this._checkBoxCanAdministerUsers.UseVisualStyleBackColor = true;
            // 
            // _checkBoxCreateUpdateGroups
            // 
            this._checkBoxCreateUpdateGroups.AutoSize = true;
            this._checkBoxCreateUpdateGroups.Location = new System.Drawing.Point(24, 86);
            this._checkBoxCreateUpdateGroups.Name = "_checkBoxCreateUpdateGroups";
            this._checkBoxCreateUpdateGroups.Size = new System.Drawing.Size(149, 17);
            this._checkBoxCreateUpdateGroups.TabIndex = 13;
            this._checkBoxCreateUpdateGroups.Text = "Create and Modify Groups";
            this._checkBoxCreateUpdateGroups.UseVisualStyleBackColor = true;
            // 
            // _labelFirstName
            // 
            this._labelFirstName.AutoSize = true;
            this._labelFirstName.Location = new System.Drawing.Point(21, 144);
            this._labelFirstName.Name = "_labelFirstName";
            this._labelFirstName.Size = new System.Drawing.Size(57, 13);
            this._labelFirstName.TabIndex = 21;
            this._labelFirstName.Text = "First Name";
            // 
            // _textBoxTitle
            // 
            this._textBoxTitle.Location = new System.Drawing.Point(109, 228);
            this._textBoxTitle.Name = "_textBoxTitle";
            this._textBoxTitle.Size = new System.Drawing.Size(197, 20);
            this._textBoxTitle.TabIndex = 6;
            // 
            // _labelMiddleName
            // 
            this._labelMiddleName.AutoSize = true;
            this._labelMiddleName.Location = new System.Drawing.Point(21, 173);
            this._labelMiddleName.Name = "_labelMiddleName";
            this._labelMiddleName.Size = new System.Drawing.Size(69, 13);
            this._labelMiddleName.TabIndex = 22;
            this._labelMiddleName.Text = "Middle Name";
            // 
            // _groupboxUserRights
            // 
            this._groupboxUserRights.Controls.Add(this._checkBoxCreateUpdateUsers);
            this._groupboxUserRights.Controls.Add(this._labelUserRightsInstructions);
            this._groupboxUserRights.Controls.Add(this._checkBoxCanAdministerSystem);
            this._groupboxUserRights.Controls.Add(this._checkBoxLoginEnabled);
            this._groupboxUserRights.Controls.Add(this._checkBoxCanAdministerUsers);
            this._groupboxUserRights.Controls.Add(this._checkBoxPublicAccessEnabled);
            this._groupboxUserRights.Controls.Add(this._checkBoxCreateUpdateGroups);
            this._groupboxUserRights.Location = new System.Drawing.Point(3, 431);
            this._groupboxUserRights.Name = "_groupboxUserRights";
            this._groupboxUserRights.Size = new System.Drawing.Size(322, 170);
            this._groupboxUserRights.TabIndex = 31;
            this._groupboxUserRights.TabStop = false;
            this._groupboxUserRights.Text = "User Privleges";
            // 
            // _labelUserRightsInstructions
            // 
            this._labelUserRightsInstructions.AutoSize = true;
            this._labelUserRightsInstructions.Location = new System.Drawing.Point(22, 22);
            this._labelUserRightsInstructions.Name = "_labelUserRightsInstructions";
            this._labelUserRightsInstructions.Size = new System.Drawing.Size(139, 13);
            this._labelUserRightsInstructions.TabIndex = 15;
            this._labelUserRightsInstructions.Text = "Select the rights for the user";
            // 
            // _checkBoxLoginEnabled
            // 
            this._checkBoxLoginEnabled.AutoSize = true;
            this._checkBoxLoginEnabled.Location = new System.Drawing.Point(24, 47);
            this._checkBoxLoginEnabled.Name = "_checkBoxLoginEnabled";
            this._checkBoxLoginEnabled.Size = new System.Drawing.Size(94, 17);
            this._checkBoxLoginEnabled.TabIndex = 11;
            this._checkBoxLoginEnabled.Text = "Login Enabled";
            this._checkBoxLoginEnabled.UseVisualStyleBackColor = true;
            // 
            // _checkBoxPublicAccessEnabled
            // 
            this._checkBoxPublicAccessEnabled.AutoSize = true;
            this._checkBoxPublicAccessEnabled.Location = new System.Drawing.Point(24, 67);
            this._checkBoxPublicAccessEnabled.Name = "_checkBoxPublicAccessEnabled";
            this._checkBoxPublicAccessEnabled.Size = new System.Drawing.Size(135, 17);
            this._checkBoxPublicAccessEnabled.TabIndex = 12;
            this._checkBoxPublicAccessEnabled.Text = "Public Access Enabled";
            this._checkBoxPublicAccessEnabled.UseVisualStyleBackColor = true;
            // 
            // _textBoxOfficeLocation
            // 
            this._textBoxOfficeLocation.Location = new System.Drawing.Point(109, 350);
            this._textBoxOfficeLocation.Name = "_textBoxOfficeLocation";
            this._textBoxOfficeLocation.Size = new System.Drawing.Size(197, 20);
            this._textBoxOfficeLocation.TabIndex = 10;
            // 
            // _labelOfficeLocation
            // 
            this._labelOfficeLocation.AutoSize = true;
            this._labelOfficeLocation.Location = new System.Drawing.Point(22, 353);
            this._labelOfficeLocation.Name = "_labelOfficeLocation";
            this._labelOfficeLocation.Size = new System.Drawing.Size(79, 13);
            this._labelOfficeLocation.TabIndex = 23;
            this._labelOfficeLocation.Text = "Office Location";
            // 
            // _comboBoxTimeZone
            // 
            this._comboBoxTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTimeZone.Items.AddRange(new object[] {
            "",
            "GMT -12:00 Dateline",
            "GMT -11:30",
            "GMT -11:00 Samoa",
            "GMT -10:30",
            "GMT -10:00 Hawaiian",
            "GMT -09:30",
            "GMT -09:00 Alaskan",
            "GMT -08:30",
            "GMT -08:00 Pacific",
            "GMT -07:30",
            "GMT -07:00 Mountain",
            "GMT -06:30",
            "GMT -06:00 Central",
            "GMT -05:30",
            "GMT -05:00 Eastern",
            "GMT -04:30",
            "GMT -04:00 Atlantic",
            "GMT -03:30 Newfoundland",
            "GMT -03:00 Brasilia, Buenos Aires",
            "GMT -02:30",
            "GMT -02:00 Mid Atlantic",
            "GMT -01:30",
            "GMT -01:00 Azores",
            "GMT -00:30",
            "Greenwich Mean Time",
            "GMT +00:30",
            "GMT +01:00 Western Europe",
            "GMT +01:30",
            "GMT +02:00 Eastern Europe",
            "GMT +02:30",
            "GMT +03:00",
            "GMT +03:30 Russia, Saudia Arabia",
            "GMT +04:00 Iran, Arabian",
            "GMT +04:30",
            "GMT +05:00 West Asia",
            "GMT +05:30 India",
            "GMT +06:00 Cental Asia",
            "GMT +06:30",
            "GMT +07:00 Bankok, Hanoi, Jakarta",
            "GMT +07:30",
            "GMT +08:00 China, Singapore, Taiwan",
            "GMT +08:00 Australia (WT)",
            "GMT +08:30",
            "GMT +09:00 Korea, Japan",
            "GMT +09:30 Australia (CT)",
            "GMT +10:00 Australia (ET)",
            "GMT +10:30",
            "GMT +11:00 Central Pacific",
            "GMT +11:30",
            "GMT +12:00 Fiji, NewZealand"});
            this._comboBoxTimeZone.Location = new System.Drawing.Point(109, 385);
            this._comboBoxTimeZone.Name = "_comboBoxTimeZone";
            this._comboBoxTimeZone.Size = new System.Drawing.Size(197, 21);
            this._comboBoxTimeZone.TabIndex = 11;
            // 
            // _labelLastName
            // 
            this._labelLastName.AutoSize = true;
            this._labelLastName.Location = new System.Drawing.Point(21, 203);
            this._labelLastName.Name = "_labelLastName";
            this._labelLastName.Size = new System.Drawing.Size(58, 13);
            this._labelLastName.TabIndex = 24;
            this._labelLastName.Text = "Last Name";
            // 
            // _textBoxFirstName
            // 
            this._textBoxFirstName.Location = new System.Drawing.Point(109, 141);
            this._textBoxFirstName.Name = "_textBoxFirstName";
            this._textBoxFirstName.Size = new System.Drawing.Size(197, 20);
            this._textBoxFirstName.TabIndex = 3;
            // 
            // _labelTimeZone
            // 
            this._labelTimeZone.AutoSize = true;
            this._labelTimeZone.Location = new System.Drawing.Point(21, 388);
            this._labelTimeZone.Name = "_labelTimeZone";
            this._labelTimeZone.Size = new System.Drawing.Size(58, 13);
            this._labelTimeZone.TabIndex = 25;
            this._labelTimeZone.Text = "Time Zone";
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Location = new System.Drawing.Point(237, 607);
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Size = new System.Drawing.Size(82, 23);
            this._buttonDelete.TabIndex = 33;
            this._buttonDelete.Text = "Delete";
            this._buttonDelete.UseVisualStyleBackColor = true;
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
            // 
            // _textBoxMiddleName
            // 
            this._textBoxMiddleName.Location = new System.Drawing.Point(109, 170);
            this._textBoxMiddleName.Name = "_textBoxMiddleName";
            this._textBoxMiddleName.Size = new System.Drawing.Size(197, 20);
            this._textBoxMiddleName.TabIndex = 4;
            // 
            // _labelFax
            // 
            this._labelFax.AutoSize = true;
            this._labelFax.Location = new System.Drawing.Point(21, 321);
            this._labelFax.Name = "_labelFax";
            this._labelFax.Size = new System.Drawing.Size(24, 13);
            this._labelFax.TabIndex = 26;
            this._labelFax.Text = "Fax";
            // 
            // _textBoxFax
            // 
            this._textBoxFax.Location = new System.Drawing.Point(109, 318);
            this._textBoxFax.Name = "_textBoxFax";
            this._textBoxFax.Size = new System.Drawing.Size(197, 20);
            this._textBoxFax.TabIndex = 9;
            // 
            // _labelEmail
            // 
            this._labelEmail.AutoSize = true;
            this._labelEmail.Location = new System.Drawing.Point(22, 260);
            this._labelEmail.Name = "_labelEmail";
            this._labelEmail.Size = new System.Drawing.Size(32, 13);
            this._labelEmail.TabIndex = 27;
            this._labelEmail.Text = "Email";
            // 
            // _textBoxLastName
            // 
            this._textBoxLastName.Location = new System.Drawing.Point(109, 200);
            this._textBoxLastName.Name = "_textBoxLastName";
            this._textBoxLastName.Size = new System.Drawing.Size(197, 20);
            this._textBoxLastName.TabIndex = 5;
            // 
            // _labelPhone
            // 
            this._labelPhone.AutoSize = true;
            this._labelPhone.Location = new System.Drawing.Point(21, 291);
            this._labelPhone.Name = "_labelPhone";
            this._labelPhone.Size = new System.Drawing.Size(38, 13);
            this._labelPhone.TabIndex = 28;
            this._labelPhone.Text = "Phone";
            // 
            // _textBoxPhone
            // 
            this._textBoxPhone.Location = new System.Drawing.Point(109, 288);
            this._textBoxPhone.Name = "_textBoxPhone";
            this._textBoxPhone.Size = new System.Drawing.Size(197, 20);
            this._textBoxPhone.TabIndex = 8;
            // 
            // _labelTitle
            // 
            this._labelTitle.AutoSize = true;
            this._labelTitle.Location = new System.Drawing.Point(20, 231);
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.Size = new System.Drawing.Size(27, 13);
            this._labelTitle.TabIndex = 29;
            this._labelTitle.Text = "Title";
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Location = new System.Drawing.Point(156, 607);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this._buttonUpdate.TabIndex = 32;
            this._buttonUpdate.Text = "Update";
            this._buttonUpdate.UseVisualStyleBackColor = true;
            this._buttonUpdate.Click += new System.EventHandler(this._buttonUpdate_Click);
            // 
            // _textBoxEmail
            // 
            this._textBoxEmail.Location = new System.Drawing.Point(109, 257);
            this._textBoxEmail.Name = "_textBoxEmail";
            this._textBoxEmail.Size = new System.Drawing.Size(197, 20);
            this._textBoxEmail.TabIndex = 7;
            // 
            // _groupboxUserInfo
            // 
            this._groupboxUserInfo.Controls.Add(this._labelGroupName);
            this._groupboxUserInfo.Controls.Add(this._comboBoxGroupName);
            this._groupboxUserInfo.Controls.Add(this._labelUserInfoInstructions);
            this._groupboxUserInfo.Controls.Add(this._textBoxPassword);
            this._groupboxUserInfo.Controls.Add(this._labelPassword);
            this._groupboxUserInfo.Controls.Add(this._textBoxUsername);
            this._groupboxUserInfo.Controls.Add(this._labelUserName);
            this._groupboxUserInfo.Controls.Add(this._labelFirstName);
            this._groupboxUserInfo.Controls.Add(this._textBoxTitle);
            this._groupboxUserInfo.Controls.Add(this._labelMiddleName);
            this._groupboxUserInfo.Controls.Add(this._textBoxOfficeLocation);
            this._groupboxUserInfo.Controls.Add(this._labelOfficeLocation);
            this._groupboxUserInfo.Controls.Add(this._comboBoxTimeZone);
            this._groupboxUserInfo.Controls.Add(this._labelLastName);
            this._groupboxUserInfo.Controls.Add(this._textBoxFirstName);
            this._groupboxUserInfo.Controls.Add(this._labelTimeZone);
            this._groupboxUserInfo.Controls.Add(this._textBoxMiddleName);
            this._groupboxUserInfo.Controls.Add(this._labelFax);
            this._groupboxUserInfo.Controls.Add(this._textBoxFax);
            this._groupboxUserInfo.Controls.Add(this._labelEmail);
            this._groupboxUserInfo.Controls.Add(this._textBoxLastName);
            this._groupboxUserInfo.Controls.Add(this._labelPhone);
            this._groupboxUserInfo.Controls.Add(this._textBoxPhone);
            this._groupboxUserInfo.Controls.Add(this._labelTitle);
            this._groupboxUserInfo.Controls.Add(this._textBoxEmail);
            this._groupboxUserInfo.Location = new System.Drawing.Point(3, 3);
            this._groupboxUserInfo.Name = "_groupboxUserInfo";
            this._groupboxUserInfo.Size = new System.Drawing.Size(322, 422);
            this._groupboxUserInfo.TabIndex = 30;
            this._groupboxUserInfo.TabStop = false;
            this._groupboxUserInfo.Text = "User Information";
            // 
            // UserInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupboxUserRights);
            this.Controls.Add(this._buttonDelete);
            this.Controls.Add(this._buttonUpdate);
            this.Controls.Add(this._groupboxUserInfo);
            this.Name = "UserInfoControl";
            this.Size = new System.Drawing.Size(335, 637);
            this._groupboxUserRights.ResumeLayout(false);
            this._groupboxUserRights.PerformLayout();
            this._groupboxUserInfo.ResumeLayout(false);
            this._groupboxUserInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox _checkBoxCreateUpdateUsers;
        private System.Windows.Forms.Label _labelGroupName;
        private System.Windows.Forms.ComboBox _comboBoxGroupName;
        private System.Windows.Forms.Label _labelUserInfoInstructions;
        private System.Windows.Forms.TextBox _textBoxPassword;
        private System.Windows.Forms.Label _labelPassword;
        private System.Windows.Forms.TextBox _textBoxUsername;
        private System.Windows.Forms.Label _labelUserName;
        private System.Windows.Forms.CheckBox _checkBoxCanAdministerSystem;
        private System.Windows.Forms.CheckBox _checkBoxCanAdministerUsers;
        private System.Windows.Forms.CheckBox _checkBoxCreateUpdateGroups;
        private System.Windows.Forms.Label _labelFirstName;
        private System.Windows.Forms.TextBox _textBoxTitle;
        private System.Windows.Forms.Label _labelMiddleName;
        private System.Windows.Forms.GroupBox _groupboxUserRights;
        private System.Windows.Forms.Label _labelUserRightsInstructions;
        private System.Windows.Forms.CheckBox _checkBoxLoginEnabled;
        private System.Windows.Forms.CheckBox _checkBoxPublicAccessEnabled;
        private System.Windows.Forms.TextBox _textBoxOfficeLocation;
        private System.Windows.Forms.Label _labelOfficeLocation;
        private System.Windows.Forms.ComboBox _comboBoxTimeZone;
        private System.Windows.Forms.Label _labelLastName;
        private System.Windows.Forms.TextBox _textBoxFirstName;
        private System.Windows.Forms.Label _labelTimeZone;
        private System.Windows.Forms.Button _buttonDelete;
        private System.Windows.Forms.TextBox _textBoxMiddleName;
        private System.Windows.Forms.Label _labelFax;
        private System.Windows.Forms.TextBox _textBoxFax;
        private System.Windows.Forms.Label _labelEmail;
        private System.Windows.Forms.TextBox _textBoxLastName;
        private System.Windows.Forms.Label _labelPhone;
        private System.Windows.Forms.TextBox _textBoxPhone;
        private System.Windows.Forms.Label _labelTitle;
        private System.Windows.Forms.Button _buttonUpdate;
        private System.Windows.Forms.TextBox _textBoxEmail;
        private System.Windows.Forms.GroupBox _groupboxUserInfo;
    }
}
