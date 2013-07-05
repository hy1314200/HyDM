namespace Hy.Check.UI.UC
{
    partial class UCMapControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMapControl));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerRight = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockLegend = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TocControl = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.dockAttribute = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ucAttribute = new Hy.Check.UI.UC.UCAttribute();
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TabCtrl = new DevExpress.XtraTab.XtraTabControl();
            this.TabPageRules = new DevExpress.XtraTab.XtraTabPage();
            this.ucRulesTree = new Hy.Check.UI.UC.UCRulesTree();
            this.TabPageCheckResults = new DevExpress.XtraTab.XtraTabPage();
            this.ucResultsTree = new Hy.Check.UI.UC.UCRulesTree();
            this.dockResults = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ucResult = new Hy.Check.UI.UC.UCResult();
            this.UcMap = new ESRI.ArcGIS.Controls.AxMapControl();
            this.Toolbar = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.popMenuTOCMap = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnOpenAllLayer = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCloseAllLayer = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnZoomLayer = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSetLayerTransparency = new DevExpress.XtraBars.BarButtonItem();
            this.popMenuTOCLayer = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerRight.SuspendLayout();
            this.dockLegend.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TocControl)).BeginInit();
            this.dockAttribute.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.hideContainerLeft.SuspendLayout();
            this.dockTree.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabCtrl)).BeginInit();
            this.TabCtrl.SuspendLayout();
            this.TabPageRules.SuspendLayout();
            this.TabPageCheckResults.SuspendLayout();
            this.dockResults.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UcMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popMenuTOCMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popMenuTOCLayer)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerRight,
            this.hideContainerLeft});
            this.dockManager1.DockMode = DevExpress.XtraBars.Docking.Helpers.DockMode.VS2005;
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockResults});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerRight
            // 
            this.hideContainerRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.hideContainerRight.Controls.Add(this.dockLegend);
            this.hideContainerRight.Controls.Add(this.dockAttribute);
            this.hideContainerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.hideContainerRight.Location = new System.Drawing.Point(1217, 0);
            this.hideContainerRight.Name = "hideContainerRight";
            this.hideContainerRight.Size = new System.Drawing.Size(20, 757);
            // 
            // dockLegend
            // 
            this.dockLegend.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockLegend.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockLegend.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockLegend.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockLegend.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockLegend.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockLegend.Controls.Add(this.controlContainer1);
            this.dockLegend.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockLegend.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockLegend.ID = new System.Guid("280b405e-d63e-4014-8cc1-938005165701");
            this.dockLegend.Location = new System.Drawing.Point(0, 0);
            this.dockLegend.Name = "dockLegend";
            this.dockLegend.Options.ShowCloseButton = false;
            this.dockLegend.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockLegend.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockLegend.SavedIndex = 0;
            this.dockLegend.Size = new System.Drawing.Size(233, 757);
            this.dockLegend.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockLegend.Text = "图层列表";
            this.dockLegend.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.TocControl);
            this.controlContainer1.Location = new System.Drawing.Point(3, 29);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(226, 724);
            this.controlContainer1.TabIndex = 0;
            // 
            // TocControl
            // 
            this.TocControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TocControl.Location = new System.Drawing.Point(0, 0);
            this.TocControl.Name = "TocControl";
            this.TocControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("TocControl.OcxState")));
            this.TocControl.Size = new System.Drawing.Size(194, 621);
            this.TocControl.TabIndex = 0;
            this.TocControl.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.TocControl_OnMouseDown);
            this.TocControl.OnBeginLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnBeginLabelEditEventHandler(this.TocControl_OnBeginLabelEdit);
            // 
            // dockAttribute
            // 
            this.dockAttribute.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockAttribute.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockAttribute.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockAttribute.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockAttribute.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockAttribute.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockAttribute.Controls.Add(this.dockPanel2_Container);
            this.dockAttribute.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockAttribute.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockAttribute.ID = new System.Guid("6264ad22-6d72-408c-b7fb-c5cee4aae6df");
            this.dockAttribute.Location = new System.Drawing.Point(0, 0);
            this.dockAttribute.Name = "dockAttribute";
            this.dockAttribute.Options.ShowCloseButton = false;
            this.dockAttribute.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockAttribute.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockAttribute.SavedIndex = 1;
            this.dockAttribute.Size = new System.Drawing.Size(233, 757);
            this.dockAttribute.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockAttribute.Text = "属性";
            this.dockAttribute.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.ucAttribute);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 29);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(226, 724);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // ucAttribute
            // 
            this.ucAttribute.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ucAttribute.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ucAttribute.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.ucAttribute.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.ucAttribute.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.ucAttribute.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.ucAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAttribute.Location = new System.Drawing.Point(0, 0);
            this.ucAttribute.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.ucAttribute.Name = "ucAttribute";
            this.ucAttribute.Size = new System.Drawing.Size(226, 724);
            this.ucAttribute.TabIndex = 0;
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.hideContainerLeft.Controls.Add(this.dockTree);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(20, 757);
            // 
            // dockTree
            // 
            this.dockTree.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockTree.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockTree.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockTree.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockTree.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockTree.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockTree.Controls.Add(this.dockPanel1_Container);
            this.dockTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockTree.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockTree.ID = new System.Guid("b316a8e4-7a20-482d-bf9a-1d6cf7e05864");
            this.dockTree.Location = new System.Drawing.Point(0, 0);
            this.dockTree.Name = "dockTree";
            this.dockTree.Options.ShowCloseButton = false;
            this.dockTree.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockTree.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockTree.SavedIndex = 1;
            this.dockTree.Size = new System.Drawing.Size(200, 757);
            this.dockTree.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockTree.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.TabCtrl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 729);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // TabCtrl
            // 
            this.TabCtrl.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.AppearancePage.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.AppearancePage.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.AppearancePage.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.AppearancePage.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.AppearancePage.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.AppearancePage.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.AppearancePage.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.AppearancePage.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.AppearancePage.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.AppearancePage.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.AppearancePage.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.AppearancePage.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.AppearancePage.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.AppearancePage.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.AppearancePage.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.AppearancePage.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.AppearancePage.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.AppearancePage.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabCtrl.AppearancePage.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabCtrl.AppearancePage.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabCtrl.AppearancePage.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabCtrl.AppearancePage.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabCtrl.AppearancePage.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabCtrl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TabCtrl.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TabCtrl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.Default;
            this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrl.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.Default;
            this.TabCtrl.HeaderButtons = DevExpress.XtraTab.TabButtons.Default;
            this.TabCtrl.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Default;
            this.TabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.TabCtrl.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.TabCtrl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.TabCtrl.MultiLine = DevExpress.Utils.DefaultBoolean.Default;
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Near;
            this.TabCtrl.SelectedTabPage = this.TabPageRules;
            this.TabCtrl.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.Default;
            this.TabCtrl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.TabCtrl.ShowToolTips = DevExpress.Utils.DefaultBoolean.Default;
            this.TabCtrl.Size = new System.Drawing.Size(194, 729);
            this.TabCtrl.TabIndex = 0;
            this.TabCtrl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPageRules,
            this.TabPageCheckResults});
            // 
            // TabPageRules
            // 
            this.TabPageRules.Appearance.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageRules.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageRules.Appearance.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageRules.Appearance.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageRules.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageRules.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageRules.Appearance.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageRules.Appearance.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageRules.Appearance.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageRules.Appearance.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageRules.Appearance.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageRules.Appearance.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageRules.Appearance.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageRules.Appearance.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageRules.Appearance.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageRules.Appearance.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageRules.Appearance.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageRules.Appearance.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageRules.Appearance.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageRules.Appearance.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageRules.Appearance.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageRules.Appearance.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageRules.Appearance.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageRules.Appearance.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageRules.Appearance.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageRules.Appearance.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageRules.Appearance.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageRules.Appearance.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageRules.Appearance.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageRules.Appearance.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageRules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TabPageRules.Controls.Add(this.ucRulesTree);
            this.TabPageRules.Name = "TabPageRules";
            this.TabPageRules.ShowCloseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.TabPageRules.Size = new System.Drawing.Size(187, 722);
            this.TabPageRules.Text = "规则列表";
            this.TabPageRules.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // ucRulesTree
            // 
            this.ucRulesTree.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ucRulesTree.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ucRulesTree.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.ucRulesTree.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.ucRulesTree.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.ucRulesTree.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.ucRulesTree.CurrentSchemaId = null;
            this.ucRulesTree.CurrentTaskName = null;
            this.ucRulesTree.CurrentTemplateRules = null;
            this.ucRulesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRulesTree.Location = new System.Drawing.Point(0, 0);
            this.ucRulesTree.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.ucRulesTree.Name = "ucRulesTree";
            this.ucRulesTree.RulesSelection = null;
            this.ucRulesTree.RuleType = Hy.Check.UI.UC.RuleShowType.DefualtType;
            this.ucRulesTree.ShowRulesCount = true;
            this.ucRulesTree.ShowType = Hy.Check.UI.UC.RuleTreeShowType.ViewRules;
            this.ucRulesTree.Size = new System.Drawing.Size(187, 722);
            this.ucRulesTree.TabIndex = 0;
            // 
            // TabPageCheckResults
            // 
            this.TabPageCheckResults.Appearance.Header.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageCheckResults.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageCheckResults.Appearance.Header.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageCheckResults.Appearance.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageCheckResults.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageCheckResults.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageCheckResults.Appearance.HeaderActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageCheckResults.Appearance.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderActive.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageCheckResults.Appearance.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageCheckResults.Appearance.HeaderActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageCheckResults.Appearance.HeaderDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageCheckResults.Appearance.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageCheckResults.Appearance.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageCheckResults.Appearance.HeaderDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageCheckResults.Appearance.HeaderHotTracked.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageCheckResults.Appearance.PageClient.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.TabPageCheckResults.Appearance.PageClient.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.TabPageCheckResults.Appearance.PageClient.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.TabPageCheckResults.Appearance.PageClient.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.TabPageCheckResults.Appearance.PageClient.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.TabPageCheckResults.Appearance.PageClient.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.TabPageCheckResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TabPageCheckResults.Controls.Add(this.ucResultsTree);
            this.TabPageCheckResults.Name = "TabPageCheckResults";
            this.TabPageCheckResults.ShowCloseButton = DevExpress.Utils.DefaultBoolean.Default;
            this.TabPageCheckResults.Size = new System.Drawing.Size(187, 722);
            this.TabPageCheckResults.Text = "检查结果";
            this.TabPageCheckResults.TooltipIconType = DevExpress.Utils.ToolTipIconType.None;
            // 
            // ucResultsTree
            // 
            this.ucResultsTree.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ucResultsTree.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ucResultsTree.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.ucResultsTree.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.ucResultsTree.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.ucResultsTree.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.ucResultsTree.CurrentSchemaId = null;
            this.ucResultsTree.CurrentTaskName = null;
            this.ucResultsTree.CurrentTemplateRules = null;
            this.ucResultsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResultsTree.Location = new System.Drawing.Point(0, 0);
            this.ucResultsTree.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.ucResultsTree.Name = "ucResultsTree";
            this.ucResultsTree.RulesSelection = null;
            this.ucResultsTree.RuleType = Hy.Check.UI.UC.RuleShowType.DefualtType;
            this.ucResultsTree.ShowRulesCount = true;
            this.ucResultsTree.ShowType = Hy.Check.UI.UC.RuleTreeShowType.ViewRuleErrors;
            this.ucResultsTree.Size = new System.Drawing.Size(187, 722);
            this.ucResultsTree.TabIndex = 0;
            // 
            // dockResults
            // 
            this.dockResults.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dockResults.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.dockResults.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.dockResults.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.dockResults.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.dockResults.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.dockResults.Controls.Add(this.dockPanel3_Container);
            this.dockResults.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockResults.DockVertical = DevExpress.Utils.DefaultBoolean.Default;
            this.dockResults.ID = new System.Guid("56e0b7b8-1c85-498a-996b-23fdbb986e26");
            this.dockResults.Location = new System.Drawing.Point(20, 530);
            this.dockResults.Name = "dockResults";
            this.dockResults.Options.ShowCloseButton = false;
            this.dockResults.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockResults.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockResults.SavedIndex = 0;
            this.dockResults.Size = new System.Drawing.Size(1020, 119);
            this.dockResults.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Bottom;
            this.dockResults.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.ucResult);
            this.dockPanel3_Container.Location = new System.Drawing.Point(3, 24);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(1014, 92);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // ucResult
            // 
            this.ucResult.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ucResult.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ucResult.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.ucResult.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.ucResult.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.ucResult.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.ucResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResult.Location = new System.Drawing.Point(0, 0);
            this.ucResult.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.ucResult.Name = "ucResult";
            this.ucResult.PageCount = 0;
            this.ucResult.Size = new System.Drawing.Size(1014, 92);
            this.ucResult.TabIndex = 0;
            // 
            // UcMap
            // 
            this.UcMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UcMap.Location = new System.Drawing.Point(20, 0);
            this.UcMap.Name = "UcMap";
            this.UcMap.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("UcMap.OcxState")));
            this.UcMap.Size = new System.Drawing.Size(1197, 757);
            this.UcMap.TabIndex = 3;
            this.UcMap.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.UcMap_OnMouseDown);
            this.UcMap.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.UcMap_OnMouseMove);
            this.UcMap.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.UcMap_OnExtentUpdated);
            this.UcMap.OnKeyDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyDownEventHandler(this.UcMap_OnKeyDown);
            this.UcMap.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.UcMap_OnMapReplaced);
            // 
            // Toolbar
            // 
            this.Toolbar.Location = new System.Drawing.Point(559, 46);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Toolbar.OcxState")));
            this.Toolbar.Size = new System.Drawing.Size(265, 28);
            this.Toolbar.TabIndex = 5;
            this.Toolbar.Visible = false;
            // 
            // popMenuTOCMap
            // 
            this.popMenuTOCMap.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnOpenAllLayer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCloseAllLayer)});
            this.popMenuTOCMap.Manager = this.barManager;
            this.popMenuTOCMap.MenuAppearance.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCMap.MenuAppearance.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCMap.MenuAppearance.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCMap.MenuAppearance.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCMap.MenuAppearance.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCMap.MenuAppearance.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCMap.MenuAppearance.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCMap.MenuAppearance.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.MenuCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCMap.MenuAppearance.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCMap.MenuAppearance.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCMap.MenuAppearance.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCMap.MenuAppearance.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCMap.MenuAppearance.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCMap.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.popMenuTOCMap.Name = "popMenuTOCMap";
            // 
            // barBtnOpenAllLayer
            // 
            this.barBtnOpenAllLayer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barBtnOpenAllLayer.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnOpenAllLayer.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnOpenAllLayer.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barBtnOpenAllLayer.Caption = "打开所有图层";
            this.barBtnOpenAllLayer.Id = 0;
            this.barBtnOpenAllLayer.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barBtnOpenAllLayer.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barBtnOpenAllLayer.Name = "barBtnOpenAllLayer";
            this.barBtnOpenAllLayer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barBtnOpenAllLayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnOpenAllLayer_ItemClick);
            // 
            // barBtnCloseAllLayer
            // 
            this.barBtnCloseAllLayer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barBtnCloseAllLayer.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnCloseAllLayer.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnCloseAllLayer.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barBtnCloseAllLayer.Caption = "关闭所有图层";
            this.barBtnCloseAllLayer.Id = 1;
            this.barBtnCloseAllLayer.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barBtnCloseAllLayer.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barBtnCloseAllLayer.Name = "barBtnCloseAllLayer";
            this.barBtnCloseAllLayer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barBtnCloseAllLayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCloseAllLayer_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockManager = this.dockManager1;
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnOpenAllLayer,
            this.barBtnCloseAllLayer,
            this.barBtnZoomLayer,
            this.barBtnSetLayerTransparency});
            this.barManager.MaxItemId = 4;
            this.barManager.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.Always;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1237, 0);
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 757);
            this.barDockControlBottom.Size = new System.Drawing.Size(1237, 0);
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
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 757);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1237, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 757);
            // 
            // barBtnZoomLayer
            // 
            this.barBtnZoomLayer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barBtnZoomLayer.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnZoomLayer.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnZoomLayer.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barBtnZoomLayer.Caption = "缩放到图层";
            this.barBtnZoomLayer.Id = 2;
            this.barBtnZoomLayer.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barBtnZoomLayer.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barBtnZoomLayer.Name = "barBtnZoomLayer";
            this.barBtnZoomLayer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barBtnZoomLayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnZoomLayer_ItemClick);
            // 
            // barBtnSetLayerTransparency
            // 
            this.barBtnSetLayerTransparency.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Default;
            this.barBtnSetLayerTransparency.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnSetLayerTransparency.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.barBtnSetLayerTransparency.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Default;
            this.barBtnSetLayerTransparency.Caption = "设置图层透明度";
            this.barBtnSetLayerTransparency.Id = 3;
            this.barBtnSetLayerTransparency.ItemClickFireMode = DevExpress.XtraBars.BarItemEventFireMode.Default;
            this.barBtnSetLayerTransparency.MergeType = DevExpress.XtraBars.BarMenuMerge.Add;
            this.barBtnSetLayerTransparency.Name = "barBtnSetLayerTransparency";
            this.barBtnSetLayerTransparency.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.barBtnSetLayerTransparency.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSetLayerTransparency_ItemClick);
            // 
            // popMenuTOCLayer
            // 
            this.popMenuTOCLayer.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomLayer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSetLayerTransparency)});
            this.popMenuTOCLayer.Manager = this.barManager;
            this.popMenuTOCLayer.MenuAppearance.Menu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCLayer.MenuAppearance.Menu.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.Menu.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCLayer.MenuAppearance.Menu.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCLayer.MenuAppearance.Menu.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.Menu.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuBar.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.MenuCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStrip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.popMenuTOCLayer.MenuAppearance.SideStripNonRecent.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.popMenuTOCLayer.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.popMenuTOCLayer.Name = "popMenuTOCLayer";
            // 
            // UCMapControl
            // 
            this.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Default;
            this.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Default;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Default;
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Default;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.UcMap);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.hideContainerRight);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.Name = "UCMapControl";
            this.Size = new System.Drawing.Size(1237, 757);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerRight.ResumeLayout(false);
            this.dockLegend.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TocControl)).EndInit();
            this.dockAttribute.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.hideContainerLeft.ResumeLayout(false);
            this.dockTree.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabCtrl)).EndInit();
            this.TabCtrl.ResumeLayout(false);
            this.TabPageRules.ResumeLayout(false);
            this.TabPageCheckResults.ResumeLayout(false);
            this.dockResults.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UcMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popMenuTOCMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popMenuTOCLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockResults;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockAttribute;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockTree;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private ESRI.ArcGIS.Controls.AxMapControl UcMap;
        private DevExpress.XtraBars.Docking.DockPanel dockLegend;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private ESRI.ArcGIS.Controls.AxTOCControl TocControl;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerRight;
        private ESRI.ArcGIS.Controls.AxToolbarControl Toolbar;
        private DevExpress.XtraTab.XtraTabControl TabCtrl;
        private DevExpress.XtraTab.XtraTabPage TabPageRules;
        private DevExpress.XtraTab.XtraTabPage TabPageCheckResults;
        private UCRulesTree ucRulesTree;
        private DevExpress.XtraBars.PopupMenu popMenuTOCMap;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.PopupMenu popMenuTOCLayer;
        private DevExpress.XtraBars.BarButtonItem barBtnOpenAllLayer;
        private DevExpress.XtraBars.BarButtonItem barBtnCloseAllLayer;
        private DevExpress.XtraBars.BarButtonItem barBtnZoomLayer;
        private DevExpress.XtraBars.BarButtonItem barBtnSetLayerTransparency;
        private UCRulesTree ucResultsTree;
        private UCResult ucResult;
        private UCAttribute ucAttribute;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
    }
}
