namespace Frame
{
    partial class FrmRuntime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRuntime));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.mainMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu();
            this.statusBarMessage = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.splitControlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanelRight = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelBottom = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitControlMain)).BeginInit();
            this.splitControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanelRight.SuspendLayout();
            this.dockPanelBottom.SuspendLayout();
            this.dockPanelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.mainMenu;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
            this.ribbon.ButtonGroupsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.statusBarMessage});
            this.ribbon.ItemsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 22;
            this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Default;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Default;
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Default;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Default;
            this.ribbon.Size = new System.Drawing.Size(830, 53);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Default;
            // 
            // mainMenu
            // 
            this.mainMenu.BottomPaneControlContainer = null;
            this.mainMenu.MenuAppearance.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.mainMenu.MenuAppearance.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.mainMenu.MenuAppearance.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.mainMenu.MenuAppearance.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.mainMenu.MenuAppearance.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.mainMenu.MenuAppearance.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.mainMenu.MenuAppearance.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.mainMenu.MenuAppearance.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.mainMenu.MenuAppearance.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.mainMenu.MenuAppearance.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.mainMenu.MenuAppearance.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.mainMenu.MenuAppearance.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.mainMenu.MenuAppearance.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.mainMenu.MenuAppearance.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.mainMenu.MenuAppearance.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.mainMenu.MenuAppearance.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.mainMenu.MenuAppearance.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.mainMenu.MenuAppearance.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.mainMenu.MenuAppearance.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.mainMenu.MenuAppearance.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.mainMenu.MenuAppearance.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.mainMenu.MenuAppearance.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.mainMenu.MenuAppearance.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.mainMenu.MenuAppearance.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.mainMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Ribbon = this.ribbon;
            this.mainMenu.RightPaneControlContainer = null;
            // 
            // statusBarMessage
            // 
            this.statusBarMessage.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.statusBarMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.statusBarMessage.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.statusBarMessage.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Content;
            this.statusBarMessage.Caption = "消息";
            this.statusBarMessage.Id = 20;
            this.statusBarMessage.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.statusBarMessage.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.statusBarMessage.Name = "statusBarMessage";
            this.statusBarMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            this.statusBarMessage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ribbonStatusBar.ItemLinks.Add(this.statusBarMessage, true);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 543);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(830, 24);
            // 
            // clientPanel
            // 
            this.clientPanel.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.clientPanel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.clientPanel.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.clientPanel.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.clientPanel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.clientPanel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.clientPanel.Controls.Add(this.splitControlMain);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 53);
            this.clientPanel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(830, 490);
            this.clientPanel.TabIndex = 2;
            // 
            // splitControlMain
            // 
            this.splitControlMain.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.AppearanceCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.AppearanceCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.CaptionImageLocation = DevExpress.Utils.GroupElementLocation.Default;
            this.splitControlMain.CaptionLocation = DevExpress.Utils.Locations.Default;
            this.splitControlMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.None;
            this.splitControlMain.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.splitControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitControlMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
            this.splitControlMain.Location = new System.Drawing.Point(0, 0);
            this.splitControlMain.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.splitControlMain.Name = "splitControlMain";
            this.splitControlMain.Panel1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.Panel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.Panel1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.Panel1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.Panel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.Panel1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.Panel1.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.Panel1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.Panel1.AppearanceCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.Panel1.AppearanceCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.Panel1.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.Panel1.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.Panel1.CaptionLocation = DevExpress.Utils.Locations.Default;
            this.splitControlMain.Panel1.Text = "Panel1";
            this.splitControlMain.Panel2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.Panel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.Panel2.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.Panel2.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.Panel2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.Panel2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.Panel2.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.splitControlMain.Panel2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.splitControlMain.Panel2.AppearanceCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.splitControlMain.Panel2.AppearanceCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.splitControlMain.Panel2.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.splitControlMain.Panel2.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.splitControlMain.Panel2.CaptionLocation = DevExpress.Utils.Locations.Default;
            this.splitControlMain.Panel2.Text = "Panel2";
            this.splitControlMain.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            this.splitControlMain.Size = new System.Drawing.Size(830, 490);
            this.splitControlMain.SplitterPosition = 393;
            this.splitControlMain.TabIndex = 0;
            this.splitControlMain.Text = "splitContainerControl1";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Black";
            this.defaultLookAndFeel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            // 
            // dockManager1
            // 
            this.dockManager1.DockMode = DevExpress.XtraBars.Docking.Helpers.DockMode.VS2005;
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelRight,
            this.dockPanelBottom,
            this.dockPanelLeft});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanelRight
            // 
            this.dockPanelRight.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockPanelRight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockPanelRight.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockPanelRight.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockPanelRight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockPanelRight.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockPanelRight.Controls.Add(this.controlContainer1);
            this.dockPanelRight.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelRight.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockPanelRight.ID = new System.Guid("f8a7e763-f770-491f-bb4f-fba48459b60f");
            this.dockPanelRight.Location = new System.Drawing.Point(0, 0);
            this.dockPanelRight.Name = "dockPanelRight";
            this.dockPanelRight.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanelRight.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelRight.SavedIndex = 0;
            this.dockPanelRight.Size = new System.Drawing.Size(200, 490);
            this.dockPanelRight.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelRight.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer1
            // 
            this.controlContainer1.Location = new System.Drawing.Point(3, 29);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(194, 458);
            this.controlContainer1.TabIndex = 0;
            // 
            // dockPanelBottom
            // 
            this.dockPanelBottom.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockPanelBottom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockPanelBottom.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockPanelBottom.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockPanelBottom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockPanelBottom.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockPanelBottom.Controls.Add(this.controlContainer2);
            this.dockPanelBottom.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelBottom.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockPanelBottom.ID = new System.Guid("211bbdb2-749f-4c90-94ed-b7ef62c7e8ab");
            this.dockPanelBottom.Location = new System.Drawing.Point(0, 395);
            this.dockPanelBottom.Name = "dockPanelBottom";
            this.dockPanelBottom.OriginalSize = new System.Drawing.Size(200, 148);
            this.dockPanelBottom.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelBottom.SavedIndex = 0;
            this.dockPanelBottom.Size = new System.Drawing.Size(830, 148);
            this.dockPanelBottom.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelBottom.Text = "dockPanel1";
            this.dockPanelBottom.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer2
            // 
            this.controlContainer2.Location = new System.Drawing.Point(3, 29);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(824, 116);
            this.controlContainer2.TabIndex = 0;
            // 
            // dockPanelLeft
            // 
            this.dockPanelLeft.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockPanelLeft.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockPanelLeft.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockPanelLeft.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockPanelLeft.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockPanelLeft.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockPanelLeft.Controls.Add(this.dockPanel1_Container);
            this.dockPanelLeft.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLeft.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockPanelLeft.ID = new System.Guid("6f43f807-6609-4263-b57a-1b15bfaddd11");
            this.dockPanelLeft.Location = new System.Drawing.Point(0, 53);
            this.dockPanelLeft.Name = "dockPanelLeft";
            this.dockPanelLeft.OriginalSize = new System.Drawing.Size(187, 200);
            this.dockPanelLeft.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLeft.SavedIndex = 0;
            this.dockPanelLeft.Size = new System.Drawing.Size(187, 490);
            this.dockPanelLeft.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelLeft.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 29);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(181, 458);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // FrmRuntime
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 567);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRuntime";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitControlMain)).EndInit();
            this.splitControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanelRight.ResumeLayout(false);
            this.dockPanelBottom.ResumeLayout(false);
            this.dockPanelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelLeft;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelRight;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraEditors.SplitContainerControl splitControlMain;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelBottom;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu mainMenu;
        private DevExpress.XtraBars.BarStaticItem statusBarMessage;
    }
}