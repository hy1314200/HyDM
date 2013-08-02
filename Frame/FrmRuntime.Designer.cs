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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRuntime));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.mainMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.statusBarMessage = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.tabCenter = new DevExpress.XtraTab.XtraTabControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelRight = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelBottom = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanelRight.SuspendLayout();
            this.dockPanelLeft.SuspendLayout();
            this.dockPanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.mainMenu;
            resources.ApplyResources(this.ribbon, "ribbon");
            this.ribbon.ButtonGroupsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.statusBarMessage});
            this.ribbon.ItemsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.MaxItemId = 22;
            this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Default;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Default;
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Default;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Default;
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
            resources.ApplyResources(this.statusBarMessage, "statusBarMessage");
            this.statusBarMessage.Id = 20;
            this.statusBarMessage.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.statusBarMessage.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.statusBarMessage.Name = "statusBarMessage";
            this.statusBarMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            this.statusBarMessage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // ribbonStatusBar
            // 
            resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
            this.ribbonStatusBar.ItemLinks.Add(this.statusBarMessage, true);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
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
            this.clientPanel.Controls.Add(this.tabCenter);
            resources.ApplyResources(this.clientPanel, "clientPanel");
            this.clientPanel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.clientPanel.Name = "clientPanel";
            // 
            // tabCenter
            // 
            this.tabCenter.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabCenter.AppearancePage.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.AppearancePage.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.AppearancePage.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabCenter.AppearancePage.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.AppearancePage.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.AppearancePage.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.AppearancePage.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.AppearancePage.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabCenter.AppearancePage.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.AppearancePage.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.AppearancePage.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.AppearancePage.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.AppearancePage.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabCenter.AppearancePage.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.AppearancePage.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.AppearancePage.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.AppearancePage.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.AppearancePage.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabCenter.AppearancePage.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabCenter.AppearancePage.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabCenter.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabCenter.AppearancePage.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabCenter.AppearancePage.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabCenter.AppearancePage.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            resources.ApplyResources(this.tabCenter, "tabCenter");
            this.tabCenter.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.tabCenter.Name = "tabCenter";
            this.tabCenter.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabCenter_SelectedPageChanged);
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
            this.dockPanelLeft,
            this.dockPanelBottom});
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
            this.dockPanelRight.FloatVertical = true;
            this.dockPanelRight.ID = new System.Guid("f8a7e763-f770-491f-bb4f-fba48459b60f");
            resources.ApplyResources(this.dockPanelRight, "dockPanelRight");
            this.dockPanelRight.Name = "dockPanelRight";
            this.dockPanelRight.OriginalSize = new System.Drawing.Size(277, 245);
            this.dockPanelRight.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelRight.SavedIndex = 0;
            this.dockPanelRight.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelRight.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer1
            // 
            resources.ApplyResources(this.controlContainer1, "controlContainer1");
            this.controlContainer1.Name = "controlContainer1";
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
            resources.ApplyResources(this.dockPanelLeft, "dockPanelLeft");
            this.dockPanelLeft.Name = "dockPanelLeft";
            this.dockPanelLeft.OriginalSize = new System.Drawing.Size(207, 245);
            this.dockPanelLeft.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLeft.SavedIndex = 0;
            this.dockPanelLeft.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelLeft.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            resources.ApplyResources(this.dockPanel1_Container, "dockPanel1_Container");
            this.dockPanel1_Container.Name = "dockPanel1_Container";
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
            this.dockPanelBottom.FloatVertical = true;
            this.dockPanelBottom.ID = new System.Guid("211bbdb2-749f-4c90-94ed-b7ef62c7e8ab");
            resources.ApplyResources(this.dockPanelBottom, "dockPanelBottom");
            this.dockPanelBottom.Name = "dockPanelBottom";
            this.dockPanelBottom.OriginalSize = new System.Drawing.Size(415, 139);
            this.dockPanelBottom.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanelBottom.SavedIndex = 0;
            this.dockPanelBottom.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelBottom.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer2
            // 
            resources.ApplyResources(this.controlContainer2, "controlContainer2");
            this.controlContainer2.Name = "controlContainer2";
            // 
            // FrmRuntime
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.Name = "FrmRuntime";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanelRight.ResumeLayout(false);
            this.dockPanelLeft.ResumeLayout(false);
            this.dockPanelBottom.ResumeLayout(false);
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
        private DevExpress.XtraBars.Docking.DockPanel dockPanelBottom;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu mainMenu;
        private DevExpress.XtraBars.BarStaticItem statusBarMessage;
        private DevExpress.XtraTab.XtraTabControl tabCenter;
    }
}