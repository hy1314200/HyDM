namespace Check.UI.UC
{
    partial class UCResult
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.ucNavigate = new Check.UI.UCNavigate();
            this.popupMenuExceptions = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnSetAllException = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSetAllNotException = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.treeListResult = new DevExpress.XtraTreeList.TreeList();
            this.gcResult = new DevExpress.XtraGrid.GridControl();
            this.gvResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuExceptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // ucNavigate
            // 
            this.ucNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucNavigate.Location = new System.Drawing.Point(0, 0);
            this.ucNavigate.Name = "ucNavigate";
            this.ucNavigate.PageCount = 0;
            this.ucNavigate.PageIndex = 0;
            this.ucNavigate.Size = new System.Drawing.Size(843, 22);
            this.ucNavigate.TabIndex = 3;
            this.ucNavigate.Visible = false;
            this.ucNavigate.RequiredPageChanged += new Check.UI.RequiredPageChangedHandle(this.ucNavigate_RequiredPageChanged);
            // 
            // popupMenuExceptions
            // 
            this.popupMenuExceptions.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSetAllException),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSetAllNotException)});
            this.popupMenuExceptions.Manager = this.barManager1;
            this.popupMenuExceptions.Name = "popupMenuExceptions";
            // 
            // barBtnSetAllException
            // 
            this.barBtnSetAllException.Caption = "全部标记为例外";
            this.barBtnSetAllException.Id = 0;
            this.barBtnSetAllException.Name = "barBtnSetAllException";
            this.barBtnSetAllException.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSetAllException_ItemClick);
            // 
            // barBtnSetAllNotException
            // 
            this.barBtnSetAllNotException.Caption = "全部取消例外";
            this.barBtnSetAllNotException.Id = 1;
            this.barBtnSetAllNotException.Name = "barBtnSetAllNotException";
            this.barBtnSetAllNotException.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSetAllNotException_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnSetAllException,
            this.barBtnSetAllNotException});
            this.barManager1.MaxItemId = 2;
            // 
            // treeListResult
            // 
            this.treeListResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListResult.Location = new System.Drawing.Point(0, 0);
            this.treeListResult.Name = "treeListResult";
            this.treeListResult.Size = new System.Drawing.Size(843, 291);
            this.treeListResult.TabIndex = 4;
            // 
            // gcResult
            // 
            this.gcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcResult.EmbeddedNavigator.Name = "";
            gridLevelNode1.RelationName = "Level1";
            this.gcResult.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcResult.Location = new System.Drawing.Point(0, 22);
            this.gcResult.MainView = this.gvResult;
            this.gcResult.Name = "gcResult";
            this.gcResult.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcResult.Size = new System.Drawing.Size(843, 269);
            this.gcResult.TabIndex = 5;
            this.gcResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvResult});
            this.gcResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gcResult_MouseClick);
            // 
            // gvResult
            // 
            this.gvResult.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gvResult.GridControl = this.gcResult;
            this.gvResult.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "总计={0}")});
            this.gvResult.Name = "gvResult";
            this.gvResult.OptionsCustomization.AllowColumnMoving = false;
            this.gvResult.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvResult.OptionsFilter.AllowFilterEditor = false;
            this.gvResult.OptionsFilter.AllowMRUFilterList = false;
            this.gvResult.OptionsMenu.EnableColumnMenu = false;
            this.gvResult.OptionsMenu.EnableFooterMenu = false;
            this.gvResult.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvResult.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvResult.OptionsView.ShowGroupPanel = false;
            this.gvResult.OptionsView.ShowIndicator = false;
            this.gvResult.DoubleClick += new System.EventHandler(this.gvResult_DoubleClick);
            this.gvResult.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvResult_FocusedColumnChanged);
            this.gvResult.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvResult_CellValueChanged);
            this.gvResult.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvResult_CellValueChanging);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // UCResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcResult);
            this.Controls.Add(this.ucNavigate);
            this.Controls.Add(this.treeListResult);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCResult";
            this.Size = new System.Drawing.Size(843, 291);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuExceptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private UCNavigate ucNavigate;
        private DevExpress.XtraBars.BarButtonItem barBtnSetAllException;
        private DevExpress.XtraBars.BarButtonItem barBtnSetAllNotException;
        private DevExpress.XtraBars.PopupMenu popupMenuExceptions;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTreeList.TreeList treeListResult;
        private DevExpress.XtraGrid.GridControl gcResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gvResult;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;

    }
}
