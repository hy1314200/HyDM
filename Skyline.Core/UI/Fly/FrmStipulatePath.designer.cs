namespace Skyline.Core.UI
{
    partial class FrmStipulatePath
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("飞行对象", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStipulatePath));
            this.tree_Stipulate = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlyParam = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // tree_Stipulate
            // 
            this.tree_Stipulate.BackColor = System.Drawing.SystemColors.Control;
            this.tree_Stipulate.ImageIndex = 0;
            this.tree_Stipulate.ImageList = this.imageList1;
            this.tree_Stipulate.Location = new System.Drawing.Point(14, 13);
            this.tree_Stipulate.Name = "tree_Stipulate";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "root";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "飞行对象";
            this.tree_Stipulate.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tree_Stipulate.PathSeparator = "";
            this.tree_Stipulate.SelectedImageIndex = 0;
            this.tree_Stipulate.Size = new System.Drawing.Size(220, 293);
            this.tree_Stipulate.TabIndex = 0;
            this.tree_Stipulate.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tree_Stipulate_AfterLabelEdit);
            this.tree_Stipulate.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_Stipulate_NodeMouseDoubleClick_1);
            this.tree_Stipulate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tree_Stipulate_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3DFlyAeroplane16.png");
            this.imageList1.Images.SetKeyName(1, "FolderOpenState16.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.参数设置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // FlyParam
            // 
            this.FlyParam.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.FlyParam.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.FlyParam.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.FlyParam.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.FlyParam.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.FlyParam.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.FlyParam.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.FlyParam.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.FlyParam.DialogResult = System.Windows.Forms.DialogResult.None;
            this.FlyParam.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.FlyParam.Location = new System.Drawing.Point(196, 322);
            this.FlyParam.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.FlyParam.Name = "FlyParam";
            this.FlyParam.Size = new System.Drawing.Size(99, 27);
            this.FlyParam.TabIndex = 1;
            this.FlyParam.Text = "飞行参数设置";
            this.FlyParam.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.FlyParam.Visible = false;
            this.FlyParam.Click += new System.EventHandler(this.FlyParam_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // FrmStipulatePath
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 317);
            this.Controls.Add(this.tree_Stipulate);
            this.Controls.Add(this.FlyParam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmStipulatePath";
            this.Text = "规定路径";
            this.TransparencyKey = System.Drawing.SystemColors.Window;
            this.Load += new System.EventHandler(this.FrmStipulatePath_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree_Stipulate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton FlyParam;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}