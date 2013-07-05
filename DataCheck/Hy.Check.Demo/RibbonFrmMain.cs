using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using DevExpress.LocalizationCHS;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraCharts.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraTreeList.Localization;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Common.UI;
using Hy.Check.Command;
using Hy.Check.Demo.Helper;

namespace Hy.Check.Demo
{
    public partial class RibbonFrmMain : RibbonForm
    {
        //private UcMapCheck m_UcMapCheck;
        private CmdDevExpressAdapter m_CmdDevExpressAdapter;
        //private SystemHotKeyManager m_SystemHotKeyManager;

        //private UCMapControl ucPropertyErrMap;
        //private UCMapControl ucTopoErrMap;
        //private UCMapControl m_UCDataMap;


        private readonly string[] strprogids =
        {
            //任务管理
            "Hy.Check.Command.CustomCommand.CreateTaskCommand",
             "Hy.Check.Command.CustomCommand.OpenTaskCommand",
             "Hy.Check.Command.CustomCommand.BatchCreateTaskCommand",
             "Hy.Check.Command.CustomCommand.ExcuteTaskCommand",
             "Hy.Check.Command.CustomCommand.CheckWorkFlowCommand",
             "Hy.Check.Command.CustomCommand.PreCheckCommand",
             "Hy.Check.Command.CustomCommand.CheckErrorsEvaluate",
             "Hy.Check.Command.CustomCommand.SqlQueryCommand",
             "Hy.Check.Command.CustomCommand.SystemHelpCommand",
             "Hy.Check.Command.CustomCommand.ExportErrosToExcelCommand",
             "Hy.Check.Command.CustomCommand.ExportErrorRecordCommand",
             "Hy.Check.Command.CustomCommand.ExportToVCTCommand",
             "Hy.Check.Command.CustomCommand.ViewTaskCheckLogCommand",
             "Hy.Check.Command.CustomCommand.ViewFeaturesStatisticCommand",

            //地图工具
            "Hy.Check.Command.CustomCommand.Identify",
            "Hy.Check.Command.MeasureCommand.ToolMeasureArea",
            "Hy.Check.Command.MeasureCommand.ToolMeasureLength",
            "Hy.Check.Command.CustomCommand.MapIdentifyCommand",
            "Hy.Check.Command.CustomCommand.ClearSelectionCommand"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonFrmMain"/> class.
        /// </summary>
        public RibbonFrmMain()
        {
            InitializeComponent();

            InitForm();
            SplashScreen.CloseForm();
        }

        /// <summary>
        /// 初始化界面框架
        /// </summary>
        private void InitForm()
        {
            LocalizeSystem();

            //CheckApplication.CurrentTask = new Hy.Check.Task.Task();
            //添加属性视图UC
            CheckApplication.m_UCDataMap = new Hy.Check.UI.UC.UCMapControl();

            CheckApplication.m_UCDataMap.MapMouseMove += ucTopoErrMap_MouseMove;
            CheckApplication.m_UCDataMap.InitUc();
            CheckApplication.m_UCDataMap.Dock = DockStyle.Fill;
            pnlMapCheck.Controls.Add(CheckApplication.m_UCDataMap);
            pnlMapCheck.Dock = DockStyle.Fill;
            pnlMapCheck.Visible = true;

            ribbon.SelectedPage = rpDbCheck;
            //注册cmd
            m_CmdDevExpressAdapter = new CmdDevExpressAdapter();
            m_CmdDevExpressAdapter.ToolbarControl = CheckApplication.m_UCDataMap.ToolbarControl;
            m_CmdDevExpressAdapter.RibbonCtrl = ribbon;
            m_CmdDevExpressAdapter.AddCommands(strprogids);
            m_CmdDevExpressAdapter.UpdateToolbar();

            //m_SystemHotKeyManager = new SystemHotKeyManager();
            //CCheckApplication.m_arrAllButtonItem = m_SystemHotKeyManager.BuilderSystemHotKey(1,this.ribbon);
        }

        /// <summary>
        /// 本地化DevExpress控件
        /// </summary>
        private static void LocalizeSystem()
        {
            //BarLocalizer.Active = new DevExpressXtraBarsLocalizationCHS();
            //Localizer.Active = new DevExpressXtraEditorsLocalizationCHS();
            //GridLocalizer.Active = new DevExpressXtraGridLocalizationCHS();
            //BarLocalizer.Active = new DevExpressXtraBarsLocalizationCHS();
            //TreeListLocalizer.Active = new DevExpressXtraTreeListLocalizationCHS();
        }

        private void ucTopoErrMap_MouseMove(IPoint pPoint)
        {
            barStaticXY.Caption = string.Format("X坐标：{0}, Y坐标：{1}", pPoint.X.ToString("f3"), pPoint.Y.ToString("f3"));
        }

        private void RibbonFrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 执行任务后的垃圾回收工作
        /// </summary>
        private void PostTaskWork()
        {
            //if (CCheckApplication.ucPropertyErrMap != null)
            //{
            //    CCheckApplication.ucPropertyErrMap.SetTask(null);
            //    //CCheckApplication.ucTopoErrMap.SetTask(null);
            //}
            //内存回收
            //GC.Collect();
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
     
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void RibbonFrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PostTaskWork();
        }
    }
}