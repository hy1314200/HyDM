namespace Check.UI.UC
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
            this.ucAttribute = new Check.UI.UC.UCAttribute();
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TabCtrl = new DevExpress.XtraTab.XtraTabControl();
            this.TabPageRules = new DevExpress.XtraTab.XtraTabPage();
            this.ucRulesTree = new Check.UI.UC.UCRulesTree();
            this.TabPageCheckResults = new DevExpress.XtraTab.XtraTabPage();
            this.ucResultsTree = new Check.UI.UC.UCRulesTree();
            this.dockResults = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ucResult = new Check.UI.UC.UCResult();
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
            this.hideContainerRight.Controls.Add(this.dockLegend);
            this.hideContainerRight.Controls.Add(this.dockAttribute);
            this.hideContainerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.hideContainerRight.Location = new System.Drawing.Point(1040, 0);
            this.hideContainerRight.Name = "hideContainerRight";
            this.hideContainerRight.Size = new System.Drawing.Size(20, 649);
            // 
            // dockLegend
            // 
            this.dockLegend.Controls.Add(this.controlContainer1);
            this.dockLegend.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockLegend.ID = new System.Guid("280b405e-d63e-4014-8cc1-938005165701");
            this.dockLegend.Location = new System.Drawing.Point(0, 0);
            this.dockLegend.Name = "dockLegend";
            this.dockLegend.Options.ShowCloseButton = false;
            this.dockLegend.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockLegend.SavedIndex = 0;
            this.dockLegend.Size = new System.Drawing.Size(200, 649);
            this.dockLegend.Text = "图层列表";
            this.dockLegend.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.TocControl);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(194, 621);
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
            this.TocControl.OnBeginLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnBeginLabelEditEventHandler(this.TocControl_OnBeginLabelEdit);
            this.TocControl.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.TocControl_OnMouseDown);
            // 
            // dockAttribute
            // 
            this.dockAttribute.Controls.Add(this.dockPanel2_Container);
            this.dockAttribute.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockAttribute.ID = new System.Guid("6264ad22-6d72-408c-b7fb-c5cee4aae6df");
            this.dockAttribute.Location = new System.Drawing.Point(0, 0);
            this.dockAttribute.Name = "dockAttribute";
            this.dockAttribute.Options.ShowCloseButton = false;
            this.dockAttribute.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockAttribute.SavedIndex = 1;
            this.dockAttribute.Size = new System.Drawing.Size(200, 649);
            this.dockAttribute.Text = "属性";
            this.dockAttribute.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.ucAttribute);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(194, 621);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // ucAttribute
            // 
            this.ucAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAttribute.Location = new System.Drawing.Point(0, 0);
            this.ucAttribute.Name = "ucAttribute";
            this.ucAttribute.Size = new System.Drawing.Size(194, 621);
            this.ucAttribute.TabIndex = 0;
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.Controls.Add(this.dockTree);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(20, 649);
            // 
            // dockTree
            // 
            this.dockTree.Controls.Add(this.dockPanel1_Container);
            this.dockTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockTree.ID = new System.Guid("b316a8e4-7a20-482d-bf9a-1d6cf7e05864");
            this.dockTree.Location = new System.Drawing.Point(-260, 0);
            this.dockTree.Name = "dockTree";
            this.dockTree.Options.ShowCloseButton = false;
            this.dockTree.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockTree.SavedIndex = 1;
            this.dockTree.Size = new System.Drawing.Size(260, 649);
            this.dockTree.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.TabCtrl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(254, 621);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // TabCtrl
            // 
            this.TabCtrl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TabCtrl.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCtrl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.TabCtrl.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.SelectedTabPage = this.TabPageRules;
            this.TabCtrl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.TabCtrl.Size = new System.Drawing.Size(254, 621);
            this.TabCtrl.TabIndex = 0;
            this.TabCtrl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPageRules,
            this.TabPageCheckResults});
            // 
            // TabPageRules
            // 
            this.TabPageRules.Controls.Add(this.ucRulesTree);
            this.TabPageRules.Name = "TabPageRules";
            this.TabPageRules.Size = new System.Drawing.Size(245, 589);
            this.TabPageRules.Text = "规则列表";
            // 
            // ucRulesTree
            // 
            this.ucRulesTree.CurrentSchemaId = null;
            this.ucRulesTree.CurrentTaskName = null;
            this.ucRulesTree.CurrentTemplateRules = null;
            this.ucRulesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRulesTree.Location = new System.Drawing.Point(0, 0);
            this.ucRulesTree.Name = "ucRulesTree";
            this.ucRulesTree.RulesSelection = null;
            this.ucRulesTree.RuleType = Check.UI.UC.RuleShowType.DefualtType;
            this.ucRulesTree.ShowRulesCount = true;
            this.ucRulesTree.ShowType = Check.UI.UC.RuleTreeShowType.ViewRules;
            this.ucRulesTree.Size = new System.Drawing.Size(245, 589);
            this.ucRulesTree.TabIndex = 0;
            // 
            // TabPageCheckResults
            // 
            this.TabPageCheckResults.Controls.Add(this.ucResultsTree);
            this.TabPageCheckResults.Name = "TabPageCheckResults";
            this.TabPageCheckResults.Size = new System.Drawing.Size(245, 612);
            this.TabPageCheckResults.Text = "检查结果";
            // 
            // ucResultsTree
            // 
            this.ucResultsTree.CurrentSchemaId = null;
            this.ucResultsTree.CurrentTaskName = null;
            this.ucResultsTree.CurrentTemplateRules = null;
            this.ucResultsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResultsTree.Location = new System.Drawing.Point(0, 0);
            this.ucResultsTree.Name = "ucResultsTree";
            this.ucResultsTree.RulesSelection = null;
            this.ucResultsTree.RuleType = Check.UI.UC.RuleShowType.DefualtType;
            this.ucResultsTree.ShowRulesCount = true;
            this.ucResultsTree.ShowType = Check.UI.UC.RuleTreeShowType.ViewRuleErrors;
            this.ucResultsTree.Size = new System.Drawing.Size(245, 612);
            this.ucResultsTree.TabIndex = 0;
            // 
            // dockResults
            // 
            this.dockResults.Controls.Add(this.dockPanel3_Container);
            this.dockResults.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockResults.ID = new System.Guid("56e0b7b8-1c85-498a-996b-23fdbb986e26");
            this.dockResults.Location = new System.Drawing.Point(20, 530);
            this.dockResults.Name = "dockResults";
            this.dockResults.Options.ShowCloseButton = false;
            this.dockResults.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockResults.SavedIndex = 0;
            this.dockResults.Size = new System.Drawing.Size(1020, 119);
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
            this.ucResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResult.Location = new System.Drawing.Point(0, 0);
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
            this.UcMap.Size = new System.Drawing.Size(1020, 649);
            this.UcMap.TabIndex = 3;
            this.UcMap.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.UcMap_OnExtentUpdated);
            this.UcMap.OnKeyDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyDownEventHandler(this.UcMap_OnKeyDown);
            this.UcMap.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.UcMap_OnMapReplaced);
            this.UcMap.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.UcMap_OnMouseDown);
            this.UcMap.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.UcMap_OnMouseMove);
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
            this.popMenuTOCMap.Name = "popMenuTOCMap";
            // 
            // barBtnOpenAllLayer
            // 
            this.barBtnOpenAllLayer.Caption = "打开所有图层";
            this.barBtnOpenAllLayer.Id = 0;
            this.barBtnOpenAllLayer.Name = "barBtnOpenAllLayer";
            this.barBtnOpenAllLayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnOpenAllLayer_ItemClick);
            // 
            // barBtnCloseAllLayer
            // 
            this.barBtnCloseAllLayer.Caption = "关闭所有图层";
            this.barBtnCloseAllLayer.Id = 1;
            this.barBtnCloseAllLayer.Name = "barBtnCloseAllLayer";
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
            // 
            // barBtnZoomLayer
            // 
            this.barBtnZoomLayer.Caption = "缩放到图层";
            this.barBtnZoomLayer.Id = 2;
            this.barBtnZoomLayer.Name = "barBtnZoomLayer";
            this.barBtnZoomLayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnZoomLayer_ItemClick);
            // 
            // barBtnSetLayerTransparency
            // 
            this.barBtnSetLayerTransparency.Caption = "设置图层透明度";
            this.barBtnSetLayerTransparency.Id = 3;
            this.barBtnSetLayerTransparency.Name = "barBtnSetLayerTransparency";
            this.barBtnSetLayerTransparency.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSetLayerTransparency_ItemClick);
            // 
            // popMenuTOCLayer
            // 
            this.popMenuTOCLayer.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnZoomLayer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSetLayerTransparency)});
            this.popMenuTOCLayer.Manager = this.barManager;
            this.popMenuTOCLayer.Name = "popMenuTOCLayer";
            // 
            // UCMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.UcMap);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.hideContainerRight);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCMapControl";
            this.Size = new System.Drawing.Size(1060, 649);
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
