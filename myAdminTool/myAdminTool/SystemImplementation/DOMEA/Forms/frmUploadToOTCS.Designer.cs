namespace myAdminTool.SystemImplementation.DOMEA
{
    partial class frmUploadToOTCS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadToOTCS));
            this.dgvUpload = new System.Windows.Forms.DataGridView();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.colIGZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEinlaufzahl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPIComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colELZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderID = new System.Windows.Forms.TextBox();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpload)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUpload
            // 
            this.dgvUpload.AllowUserToAddRows = false;
            this.dgvUpload.AllowUserToResizeRows = false;
            this.dgvUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUpload.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.colIGZ,
            this.colEinlaufzahl,
            this.colGZ,
            this.colPIComment,
            this.colELZ,
            this.colDocComment});
            this.dgvUpload.Location = new System.Drawing.Point(2, 19);
            this.dgvUpload.Name = "dgvUpload";
            this.dgvUpload.RowHeadersVisible = false;
            this.dgvUpload.Size = new System.Drawing.Size(418, 101);
            this.dgvUpload.TabIndex = 0;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(1, 211);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(350, 211);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Abbrechen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = null;
            this.colChecked.CheckValueChecked = "1";
            this.colChecked.CheckValueUnchecked = "0";
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.Width = 20;
            // 
            // colIGZ
            // 
            this.colIGZ.HeaderText = "IGZ";
            this.colIGZ.Name = "colIGZ";
            this.colIGZ.Visible = false;
            // 
            // colEinlaufzahl
            // 
            this.colEinlaufzahl.HeaderText = "Einlaufzahl";
            this.colEinlaufzahl.Name = "colEinlaufzahl";
            this.colEinlaufzahl.Visible = false;
            // 
            // colGZ
            // 
            this.colGZ.HeaderText = "Aktenzahl";
            this.colGZ.Name = "colGZ";
            // 
            // colPIComment
            // 
            this.colPIComment.HeaderText = "Akt Anmerkung";
            this.colPIComment.Name = "colPIComment";
            this.colPIComment.Width = 150;
            // 
            // colELZ
            // 
            this.colELZ.HeaderText = "Schriftstücknummer";
            this.colELZ.Name = "colELZ";
            this.colELZ.Width = 125;
            // 
            // colDocComment
            // 
            this.colDocComment.HeaderText = "Dokument Anmerkung";
            this.colDocComment.Name = "colDocComment";
            this.colDocComment.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvUpload);
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 126);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DOMEA";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtFolderName);
            this.groupBox2.Controls.Add(this.txtFolderID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 70);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ContentServer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FolderID:";
            // 
            // txtFolderID
            // 
            this.txtFolderID.Location = new System.Drawing.Point(95, 17);
            this.txtFolderID.Name = "txtFolderID";
            this.txtFolderID.ReadOnly = true;
            this.txtFolderID.Size = new System.Drawing.Size(316, 20);
            this.txtFolderID.TabIndex = 1;
            this.txtFolderID.TabStop = false;
            this.txtFolderID.TextChanged += new System.EventHandler(this.txtFolderID_TextChanged);
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(95, 43);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.ReadOnly = true;
            this.txtFolderName.Size = new System.Drawing.Size(316, 20);
            this.txtFolderName.TabIndex = 2;
            this.txtFolderName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Foldername:";
            // 
            // frmUploadToOTCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 235);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUploadToOTCS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Upload DOMEA to OTCS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUploadToOTCS_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpload)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUpload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnClose;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIGZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEinlaufzahl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPIComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colELZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocComment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.TextBox txtFolderID;
        private System.Windows.Forms.Label label1;
    }
}