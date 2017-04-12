using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace myAdminTool.OTCS
{
    public partial class frmOTLoginForm : Form
    {
        public frmOTLoginForm()
        {
            InitializeComponent();
            
            fAuthenticationModeComboBox.DataSource = Enum.GetValues(typeof(AuthenticationMode));

            LoadSettings();
        }

        /// <summary>
        /// Load the user login settings.
        /// </summary>
        private void LoadSettings()
        {
            Properties.Settings props = Properties.Settings.Default;
            
            this.fUrlTextbox.Text = props.URL;

            this.fAuthenticationModeComboBox.SelectedItem = AuthenticationMode.CWSAuthentication;

            try
            {
                this.fAuthenticationModeComboBox.SelectedItem = Enum.Parse(typeof(AuthenticationMode), props.AuthenticationMethod);
            }
            catch (ArgumentException) { }

            this.fRCSAuthURLTextBox.Text = props.RCSAuthURL;

            this.fUserNameTextbox.Text = props.UserName;
            this.fPasswordTextbox.Text = Util.Decrypt(props.Password);
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings props = Properties.Settings.Default;

            props.URL = this.fUrlTextbox.Text;
            props.AuthenticationMethod = ((AuthenticationMode)this.fAuthenticationModeComboBox.SelectedItem).ToString();
            props.RCSAuthURL = this.fRCSAuthURLTextBox.Text;

            props.UserName = this.fUserNameTextbox.Text;
            props.Password = Util.Encrypt(this.fPasswordTextbox.Text);

            props.Save();
        }

        private void fCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void fLoginButton_Click(object sender, EventArgs e)
        {
            SaveSettings();

            this.DialogResult = DialogResult.OK;
        }

        private void fAuthenticationModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fAuthenticationModeComboBox.SelectedItem.Equals(AuthenticationMode.RCSAuthentication) || 
                fAuthenticationModeComboBox.SelectedItem.Equals(AuthenticationMode.LegacyRCSAuthentication))
            {
                fRCSAuthURLTextBox.Enabled = true;
            }
            else
            {
                fRCSAuthURLTextBox.Enabled = false;
            }
        }
    }
}