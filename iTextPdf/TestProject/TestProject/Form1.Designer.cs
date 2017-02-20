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
            this.label2 = new System.Windows.Forms.Label();
            this.txtWatermark2 = new System.Windows.Forms.TextBox();
            this.btnAddWatermark2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddWatermark
            // 
            this.btnAddWatermark.Enabled = false;
            this.btnAddWatermark.Location = new System.Drawing.Point(299, 38);
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
            this.txtWatermark.Location = new System.Drawing.Point(79, 38);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(214, 20);
            this.txtWatermark.TabIndex = 3;
            this.txtWatermark.Text = "TOP SECRET";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Watermark:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Watermark:";
            // 
            // txtWatermark2
            // 
            this.txtWatermark2.Location = new System.Drawing.Point(79, 67);
            this.txtWatermark2.Name = "txtWatermark2";
            this.txtWatermark2.Size = new System.Drawing.Size(214, 20);
            this.txtWatermark2.TabIndex = 6;
            this.txtWatermark2.Text = "CONFIDENTIAL";
            // 
            // btnAddWatermark2
            // 
            this.btnAddWatermark2.Enabled = false;
            this.btnAddWatermark2.Location = new System.Drawing.Point(299, 67);
            this.btnAddWatermark2.Name = "btnAddWatermark2";
            this.btnAddWatermark2.Size = new System.Drawing.Size(178, 23);
            this.btnAddWatermark2.TabIndex = 5;
            this.btnAddWatermark2.Text = "add Watermark (first Page)";
            this.btnAddWatermark2.UseVisualStyleBackColor = true;
            this.btnAddWatermark2.Click += new System.EventHandler(this.btnAddWatermark2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 97);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWatermark2);
            this.Controls.Add(this.btnAddWatermark2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWatermark);
            this.Controls.Add(this.btnLoadPDF);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnAddWatermark);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(502, 135);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(502, 135);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddWatermark;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnLoadPDF;
        private System.Windows.Forms.TextBox txtWatermark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWatermark2;
        private System.Windows.Forms.Button btnAddWatermark2;
    }
}

