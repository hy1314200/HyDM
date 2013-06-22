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
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.statusBarMessage = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.splitControlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.uC3DWindow1 = new Frame.UC3DWindow();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelRight = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanelLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tabControlTree = new DevExpress.XtraTab.XtraTabControl();
            this.tpTeTree = new DevExpress.XtraTab.XtraTabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axTEInformationWindow1 = new AxTerraExplorerX.AxTEInformationWindow();
            this.tpToc = new DevExpress.XtraTab.XtraTabPage();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitControlMain)).BeginInit();
            this.splitControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanelRight.SuspendLayout();
            this.dockPanelLeft.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlTree)).BeginInit();
            this.tabControlTree.SuspendLayout();
            this.tpTeTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTEInformationWindow1)).BeginInit();
            this.tpToc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
            this.ribbon.ButtonGroupsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.statusBarMessage});
            this.ribbon.ItemsVertAlign = DevExpress.Utils.VertAlignment.Default;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 12;
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
            // applicationMenu1
            // 
            this.applicationMenu1.BottomPaneControlContainer = null;
            this.applicationMenu1.MenuAppearance.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.applicationMenu1.MenuAppearance.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.applicationMenu1.MenuAppearance.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.applicationMenu1.MenuAppearance.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.applicationMenu1.MenuAppearance.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.applicationMenu1.MenuAppearance.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.applicationMenu1.MenuAppearance.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.applicationMenu1.MenuAppearance.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.applicationMenu1.MenuAppearance.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.applicationMenu1.MenuAppearance.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.applicationMenu1.MenuAppearance.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.applicationMenu1.MenuAppearance.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.applicationMenu1.MenuAppearance.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.applicationMenu1.MenuAppearance.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.applicationMenu1.MenuAppearance.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.applicationMenu1.MenuAppearance.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.applicationMenu1.MenuAppearance.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.applicationMenu1.MenuAppearance.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.applicationMenu1.MenuAppearance.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.applicationMenu1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbon;
            this.applicationMenu1.RightPaneControlContainer = null;
            // 
            // statusBarMessage
            // 
            this.statusBarMessage.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.statusBarMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.statusBarMessage.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.statusBarMessage.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.statusBarMessage.Caption = "消息";
            this.statusBarMessage.Id = 11;
            this.statusBarMessage.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.statusBarMessage.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.statusBarMessage.Name = "statusBarMessage";
            this.statusBarMessage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.clientPanel.Location = new System.Drawing.Point(187, 53);
            this.clientPanel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(643, 490);
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
            this.splitControlMain.Panel1.Controls.Add(this.uC3DWindow1);
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
            this.splitControlMain.Panel2.Controls.Add(this.axMapControl1);
            this.splitControlMain.Panel2.Text = "Panel2";
            this.splitControlMain.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            this.splitControlMain.Size = new System.Drawing.Size(643, 490);
            this.splitControlMain.SplitterPosition = 318;
            this.splitControlMain.TabIndex = 0;
            this.splitControlMain.Text = "splitContainerControl1";
            // 
            // uC3DWindow1
            // 
            this.uC3DWindow1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uC3DWindow1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.uC3DWindow1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.uC3DWindow1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.uC3DWindow1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.uC3DWindow1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.uC3DWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC3DWindow1.Location = new System.Drawing.Point(0, 0);
            this.uC3DWindow1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.uC3DWindow1.Name = "uC3DWindow1";
            this.uC3DWindow1.Size = new System.Drawing.Size(318, 490);
            this.uC3DWindow1.TabIndex = 1;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(319, 490);
            this.axMapControl1.TabIndex = 0;
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
            this.dockPanelRight});
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
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
            this.dockPanelLeft.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanelLeft.Size = new System.Drawing.Size(187, 490);
            this.dockPanelLeft.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockPanelLeft.Text = "图层目录";
            this.dockPanelLeft.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.tabControlTree);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 29);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(181, 458);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // tabControlTree
            // 
            this.tabControlTree.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.AppearancePage.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.AppearancePage.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.AppearancePage.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.AppearancePage.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.AppearancePage.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.AppearancePage.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.AppearancePage.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.AppearancePage.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.AppearancePage.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.AppearancePage.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.AppearancePage.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.AppearancePage.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.AppearancePage.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.AppearancePage.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.AppearancePage.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.AppearancePage.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.AppearancePage.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabControlTree.AppearancePage.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tabControlTree.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tabControlTree.AppearancePage.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tabControlTree.AppearancePage.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tabControlTree.AppearancePage.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tabControlTree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.tabControlTree.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.tabControlTree.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.Default;
            this.tabControlTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTree.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.Default;
            this.tabControlTree.HeaderButtons = DevExpress.XtraTab.TabButtons.Default;
            this.tabControlTree.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Default;
            this.tabControlTree.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.tabControlTree.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Default;
            this.tabControlTree.Location = new System.Drawing.Point(0, 0);
            this.tabControlTree.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.tabControlTree.MultiLine = DevExpress.Utils.DefaultBoolean.Default;
            this.tabControlTree.Name = "tabControlTree";
            this.tabControlTree.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Near;
            this.tabControlTree.SelectedTabPage = this.tpTeTree;
            this.tabControlTree.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.Default;
            this.tabControlTree.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlTree.ShowToolTips = DevExpress.Utils.DefaultBoolean.Default;
            this.tabControlTree.Size = new System.Drawing.Size(181, 458);
            this.tabControlTree.TabIndex = 0;
            this.tabControlTree.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpTeTree,
            this.tpToc});
            // 
            // tpTeTree
            // 
            this.tpTeTree.Appearance.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpTeTree.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpTeTree.Appearance.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpTeTree.Appearance.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpTeTree.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpTeTree.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpTeTree.Appearance.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpTeTree.Appearance.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpTeTree.Appearance.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpTeTree.Appearance.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpTeTree.Appearance.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpTeTree.Appearance.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpTeTree.Appearance.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpTeTree.Appearance.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpTeTree.Appearance.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpTeTree.Appearance.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpTeTree.Appearance.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpTeTree.Appearance.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpTeTree.Appearance.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpTeTree.Appearance.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpTeTree.Appearance.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpTeTree.Appearance.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpTeTree.Appearance.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpTeTree.Appearance.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpTeTree.Appearance.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpTeTree.Appearance.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpTeTree.Appearance.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpTeTree.Appearance.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpTeTree.Appearance.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpTeTree.Appearance.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpTeTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpTeTree.Controls.Add(this.axLicenseControl1);
            this.tpTeTree.Controls.Add(this.axTEInformationWindow1);
            this.tpTeTree.Name = "tpTeTree";
            this.tpTeTree.ShowCloseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.tpTeTree.Size = new System.Drawing.Size(174, 451);
            this.tpTeTree.Text = "三维数据";
            this.tpTeTree.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(47, 154);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 2;
            // 
            // axTEInformationWindow1
            // 
            this.axTEInformationWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTEInformationWindow1.Enabled = true;
            this.axTEInformationWindow1.Location = new System.Drawing.Point(0, 0);
            this.axTEInformationWindow1.Name = "axTEInformationWindow1";
            this.axTEInformationWindow1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTEInformationWindow1.OcxState")));
            this.axTEInformationWindow1.Size = new System.Drawing.Size(174, 451);
            this.axTEInformationWindow1.TabIndex = 0;
            // 
            // tpToc
            // 
            this.tpToc.Appearance.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpToc.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpToc.Appearance.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpToc.Appearance.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpToc.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpToc.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpToc.Appearance.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpToc.Appearance.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpToc.Appearance.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpToc.Appearance.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpToc.Appearance.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpToc.Appearance.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpToc.Appearance.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpToc.Appearance.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpToc.Appearance.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpToc.Appearance.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpToc.Appearance.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpToc.Appearance.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpToc.Appearance.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpToc.Appearance.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpToc.Appearance.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpToc.Appearance.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpToc.Appearance.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpToc.Appearance.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpToc.Appearance.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tpToc.Appearance.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.tpToc.Appearance.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.tpToc.Appearance.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.tpToc.Appearance.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.tpToc.Appearance.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.tpToc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tpToc.Controls.Add(this.axTOCControl1);
            this.tpToc.Name = "tpToc";
            this.tpToc.PageVisible = false;
            this.tpToc.ShowCloseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.tpToc.Size = new System.Drawing.Size(174, 451);
            this.tpToc.Text = "二维图层";
            this.tpToc.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(174, 451);
            this.axTOCControl1.TabIndex = 2;
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
            this.Controls.Add(this.dockPanelLeft);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRuntime";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitControlMain)).EndInit();
            this.splitControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanelRight.ResumeLayout(false);
            this.dockPanelLeft.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlTree)).EndInit();
            this.tabControlTree.ResumeLayout(false);
            this.tpTeTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTEInformationWindow1)).EndInit();
            this.tpToc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
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
        private DevExpress.XtraTab.XtraTabControl tabControlTree;
        private DevExpress.XtraTab.XtraTabPage tpTeTree;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private AxTerraExplorerX.AxTEInformationWindow axTEInformationWindow1;
        private DevExpress.XtraTab.XtraTabPage tpToc;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitControlMain;
        private UC3DWindow uC3DWindow1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.BarButtonItem statusBarMessage;
    }
}