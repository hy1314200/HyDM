namespace Check.UI.UC
{
    partial class UCRulesTree
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
            this.radioType = new DevExpress.XtraEditors.RadioGroup();
            this.treeListRule = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.radioType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListRule)).BeginInit();
            this.SuspendLayout();
            // 
            // radioType
            // 
            this.radioType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioType.EditValue = 0;
            this.radioType.Location = new System.Drawing.Point(0, 0);
            this.radioType.Name = "radioType";
            this.radioType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "按检查类型分类"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "按图层分类")});
            this.radioType.Size = new System.Drawing.Size(224, 30);
            this.radioType.TabIndex = 0;
            this.radioType.SelectedIndexChanged += new System.EventHandler(this.radioType_SelectedIndexChanged);
            // 
            // treeListRule
            // 
            this.treeListRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListRule.Appearance.SelectedRow.BackColor = System.Drawing.Color.DodgerBlue;
            this.treeListRule.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeListRule.Location = new System.Drawing.Point(0, 30);
            this.treeListRule.Name = "treeListRule";
            this.treeListRule.OptionsBehavior.Editable = false;
            this.treeListRule.OptionsView.AutoWidth = false;
            this.treeListRule.OptionsView.ShowIndicator = false;
            this.treeListRule.Size = new System.Drawing.Size(224, 423);
            this.treeListRule.TabIndex = 1;
            this.treeListRule.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.treeListRule_BeforeCheckNode);
            this.treeListRule.Click += new System.EventHandler(this.treeListRule_Click);
            this.treeListRule.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListRule_AfterCheckNode);
            // 
            // UCRulesTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListRule);
            this.Controls.Add(this.radioType);
            this.Name = "UCRulesTree";
            this.Size = new System.Drawing.Size(224, 456);
            this.Resize += new System.EventHandler(this.UCRulesTree_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.radioType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListRule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioType;
        private DevExpress.XtraTreeList.TreeList treeListRule;
    }
}
