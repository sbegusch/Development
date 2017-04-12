namespace myAdminTool.OTCS
{
	partial class frmOTLoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOTLoginForm));
            this.fUrlLabel = new System.Windows.Forms.Label();
            this.fUrlTextbox = new System.Windows.Forms.TextBox();
            this.fLoginButton = new System.Windows.Forms.Button();
            this.fCancelButton = new System.Windows.Forms.Button();
            this.fUserNameLabel = new System.Windows.Forms.Label();
            this.fPasswordLabel = new System.Windows.Forms.Label();
            this.fUserNameTextbox = new System.Windows.Forms.TextBox();
            this.fPasswordTextbox = new System.Windows.Forms.TextBox();
            this.fAuthenticationModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fRCSAuthURLTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fUrlLabel
            // 
            this.fUrlLabel.AutoSize = true;
            this.fUrlLabel.Location = new System.Drawing.Point(11, 9);
            this.fUrlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fUrlLabel.Name = "fUrlLabel";
            this.fUrlLabel.Size = new System.Drawing.Size(168, 13);
            this.fUrlLabel.TabIndex = 0;
            this.fUrlLabel.Text = "CWS Authentication service URL:";
            // 
            // fUrlTextbox
            // 
            this.fUrlTextbox.Location = new System.Drawing.Point(183, 11);
            this.fUrlTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.fUrlTextbox.Name = "fUrlTextbox";
            this.fUrlTextbox.Size = new System.Drawing.Size(241, 20);
            this.fUrlTextbox.TabIndex = 7;
            // 
            // fLoginButton
            // 
            this.fLoginButton.Location = new System.Drawing.Point(248, 147);
            this.fLoginButton.Margin = new System.Windows.Forms.Padding(2);
            this.fLoginButton.Name = "fLoginButton";
            this.fLoginButton.Size = new System.Drawing.Size(89, 21);
            this.fLoginButton.TabIndex = 5;
            this.fLoginButton.Text = "Login";
            this.fLoginButton.UseVisualStyleBackColor = true;
            this.fLoginButton.Click += new System.EventHandler(this.fLoginButton_Click);
            // 
            // fCancelButton
            // 
            this.fCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.fCancelButton.Location = new System.Drawing.Point(341, 147);
            this.fCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.fCancelButton.Name = "fCancelButton";
            this.fCancelButton.Size = new System.Drawing.Size(83, 21);
            this.fCancelButton.TabIndex = 6;
            this.fCancelButton.Text = "Cancel";
            this.fCancelButton.UseVisualStyleBackColor = true;
            this.fCancelButton.Click += new System.EventHandler(this.fCancelButton_Click);
            // 
            // fUserNameLabel
            // 
            this.fUserNameLabel.AutoSize = true;
            this.fUserNameLabel.Location = new System.Drawing.Point(11, 91);
            this.fUserNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fUserNameLabel.Name = "fUserNameLabel";
            this.fUserNameLabel.Size = new System.Drawing.Size(58, 13);
            this.fUserNameLabel.TabIndex = 5;
            this.fUserNameLabel.Text = "Username:";
            // 
            // fPasswordLabel
            // 
            this.fPasswordLabel.AutoSize = true;
            this.fPasswordLabel.Location = new System.Drawing.Point(11, 115);
            this.fPasswordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fPasswordLabel.Name = "fPasswordLabel";
            this.fPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.fPasswordLabel.TabIndex = 6;
            this.fPasswordLabel.Text = "Password:";
            // 
            // fUserNameTextbox
            // 
            this.fUserNameTextbox.Location = new System.Drawing.Point(183, 88);
            this.fUserNameTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.fUserNameTextbox.Name = "fUserNameTextbox";
            this.fUserNameTextbox.Size = new System.Drawing.Size(241, 20);
            this.fUserNameTextbox.TabIndex = 1;
            // 
            // fPasswordTextbox
            // 
            this.fPasswordTextbox.Location = new System.Drawing.Point(183, 112);
            this.fPasswordTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.fPasswordTextbox.Name = "fPasswordTextbox";
            this.fPasswordTextbox.Size = new System.Drawing.Size(241, 20);
            this.fPasswordTextbox.TabIndex = 2;
            this.fPasswordTextbox.UseSystemPasswordChar = true;
            // 
            // fAuthenticationModeComboBox
            // 
            this.fAuthenticationModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fAuthenticationModeComboBox.FormattingEnabled = true;
            this.fAuthenticationModeComboBox.Items.AddRange(new object[] {
            "CWS Authentication",
            "RCS Authentication",
            "Single Sign On"});
            this.fAuthenticationModeComboBox.Location = new System.Drawing.Point(183, 36);
            this.fAuthenticationModeComboBox.Name = "fAuthenticationModeComboBox";
            this.fAuthenticationModeComboBox.Size = new System.Drawing.Size(121, 21);
            this.fAuthenticationModeComboBox.TabIndex = 8;
            this.fAuthenticationModeComboBox.SelectedIndexChanged += new System.EventHandler(this.fAuthenticationModeComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Authentication Mode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "RCS Authentication service URL:";
            // 
            // fRCSAuthURLTextBox
            // 
            this.fRCSAuthURLTextBox.Location = new System.Drawing.Point(183, 63);
            this.fRCSAuthURLTextBox.Name = "fRCSAuthURLTextBox";
            this.fRCSAuthURLTextBox.Size = new System.Drawing.Size(240, 20);
            this.fRCSAuthURLTextBox.TabIndex = 10;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.fLoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.fCancelButton;
            this.ClientSize = new System.Drawing.Size(435, 179);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fRCSAuthURLTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fAuthenticationModeComboBox);
            this.Controls.Add(this.fPasswordTextbox);
            this.Controls.Add(this.fUserNameTextbox);
            this.Controls.Add(this.fPasswordLabel);
            this.Controls.Add(this.fUserNameLabel);
            this.Controls.Add(this.fCancelButton);
            this.Controls.Add(this.fLoginButton);
            this.Controls.Add(this.fUrlTextbox);
            this.Controls.Add(this.fUrlLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label fUrlLabel;
		private System.Windows.Forms.TextBox fUrlTextbox;
		private System.Windows.Forms.Button fLoginButton;
        private System.Windows.Forms.Button fCancelButton;
		private System.Windows.Forms.Label fUserNameLabel;
        private System.Windows.Forms.Label fPasswordLabel;
		private System.Windows.Forms.TextBox fUserNameTextbox;
		private System.Windows.Forms.TextBox fPasswordTextbox;
        private System.Windows.Forms.ComboBox fAuthenticationModeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fRCSAuthURLTextBox;
	}
}