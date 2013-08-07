namespace Hy.Esri.Catalog.UI
{
    partial class FrmFeatureClassExport
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
            this.ucClassPath1 = new Hy.Esri.Utility.UCClassOutPath();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // ucClassPath1
            // 
            this.ucClassPath1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ucClassPath1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ucClassPath1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.ucClassPath1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.ucClassPath1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.ucClassPath1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.ucClassPath1.Location = new System.Drawing.Point(12, 12);
            this.ucClassPath1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.ucClassPath1.Name = "ucClassPath1";
            this.ucClassPath1.RasterName = "";
            this.ucClassPath1.Size = new System.Drawing.Size(387, 123);
            this.ucClassPath1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnOK.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnOK.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnOK.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnOK.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnOK.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnOK.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnOK.Location = new System.Drawing.Point(200, 141);
            this.btnOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButton2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButton2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButton2.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButton2.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButton2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButton2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButton2.Location = new System.Drawing.Point(314, 141);
            this.simpleButton2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // FrmFeatureClassExport
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 176);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucClassPath1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmFeatureClassExport";
            this.Text = "导出要素类";
            this.ResumeLayout(false);

        }

        #endregion

        private Hy.Esri.Utility.UCClassOutPath ucClassPath1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}