namespace Hy.Esri.Catalog.UI
{
    partial class FrmLocalWorkspaceCreate
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbWorkspaceType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtWorkspacePath = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtWorkspaceName = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.folderWorkspace = new System.Windows.Forms.FolderBrowserDialog();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtWorkspaceAlias = new DevExpress.XtraEditors.TextEdit();
            this.cbExistDB = new DevExpress.XtraEditors.CheckEdit();
            this.btnWorkspaceSelect = new DevExpress.XtraEditors.SimpleButton();
            this.dlgWorkspace = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkspaceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspacePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspaceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspaceAlias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExistDB.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
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
            this.labelControl1.Location = new System.Drawing.Point(13, 25);
            this.labelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "本地数据库类型：";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // cmbWorkspaceType
            // 
            this.cmbWorkspaceType.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbWorkspaceType.EditValue = "个人数据库（PGDB）";
            this.cmbWorkspaceType.Location = new System.Drawing.Point(115, 22);
            this.cmbWorkspaceType.Name = "cmbWorkspaceType";
            this.cmbWorkspaceType.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cmbWorkspaceType.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbWorkspaceType.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbWorkspaceType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbWorkspaceType.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbWorkspaceType.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbWorkspaceType.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbWorkspaceType.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbWorkspaceType.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbWorkspaceType.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbWorkspaceType.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbWorkspaceType.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject3.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            serializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            serializableAppearanceObject3.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            serializableAppearanceObject3.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            serializableAppearanceObject3.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            serializableAppearanceObject3.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbWorkspaceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.cmbWorkspaceType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbWorkspaceType.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbWorkspaceType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cmbWorkspaceType.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.cmbWorkspaceType.Properties.Items.AddRange(new object[] {
            "个人数据库（PGDB）",
            "文件数据库（File GDB）"});
            this.cmbWorkspaceType.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cmbWorkspaceType.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.cmbWorkspaceType.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.cmbWorkspaceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbWorkspaceType.Size = new System.Drawing.Size(146, 21);
            this.cmbWorkspaceType.TabIndex = 1;
            this.cmbWorkspaceType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.cmbWorkspaceType.SelectedIndexChanged += new System.EventHandler(this.cmbWorkspaceType_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
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
            this.labelControl2.Location = new System.Drawing.Point(13, 65);
            this.labelControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "数据库路径：";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtWorkspacePath
            // 
            this.txtWorkspacePath.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspacePath.Location = new System.Drawing.Point(115, 58);
            this.txtWorkspacePath.Name = "txtWorkspacePath";
            this.txtWorkspacePath.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtWorkspacePath.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspacePath.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspacePath.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspacePath.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspacePath.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspacePath.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspacePath.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspacePath.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspacePath.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspacePath.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspacePath.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspacePath.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspacePath.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspacePath.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspacePath.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspacePath.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspacePath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            serializableAppearanceObject1.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            serializableAppearanceObject1.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            serializableAppearanceObject1.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            serializableAppearanceObject1.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspacePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtWorkspacePath.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtWorkspacePath.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtWorkspacePath.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtWorkspacePath.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtWorkspacePath.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtWorkspacePath.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtWorkspacePath.Properties.ReadOnly = true;
            this.txtWorkspacePath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtWorkspacePath.Size = new System.Drawing.Size(318, 21);
            this.txtWorkspacePath.TabIndex = 3;
            this.txtWorkspacePath.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.txtWorkspacePath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtWorkspacePath_ButtonClick);
            // 
            // labelControl3
            // 
            this.labelControl3.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl3.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl3.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl3.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl3.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl3.Location = new System.Drawing.Point(12, 95);
            this.labelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "数据库名称：";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtWorkspaceName
            // 
            this.txtWorkspaceName.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspaceName.Location = new System.Drawing.Point(115, 92);
            this.txtWorkspaceName.Name = "txtWorkspaceName";
            this.txtWorkspaceName.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtWorkspaceName.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspaceName.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceName.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceName.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceName.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceName.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceName.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceName.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceName.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceName.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceName.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceName.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceName.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceName.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtWorkspaceName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtWorkspaceName.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtWorkspaceName.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtWorkspaceName.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtWorkspaceName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtWorkspaceName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtWorkspaceName.Size = new System.Drawing.Size(318, 21);
            this.txtWorkspaceName.TabIndex = 4;
            this.txtWorkspaceName.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.txtWorkspaceName.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkspaceName_Validating);
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
            this.btnOK.Location = new System.Drawing.Point(268, 152);
            this.btnOK.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
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
            this.simpleButton2.Location = new System.Drawing.Point(358, 152);
            this.simpleButton2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl4
            // 
            this.labelControl4.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl4.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl4.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl4.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl4.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl4.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl4.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl4.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl4.Location = new System.Drawing.Point(12, 131);
            this.labelControl4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "数据库别名：";
            this.labelControl4.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtWorkspaceAlias
            // 
            this.txtWorkspaceAlias.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspaceAlias.Location = new System.Drawing.Point(115, 125);
            this.txtWorkspaceAlias.Name = "txtWorkspaceAlias";
            this.txtWorkspaceAlias.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtWorkspaceAlias.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtWorkspaceAlias.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceAlias.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceAlias.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceAlias.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceAlias.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceAlias.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtWorkspaceAlias.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtWorkspaceAlias.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtWorkspaceAlias.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtWorkspaceAlias.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtWorkspaceAlias.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtWorkspaceAlias.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtWorkspaceAlias.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtWorkspaceAlias.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtWorkspaceAlias.Size = new System.Drawing.Size(318, 21);
            this.txtWorkspaceAlias.TabIndex = 4;
            this.txtWorkspaceAlias.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.txtWorkspaceAlias.Validating += new System.ComponentModel.CancelEventHandler(this.txtWorkspaceName_Validating);
            // 
            // cbExistDB
            // 
            this.cbExistDB.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.cbExistDB.Location = new System.Drawing.Point(267, 24);
            this.cbExistDB.Name = "cbExistDB";
            this.cbExistDB.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cbExistDB.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cbExistDB.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cbExistDB.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cbExistDB.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cbExistDB.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cbExistDB.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cbExistDB.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cbExistDB.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cbExistDB.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cbExistDB.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cbExistDB.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cbExistDB.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cbExistDB.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cbExistDB.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cbExistDB.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cbExistDB.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cbExistDB.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cbExistDB.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cbExistDB.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cbExistDB.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cbExistDB.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cbExistDB.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cbExistDB.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cbExistDB.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cbExistDB.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cbExistDB.Properties.Caption = "从现有库选择：";
            this.cbExistDB.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            this.cbExistDB.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cbExistDB.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cbExistDB.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cbExistDB.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.InactiveChecked;
            this.cbExistDB.Size = new System.Drawing.Size(93, 19);
            this.cbExistDB.TabIndex = 6;
            this.cbExistDB.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.cbExistDB.CheckedChanged += new System.EventHandler(this.cbExistDB_CheckedChanged);
            // 
            // btnWorkspaceSelect
            // 
            this.btnWorkspaceSelect.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnWorkspaceSelect.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnWorkspaceSelect.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnWorkspaceSelect.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnWorkspaceSelect.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnWorkspaceSelect.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnWorkspaceSelect.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnWorkspaceSelect.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnWorkspaceSelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnWorkspaceSelect.Enabled = false;
            this.btnWorkspaceSelect.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnWorkspaceSelect.Location = new System.Drawing.Point(366, 20);
            this.btnWorkspaceSelect.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnWorkspaceSelect.Name = "btnWorkspaceSelect";
            this.btnWorkspaceSelect.Size = new System.Drawing.Size(59, 23);
            this.btnWorkspaceSelect.TabIndex = 5;
            this.btnWorkspaceSelect.Text = "浏览…";
            this.btnWorkspaceSelect.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnWorkspaceSelect.Click += new System.EventHandler(this.btnWorkspaceSelect_Click);
            // 
            // dlgWorkspace
            // 
            this.dlgWorkspace.Filter = "个人空间数据库(PGDB)|*.mdb";
            // 
            // FrmLocalWorkspaceCreate
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 184);
            this.Controls.Add(this.cbExistDB);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.btnWorkspaceSelect);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtWorkspaceAlias);
            this.Controls.Add(this.txtWorkspaceName);
            this.Controls.Add(this.txtWorkspacePath);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cmbWorkspaceType);
            this.Controls.Add(this.labelControl1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmLocalWorkspaceCreate";
            this.Text = "本地数据库创建和连接";
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkspaceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspacePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspaceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkspaceAlias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExistDB.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbWorkspaceType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit txtWorkspacePath;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtWorkspaceName;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.FolderBrowserDialog folderWorkspace;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtWorkspaceAlias;
        private DevExpress.XtraEditors.CheckEdit cbExistDB;
        private DevExpress.XtraEditors.SimpleButton btnWorkspaceSelect;
        private System.Windows.Forms.OpenFileDialog dlgWorkspace;
    }
}