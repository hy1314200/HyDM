namespace Skyline.Core.UI
{
    partial class FrmQueryCoordinate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryCoordinate));
            this.grp_info = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_height = new System.Windows.Forms.Label();
            this.lab_latitude = new System.Windows.Forms.Label();
            this.lab_longitude = new System.Windows.Forms.Label();
            this.lab_Y = new System.Windows.Forms.Label();
            this.lab_X = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lab_info = new System.Windows.Forms.Label();
            this.grp_info.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_info
            // 
            this.grp_info.BackColor = System.Drawing.Color.Transparent;
            this.grp_info.Controls.Add(this.panel1);
            this.grp_info.ForeColor = System.Drawing.Color.Navy;
            this.grp_info.Location = new System.Drawing.Point(14, 28);
            this.grp_info.Name = "grp_info";
            this.grp_info.Size = new System.Drawing.Size(446, 161);
            this.grp_info.TabIndex = 0;
            this.grp_info.TabStop = false;
            this.grp_info.Text = "当前鼠标信息";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lab_height);
            this.panel1.Controls.Add(this.lab_latitude);
            this.panel1.Controls.Add(this.lab_longitude);
            this.panel1.Controls.Add(this.lab_Y);
            this.panel1.Controls.Add(this.lab_X);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 137);
            this.panel1.TabIndex = 0;
            // 
            // lab_height
            // 
            this.lab_height.AutoSize = true;
            this.lab_height.ForeColor = System.Drawing.Color.Red;
            this.lab_height.Location = new System.Drawing.Point(209, 106);
            this.lab_height.Name = "lab_height";
            this.lab_height.Size = new System.Drawing.Size(14, 14);
            this.lab_height.TabIndex = 1;
            this.lab_height.Text = "0";
            // 
            // lab_latitude
            // 
            this.lab_latitude.AutoSize = true;
            this.lab_latitude.ForeColor = System.Drawing.Color.Red;
            this.lab_latitude.Location = new System.Drawing.Point(209, 56);
            this.lab_latitude.Name = "lab_latitude";
            this.lab_latitude.Size = new System.Drawing.Size(14, 14);
            this.lab_latitude.TabIndex = 1;
            this.lab_latitude.Text = "0";
            // 
            // lab_longitude
            // 
            this.lab_longitude.AutoSize = true;
            this.lab_longitude.ForeColor = System.Drawing.Color.Red;
            this.lab_longitude.Location = new System.Drawing.Point(209, 17);
            this.lab_longitude.Name = "lab_longitude";
            this.lab_longitude.Size = new System.Drawing.Size(14, 14);
            this.lab_longitude.TabIndex = 1;
            this.lab_longitude.Text = "0";
            // 
            // lab_Y
            // 
            this.lab_Y.AutoSize = true;
            this.lab_Y.ForeColor = System.Drawing.Color.Red;
            this.lab_Y.Location = new System.Drawing.Point(209, 194);
            this.lab_Y.Name = "lab_Y";
            this.lab_Y.Size = new System.Drawing.Size(14, 14);
            this.lab_Y.TabIndex = 1;
            this.lab_Y.Text = "0";
            this.lab_Y.Visible = false;
            // 
            // lab_X
            // 
            this.lab_X.AutoSize = true;
            this.lab_X.ForeColor = System.Drawing.Color.Red;
            this.lab_X.Location = new System.Drawing.Point(209, 146);
            this.lab_X.Name = "lab_X";
            this.lab_X.Size = new System.Drawing.Size(14, 14);
            this.lab_X.TabIndex = 1;
            this.lab_X.Text = "0";
            this.lab_X.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(17, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "点击位置  地形高度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(17, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "点击位置  纬度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "点击位置  经度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(17, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "点击位置当前屏幕Y坐标：";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(17, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "点击位置当前屏幕X坐标：";
            this.label1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lab_info);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(14, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 69);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作说明";
            // 
            // lab_info
            // 
            this.lab_info.AutoSize = true;
            this.lab_info.ForeColor = System.Drawing.Color.Red;
            this.lab_info.Location = new System.Drawing.Point(19, 26);
            this.lab_info.Name = "lab_info";
            this.lab_info.Size = new System.Drawing.Size(347, 28);
            this.lab_info.TabIndex = 0;
            this.lab_info.Text = "鼠标点击地球要查询信息位置\r\n将显示 当前点击位置的屏幕当前坐标，经度，纬度，和地形高度";
            // 
            // FrmQueryCoordinate
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 292);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grp_info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmQueryCoordinate";
            this.Opacity = 0.85D;
            this.ShowIcon = false;
            this.Text = "查询坐标";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmQueryCoordinate_FormClosing);
            this.Load += new System.EventHandler(this.FrmQueryCoordinate_Load);
            this.grp_info.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_info;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_height;
        private System.Windows.Forms.Label lab_latitude;
        private System.Windows.Forms.Label lab_longitude;
        private System.Windows.Forms.Label lab_Y;
        private System.Windows.Forms.Label lab_X;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lab_info;
    }
}