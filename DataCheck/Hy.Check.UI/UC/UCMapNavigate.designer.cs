
namespace Hy.Check.UI.UC
{
    partial class UCMapNavigate
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
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.butIn = new DevExpress.XtraEditors.SimpleButton();
            this.butOut = new DevExpress.XtraEditors.SimpleButton();
            this.butFull = new DevExpress.XtraEditors.SimpleButton();
            this.butDown = new DevExpress.XtraEditors.SimpleButton();
            this.butUp = new DevExpress.XtraEditors.SimpleButton();
            this.butRight = new DevExpress.XtraEditors.SimpleButton();
            this.butLeft = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = null;
            this.trackBarControl1.Location = new System.Drawing.Point(6, 76);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.trackBarControl1.Properties.Appearance.Options.UseBackColor = true;
            this.trackBarControl1.Properties.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarControl1.Properties.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarControl1.Size = new System.Drawing.Size(42, 152);
            this.trackBarControl1.TabIndex = 8;
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.trackBarControl1_EditValueChanged);
            // 
            // butIn
            // 
            this.butIn.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.butIn.Location = new System.Drawing.Point(18, 225);
            this.butIn.Name = "butIn";
            this.butIn.Size = new System.Drawing.Size(21, 21);
            this.butIn.TabIndex = 6;
            this.butIn.ToolTip = "缩小地图";
            this.butIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butOut
            // 
            this.butOut.Image = global::Hy.Check.UI.Properties.Resources.ZoomOut;
            this.butOut.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.butOut.Location = new System.Drawing.Point(18, 58);
            this.butOut.Name = "butOut";
            this.butOut.Size = new System.Drawing.Size(21, 21);
            this.butOut.TabIndex = 5;
            this.butOut.ToolTip = "放大地图";
            this.butOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butFull
            // 
            this.butFull.Image = global::Hy.Check.UI.Properties.Resources.Full;
            this.butFull.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.butFull.Location = new System.Drawing.Point(16, 16);
            this.butFull.Name = "butFull";
            this.butFull.Size = new System.Drawing.Size(25, 25);
            this.butFull.TabIndex = 4;
            this.butFull.ToolTip = "全地图查看";
            this.butFull.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butDown
            // 
            this.butDown.Image = global::Hy.Check.UI.Properties.Resources.Down;
            this.butDown.Location = new System.Drawing.Point(19, 38);
            this.butDown.Name = "butDown";
            this.butDown.Size = new System.Drawing.Size(19, 19);
            this.butDown.TabIndex = 3;
            this.butDown.ToolTip = "向下平移地图";
            this.butDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butUp
            // 
            this.butUp.Image = global::Hy.Check.UI.Properties.Resources.Up;
            this.butUp.Location = new System.Drawing.Point(19, 0);
            this.butUp.Name = "butUp";
            this.butUp.Size = new System.Drawing.Size(19, 19);
            this.butUp.TabIndex = 2;
            this.butUp.ToolTip = "向上平移地图";
            this.butUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butRight
            // 
            this.butRight.Image = global::Hy.Check.UI.Properties.Resources.Right;
            this.butRight.Location = new System.Drawing.Point(39, 19);
            this.butRight.Name = "butRight";
            this.butRight.Size = new System.Drawing.Size(19, 19);
            this.butRight.TabIndex = 1;
            this.butRight.ToolTip = "向右平移地图";
            this.butRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // butLeft
            // 
            this.butLeft.Image = global::Hy.Check.UI.Properties.Resources.Left;
            this.butLeft.Location = new System.Drawing.Point(-1, 19);
            this.butLeft.Name = "butLeft";
            this.butLeft.Size = new System.Drawing.Size(19, 19);
            this.butLeft.TabIndex = 0;
            this.butLeft.ToolTip = "向左平移地图";
            this.butLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NavigateMouseDown);
            // 
            // UCMapNavigate
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBarControl1);
            this.Controls.Add(this.butIn);
            this.Controls.Add(this.butOut);
            this.Controls.Add(this.butFull);
            this.Controls.Add(this.butDown);
            this.Controls.Add(this.butUp);
            this.Controls.Add(this.butRight);
            this.Controls.Add(this.butLeft);
            this.Name = "UCMapNavigate";
            this.Size = new System.Drawing.Size(57, 244);
            this.Resize += new System.EventHandler(this.UCMapNav_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton butLeft;
        private DevExpress.XtraEditors.SimpleButton butRight;
        private DevExpress.XtraEditors.SimpleButton butUp;
        private DevExpress.XtraEditors.SimpleButton butDown;
        private DevExpress.XtraEditors.SimpleButton butFull;
        private DevExpress.XtraEditors.SimpleButton butOut;
        private DevExpress.XtraEditors.SimpleButton butIn;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
    }
}
