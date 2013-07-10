namespace Hy.Metadata.UI
{
    partial class FrmStandardProperty
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.ucStandardProperty1 = new Hy.Metadata.UI.UCStandardProperty();
            this.SuspendLayout();
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
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnOK.Location = new System.Drawing.Point(388, 490);
            this.btnOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "保存";
            this.btnOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
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
            this.simpleButton2.CausesValidation = false;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButton2.Location = new System.Drawing.Point(482, 490);
            this.simpleButton2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "关闭";
            this.simpleButton2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // ucStandardProperty1
            // 
            this.ucStandardProperty1.Location = new System.Drawing.Point(9, 13);
            this.ucStandardProperty1.Name = "ucStandardProperty1";
            this.ucStandardProperty1.Size = new System.Drawing.Size(548, 475);
            this.ucStandardProperty1.TabIndex = 0;
            // 
            // FrmStandardProperty
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 516);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ucStandardProperty1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmStandardProperty";
            this.Text = "元数据标准";
            this.ResumeLayout(false);

        }

        #endregion

        private UCStandardProperty ucStandardProperty1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}