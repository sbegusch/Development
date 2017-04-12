namespace myAdminTool.SystemImplementation.OTContentServer.Forms
{
    partial class frmCategories
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCategories));
            this.TreeViewCategories = new System.Windows.Forms.TreeView();
            this.btn_Uebernehmen = new System.Windows.Forms.Button();
            this.btn_abbrechen = new System.Windows.Forms.Button();
            this.TreeIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // TreeViewCategories
            // 
            this.TreeViewCategories.Location = new System.Drawing.Point(12, 12);
            this.TreeViewCategories.Name = "TreeViewCategories";
            this.TreeViewCategories.Size = new System.Drawing.Size(281, 376);
            this.TreeViewCategories.TabIndex = 0;
            this.TreeViewCategories.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewCategories_BeforeExpand);
            // 
            // btn_Uebernehmen
            // 
            this.btn_Uebernehmen.Image = ((System.Drawing.Image)(resources.GetObject("btn_Uebernehmen.Image")));
            this.btn_Uebernehmen.Location = new System.Drawing.Point(12, 394);
            this.btn_Uebernehmen.Name = "btn_Uebernehmen";
            this.btn_Uebernehmen.Size = new System.Drawing.Size(43, 23);
            this.btn_Uebernehmen.TabIndex = 1;
            this.btn_Uebernehmen.UseVisualStyleBackColor = true;
            this.btn_Uebernehmen.Click += new System.EventHandler(this.btn_Uebernehmen_Click);
            // 
            // btn_abbrechen
            // 
            this.btn_abbrechen.Image = ((System.Drawing.Image)(resources.GetObject("btn_abbrechen.Image")));
            this.btn_abbrechen.Location = new System.Drawing.Point(60, 394);
            this.btn_abbrechen.Name = "btn_abbrechen";
            this.btn_abbrechen.Size = new System.Drawing.Size(43, 23);
            this.btn_abbrechen.TabIndex = 2;
            this.btn_abbrechen.UseVisualStyleBackColor = true;
            this.btn_abbrechen.Click += new System.EventHandler(this.btn_abbrechen_Click);
            // 
            // TreeIcons
            // 
            this.TreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeIcons.ImageStream")));
            this.TreeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeIcons.Images.SetKeyName(0, "category16.gif");
            this.TreeIcons.Images.SetKeyName(1, "discussion16.gif");
            this.TreeIcons.Images.SetKeyName(2, "document16.png");
            this.TreeIcons.Images.SetKeyName(3, "folder16.png");
            this.TreeIcons.Images.SetKeyName(4, "poll16.gif");
            this.TreeIcons.Images.SetKeyName(5, "project16.gif");
            this.TreeIcons.Images.SetKeyName(6, "report16.gif");
            this.TreeIcons.Images.SetKeyName(7, "shortcut16.gif");
            this.TreeIcons.Images.SetKeyName(8, "task16.gif");
            this.TreeIcons.Images.SetKeyName(9, "taskgroup16.gif");
            this.TreeIcons.Images.SetKeyName(10, "tasklist16.gif");
            this.TreeIcons.Images.SetKeyName(11, "milestone16.png");
            this.TreeIcons.Images.SetKeyName(12, "url16.gif");
            this.TreeIcons.Images.SetKeyName(13, "16topic.png");
            this.TreeIcons.Images.SetKeyName(14, "16reply.png");
            this.TreeIcons.Images.SetKeyName(15, "unknown16.png");
            // 
            // frmCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 424);
            this.Controls.Add(this.btn_abbrechen);
            this.Controls.Add(this.btn_Uebernehmen);
            this.Controls.Add(this.TreeViewCategories);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCategories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Categories";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCategories_FormClosing);
            this.Load += new System.EventHandler(this.Categories_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreeViewCategories;
        private System.Windows.Forms.Button btn_Uebernehmen;
        private System.Windows.Forms.Button btn_abbrechen;
        private System.Windows.Forms.ImageList TreeIcons;
    }
}