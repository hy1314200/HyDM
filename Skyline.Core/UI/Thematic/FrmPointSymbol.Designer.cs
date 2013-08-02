namespace Skyline.Core.UI
{
    partial class FrmPointSymbol
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPointSymbol));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.imageComboPointType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.imagePointTypeCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.spinEditPointSize = new DevExpress.XtraEditors.SpinEdit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboPointType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePointTypeCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPointSize.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "大小";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(269, 149);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.simpleButtonCancel);
            this.tabPage1.Controls.Add(this.simpleButtonOK);
            this.tabPage1.Controls.Add(this.imageComboPointType);
            this.tabPage1.Controls.Add(this.spinEditPointSize);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(261, 123);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "二维符号";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButtonCancel.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButtonCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButtonCancel.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButtonCancel.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButtonCancel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButtonCancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButtonCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleButtonCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButtonCancel.Location = new System.Drawing.Point(147, 88);
            this.simpleButtonCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            this.simpleButtonCancel.Size = new System.Drawing.Size(46, 23);
            this.simpleButtonCancel.TabIndex = 24;
            this.simpleButtonCancel.Text = "取消";
            this.simpleButtonCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButtonOK.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButtonOK.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButtonOK.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButtonOK.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButtonOK.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButtonOK.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButtonOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.simpleButtonOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButtonOK.Location = new System.Drawing.Point(85, 88);
            this.simpleButtonOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(46, 23);
            this.simpleButtonOK.TabIndex = 23;
            this.simpleButtonOK.Text = "确定";
            this.simpleButtonOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // imageComboPointType
            // 
            this.imageComboPointType.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.imageComboPointType.EditValue = "圆";
            this.imageComboPointType.Location = new System.Drawing.Point(67, 20);
            this.imageComboPointType.Name = "imageComboPointType";
            this.imageComboPointType.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.imageComboPointType.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.imageComboPointType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.imageComboPointType.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.imageComboPointType.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.imageComboPointType.Properties.Appearance.Options.UseBackColor = true;
            this.imageComboPointType.Properties.Appearance.Options.UseForeColor = true;
            this.imageComboPointType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.imageComboPointType.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.imageComboPointType.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.imageComboPointType.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.imageComboPointType.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.imageComboPointType.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.imageComboPointType.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.imageComboPointType.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.imageComboPointType.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.imageComboPointType.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.White;
            this.imageComboPointType.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.imageComboPointType.Properties.AppearanceDropDown.Options.UseBackColor = true;
            this.imageComboPointType.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.imageComboPointType.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.imageComboPointType.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.imageComboPointType.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.imageComboPointType.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.imageComboPointType.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.imageComboPointType.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.imageComboPointType.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.imageComboPointType.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.imageComboPointType.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.imageComboPointType.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.imageComboPointType.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.imageComboPointType.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.imageComboPointType.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.imageComboPointType.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject3.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            serializableAppearanceObject3.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            serializableAppearanceObject3.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            serializableAppearanceObject3.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            serializableAppearanceObject3.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.imageComboPointType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.imageComboPointType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.imageComboPointType.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.imageComboPointType.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.imageComboPointType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.imageComboPointType.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.imageComboPointType.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.imageComboPointType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("圆", "圆", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("三角形", "三角形", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("正方形", "正方形", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("五边形", "五边形", 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("六边形", "六边形", 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("箭头", "箭头", 5)});
            this.imageComboPointType.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.imageComboPointType.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.imageComboPointType.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.imageComboPointType.Properties.SmallImages = this.imagePointTypeCollection;
            this.imageComboPointType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.imageComboPointType.Size = new System.Drawing.Size(154, 21);
            this.imageComboPointType.TabIndex = 22;
            this.imageComboPointType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // imagePointTypeCollection
            // 
            this.imagePointTypeCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imagePointTypeCollection.ImageStream")));
            this.imagePointTypeCollection.TransparentColor = System.Drawing.Color.White;
            this.imagePointTypeCollection.Images.SetKeyName(0, "Circle.jpg");
            this.imagePointTypeCollection.Images.SetKeyName(1, "Triangle.jpg");
            this.imagePointTypeCollection.Images.SetKeyName(2, "Rectangle.jpg");
            this.imagePointTypeCollection.Images.SetKeyName(3, "Pentagon.jpg");
            this.imagePointTypeCollection.Images.SetKeyName(4, "Hexagon.jpg");
            this.imagePointTypeCollection.Images.SetKeyName(5, "Arrowblue.png");
            // 
            // spinEditPointSize
            // 
            this.spinEditPointSize.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.spinEditPointSize.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditPointSize.Location = new System.Drawing.Point(67, 54);
            this.spinEditPointSize.Name = "spinEditPointSize";
            this.spinEditPointSize.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.spinEditPointSize.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.spinEditPointSize.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.spinEditPointSize.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.spinEditPointSize.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.spinEditPointSize.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.spinEditPointSize.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.spinEditPointSize.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.spinEditPointSize.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.spinEditPointSize.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.spinEditPointSize.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.spinEditPointSize.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.spinEditPointSize.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.spinEditPointSize.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.spinEditPointSize.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.spinEditPointSize.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.spinEditPointSize.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.spinEditPointSize.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.spinEditPointSize.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.spinEditPointSize.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.spinEditPointSize.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.spinEditPointSize.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            serializableAppearanceObject1.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            serializableAppearanceObject1.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            serializableAppearanceObject1.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            serializableAppearanceObject1.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.spinEditPointSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.spinEditPointSize.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.spinEditPointSize.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.spinEditPointSize.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.spinEditPointSize.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.spinEditPointSize.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.spinEditPointSize.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.spinEditPointSize.Properties.SpinStyle = DevExpress.XtraEditors.Controls.SpinStyles.Vertical;
            this.spinEditPointSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.spinEditPointSize.Size = new System.Drawing.Size(154, 21);
            this.spinEditPointSize.TabIndex = 21;
            this.spinEditPointSize.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // FrmPointSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 149);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPointSymbol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "点符号化";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPointSymbol_FormClosed);
            this.Load += new System.EventHandler(this.FrmPointSymbol_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboPointType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagePointTypeCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPointSize.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SpinEdit spinEditPointSize;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboPointType;
        private DevExpress.Utils.ImageCollection imagePointTypeCollection;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
    }
}