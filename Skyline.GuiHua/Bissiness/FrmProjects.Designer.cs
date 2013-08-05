namespace Skyline.GuiHua.Bussiness
{
    partial class FrmProjects
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
            this.ucPorject1 = new Skyline.GuiHua.Bussiness.UCPorject();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClause = new DevExpress.XtraEditors.TextEdit();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClause.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ucPorject1
            // 
            this.ucPorject1.Location = new System.Drawing.Point(3, 54);
            this.ucPorject1.Name = "ucPorject1";
            this.ucPorject1.Size = new System.Drawing.Size(703, 309);
            this.ucPorject1.TabIndex = 0;
            this.ucPorject1.FocusedProjectChanged += new Skyline.GuiHua.Bussiness.ProjectEventHandle(this.ucPorject1_FocusedProjectChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnSearch.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnSearch.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnSearch.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnSearch.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnSearch.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnSearch.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnSearch.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnSearch.Location = new System.Drawing.Point(381, 16);
            this.btnSearch.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查找";
            this.btnSearch.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClause);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(692, 48);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "项目搜索";
            // 
            // txtClause
            // 
            this.txtClause.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.txtClause.Location = new System.Drawing.Point(81, 18);
            this.txtClause.Name = "txtClause";
            this.txtClause.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.Default;
            this.txtClause.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            this.txtClause.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtClause.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtClause.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtClause.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtClause.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtClause.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtClause.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtClause.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtClause.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtClause.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtClause.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtClause.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtClause.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtClause.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtClause.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtClause.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtClause.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtClause.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtClause.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.txtClause.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txtClause.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.txtClause.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.txtClause.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.txtClause.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.txtClause.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtClause.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtClause.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtClause.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Default;
            this.txtClause.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.txtClause.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Default;
            this.txtClause.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtClause.Size = new System.Drawing.Size(282, 21);
            this.txtClause.TabIndex = 2;
            this.txtClause.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // btnOpen
            // 
            this.btnOpen.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnOpen.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnOpen.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnOpen.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnOpen.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnOpen.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnOpen.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnOpen.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOpen.Enabled = false;
            this.btnOpen.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnOpen.Location = new System.Drawing.Point(507, 369);
            this.btnOpen.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开";
            this.btnOpen.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.simpleButton3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.simpleButton3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.simpleButton3.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.simpleButton3.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.simpleButton3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.simpleButton3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.simpleButton3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.simpleButton3.Location = new System.Drawing.Point(620, 369);
            this.simpleButton3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "关闭";
            this.simpleButton3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // btnImport
            // 
            this.btnImport.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnImport.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnImport.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnImport.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnImport.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnImport.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnImport.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnImport.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnImport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImport.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnImport.Location = new System.Drawing.Point(84, 369);
            this.btnImport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "导入项目";
            this.btnImport.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.btnDelete.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.btnDelete.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.btnDelete.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.btnDelete.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.btnDelete.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.btnDelete.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.btnDelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.Default;
            this.btnDelete.Location = new System.Drawing.Point(211, 369);
            this.btnDelete.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除项目";
            this.btnDelete.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FrmProjects
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 398);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.ucPorject1);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmProjects";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtClause.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCPorject ucPorject1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtClause;
        private DevExpress.XtraEditors.SimpleButton btnOpen;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}