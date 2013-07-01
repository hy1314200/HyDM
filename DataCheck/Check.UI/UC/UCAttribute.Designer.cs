namespace Check.UI.UC
{
    partial class UCAttribute
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
            this.treeListAttribute = new DevExpress.XtraTreeList.TreeList();
            this.colFieldName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFieldValue = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeListAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListAttribute
            // 
            this.treeListAttribute.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colFieldName,
            this.colFieldValue});
            this.treeListAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListAttribute.Location = new System.Drawing.Point(0, 0);
            this.treeListAttribute.Name = "treeListAttribute";
            this.treeListAttribute.OptionsView.ShowIndicator = false;
            this.treeListAttribute.Size = new System.Drawing.Size(211, 418);
            this.treeListAttribute.TabIndex = 0;
            // 
            // colFieldName
            // 
            this.colFieldName.Caption = "×Ö¶Î";
            this.colFieldName.FieldName = "×Ö¶Î";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.Visible = true;
            this.colFieldName.VisibleIndex = 0;
            // 
            // colFieldValue
            // 
            this.colFieldValue.Caption = "Öµ";
            this.colFieldValue.FieldName = "Öµ";
            this.colFieldValue.Name = "colFieldValue";
            this.colFieldValue.Visible = true;
            this.colFieldValue.VisibleIndex = 1;
            // 
            // UCAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListAttribute);
            this.Name = "UCAttribute";
            this.Size = new System.Drawing.Size(211, 418);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAttribute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListAttribute;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFieldName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFieldValue;
    }
}
