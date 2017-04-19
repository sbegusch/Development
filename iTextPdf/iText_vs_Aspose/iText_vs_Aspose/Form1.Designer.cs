namespace iText_vs_Aspose
{
    partial class Form1
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
            this.btnLoadPdf = new System.Windows.Forms.Button();
            this.txtPdfFilePath = new System.Windows.Forms.TextBox();
            this.btnITextWatermark = new System.Windows.Forms.Button();
            this.lblMSiText = new System.Windows.Forms.Label();
            this.lblMSAspose = new System.Windows.Forms.Label();
            this.btnAsposeWatermark = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadPdf
            // 
            this.btnLoadPdf.Location = new System.Drawing.Point(228, 13);
            this.btnLoadPdf.Name = "btnLoadPdf";
            this.btnLoadPdf.Size = new System.Drawing.Size(75, 23);
            this.btnLoadPdf.TabIndex = 0;
            this.btnLoadPdf.Text = "load PDF";
            this.btnLoadPdf.UseVisualStyleBackColor = true;
            this.btnLoadPdf.Click += new System.EventHandler(this.btnLoadPdf_Click);
            // 
            // txtPdfFilePath
            // 
            this.txtPdfFilePath.Location = new System.Drawing.Point(12, 15);
            this.txtPdfFilePath.Name = "txtPdfFilePath";
            this.txtPdfFilePath.ReadOnly = true;
            this.txtPdfFilePath.Size = new System.Drawing.Size(210, 20);
            this.txtPdfFilePath.TabIndex = 1;
            // 
            // btnITextWatermark
            // 
            this.btnITextWatermark.Enabled = false;
            this.btnITextWatermark.Location = new System.Drawing.Point(54, 52);
            this.btnITextWatermark.Name = "btnITextWatermark";
            this.btnITextWatermark.Size = new System.Drawing.Size(142, 23);
            this.btnITextWatermark.TabIndex = 3;
            this.btnITextWatermark.Text = "addWaterMark (iText)";
            this.btnITextWatermark.UseVisualStyleBackColor = true;
            this.btnITextWatermark.Click += new System.EventHandler(this.btnITextWatermark_Click);
            // 
            // lblMSiText
            // 
            this.lblMSiText.AutoSize = true;
            this.lblMSiText.Location = new System.Drawing.Point(202, 57);
            this.lblMSiText.Name = "lblMSiText";
            this.lblMSiText.Size = new System.Drawing.Size(62, 13);
            this.lblMSiText.TabIndex = 5;
            this.lblMSiText.Text = "Miliseconds";
            // 
            // lblMSAspose
            // 
            this.lblMSAspose.AutoSize = true;
            this.lblMSAspose.Location = new System.Drawing.Point(202, 86);
            this.lblMSAspose.Name = "lblMSAspose";
            this.lblMSAspose.Size = new System.Drawing.Size(62, 13);
            this.lblMSAspose.TabIndex = 9;
            this.lblMSAspose.Text = "Miliseconds";
            // 
            // btnAsposeWatermark
            // 
            this.btnAsposeWatermark.Enabled = false;
            this.btnAsposeWatermark.Location = new System.Drawing.Point(54, 81);
            this.btnAsposeWatermark.Name = "btnAsposeWatermark";
            this.btnAsposeWatermark.Size = new System.Drawing.Size(142, 23);
            this.btnAsposeWatermark.TabIndex = 7;
            this.btnAsposeWatermark.Text = "addWaterMark (Aspose)";
            this.btnAsposeWatermark.UseVisualStyleBackColor = true;
            this.btnAsposeWatermark.Click += new System.EventHandler(this.btnAsposeWatermark_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 147);
            this.Controls.Add(this.lblMSAspose);
            this.Controls.Add(this.btnAsposeWatermark);
            this.Controls.Add(this.lblMSiText);
            this.Controls.Add(this.btnITextWatermark);
            this.Controls.Add(this.txtPdfFilePath);
            this.Controls.Add(this.btnLoadPdf);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadPdf;
        private System.Windows.Forms.TextBox txtPdfFilePath;
        private System.Windows.Forms.Button btnITextWatermark;
        private System.Windows.Forms.Label lblMSiText;
        private System.Windows.Forms.Label lblMSAspose;
        private System.Windows.Forms.Button btnAsposeWatermark;
    }
}

