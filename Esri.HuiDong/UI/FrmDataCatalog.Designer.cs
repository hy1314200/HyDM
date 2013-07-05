namespace Esri.HuiDong.UI
{
    partial class FrmDataCatalog
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 12);
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
            this.treeView1.Size = new System.Drawing.Size(200, 385);
            this.treeView1.TabIndex = 0;
            // 
            // FrmDataCatalog
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 501);
            this.Controls.Add(this.treeView1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmDataCatalog";
            this.Text = "FrmDataCatalog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
    }
}