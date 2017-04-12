namespace myAdminTool.SystemImplementation.OTContentServer.Forms
{
    partial class frmVersions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersions));
            this.dgv_Versions = new System.Windows.Forms.DataGridView();
            this.btn_DownloadDoc = new System.Windows.Forms.Button();
            this.btnOTCSCloseVersion = new System.Windows.Forms.Button();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDatum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateiname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErsteller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersionNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateityp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Versions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Versions
            // 
            this.dgv_Versions.AllowUserToAddRows = false;
            this.dgv_Versions.AllowUserToDeleteRows = false;
            this.dgv_Versions.AllowUserToResizeColumns = false;
            this.dgv_Versions.AllowUserToResizeRows = false;
            this.dgv_Versions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Versions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Versions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Versions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVersion,
            this.colDatum,
            this.colDateiname,
            this.colErsteller,
            this.colDocID,
            this.colVersionNum,
            this.colDateityp});
            this.dgv_Versions.EnableHeadersVisualStyles = false;
            this.dgv_Versions.Location = new System.Drawing.Point(12, 12);
            this.dgv_Versions.MultiSelect = false;
            this.dgv_Versions.Name = "dgv_Versions";
            this.dgv_Versions.ReadOnly = true;
            this.dgv_Versions.RowHeadersVisible = false;
            this.dgv_Versions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Versions.Size = new System.Drawing.Size(618, 218);
            this.dgv_Versions.TabIndex = 1;
            this.dgv_Versions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Versions_CellContentClick);
            // 
            // btn_DownloadDoc
            // 
            this.btn_DownloadDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_DownloadDoc.Location = new System.Drawing.Point(12, 236);
            this.btn_DownloadDoc.Name = "btn_DownloadDoc";
            this.btn_DownloadDoc.Size = new System.Drawing.Size(110, 23);
            this.btn_DownloadDoc.TabIndex = 0;
            this.btn_DownloadDoc.Text = "Datei herunterladen";
            this.btn_DownloadDoc.UseVisualStyleBackColor = true;
            this.btn_DownloadDoc.Click += new System.EventHandler(this.btn_DownloadDoc_Click);
            // 
            // btnOTCSCloseVersion
            // 
            this.btnOTCSCloseVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOTCSCloseVersion.Location = new System.Drawing.Point(555, 236);
            this.btnOTCSCloseVersion.Name = "btnOTCSCloseVersion";
            this.btnOTCSCloseVersion.Size = new System.Drawing.Size(75, 23);
            this.btnOTCSCloseVersion.TabIndex = 2;
            this.btnOTCSCloseVersion.Text = "Schliessen";
            this.btnOTCSCloseVersion.UseVisualStyleBackColor = true;
            this.btnOTCSCloseVersion.Click += new System.EventHandler(this.btnOTCSCloseVersion_Click);
            // 
            // colVersion
            // 
            this.colVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colVersion.HeaderText = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.ReadOnly = true;
            this.colVersion.Width = 67;
            // 
            // colDatum
            // 
            this.colDatum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDatum.HeaderText = "Erstelldatum";
            this.colDatum.Name = "colDatum";
            this.colDatum.ReadOnly = true;
            this.colDatum.Width = 89;
            // 
            // colDateiname
            // 
            this.colDateiname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDateiname.HeaderText = "Dateiname";
            this.colDateiname.Name = "colDateiname";
            this.colDateiname.ReadOnly = true;
            this.colDateiname.Width = 83;
            // 
            // colErsteller
            // 
            this.colErsteller.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colErsteller.HeaderText = "Ersteller";
            this.colErsteller.Name = "colErsteller";
            this.colErsteller.ReadOnly = true;
            this.colErsteller.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colErsteller.Width = 69;
            // 
            // colDocID
            // 
            this.colDocID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDocID.HeaderText = "DokumentID";
            this.colDocID.Name = "colDocID";
            this.colDocID.ReadOnly = true;
            this.colDocID.Width = 92;
            // 
            // colVersionNum
            // 
            this.colVersionNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colVersionNum.HeaderText = "Version";
            this.colVersionNum.Name = "colVersionNum";
            this.colVersionNum.ReadOnly = true;
            this.colVersionNum.Width = 67;
            // 
            // colDateityp
            // 
            this.colDateityp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDateityp.HeaderText = "Dateityp";
            this.colDateityp.Name = "colDateityp";
            this.colDateityp.ReadOnly = true;
            this.colDateityp.Width = 71;
            // 
            // frmVersions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 264);
            this.Controls.Add(this.btnOTCSCloseVersion);
            this.Controls.Add(this.btn_DownloadDoc);
            this.Controls.Add(this.dgv_Versions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVersions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Versionen zu Dokument:";
            this.Load += new System.EventHandler(this.frmVersions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Versions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Versions;
        private System.Windows.Forms.Button btn_DownloadDoc;
        private System.Windows.Forms.Button btnOTCSCloseVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDatum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateiname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErsteller;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersionNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateityp;
    }
}