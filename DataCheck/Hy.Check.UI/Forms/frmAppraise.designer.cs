namespace Hy.Check.UI.Forms
{
    partial class FrmAppraise
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlMark = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupOption = new DevExpress.XtraEditors.RadioGroup();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.ReportPage = new DevExpress.XtraTab.XtraTabPage();
            this.ReportListView = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ChartPage = new DevExpress.XtraTab.XtraTabPage();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.ReportPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.ChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.labelControlMark);
            this.panelControl1.Controls.Add(this.radioGroupOption);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 416);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(804, 62);
            this.panelControl1.TabIndex = 6;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(591, 22);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出为Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(718, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControlMark
            // 
            this.labelControlMark.Appearance.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControlMark.Appearance.Options.UseFont = true;
            this.labelControlMark.Location = new System.Drawing.Point(12, 12);
            this.labelControlMark.Name = "labelControlMark";
            this.labelControlMark.Size = new System.Drawing.Size(324, 35);
            this.labelControlMark.TabIndex = 3;
            this.labelControlMark.Text = "总分: 100 没有错误";
            // 
            // radioGroupOption
            // 
            this.radioGroupOption.EditValue = true;
            this.radioGroupOption.Location = new System.Drawing.Point(409, 22);
            this.radioGroupOption.Name = "radioGroupOption";
            this.radioGroupOption.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupOption.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupOption.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "检查类型"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "检查图层")});
            this.radioGroupOption.Size = new System.Drawing.Size(176, 23);
            this.radioGroupOption.TabIndex = 2;
            this.radioGroupOption.SelectedIndexChanged += new System.EventHandler(this.radioGroupOption_SelectedIndexChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.ReportPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(804, 416);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.ReportPage,
            this.ChartPage});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // ReportPage
            // 
            this.ReportPage.Controls.Add(this.ReportListView);
            this.ReportPage.Name = "ReportPage";
            this.ReportPage.Size = new System.Drawing.Size(795, 385);
            this.ReportPage.Text = "检查结果统计表";
            // 
            // ReportListView
            // 
            this.ReportListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportListView.EmbeddedNavigator.Name = "";
            gridLevelNode1.RelationName = "Level1";
            this.ReportListView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.ReportListView.Location = new System.Drawing.Point(0, 0);
            this.ReportListView.MainView = this.gridViewMain;
            this.ReportListView.Name = "ReportListView";
            this.ReportListView.Size = new System.Drawing.Size(795, 385);
            this.ReportListView.TabIndex = 1;
            this.ReportListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.ReportListView;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsBehavior.Editable = false;
            this.gridViewMain.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewMain.OptionsCustomization.AllowFilter = false;
            this.gridViewMain.OptionsMenu.EnableColumnMenu = false;
            this.gridViewMain.OptionsMenu.EnableFooterMenu = false;
            this.gridViewMain.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.OptionsView.ShowIndicator = false;
            // 
            // ChartPage
            // 
            this.ChartPage.Controls.Add(this.chartControl1);
            this.ChartPage.Name = "ChartPage";
            this.ChartPage.Size = new System.Drawing.Size(795, 385);
            this.ChartPage.Text = "检查结果统计图";
            // 
            // chartControl1
            // 
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            pieSeriesView1.RuntimeExploding = false;
            series1.View = pieSeriesView1;
            series1.ShowInLegend = false;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pieSeriesView2.RuntimeExploding = false;
            this.chartControl1.SeriesTemplate.View = pieSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(795, 385);
            this.chartControl1.TabIndex = 0;
            // 
            // frmAppraise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 478);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAppraise";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检查结果分析评价";
            this.Load += new System.EventHandler(this.frmAppraise_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ReportPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.ChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControlMark;
        private DevExpress.XtraEditors.RadioGroup radioGroupOption;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage ReportPage;
        private DevExpress.XtraGrid.GridControl ReportListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraTab.XtraTabPage ChartPage;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}