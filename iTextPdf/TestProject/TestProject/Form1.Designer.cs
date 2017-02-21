namespace TestProject
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddWatermark = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnLoadPDF = new System.Windows.Forms.Button();
            this.txtWatermark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddWatermark2 = new System.Windows.Forms.Button();
            this.txtWatermark2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblITextSharpAllPages = new System.Windows.Forms.Label();
            this.lblITextSharpFirstPage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddWatermark
            // 
            this.btnAddWatermark.Enabled = false;
            this.btnAddWatermark.Location = new System.Drawing.Point(298, 24);
            this.btnAddWatermark.Name = "btnAddWatermark";
            this.btnAddWatermark.Size = new System.Drawing.Size(178, 23);
            this.btnAddWatermark.TabIndex = 0;
            this.btnAddWatermark.Text = "add Watermark (all Pages)";
            this.btnAddWatermark.UseVisualStyleBackColor = true;
            this.btnAddWatermark.Click += new System.EventHandler(this.btnAddWatermark_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(364, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnLoadPDF
            // 
            this.btnLoadPDF.Location = new System.Drawing.Point(382, 9);
            this.btnLoadPDF.Name = "btnLoadPDF";
            this.btnLoadPDF.Size = new System.Drawing.Size(95, 23);
            this.btnLoadPDF.TabIndex = 2;
            this.btnLoadPDF.Text = "load PDF";
            this.btnLoadPDF.UseVisualStyleBackColor = true;
            this.btnLoadPDF.Click += new System.EventHandler(this.btnLoadPDF_Click);
            // 
            // txtWatermark
            // 
            this.txtWatermark.Location = new System.Drawing.Point(78, 24);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(214, 20);
            this.txtWatermark.TabIndex = 3;
            this.txtWatermark.Text = "TOP SECRET";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Watermark:";
            // 
            // btnAddWatermark2
            // 
            this.btnAddWatermark2.Enabled = false;
            this.btnAddWatermark2.Location = new System.Drawing.Point(298, 50);
            this.btnAddWatermark2.Name = "btnAddWatermark2";
            this.btnAddWatermark2.Size = new System.Drawing.Size(178, 23);
            this.btnAddWatermark2.TabIndex = 5;
            this.btnAddWatermark2.Text = "add Watermark (first Page)";
            this.btnAddWatermark2.UseVisualStyleBackColor = true;
            this.btnAddWatermark2.Click += new System.EventHandler(this.btnAddWatermark2_Click);
            // 
            // txtWatermark2
            // 
            this.txtWatermark2.Location = new System.Drawing.Point(78, 50);
            this.txtWatermark2.Name = "txtWatermark2";
            this.txtWatermark2.Size = new System.Drawing.Size(214, 20);
            this.txtWatermark2.TabIndex = 6;
            this.txtWatermark2.Text = "CONFIDENTIAL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Watermark:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblITextSharpFirstPage);
            this.groupBox1.Controls.Add(this.lblITextSharpAllPages);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnAddWatermark);
            this.groupBox1.Controls.Add(this.txtWatermark2);
            this.groupBox1.Controls.Add(this.txtWatermark);
            this.groupBox1.Controls.Add(this.btnAddWatermark2);
            this.groupBox1.Location = new System.Drawing.Point(0, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 84);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "iTextSharp";
            // 
            // lblITextSharpAllPages
            // 
            this.lblITextSharpAllPages.AutoSize = true;
            this.lblITextSharpAllPages.Location = new System.Drawing.Point(482, 29);
            this.lblITextSharpAllPages.Name = "lblITextSharpAllPages";
            this.lblITextSharpAllPages.Size = new System.Drawing.Size(70, 13);
            this.lblITextSharpAllPages.TabIndex = 9;
            this.lblITextSharpAllPages.Text = "Milliseconds: ";
            // 
            // lblITextSharpFirstPage
            // 
            this.lblITextSharpFirstPage.AutoSize = true;
            this.lblITextSharpFirstPage.Location = new System.Drawing.Point(482, 55);
            this.lblITextSharpFirstPage.Name = "lblITextSharpFirstPage";
            this.lblITextSharpFirstPage.Size = new System.Drawing.Size(70, 13);
            this.lblITextSharpFirstPage.TabIndex = 10;
            this.lblITextSharpFirstPage.Text = "Milliseconds: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 266);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadPDF);
            this.Controls.Add(this.txtFilePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(502, 135);
            this.Name = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddWatermark;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnLoadPDF;
        private System.Windows.Forms.TextBox txtWatermark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddWatermark2;
        private System.Windows.Forms.TextBox txtWatermark2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblITextSharpFirstPage;
        private System.Windows.Forms.Label lblITextSharpAllPages;
    }
}

