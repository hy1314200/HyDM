namespace Hy.Check.UI
{
    partial class UCNavigate
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPage = new DevExpress.XtraEditors.TextEdit();
            this.btnPre = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnNext.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnNext.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnNext.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnNext.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnNext.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnNext.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNext.Enabled = false;
            this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnNext.Location = new System.Drawing.Point(353, 1);
            this.btnNext.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 21);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "下一页";
            this.btnNext.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl1.Location = new System.Drawing.Point(172, 4);
            this.labelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "当前第";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl2.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl2.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl2.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl2.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl2.Location = new System.Drawing.Point(264, 4);
            this.labelControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "页";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtPage
            // 
            this.txtPage.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPage.Location = new System.Drawing.Point(212, 1);
            this.txtPage.Name = "txtPage";
            this.txtPage.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtPage.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtPage.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtPage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtPage.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtPage.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtPage.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtPage.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtPage.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtPage.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtPage.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtPage.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtPage.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtPage.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtPage.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtPage.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtPage.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtPage.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtPage.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtPage.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtPage.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtPage.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtPage.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtPage.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtPage.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtPage.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtPage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtPage.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPage.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtPage.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtPage.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtPage.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtPage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtPage.Properties.ReadOnly = true;
            this.txtPage.Size = new System.Drawing.Size(48, 21);
            this.txtPage.TabIndex = 2;
            this.txtPage.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // btnPre
            // 
            this.btnPre.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPre.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnPre.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnPre.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnPre.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnPre.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnPre.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnPre.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnPre.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPre.Enabled = false;
            this.btnPre.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnPre.Location = new System.Drawing.Point(287, 1);
            this.btnPre.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(60, 21);
            this.btnPre.TabIndex = 0;
            this.btnPre.Text = "上一页";
            this.btnPre.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // UCNavigate
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPre);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "UCNavigate";
            this.Size = new System.Drawing.Size(419, 23);
            ((System.ComponentModel.ISupportInitialize)(this.txtPage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPage;
        private DevExpress.XtraEditors.SimpleButton btnPre;
    }
}
