namespace Skyline.Core.UI
{
    partial class FrmQueryObject
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryObject));
            this.grp_tree = new System.Windows.Forms.GroupBox();
            this.tree_dataSet = new System.Windows.Forms.TreeView();
            this.grp_where = new System.Windows.Forms.GroupBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txt_selectWhere = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.grp_tree.SuspendLayout();
            this.grp_where.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_tree
            // 
            this.grp_tree.BackColor = System.Drawing.Color.Transparent;
            this.grp_tree.Controls.Add(this.tree_dataSet);
            this.grp_tree.ForeColor = System.Drawing.Color.Navy;
            this.grp_tree.Location = new System.Drawing.Point(14, 111);
            this.grp_tree.Name = "grp_tree";
            this.grp_tree.Size = new System.Drawing.Size(251, 276);
            this.grp_tree.TabIndex = 2;
            this.grp_tree.TabStop = false;
            this.grp_tree.Text = "查询结果";
            // 
            // tree_dataSet
            // 
            this.tree_dataSet.BackColor = System.Drawing.SystemColors.Control;
            this.tree_dataSet.Location = new System.Drawing.Point(16, 21);
            this.tree_dataSet.Name = "tree_dataSet";
            this.tree_dataSet.Size = new System.Drawing.Size(220, 238);
            this.tree_dataSet.TabIndex = 0;
            this.tree_dataSet.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_dataSet_NodeMouseDoubleClick);
            // 
            // grp_where
            // 
            this.grp_where.BackColor = System.Drawing.Color.Transparent;
            this.grp_where.Controls.Add(this.simpleButton1);
            this.grp_where.Controls.Add(this.txt_selectWhere);
            this.grp_where.ForeColor = System.Drawing.Color.Navy;
            this.grp_where.Location = new System.Drawing.Point(14, 13);
            this.grp_where.Name = "grp_where";
            this.grp_where.Size = new System.Drawing.Size(251, 91);
            this.grp_where.TabIndex = 1;
            this.grp_where.TabStop = false;
            this.grp_where.Text = "查询";
            // 
            // simpleButton1
            // 
            this.simpleButton1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButton1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButton1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButton1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButton1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButton1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButton1.Location = new System.Drawing.Point(155, 49);
            this.simpleButton1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(81, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txt_selectWhere
            // 
            this.txt_selectWhere.BackColor = System.Drawing.SystemColors.Control;
            this.txt_selectWhere.Location = new System.Drawing.Point(15, 21);
            this.txt_selectWhere.Name = "txt_selectWhere";
            this.txt_selectWhere.Size = new System.Drawing.Size(221, 22);
            this.txt_selectWhere.TabIndex = 0;
            this.txt_selectWhere.Text = "请输入要查询的物体名称";
            this.txt_selectWhere.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_selectWhere_MouseClick);
            this.txt_selectWhere.Leave += new System.EventHandler(this.txt_selectWhere_Leave);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CatalogHome16.png");
            this.imageList1.Images.SetKeyName(1, "GeocodeAddressInspectorTool16.png");
            this.imageList1.Images.SetKeyName(2, "GeocodeAddressLocator16.png");
            // 
            // FrmQueryObject
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 400);
            this.Controls.Add(this.grp_tree);
            this.Controls.Add(this.grp_where);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmQueryObject";
            this.Text = "查询物体";
            this.TransparencyKey = System.Drawing.SystemColors.InactiveCaption;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmQueryObject_FormClosing);
            this.Load += new System.EventHandler(this.FrmQueryObject_Load);
            this.grp_tree.ResumeLayout(false);
            this.grp_where.ResumeLayout(false);
            this.grp_where.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_selectWhere;
        private System.Windows.Forms.GroupBox grp_where;
        private System.Windows.Forms.GroupBox grp_tree;
        private System.Windows.Forms.TreeView tree_dataSet;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ImageList imageList1;
    }
}