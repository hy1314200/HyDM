namespace Check.UI.Forms
{
    partial class FrmBatchTasksWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchTasksWizard));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            this.Taskwizard = new DevExpress.XtraWizard.WizardControl();
            this.wpSourceSelect = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.labTaskPath = new DevExpress.XtraEditors.LabelControl();
            this.labDatasource = new DevExpress.XtraEditors.LabelControl();
            this.txtTaskPath = new DevExpress.XtraEditors.ButtonEdit();
            this.txtSourceFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl = new DevExpress.XtraEditors.LabelControl();
            this.wpTaskSetting = new DevExpress.XtraWizard.WizardPage();
            this.GridCheckSetting = new DevExpress.XtraGrid.GridControl();
            this.gridViewCheckSetting = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.wpComplete = new DevExpress.XtraWizard.CompletionWizardPage();
            this.memoRemark = new DevExpress.XtraEditors.MemoEdit();
            this.wpParamterSet = new DevExpress.XtraWizard.WizardPage();
            this.cmbSchema = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbStandard = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSpatialRefLayer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cmbScale = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtTopoTolerance = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Taskwizard)).BeginInit();
            this.Taskwizard.SuspendLayout();
            this.wpSourceSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceFolder.Properties)).BeginInit();
            this.wpTaskSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCheckSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCheckSetting)).BeginInit();
            this.wpComplete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).BeginInit();
            this.wpParamterSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchema.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStandard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRefLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopoTolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Taskwizard
            // 
            this.Taskwizard.AnimationInterval = 50;
            this.Taskwizard.Appearance.AeroWizardTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Taskwizard.Appearance.AeroWizardTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Taskwizard.Appearance.AeroWizardTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Taskwizard.Appearance.AeroWizardTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Taskwizard.Appearance.AeroWizardTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Taskwizard.Appearance.AeroWizardTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.Taskwizard.Appearance.ExteriorPage.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Taskwizard.Appearance.ExteriorPage.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Taskwizard.Appearance.ExteriorPage.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Taskwizard.Appearance.ExteriorPage.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Taskwizard.Appearance.ExteriorPage.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Taskwizard.Appearance.ExteriorPage.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.Taskwizard.Appearance.ExteriorPageTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Taskwizard.Appearance.ExteriorPageTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Taskwizard.Appearance.ExteriorPageTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Taskwizard.Appearance.ExteriorPageTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Taskwizard.Appearance.ExteriorPageTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Taskwizard.Appearance.ExteriorPageTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.Taskwizard.Appearance.Page.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Taskwizard.Appearance.Page.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Taskwizard.Appearance.Page.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Taskwizard.Appearance.Page.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Taskwizard.Appearance.Page.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Taskwizard.Appearance.Page.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.Taskwizard.Appearance.PageTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Taskwizard.Appearance.PageTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Taskwizard.Appearance.PageTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Taskwizard.Appearance.PageTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Taskwizard.Appearance.PageTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Taskwizard.Appearance.PageTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.Taskwizard.CancelText = "关闭";
            this.Taskwizard.Controls.Add(this.wpSourceSelect);
            this.Taskwizard.Controls.Add(this.wpTaskSetting);
            this.Taskwizard.Controls.Add(this.wpComplete);
            this.Taskwizard.Controls.Add(this.wpParamterSet);
            this.Taskwizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Taskwizard.FinishText = "完成";
            this.Taskwizard.HelpText = "帮助";
            this.Taskwizard.Image = ((System.Drawing.Image)(resources.GetObject("Taskwizard.Image")));
            this.Taskwizard.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Taskwizard.ImageWidth = 180;
            this.Taskwizard.Location = new System.Drawing.Point(0, 0);
            this.Taskwizard.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Taskwizard.Name = "Taskwizard";
            this.Taskwizard.NavigationMode = DevExpress.XtraWizard.NavigationMode.Sequential;
            this.Taskwizard.NextText = "下一步 >";
            this.Taskwizard.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.wpSourceSelect,
            this.wpParamterSet,
            this.wpTaskSetting,
            this.wpComplete});
            this.Taskwizard.PreviousText = "< &上一步";
            this.Taskwizard.ShowHeaderImage = true;
            this.Taskwizard.Size = new System.Drawing.Size(730, 476);
            this.Taskwizard.Text = "欢迎使用批量质检任务创建向导";
            this.Taskwizard.WizardStyle = DevExpress.XtraWizard.WizardStyle.Wizard97;
            this.Taskwizard.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.Taskwizard.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            // 
            // wpSourceSelect
            // 
            this.wpSourceSelect.Controls.Add(this.labTaskPath);
            this.wpSourceSelect.Controls.Add(this.labDatasource);
            this.wpSourceSelect.Controls.Add(this.txtTaskPath);
            this.wpSourceSelect.Controls.Add(this.txtSourceFolder);
            this.wpSourceSelect.Controls.Add(this.labelControl1);
            this.wpSourceSelect.Controls.Add(this.labelControl);
            this.wpSourceSelect.IntroductionText = "";
            this.wpSourceSelect.Name = "wpSourceSelect";
            this.wpSourceSelect.ProceedText = "";
            this.wpSourceSelect.Size = new System.Drawing.Size(518, 343);
            this.wpSourceSelect.Text = "请选择源数据成果目录";
            // 
            // labTaskPath
            // 
            this.labTaskPath.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labTaskPath.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labTaskPath.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labTaskPath.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTaskPath.Appearance.Options.UseForeColor = true;
            this.labTaskPath.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labTaskPath.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labTaskPath.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labTaskPath.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labTaskPath.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labTaskPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labTaskPath.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labTaskPath.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labTaskPath.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labTaskPath.Location = new System.Drawing.Point(28, 233);
            this.labTaskPath.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labTaskPath.Name = "labTaskPath";
            this.labTaskPath.Size = new System.Drawing.Size(48, 14);
            this.labTaskPath.TabIndex = 9;
            this.labTaskPath.Text = "不能为空";
            this.labTaskPath.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labDatasource
            // 
            this.labDatasource.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labDatasource.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labDatasource.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labDatasource.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labDatasource.Appearance.Options.UseForeColor = true;
            this.labDatasource.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labDatasource.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labDatasource.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labDatasource.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labDatasource.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labDatasource.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labDatasource.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labDatasource.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labDatasource.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labDatasource.Location = new System.Drawing.Point(28, 114);
            this.labDatasource.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labDatasource.Name = "labDatasource";
            this.labDatasource.Size = new System.Drawing.Size(48, 14);
            this.labDatasource.TabIndex = 8;
            this.labDatasource.Text = "不能为空";
            this.labDatasource.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtTaskPath
            // 
            this.txtTaskPath.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.dxErrorProvider.SetErrorType(this.txtTaskPath, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.txtTaskPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtTaskPath.Location = new System.Drawing.Point(28, 202);
            this.txtTaskPath.Name = "txtTaskPath";
            this.txtTaskPath.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtTaskPath.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtTaskPath.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTaskPath.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTaskPath.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTaskPath.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTaskPath.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTaskPath.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTaskPath.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTaskPath.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTaskPath.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTaskPath.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTaskPath.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTaskPath.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTaskPath.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTaskPath.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTaskPath.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTaskPath.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTaskPath.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTaskPath.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTaskPath.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTaskPath.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTaskPath.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTaskPath.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTaskPath.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTaskPath.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTaskPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTaskPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtTaskPath.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTaskPath.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtTaskPath.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtTaskPath.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtTaskPath.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtTaskPath.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtTaskPath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtTaskPath.Size = new System.Drawing.Size(432, 21);
            this.txtTaskPath.TabIndex = 5;
            this.txtTaskPath.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.txtTaskPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtTaskPath_ButtonClick);
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.dxErrorProvider.SetErrorType(this.txtSourceFolder, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.txtSourceFolder, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtSourceFolder.Location = new System.Drawing.Point(28, 83);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtSourceFolder.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtSourceFolder.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtSourceFolder.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtSourceFolder.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtSourceFolder.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtSourceFolder.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtSourceFolder.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtSourceFolder.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtSourceFolder.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtSourceFolder.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtSourceFolder.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtSourceFolder.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtSourceFolder.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtSourceFolder.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtSourceFolder.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtSourceFolder.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtSourceFolder.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtSourceFolder.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtSourceFolder.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtSourceFolder.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtSourceFolder.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtSourceFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.txtSourceFolder.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSourceFolder.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSourceFolder.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtSourceFolder.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtSourceFolder.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtSourceFolder.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtSourceFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtSourceFolder.Size = new System.Drawing.Size(432, 21);
            this.txtSourceFolder.TabIndex = 4;
            this.txtSourceFolder.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.txtSourceFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtSourceFolder_ButtonClick);
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
            this.labelControl1.Location = new System.Drawing.Point(28, 178);
            this.labelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "请选择创建质检任务目录：";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl
            // 
            this.labelControl.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl.Location = new System.Drawing.Point(28, 59);
            this.labelControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl.Name = "labelControl";
            this.labelControl.Size = new System.Drawing.Size(132, 14);
            this.labelControl.TabIndex = 1;
            this.labelControl.Text = "请选择源数据所在目录：";
            this.labelControl.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // wpTaskSetting
            // 
            this.wpTaskSetting.Controls.Add(this.GridCheckSetting);
            this.wpTaskSetting.DescriptionText = "";
            this.wpTaskSetting.Name = "wpTaskSetting";
            this.wpTaskSetting.Size = new System.Drawing.Size(698, 331);
            this.wpTaskSetting.Text = "任务参数修改";
            // 
            // GridCheckSetting
            // 
            this.GridCheckSetting.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.Default;
            this.GridCheckSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridCheckSetting.EmbeddedNavigator.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.GridCheckSetting.EmbeddedNavigator.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.GridCheckSetting.EmbeddedNavigator.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.GridCheckSetting.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.GridCheckSetting.EmbeddedNavigator.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.GridCheckSetting.Location = new System.Drawing.Point(0, 0);
            this.GridCheckSetting.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.GridCheckSetting.MainView = this.gridViewCheckSetting;
            this.GridCheckSetting.Name = "GridCheckSetting";
            this.GridCheckSetting.Size = new System.Drawing.Size(698, 331);
            this.GridCheckSetting.TabIndex = 1;
            this.GridCheckSetting.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCheckSetting});
            // 
            // gridViewCheckSetting
            // 
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.ColumnFilterButtonActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.CustomizationFormHint.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.DetailTip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.DetailTip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.DetailTip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.DetailTip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.DetailTip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.DetailTip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.Empty.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.Empty.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.Empty.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.Empty.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.Empty.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.Empty.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.EvenRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.EvenRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FilterCloseButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FilterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FilterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FixedLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FixedLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FixedLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FixedLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FixedLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FixedLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FocusedCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FocusedCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FocusedCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FocusedCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FocusedRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FocusedRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FocusedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.FooterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.FooterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.GroupButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.GroupButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.GroupButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.GroupButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.GroupFooter.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupFooter.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.GroupFooter.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.GroupPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.GroupPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.GroupRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.GroupRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.HeaderPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.HeaderPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.HideSelectionRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.HorzLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.HorzLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.HorzLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.HorzLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.HorzLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.HorzLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.OddRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.OddRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.Preview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.Preview.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.Preview.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.Row.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.Row.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.Row.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.RowSeparator.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.RowSeparator.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.RowSeparator.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.RowSeparator.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.RowSeparator.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.RowSeparator.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.SelectedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.SelectedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.SelectedRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.SelectedRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.SelectedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.SelectedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.TopNewRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.TopNewRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.TopNewRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.TopNewRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.TopNewRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.TopNewRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.VertLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.VertLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.VertLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.VertLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.VertLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.Appearance.ViewCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.Appearance.ViewCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.Appearance.ViewCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.Lines.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.Lines.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Lines.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.Lines.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.Lines.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.OddRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.OddRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.Preview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Preview.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.Preview.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.AppearancePrint.Row.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gridViewCheckSetting.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Row.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gridViewCheckSetting.AppearancePrint.Row.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gridViewCheckSetting.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gridViewCheckSetting.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gridViewCheckSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.gridViewCheckSetting.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
            this.gridViewCheckSetting.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            this.gridViewCheckSetting.GridControl = this.GridCheckSetting;
            this.gridViewCheckSetting.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded;
            this.gridViewCheckSetting.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.gridViewCheckSetting.Name = "gridViewCheckSetting";
            this.gridViewCheckSetting.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.Default;
            this.gridViewCheckSetting.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.Default;
            this.gridViewCheckSetting.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.CacheAll;
            this.gridViewCheckSetting.OptionsBehavior.Editable = false;
            this.gridViewCheckSetting.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Default;
            this.gridViewCheckSetting.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewCheckSetting.OptionsCustomization.AllowFilter = false;
            this.gridViewCheckSetting.OptionsCustomization.AllowGroup = false;
            this.gridViewCheckSetting.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.Default;
            this.gridViewCheckSetting.OptionsMenu.EnableColumnMenu = false;
            this.gridViewCheckSetting.OptionsMenu.EnableFooterMenu = false;
            this.gridViewCheckSetting.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewCheckSetting.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewCheckSetting.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.Default;
            this.gridViewCheckSetting.OptionsView.ColumnAutoWidth = false;
            this.gridViewCheckSetting.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Default;
            this.gridViewCheckSetting.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Default;
            this.gridViewCheckSetting.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.gridViewCheckSetting.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            this.gridViewCheckSetting.OptionsView.ShowGroupPanel = false;
            this.gridViewCheckSetting.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.gridViewCheckSetting.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.gridViewCheckSetting.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gridViewCheckSetting_FocusedColumnChanged);
            this.gridViewCheckSetting.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridViewCheckSetting_ValidatingEditor);
            // 
            // wpComplete
            // 
            this.wpComplete.Controls.Add(this.memoRemark);
            this.wpComplete.FinishText = "完成";
            this.wpComplete.Name = "wpComplete";
            this.wpComplete.ProceedText = "";
            this.wpComplete.Size = new System.Drawing.Size(518, 343);
            this.wpComplete.Text = "任务信息";
            // 
            // memoRemark
            // 
            this.memoRemark.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.memoRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.memoRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxErrorProvider.SetErrorType(this.memoRemark, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.memoRemark, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.memoRemark.Location = new System.Drawing.Point(0, 0);
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.memoRemark.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.memoRemark.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.memoRemark.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.memoRemark.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.memoRemark.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.memoRemark.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.memoRemark.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.memoRemark.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.memoRemark.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.memoRemark.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.memoRemark.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.memoRemark.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.memoRemark.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.memoRemark.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.memoRemark.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.memoRemark.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.memoRemark.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.memoRemark.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.memoRemark.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.memoRemark.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.memoRemark.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.memoRemark.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.memoRemark.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.memoRemark.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.memoRemark.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.memoRemark.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.memoRemark.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.memoRemark.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.memoRemark.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.memoRemark.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.memoRemark.Properties.ReadOnly = true;
            this.memoRemark.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.memoRemark.Size = new System.Drawing.Size(518, 343);
            this.memoRemark.TabIndex = 3;
            this.memoRemark.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // wpParamterSet
            // 
            this.wpParamterSet.Controls.Add(this.cmbSchema);
            this.wpParamterSet.Controls.Add(this.cmbStandard);
            this.wpParamterSet.Controls.Add(this.labelControl3);
            this.wpParamterSet.Controls.Add(this.labelControl2);
            this.wpParamterSet.Controls.Add(this.cmbSpatialRefLayer);
            this.wpParamterSet.Controls.Add(this.labelControl8);
            this.wpParamterSet.Controls.Add(this.cmbScale);
            this.wpParamterSet.Controls.Add(this.txtTopoTolerance);
            this.wpParamterSet.Controls.Add(this.labelControl6);
            this.wpParamterSet.Controls.Add(this.labelControl4);
            this.wpParamterSet.Controls.Add(this.labelControl5);
            this.wpParamterSet.DescriptionText = "";
            this.wpParamterSet.Name = "wpParamterSet";
            this.wpParamterSet.Size = new System.Drawing.Size(698, 331);
            this.wpParamterSet.Text = "参数设置";
            // 
            // cmbSchema
            // 
            this.cmbSchema.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.dxErrorProvider.SetErrorType(this.cmbSchema, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.cmbSchema, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cmbSchema.Location = new System.Drawing.Point(274, 70);
            this.cmbSchema.Name = "cmbSchema";
            this.cmbSchema.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cmbSchema.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbSchema.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSchema.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSchema.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSchema.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSchema.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSchema.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSchema.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSchema.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSchema.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSchema.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSchema.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSchema.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSchema.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSchema.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSchema.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSchema.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSchema.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSchema.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSchema.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSchema.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSchema.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSchema.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSchema.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSchema.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSchema.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSchema.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject3.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSchema.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
            this.cmbSchema.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbSchema.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbSchema.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cmbSchema.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.cmbSchema.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cmbSchema.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.cmbSchema.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.cmbSchema.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSchema.Size = new System.Drawing.Size(283, 21);
            this.cmbSchema.TabIndex = 35;
            this.cmbSchema.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // cmbStandard
            // 
            this.cmbStandard.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.dxErrorProvider.SetErrorType(this.cmbStandard, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.cmbStandard, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cmbStandard.Location = new System.Drawing.Point(274, 24);
            this.cmbStandard.Name = "cmbStandard";
            this.cmbStandard.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cmbStandard.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbStandard.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbStandard.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbStandard.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbStandard.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbStandard.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbStandard.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbStandard.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbStandard.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbStandard.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbStandard.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbStandard.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbStandard.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbStandard.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbStandard.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbStandard.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbStandard.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbStandard.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbStandard.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbStandard.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbStandard.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbStandard.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbStandard.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbStandard.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbStandard.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbStandard.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbStandard.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject4.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbStandard.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
            this.cmbStandard.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbStandard.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbStandard.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cmbStandard.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.cmbStandard.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cmbStandard.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.cmbStandard.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.cmbStandard.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStandard.Size = new System.Drawing.Size(283, 21);
            this.cmbStandard.TabIndex = 36;
            this.cmbStandard.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.cmbStandard.SelectedIndexChanged += new System.EventHandler(this.cmbStandard_SelectedIndexChanged);
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
            this.labelControl3.Location = new System.Drawing.Point(192, 73);
            this.labelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "质检方案：";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
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
            this.labelControl2.Location = new System.Drawing.Point(192, 28);
            this.labelControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 34;
            this.labelControl2.Text = "数据标准：";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // cmbSpatialRefLayer
            // 
            this.cmbSpatialRefLayer.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.dxErrorProvider.SetErrorType(this.cmbSpatialRefLayer, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.cmbSpatialRefLayer, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cmbSpatialRefLayer.Location = new System.Drawing.Point(274, 237);
            this.cmbSpatialRefLayer.Name = "cmbSpatialRefLayer";
            this.cmbSpatialRefLayer.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cmbSpatialRefLayer.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbSpatialRefLayer.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSpatialRefLayer.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSpatialRefLayer.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSpatialRefLayer.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSpatialRefLayer.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbSpatialRefLayer.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbSpatialRefLayer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject5.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbSpatialRefLayer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true)});
            this.cmbSpatialRefLayer.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbSpatialRefLayer.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbSpatialRefLayer.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cmbSpatialRefLayer.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.cmbSpatialRefLayer.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cmbSpatialRefLayer.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.cmbSpatialRefLayer.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.cmbSpatialRefLayer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSpatialRefLayer.Size = new System.Drawing.Size(283, 21);
            this.cmbSpatialRefLayer.TabIndex = 32;
            this.cmbSpatialRefLayer.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl8
            // 
            this.labelControl8.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl8.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl8.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl8.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl8.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl8.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl8.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl8.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl8.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl8.Location = new System.Drawing.Point(136, 241);
            this.labelControl8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(108, 14);
            this.labelControl8.TabIndex = 31;
            this.labelControl8.Text = "空间参考来源图层：";
            this.labelControl8.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // cmbScale
            // 
            this.cmbScale.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbScale.EditValue = "1:10000";
            this.dxErrorProvider.SetErrorType(this.cmbScale, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.cmbScale, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.cmbScale.Location = new System.Drawing.Point(274, 185);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.cmbScale.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.cmbScale.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbScale.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbScale.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbScale.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbScale.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbScale.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbScale.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbScale.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbScale.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbScale.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbScale.Properties.AppearanceDropDown.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbScale.Properties.AppearanceDropDown.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbScale.Properties.AppearanceDropDown.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbScale.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbScale.Properties.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbScale.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbScale.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbScale.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbScale.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbScale.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbScale.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.cmbScale.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.cmbScale.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.cmbScale.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.cmbScale.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.cmbScale.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject6.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.cmbScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", null, null, true)});
            this.cmbScale.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbScale.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbScale.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.cmbScale.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Default;
            this.cmbScale.Properties.Items.AddRange(new object[] {
            "1:500",
            "1:1000",
            "1:2000",
            "1:5000",
            "1:10000",
            "1:25000",
            "1:50000",
            "1:100000",
            "1:250000",
            "1:500000",
            "1:1000000"});
            this.cmbScale.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.cmbScale.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.Default;
            this.cmbScale.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.cmbScale.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbScale.Size = new System.Drawing.Size(283, 21);
            this.cmbScale.TabIndex = 30;
            this.cmbScale.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // txtTopoTolerance
            // 
            this.txtTopoTolerance.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtTopoTolerance.EditValue = "0.0001";
            this.dxErrorProvider.SetErrorType(this.txtTopoTolerance, DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
            this.dxErrorProvider.SetIconAlignment(this.txtTopoTolerance, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtTopoTolerance.Location = new System.Drawing.Point(274, 117);
            this.txtTopoTolerance.Name = "txtTopoTolerance";
            this.txtTopoTolerance.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtTopoTolerance.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtTopoTolerance.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTopoTolerance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTopoTolerance.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTopoTolerance.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTopoTolerance.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTopoTolerance.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTopoTolerance.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTopoTolerance.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTopoTolerance.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTopoTolerance.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTopoTolerance.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTopoTolerance.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTopoTolerance.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTopoTolerance.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtTopoTolerance.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtTopoTolerance.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtTopoTolerance.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTopoTolerance.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtTopoTolerance.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtTopoTolerance.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtTopoTolerance.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtTopoTolerance.Properties.Mask.EditMask = "0\\.00[0]{0,11}[1-9]";
            this.txtTopoTolerance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTopoTolerance.Size = new System.Drawing.Size(282, 21);
            this.txtTopoTolerance.TabIndex = 28;
            this.txtTopoTolerance.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl6
            // 
            this.labelControl6.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl6.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl6.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl6.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl6.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl6.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl6.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl6.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl6.Location = new System.Drawing.Point(274, 148);
            this.labelControl6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(160, 14);
            this.labelControl6.TabIndex = 27;
            this.labelControl6.Text = "(0.001~0.00000000000001)";
            this.labelControl6.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
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
            this.labelControl4.Location = new System.Drawing.Point(178, 189);
            this.labelControl4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "地图比例尺：";
            this.labelControl4.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // labelControl5
            // 
            this.labelControl5.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.labelControl5.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.labelControl5.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.labelControl5.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.labelControl5.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.labelControl5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
            this.labelControl5.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None;
            this.labelControl5.LineLocation = DevExpress.XtraEditors.LineLocation.Default;
            this.labelControl5.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Default;
            this.labelControl5.Location = new System.Drawing.Point(192, 120);
            this.labelControl5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "拓扑容限：";
            this.labelControl5.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.FileName = "openFileDialog1";
            // 
            // FrmBatchTasksWizard
            // 
            this.Appearance.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 476);
            this.ControlBox = false;
            this.Controls.Add(this.Taskwizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBatchTasksWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMoreTasksDevWiazard_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Taskwizard)).EndInit();
            this.Taskwizard.ResumeLayout(false);
            this.wpSourceSelect.ResumeLayout(false);
            this.wpSourceSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceFolder.Properties)).EndInit();
            this.wpTaskSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCheckSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCheckSetting)).EndInit();
            this.wpComplete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).EndInit();
            this.wpParamterSet.ResumeLayout(false);
            this.wpParamterSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchema.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStandard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpatialRefLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopoTolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl Taskwizard;
        private DevExpress.XtraWizard.WelcomeWizardPage wpSourceSelect;
        private DevExpress.XtraWizard.WizardPage wpTaskSetting;
        private DevExpress.XtraWizard.CompletionWizardPage wpComplete;
        private DevExpress.XtraEditors.LabelControl labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtSourceFolder;
        private DevExpress.XtraEditors.ButtonEdit txtTaskPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private DevExpress.XtraGrid.GridControl GridCheckSetting;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCheckSetting;
        private DevExpress.XtraEditors.MemoEdit memoRemark;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraWizard.WizardPage wpParamterSet;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSchema;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStandard;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSpatialRefLayer;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit cmbScale;
        private DevExpress.XtraEditors.TextEdit txtTopoTolerance;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labTaskPath;
        private DevExpress.XtraEditors.LabelControl labDatasource;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    }
}