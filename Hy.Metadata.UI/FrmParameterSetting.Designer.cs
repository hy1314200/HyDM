namespace Hy.Metadata.UI
{
    partial class FrmParameterSetting
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gvSetting = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolTag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSetting = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            this.barManager1.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.Always;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.bar2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.bar2.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.bar2.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.bar2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.bar2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = ((DevExpress.XtraBars.BarCanDockStyle)((((((DevExpress.XtraBars.BarCanDockStyle.Floating | DevExpress.XtraBars.BarCanDockStyle.Left)
                        | DevExpress.XtraBars.BarCanDockStyle.Top)
                        | DevExpress.XtraBars.BarCanDockStyle.Right)
                        | DevExpress.XtraBars.BarCanDockStyle.Bottom)
                        | DevExpress.XtraBars.BarCanDockStyle.Standalone)));
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true)});
            this.bar2.OptionsBar.BarState = DevExpress.XtraBars.BarState.Expanded;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem4.Caption = "刷新";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem4.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem1.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem1.Caption = "添加";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem1.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem2.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem2.Caption = "删除";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem2.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barButtonItem3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem3.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barButtonItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barButtonItem3.Caption = "保存";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barButtonItem3.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // bar3
            // 
            this.bar3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.bar3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.bar3.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.bar3.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.bar3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.bar3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.BarState = DevExpress.XtraBars.BarState.Expanded;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesBar.Bar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.Bar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.Bar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.Bar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.Bar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.Bar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.Dock.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.Dock.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.Dock.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.Dock.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.Dock.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.Dock.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.MainMenu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.MainMenu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.MainMenu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.MainMenu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.MainMenu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.MainMenu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.StatusBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.StatusBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.StatusBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.StatusBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.StatusBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.StatusBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.MenuCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesBar.SubMenu.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.ActiveTab.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.HideContainer.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.HideContainer.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HideContainer.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.HideContainer.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.HideContainer.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HideContainer.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.HidePanelButtonActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.Panel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.Panel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.Panel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.Panel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.Panel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.Panel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesDocking.Tabs.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesDocking.Tabs.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.Tabs.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesDocking.Tabs.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesDocking.Tabs.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesDocking.Tabs.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.FormCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.FilterPanelCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.GroupCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Gallery.ItemDescription.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.Item.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.Item.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Item.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.Item.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.Item.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.Item.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescription.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDescriptionDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.ItemDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageCategory.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageGroupCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barAndDockingController1.AppearancesRibbon.PageHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barAndDockingController1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.barAndDockingController1.PaintStyleName = "Skin";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController1.PropertiesBar.MenuAnimationType = DevExpress.XtraBars.AnimationType.System;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlTop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlTop.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlTop.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlTop.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlTop.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(598, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlBottom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlBottom.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlBottom.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlBottom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlBottom.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 322);
            this.barDockControlBottom.Size = new System.Drawing.Size(598, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlLeft.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlLeft.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlLeft.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlLeft.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlLeft.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 296);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.barDockControlRight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.barDockControlRight.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.barDockControlRight.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.barDockControlRight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.barDockControlRight.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(598, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 296);
            // 
            // gvSetting
            // 
            this.gvSetting.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.ColumnFilterButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.ColumnFilterButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.ColumnFilterButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.ColumnFilterButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.ColumnFilterButtonActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.ColumnFilterButtonActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.ColumnFilterButtonActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.ColumnFilterButtonActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.ColumnFilterButtonActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.CustomizationFormHint.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.CustomizationFormHint.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.CustomizationFormHint.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.CustomizationFormHint.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.CustomizationFormHint.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.CustomizationFormHint.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.DetailTip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.DetailTip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.DetailTip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.DetailTip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.DetailTip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.DetailTip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.Empty.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.Empty.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.Empty.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.Empty.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.Empty.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.Empty.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.EvenRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.EvenRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FilterCloseButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FilterCloseButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FilterCloseButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FilterCloseButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FilterCloseButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FilterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FilterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FixedLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FixedLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FixedLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FixedLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FixedLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FixedLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FocusedCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FocusedCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FocusedCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FocusedCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FocusedRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FocusedRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FocusedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.FooterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.FooterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.GroupButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.GroupButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.GroupButton.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.GroupButton.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.GroupButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.GroupButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.GroupFooter.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.GroupFooter.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.GroupFooter.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.GroupPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.GroupPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.GroupPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.GroupPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.GroupRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.GroupRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.GroupRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.HeaderPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.HeaderPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.HideSelectionRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.HideSelectionRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.HideSelectionRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.HideSelectionRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.HideSelectionRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.HideSelectionRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.HorzLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.HorzLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.HorzLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.HorzLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.HorzLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.HorzLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.OddRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.OddRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.Preview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.Preview.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.Preview.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.Row.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.Row.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.Row.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.RowSeparator.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.RowSeparator.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.RowSeparator.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.RowSeparator.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.RowSeparator.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.RowSeparator.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.SelectedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.SelectedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.SelectedRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.SelectedRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.SelectedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.SelectedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.TopNewRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.TopNewRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.TopNewRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.TopNewRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.TopNewRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.TopNewRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.VertLine.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.VertLine.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.VertLine.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.VertLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.VertLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.Appearance.ViewCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.Appearance.ViewCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.Appearance.ViewCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.EvenRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.EvenRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.FilterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.FilterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.FooterPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.FooterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.GroupFooter.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.GroupFooter.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.GroupFooter.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.GroupRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.GroupRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.GroupRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.HeaderPanel.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.HeaderPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.Lines.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.Lines.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.Lines.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.Lines.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.Lines.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.OddRow.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.OddRow.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.Preview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.Preview.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.Preview.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.AppearancePrint.Row.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gvSetting.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gvSetting.AppearancePrint.Row.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gvSetting.AppearancePrint.Row.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gvSetting.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gvSetting.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gvSetting.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.gvSetting.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolKey,
            this.gcolValue,
            this.gcolDescription,
            this.gcolTag});
            this.gvSetting.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
            this.gvSetting.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            this.gvSetting.GridControl = this.gcSetting;
            this.gvSetting.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded;
            this.gvSetting.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.gvSetting.Name = "gvSetting";
            this.gvSetting.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.Default;
            this.gvSetting.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.Default;
            this.gvSetting.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.CacheAll;
            this.gvSetting.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Default;
            this.gvSetting.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.Default;
            this.gvSetting.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            this.gvSetting.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.Default;
            this.gvSetting.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Default;
            this.gvSetting.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Default;
            this.gvSetting.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.gvSetting.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            this.gvSetting.OptionsView.ShowGroupPanel = false;
            this.gvSetting.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.gvSetting.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.gvSetting.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvSetting_FocusedRowChanged);
            // 
            // gcolKey
            // 
            this.gcolKey.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolKey.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolKey.AppearanceCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolKey.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolKey.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolKey.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolKey.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolKey.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolKey.AppearanceHeader.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolKey.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolKey.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolKey.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolKey.Caption = "配置项名称";
            this.gcolKey.FieldName = "ItemKey";
            this.gcolKey.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.Value;
            this.gcolKey.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            this.gcolKey.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Default;
            this.gcolKey.ImageAlignment = System.Drawing.StringAlignment.Near;
            this.gcolKey.Name = "gcolKey";
            this.gcolKey.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolKey.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolKey.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolKey.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Default;
            this.gcolKey.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Default;
            this.gcolKey.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolKey.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolKey.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.gcolKey.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
            this.gcolKey.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
            this.gcolKey.Visible = true;
            this.gcolKey.VisibleIndex = 0;
            this.gcolKey.Width = 142;
            // 
            // gcolValue
            // 
            this.gcolValue.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolValue.AppearanceCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolValue.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolValue.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolValue.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolValue.AppearanceHeader.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolValue.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolValue.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolValue.Caption = "取值";
            this.gcolValue.FieldName = "ItemValue";
            this.gcolValue.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.Value;
            this.gcolValue.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            this.gcolValue.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Default;
            this.gcolValue.ImageAlignment = System.Drawing.StringAlignment.Near;
            this.gcolValue.Name = "gcolValue";
            this.gcolValue.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolValue.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Default;
            this.gcolValue.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Default;
            this.gcolValue.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolValue.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolValue.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.gcolValue.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
            this.gcolValue.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
            this.gcolValue.Visible = true;
            this.gcolValue.VisibleIndex = 1;
            this.gcolValue.Width = 206;
            // 
            // gcolDescription
            // 
            this.gcolDescription.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolDescription.AppearanceCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolDescription.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolDescription.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolDescription.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolDescription.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolDescription.AppearanceHeader.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolDescription.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolDescription.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolDescription.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolDescription.Caption = "说明";
            this.gcolDescription.FieldName = "Description";
            this.gcolDescription.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.Value;
            this.gcolDescription.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            this.gcolDescription.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Default;
            this.gcolDescription.ImageAlignment = System.Drawing.StringAlignment.Near;
            this.gcolDescription.Name = "gcolDescription";
            this.gcolDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolDescription.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Default;
            this.gcolDescription.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Default;
            this.gcolDescription.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolDescription.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolDescription.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.gcolDescription.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
            this.gcolDescription.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
            this.gcolDescription.Visible = true;
            this.gcolDescription.VisibleIndex = 2;
            this.gcolDescription.Width = 211;
            // 
            // gcolTag
            // 
            this.gcolTag.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolTag.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolTag.AppearanceCell.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolTag.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolTag.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolTag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolTag.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcolTag.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcolTag.AppearanceHeader.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcolTag.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcolTag.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcolTag.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcolTag.Caption = "绑定";
            this.gcolTag.FieldName = "ID";
            this.gcolTag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.Value;
            this.gcolTag.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            this.gcolTag.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Default;
            this.gcolTag.ImageAlignment = System.Drawing.StringAlignment.Near;
            this.gcolTag.Name = "gcolTag";
            this.gcolTag.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolTag.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolTag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolTag.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Default;
            this.gcolTag.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Default;
            this.gcolTag.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolTag.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.Default;
            this.gcolTag.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            this.gcolTag.SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
            this.gcolTag.UnboundType = DevExpress.Data.UnboundColumnType.Bound;
            // 
            // gcSetting
            // 
            this.gcSetting.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.Default;
            this.gcSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSetting.EmbeddedNavigator.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.Default;
            this.gcSetting.EmbeddedNavigator.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.gcSetting.EmbeddedNavigator.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.gcSetting.EmbeddedNavigator.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.gcSetting.EmbeddedNavigator.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.gcSetting.EmbeddedNavigator.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.gcSetting.EmbeddedNavigator.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.gcSetting.EmbeddedNavigator.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.gcSetting.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.gcSetting.EmbeddedNavigator.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            this.gcSetting.Location = new System.Drawing.Point(0, 26);
            this.gcSetting.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.gcSetting.MainView = this.gvSetting;
            this.gcSetting.MenuManager = this.barManager1;
            this.gcSetting.Name = "gcSetting";
            this.gcSetting.Size = new System.Drawing.Size(598, 296);
            this.gcSetting.TabIndex = 4;
            this.gcSetting.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSetting});
            // 
            // FrmParameterSetting
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 344);
            this.Controls.Add(this.gcSetting);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "FrmParameterSetting";
            this.Text = "参数设置";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraGrid.GridControl gcSetting;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSetting;
        private DevExpress.XtraGrid.Columns.GridColumn gcolKey;
        private DevExpress.XtraGrid.Columns.GridColumn gcolValue;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gcolTag;
    }
}