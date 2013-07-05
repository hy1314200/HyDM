namespace Esri.HuiDong.UI
{
    partial class FrmDataCheck
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("图层完整性");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("字段完整性");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("完整性", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("坐标系");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("数据有效性");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("数学基础", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("空数据");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("自相交");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("间隙");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("压盖");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("拓扑", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("所有质检规则", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode18,
            treeNode23});
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Appearance.AeroWizardTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.wizardControl1.Appearance.AeroWizardTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.wizardControl1.Appearance.AeroWizardTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.wizardControl1.Appearance.AeroWizardTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.wizardControl1.Appearance.AeroWizardTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.wizardControl1.Appearance.AeroWizardTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.wizardControl1.Appearance.ExteriorPage.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.wizardControl1.Appearance.ExteriorPage.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.wizardControl1.Appearance.ExteriorPage.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.wizardControl1.Appearance.ExteriorPage.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.wizardControl1.Appearance.ExteriorPage.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.wizardControl1.Appearance.ExteriorPage.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.wizardControl1.Appearance.ExteriorPageTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.wizardControl1.Appearance.ExteriorPageTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.wizardControl1.Appearance.ExteriorPageTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.wizardControl1.Appearance.ExteriorPageTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.wizardControl1.Appearance.ExteriorPageTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.wizardControl1.Appearance.ExteriorPageTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.wizardControl1.Appearance.Page.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.wizardControl1.Appearance.Page.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.wizardControl1.Appearance.Page.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.wizardControl1.Appearance.Page.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.wizardControl1.Appearance.Page.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.wizardControl1.Appearance.Page.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.wizardControl1.Appearance.PageTitle.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.wizardControl1.Appearance.PageTitle.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.wizardControl1.Appearance.PageTitle.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.wizardControl1.Appearance.PageTitle.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.wizardControl1.Appearance.PageTitle.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.wizardControl1.Appearance.PageTitle.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.wizardControl1.CancelText = "取消";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage2);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishText = "完成";
            this.wizardControl1.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NavigationMode = DevExpress.XtraWizard.NavigationMode.Sequential;
            this.wizardControl1.NextText = "下一步";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "上一步";
            this.wizardControl1.Size = new System.Drawing.Size(513, 353);
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.Wizard97;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.radioGroup1);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.IntroductionText = "";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(296, 220);
            this.welcomeWizardPage1.Text = "第一步 选择数据类型";
            // 
            // radioGroup1
            // 
            this.radioGroup1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.radioGroup1.Location = new System.Drawing.Point(4, 26);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.radioGroup1.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.radioGroup1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.radioGroup1.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.radioGroup1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.radioGroup1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.radioGroup1.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.radioGroup1.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.radioGroup1.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.radioGroup1.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.radioGroup1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.radioGroup1.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.radioGroup1.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.radioGroup1.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.radioGroup1.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.radioGroup1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.radioGroup1.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.radioGroup1.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioGroup1.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.radioGroup1.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.radioGroup1.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.radioGroup1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.radioGroup1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.radioGroup1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.radioGroup1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "个人数据库（PGDB）"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "文件数据库（FileGDB）"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Shp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Coverage"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "E00"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CAD（DWG）"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "VCT")});
            this.radioGroup1.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.radioGroup1.Size = new System.Drawing.Size(289, 191);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
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
            this.labelControl1.Location = new System.Drawing.Point(4, 5);
            this.labelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(156, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "请选择要进行质检的数据格式";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.buttonEdit1);
            this.wizardPage1.Controls.Add(this.labelControl2);
            this.wizardPage1.DescriptionText = "选择个人数据库所在的路径";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(481, 208);
            this.wizardPage1.Text = "第二步 数据路径";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.buttonEdit1.Location = new System.Drawing.Point(80, 100);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.buttonEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.buttonEdit1.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.buttonEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.buttonEdit1.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.buttonEdit1.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.buttonEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.buttonEdit1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.buttonEdit1.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.buttonEdit1.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.buttonEdit1.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.buttonEdit1.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.buttonEdit1.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.buttonEdit1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.buttonEdit1.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.buttonEdit1.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.buttonEdit1.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.buttonEdit1.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.buttonEdit1.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.buttonEdit1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.buttonEdit1.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.buttonEdit1.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.buttonEdit1.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.buttonEdit1.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.buttonEdit1.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.buttonEdit1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.buttonEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            serializableAppearanceObject2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            serializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            serializableAppearanceObject2.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            serializableAppearanceObject2.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            serializableAppearanceObject2.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            serializableAppearanceObject2.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.buttonEdit1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.buttonEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.buttonEdit1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.buttonEdit1.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.buttonEdit1.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.buttonEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.buttonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.buttonEdit1.Size = new System.Drawing.Size(359, 21);
            this.buttonEdit1.TabIndex = 1;
            this.buttonEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
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
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(150, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "个人数据库（GPDB）路径：";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.labelControl3);
            this.completionWizardPage1.FinishText = "";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.ProceedText = "";
            this.completionWizardPage1.Size = new System.Drawing.Size(296, 220);
            this.completionWizardPage1.Text = "完成";
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
            this.labelControl3.Location = new System.Drawing.Point(55, 78);
            this.labelControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(156, 42);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "恭喜您已经完成数据质检设置\r\n\r\n点击完成进行数据质检";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.treeView1);
            this.wizardPage2.DescriptionText = "请选择要对当前数据质检的规则";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(481, 208);
            this.wizardPage2.Text = "第三步 选择质检规则";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode13.Name = "节点3";
            treeNode13.Text = "图层完整性";
            treeNode14.Name = "节点4";
            treeNode14.Text = "字段完整性";
            treeNode15.Name = "节点2";
            treeNode15.Text = "完整性";
            treeNode16.Name = "节点14";
            treeNode16.Text = "坐标系";
            treeNode17.Name = "节点6";
            treeNode17.Text = "数据有效性";
            treeNode18.Name = "节点1";
            treeNode18.Text = "数学基础";
            treeNode19.Name = "节点7";
            treeNode19.Text = "空数据";
            treeNode20.Name = "节点26";
            treeNode20.Text = "自相交";
            treeNode21.Name = "节点8";
            treeNode21.Text = "间隙";
            treeNode22.Name = "节点0";
            treeNode22.Text = "压盖";
            treeNode23.Name = "节点22";
            treeNode23.Text = "拓扑";
            treeNode24.Name = "节点0";
            treeNode24.Text = "所有质检规则";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode24});
            this.treeView1.Size = new System.Drawing.Size(481, 208);
            this.treeView1.TabIndex = 4;
            // 
            // FrmDataCheck
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 353);
            this.Controls.Add(this.wizardControl1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmDataCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据质检";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private System.Windows.Forms.TreeView treeView1;
    }
}