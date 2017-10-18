namespace moveToFolder
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
            this.dgvBIG_FOLDER_RESTORE_TMP = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDOMEAConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvWorkGroups = new System.Windows.Forms.DataGridView();
            this.dgvCreateFolder = new System.Windows.Forms.DataGridView();
            this.btnCreateFolder = new System.Windows.Forms.Button();
            this.lblWorkGroups = new System.Windows.Forms.Label();
            this.lblFolder4Workgroup = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBIG_FOLDER_RESTORE_TMP)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreateFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBIG_FOLDER_RESTORE_TMP
            // 
            this.dgvBIG_FOLDER_RESTORE_TMP.AllowUserToAddRows = false;
            this.dgvBIG_FOLDER_RESTORE_TMP.AllowUserToResizeRows = false;
            this.dgvBIG_FOLDER_RESTORE_TMP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBIG_FOLDER_RESTORE_TMP.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBIG_FOLDER_RESTORE_TMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBIG_FOLDER_RESTORE_TMP.Location = new System.Drawing.Point(3, 3);
            this.dgvBIG_FOLDER_RESTORE_TMP.Name = "dgvBIG_FOLDER_RESTORE_TMP";
            this.dgvBIG_FOLDER_RESTORE_TMP.RowHeadersVisible = false;
            this.dgvBIG_FOLDER_RESTORE_TMP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBIG_FOLDER_RESTORE_TMP.Size = new System.Drawing.Size(956, 417);
            this.dgvBIG_FOLDER_RESTORE_TMP.TabIndex = 0;
            this.dgvBIG_FOLDER_RESTORE_TMP.BindingContextChanged += new System.EventHandler(this.dgvBIG_FOLDER_RESTORE_TMP_BindingContextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDOMEAConnection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDOMEAConnection
            // 
            this.lblDOMEAConnection.Name = "lblDOMEAConnection";
            this.lblDOMEAConnection.Size = new System.Drawing.Size(118, 17);
            this.lblDOMEAConnection.Text = "toolStripStatusLabel1";
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowCount.Location = new System.Drawing.Point(6, 432);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(41, 13);
            this.lblRowCount.TabIndex = 2;
            this.lblRowCount.Text = "label1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(970, 482);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.dgvBIG_FOLDER_RESTORE_TMP);
            this.tabPage1.Controls.Add(this.lblRowCount);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(962, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "BIG_FOLDER_RESTORE_TMP";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(962, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "create Folder";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblWorkGroups);
            this.splitContainer1.Panel1.Controls.Add(this.dgvWorkGroups);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCreateFolder);
            this.splitContainer1.Panel2.Controls.Add(this.btnCreateFolder);
            this.splitContainer1.Panel2.Controls.Add(this.lblFolder4Workgroup);
            this.splitContainer1.Size = new System.Drawing.Size(950, 446);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgvWorkGroups
            // 
            this.dgvWorkGroups.AllowUserToAddRows = false;
            this.dgvWorkGroups.AllowUserToResizeRows = false;
            this.dgvWorkGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkGroups.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvWorkGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkGroups.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvWorkGroups.Location = new System.Drawing.Point(0, 0);
            this.dgvWorkGroups.Name = "dgvWorkGroups";
            this.dgvWorkGroups.RowHeadersVisible = false;
            this.dgvWorkGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkGroups.Size = new System.Drawing.Size(316, 417);
            this.dgvWorkGroups.TabIndex = 0;
            this.dgvWorkGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWorkGroups_CellDoubleClick);
            // 
            // dgvCreateFolder
            // 
            this.dgvCreateFolder.AllowUserToAddRows = false;
            this.dgvCreateFolder.AllowUserToResizeRows = false;
            this.dgvCreateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCreateFolder.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCreateFolder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreateFolder.Location = new System.Drawing.Point(0, 0);
            this.dgvCreateFolder.Name = "dgvCreateFolder";
            this.dgvCreateFolder.RowHeadersVisible = false;
            this.dgvCreateFolder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCreateFolder.Size = new System.Drawing.Size(630, 417);
            this.dgvCreateFolder.TabIndex = 1;
            // 
            // btnCreateFolder
            // 
            this.btnCreateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateFolder.Location = new System.Drawing.Point(513, 420);
            this.btnCreateFolder.Name = "btnCreateFolder";
            this.btnCreateFolder.Size = new System.Drawing.Size(114, 23);
            this.btnCreateFolder.TabIndex = 0;
            this.btnCreateFolder.Text = "Create Folder";
            this.btnCreateFolder.UseVisualStyleBackColor = true;
            this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
            // 
            // lblWorkGroups
            // 
            this.lblWorkGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWorkGroups.AutoSize = true;
            this.lblWorkGroups.Location = new System.Drawing.Point(3, 425);
            this.lblWorkGroups.Name = "lblWorkGroups";
            this.lblWorkGroups.Size = new System.Drawing.Size(35, 13);
            this.lblWorkGroups.TabIndex = 1;
            this.lblWorkGroups.Text = "label1";
            // 
            // lblFolder4Workgroup
            // 
            this.lblFolder4Workgroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFolder4Workgroup.AutoSize = true;
            this.lblFolder4Workgroup.Location = new System.Drawing.Point(3, 425);
            this.lblFolder4Workgroup.Name = "lblFolder4Workgroup";
            this.lblFolder4Workgroup.Size = new System.Drawing.Size(35, 13);
            this.lblFolder4Workgroup.TabIndex = 2;
            this.lblFolder4Workgroup.Text = "TEST";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 504);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CR17DOMEA004 Repair";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBIG_FOLDER_RESTORE_TMP)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreateFolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBIG_FOLDER_RESTORE_TMP;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblDOMEAConnection;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.DataGridView dgvCreateFolder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvWorkGroups;
        private System.Windows.Forms.Label lblWorkGroups;
        private System.Windows.Forms.Label lblFolder4Workgroup;
    }
}

