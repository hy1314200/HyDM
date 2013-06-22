namespace Frame
{
    partial class FrmResourceRegister
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.ucResourceRegister1 = new Frame.UCResourceRegister();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnOK.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnOK.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnOK.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnOK.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnOK.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnOK.Location = new System.Drawing.Point(591, 500);
            this.btnOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnClose.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnClose.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnClose.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnClose.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnClose.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnClose.Location = new System.Drawing.Point(704, 500);
            this.btnClose.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // ucResourceRegister1
            // 
            this.ucResourceRegister1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucResourceRegister1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResourceRegister1.Location = new System.Drawing.Point(0, 0);
            this.ucResourceRegister1.Name = "ucResourceRegister1";
            this.ucResourceRegister1.Size = new System.Drawing.Size(791, 496);
            this.ucResourceRegister1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(2, 505);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 12);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "消息";
            // 
            // FrmResourceRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 526);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucResourceRegister1);
            this.Name = "FrmResourceRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "资源注册";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCResourceRegister ucResourceRegister1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Label lblStatus;
    }
}