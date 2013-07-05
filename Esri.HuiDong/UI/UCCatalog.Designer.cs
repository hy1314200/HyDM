namespace Esri.HuiDong.UI
{
    partial class UCCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCatalog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("行政区面");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("行政区线");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("行政区点");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("行政区注记");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("行政区", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("居民地面");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("居民地线");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("居民地点");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("居民地注记");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("居民地", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点19");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("测量控制", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点22");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("交通", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("节点23");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("水利", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("节点25");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("其它", new System.Windows.Forms.TreeNode[] {
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("1：500基础数据", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode10,
            treeNode12,
            treeNode14,
            treeNode16,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("节点26");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("1：1000基础数据", new System.Windows.Forms.TreeNode[] {
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("基础数据", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("土地利用现状");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("土地利用规划");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("道路红线图");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("专题数据", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("数据库目录", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode26});
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(209, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton1.Text = "刷新";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton2.Text = "打开";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton3.Text = "添加子目录";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton5.Text = "打开";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripButton4,
            this.toolStripButton7,
            this.toolStripButton8});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(209, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "导入";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton6.Text = "导入";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton4.Text = "导出";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton7.Text = "删除";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton8.Text = "字段";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 50);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点9";
            treeNode1.Text = "行政区面";
            treeNode2.Name = "节点10";
            treeNode2.Text = "行政区线";
            treeNode3.Name = "节点11";
            treeNode3.Text = "行政区点";
            treeNode4.Name = "节点12";
            treeNode4.Text = "行政区注记";
            treeNode5.Name = "节点8";
            treeNode5.Text = "行政区";
            treeNode6.Name = "节点15";
            treeNode6.Text = "居民地面";
            treeNode7.Name = "节点16";
            treeNode7.Text = "居民地线";
            treeNode8.Name = "节点17";
            treeNode8.Text = "居民地点";
            treeNode9.Name = "节点18";
            treeNode9.Text = "居民地注记";
            treeNode10.Name = "节点13";
            treeNode10.Text = "居民地";
            treeNode11.Name = "节点19";
            treeNode11.Text = "节点19";
            treeNode12.Name = "节点14";
            treeNode12.Text = "测量控制";
            treeNode13.Name = "节点22";
            treeNode13.Text = "节点22";
            treeNode14.Name = "节点20";
            treeNode14.Text = "交通";
            treeNode15.Name = "节点23";
            treeNode15.Text = "节点23";
            treeNode16.Name = "节点21";
            treeNode16.Text = "水利";
            treeNode17.Name = "节点25";
            treeNode17.Text = "节点25";
            treeNode18.Name = "节点24";
            treeNode18.Text = "其它";
            treeNode19.Name = "节点6";
            treeNode19.Text = "1：500基础数据";
            treeNode20.Name = "节点26";
            treeNode20.Text = "节点26";
            treeNode21.Name = "节点7";
            treeNode21.Text = "1：1000基础数据";
            treeNode22.Name = "节点1";
            treeNode22.Text = "基础数据";
            treeNode23.Name = "节点3";
            treeNode23.Text = "土地利用现状";
            treeNode24.Name = "节点4";
            treeNode24.Text = "土地利用规划";
            treeNode25.Name = "节点5";
            treeNode25.Text = "道路红线图";
            treeNode26.Name = "节点2";
            treeNode26.Text = "专题数据";
            treeNode27.Name = "节点0";
            treeNode27.Text = "数据库目录";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode27});
            this.treeView1.Size = new System.Drawing.Size(209, 448);
            this.treeView1.TabIndex = 3;
            // 
            // UCCatalog
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "UCCatalog";
            this.Size = new System.Drawing.Size(209, 498);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.TreeView treeView1;
    }
}
