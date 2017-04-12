namespace myAdminTool.OTContentServer.Controls
{
    partial class GroupInfoControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._labelGroupInfoInstructions = new System.Windows.Forms.Label();
            this._textBoxGroupName = new System.Windows.Forms.TextBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this._buttonDelete = new System.Windows.Forms.Button();
            this._buttonUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._labelGroupInfoInstructions);
            this.groupBox1.Controls.Add(this._textBoxGroupName);
            this.groupBox1.Controls.Add(this.groupLabel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 76);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group Information";
            // 
            // _labelGroupInfoInstructions
            // 
            this._labelGroupInfoInstructions.AutoSize = true;
            this._labelGroupInfoInstructions.Location = new System.Drawing.Point(6, 16);
            this._labelGroupInfoInstructions.Name = "_labelGroupInfoInstructions";
            this._labelGroupInfoInstructions.Size = new System.Drawing.Size(103, 13);
            this._labelGroupInfoInstructions.TabIndex = 5;
            this._labelGroupInfoInstructions.Text = "Enter a group name.";
            // 
            // _textBoxGroupName
            // 
            this._textBoxGroupName.Location = new System.Drawing.Point(79, 35);
            this._textBoxGroupName.Name = "_textBoxGroupName";
            this._textBoxGroupName.Size = new System.Drawing.Size(276, 20);
            this._textBoxGroupName.TabIndex = 1;
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.Location = new System.Drawing.Point(6, 38);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(67, 13);
            this.groupLabel.TabIndex = 0;
            this.groupLabel.Text = "Group Name";
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Location = new System.Drawing.Point(282, 85);
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Size = new System.Drawing.Size(82, 23);
            this._buttonDelete.TabIndex = 34;
            this._buttonDelete.Text = "Delete";
            this._buttonDelete.UseVisualStyleBackColor = true;
            this._buttonDelete.Click += new System.EventHandler(this._buttonDelete_Click);
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Location = new System.Drawing.Point(201, 85);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this._buttonUpdate.TabIndex = 33;
            this._buttonUpdate.Text = "Update";
            this._buttonUpdate.UseVisualStyleBackColor = true;
            this._buttonUpdate.Click += new System.EventHandler(this._buttonUpdate_Click);
            // 
            // GroupInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._buttonDelete);
            this.Controls.Add(this._buttonUpdate);
            this.Name = "GroupInfoControl";
            this.Size = new System.Drawing.Size(372, 115);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label _labelGroupInfoInstructions;
        private System.Windows.Forms.TextBox _textBoxGroupName;
        private System.Windows.Forms.Label groupLabel;
        private System.Windows.Forms.Button _buttonDelete;
        private System.Windows.Forms.Button _buttonUpdate;
    }
}
