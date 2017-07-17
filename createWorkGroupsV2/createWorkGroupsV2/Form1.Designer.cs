namespace createWorkGroupsV2
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWGStatement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.rbPrimaryUser = new System.Windows.Forms.RadioButton();
            this.rbSelectUser = new System.Windows.Forms.RadioButton();
            this.txtUserStatement = new System.Windows.Forms.TextBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.btnLoadUser = new System.Windows.Forms.Button();
            this.btnCreateWorkGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWGStatement
            // 
            this.txtWGStatement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWGStatement.Location = new System.Drawing.Point(1, 22);
            this.txtWGStatement.Multiline = true;
            this.txtWGStatement.Name = "txtWGStatement";
            this.txtWGStatement.Size = new System.Drawing.Size(564, 84);
            this.txtWGStatement.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WorkGroup Statement:";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPreview.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Location = new System.Drawing.Point(1, 141);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.RowHeadersVisible = false;
            this.dgvPreview.Size = new System.Drawing.Size(564, 287);
            this.dgvPreview.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(1, 112);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(564, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Vorschau laden";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // rbPrimaryUser
            // 
            this.rbPrimaryUser.AutoSize = true;
            this.rbPrimaryUser.Checked = true;
            this.rbPrimaryUser.Location = new System.Drawing.Point(15, 434);
            this.rbPrimaryUser.Name = "rbPrimaryUser";
            this.rbPrimaryUser.Size = new System.Drawing.Size(202, 17);
            this.rbPrimaryUser.TabIndex = 4;
            this.rbPrimaryUser.TabStop = true;
            this.rbPrimaryUser.Text = "User über primäre Organisation finden";
            this.rbPrimaryUser.UseVisualStyleBackColor = true;
            // 
            // rbSelectUser
            // 
            this.rbSelectUser.AutoSize = true;
            this.rbSelectUser.Location = new System.Drawing.Point(15, 457);
            this.rbSelectUser.Name = "rbSelectUser";
            this.rbSelectUser.Size = new System.Drawing.Size(187, 17);
            this.rbSelectUser.TabIndex = 5;
            this.rbSelectUser.Text = "User über Select Statement finden";
            this.rbSelectUser.UseVisualStyleBackColor = true;
            this.rbSelectUser.CheckedChanged += new System.EventHandler(this.rbSelectUser_CheckedChanged);
            // 
            // txtUserStatement
            // 
            this.txtUserStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserStatement.Location = new System.Drawing.Point(223, 431);
            this.txtUserStatement.Multiline = true;
            this.txtUserStatement.Name = "txtUserStatement";
            this.txtUserStatement.Size = new System.Drawing.Size(342, 43);
            this.txtUserStatement.TabIndex = 6;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUser.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Location = new System.Drawing.Point(1, 508);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.Size = new System.Drawing.Size(564, 90);
            this.dgvUser.TabIndex = 7;
            // 
            // btnLoadUser
            // 
            this.btnLoadUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadUser.Location = new System.Drawing.Point(1, 480);
            this.btnLoadUser.Name = "btnLoadUser";
            this.btnLoadUser.Size = new System.Drawing.Size(564, 23);
            this.btnLoadUser.TabIndex = 8;
            this.btnLoadUser.Text = "Benuter laden";
            this.btnLoadUser.UseVisualStyleBackColor = true;
            this.btnLoadUser.Click += new System.EventHandler(this.btnLoadUser_Click);
            // 
            // btnCreateWorkGroup
            // 
            this.btnCreateWorkGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWorkGroup.Location = new System.Drawing.Point(1, 604);
            this.btnCreateWorkGroup.Name = "btnCreateWorkGroup";
            this.btnCreateWorkGroup.Size = new System.Drawing.Size(564, 23);
            this.btnCreateWorkGroup.TabIndex = 9;
            this.btnCreateWorkGroup.Text = "Arbeitsgruppen anlegen und Benutzer zuordnen";
            this.btnCreateWorkGroup.UseVisualStyleBackColor = true;
            this.btnCreateWorkGroup.Click += new System.EventHandler(this.btnCreateWorkGroup_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 633);
            this.Controls.Add(this.btnCreateWorkGroup);
            this.Controls.Add(this.btnLoadUser);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.txtUserStatement);
            this.Controls.Add(this.rbSelectUser);
            this.Controls.Add(this.rbPrimaryUser);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dgvPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWGStatement);
            this.Name = "frmMain";
            this.Text = "createWorkGroups";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWGStatement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.RadioButton rbPrimaryUser;
        private System.Windows.Forms.RadioButton rbSelectUser;
        private System.Windows.Forms.TextBox txtUserStatement;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Button btnLoadUser;
        private System.Windows.Forms.Button btnCreateWorkGroup;
    }
}

