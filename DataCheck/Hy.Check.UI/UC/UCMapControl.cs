using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Hy.Check.Task;
using ESRI.ArcGIS.esriSystem;
using DevExpress.XtraBars.Ribbon;
using ESRI.ArcGIS.Carto;
using Hy.Common.Utility.Esri;
using Hy.Check.UI.Forms;
using DevExpress.XtraBars.Docking;
using Hy.Common.Utility.Data;
using Hy.Common.UI;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Hy.Check.UI.UC.Sundary;
using ESRI.ArcGIS.Geodatabase;
using CheckTask = Hy.Check.Task.Task;
using Hy.Check.Utility;

namespace Hy.Check.UI.UC
{
    public partial class UCMapControl : DevExpress.XtraEditors.XtraUserControl
    {

        #region 委托

        public delegate void MouseMapExtentChangeEvent();

        public delegate void MouseMoveEvent(IPoint pPoint);

        #endregion

        #region 事件
          public event MouseMoveEvent MapMouseMove;
          public event MouseMapExtentChangeEvent MapExtentChanged;

        #endregion

        #region
        /// <summary>
        /// 当前地图控件
        /// </summary>
        public AxMapControl CurActivityMapControl
        {
            get;
            set;
        }
        public ITOCControl2 CurTOCControl
        {
            get;
            set;
        }

        public AxToolbarControl ToolbarControl
        {
            get;
            private set;
        }

        #endregion

        private CheckTask m_CurrentTask = null;
        private MapLayersController m_MapLayersController = null;
        private ILayer m_CurrentSelectLayer = null;
        //private DataTable m_RulesDt = null;
        private delegate void LoadRules();
        /// <summary>
        /// 当前页数
        /// </summary>
        private int errCount = -1;

        /// <summary>
        /// 当前的临时规则列表
        /// </summary>
        private DataTable m_DtSchema = null;

        private IFeatureDataset m_pDatasetSon = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UCMapControlNew"/> class.
        /// </summary>
        public UCMapControl()
        {
            InitializeComponent();
            Toolbar.SetBuddyControl(UcMap.Object);
            this.ucRulesTree.TreeListNodeIsSelected+=new UCRulesTree.TreeNodeSelected(ucRulesTree1_TreeListNodeIsSelected);
            this.ucResultsTree.TreeListNodeIsSelected+=new UCRulesTree.TreeNodeSelected(ucResultsTree_TreeListNodeIsSelected);
            this.ucResult.DatasourceGetter = this.GetError;
            this.ucResult.SelectedErrorFeatureChanged += new FeatureChangedHandler(ucResult_SelectedErrorFeatureChanged);
        }

        void ucResult_SelectedErrorFeatureChanged(ESRI.ArcGIS.Geodatabase.IFeature selErrorFeature, ESRI.ArcGIS.Geodatabase.IFeature selReferFeature)
        {
            if (selReferFeature == null)
            {
                this.ucAttribute.SetErrorFeature(selErrorFeature, null);
            }
            else
            {
                this.ucAttribute.SetErrorFeature(selErrorFeature, "源数据");
                this.ucAttribute.SetReferFeature(selReferFeature, "目标数据");

                if (this.dockLegend.Visibility == DockVisibility.Visible)
                    this.dockLegend.Visibility = DockVisibility.AutoHide;

                if (this.dockAttribute.Visibility != DockVisibility.Visible)
                    this.dockAttribute.Visibility = DockVisibility.Visible;
            }
        }

        /// <summary>
        /// 初始化当前地图控件
        /// </summary>
        public void InitUc()
        {
            UcMap.Map.Name = "地图";
            UcMap.MapUnits = esriUnits.esriMeters;
            TocControl.SetBuddyControl(UcMap.Object);
            CurTOCControl = TocControl.Object as ITOCControl2;
            CurActivityMapControl = UcMap;
            ToolbarControl = Toolbar;
            TabCtrl.SelectedTabPage = this.TabPageRules;
            //设置ucresult控件
            this.ucResult.Hook = this.CurActivityMapControl.Object;
        }

        /// <summary>
        /// Sets the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void SetTask(CheckTask task)
        {
            XGifProgress progressbar = new XGifProgress();
            try
            {

                //如果task为null则认为释放资源
                if (task == null)
                {
                    UCDispose();
                    return;
                }

                //如果不为空，做处理
                m_CurrentTask = task;

                //如果打开新建的任务
                progressbar.ShowHint("正在打开当前质检任务.....");
                //初始化规则树

                this.ucRulesTree.CurrentSchemaId = task.SchemaID;
                this.ucRulesTree.CurrentTaskName = task.Name;
                //采用委托，在创建此控件的线程上调用加载tree方法，防止出现“当前对象正在其他地方使用”的错误;
                LoadRules Load = new LoadRules(this.ucRulesTree.LoadRulesTree);
                this.BeginInvoke(Load);

                this.ucResult.CurrentTask = m_CurrentTask;
                if (File.Exists(task.GetMXDFile()))
                {
                    //打开配置的mxd模板图层
                    UcMap.LoadMxFile(task.GetMXDFile());
                    TocControl.SetBuddyControl(UcMap.Object);
                    TocControl.Update();
                    UcMap.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    m_MapLayersController = new MapLayersController(UcMap.Map);
                }
                else
                {
                    progressbar.Hide();
                    MessageBoxApi.ShowErrorMessageBox(task.GetMXDFile() + "不存在！无法打开此任务");
                    return;
                }
                //加载结果
                if (task.ResultConnection != null)
                {
                    //加载已经检查完成的任务
                    this.TabCtrl.ShowTabHeader = DefaultBoolean.True;
                    this.TabCtrl.SelectedTabPage = this.TabPageCheckResults;
                    this.ucResultsTree.CurrentSchemaId = task.SchemaID;
                    this.ucResultsTree.CurrentTaskName = task.Name;
                    Sundary.ResultDbOper resultOper = new Hy.Check.UI.UC.Sundary.ResultDbOper(task.ResultConnection);
                    this.ucResultsTree.LayersResultsDt = resultOper.GetLayersResults();
                    this.ucResultsTree.ResultsDt = resultOper.GetAllResults();
                    this.ucResultsTree.CheckResultsCount = resultOper.GetResultsCount();
                    this.ucResultsTree.LoadResultsTree();

                }
                else
                {
                    TabCtrl.SelectedTabPage = this.TabPageRules;
                    this.TabCtrl.ShowTabHeader = DefaultBoolean.False;
                    this.ucResultsTree.CurrentTreeList.Nodes.Clear();
                }
                //加载top错误图层到map
                if (task.TopoWorkspace != null)
                {
                    //获取当前任务子库中的图层集名
                    m_pDatasetSon = TopoOperAPI.GetCurrentSonDataSet(task.TopoWorkspace as IFeatureWorkspace);
                    // 获取子库中所有的要素类的名称
                    TopoOperAPI.GetFcTopoNameInSon(UcMap, task.TopoWorkspace as IFeatureWorkspace, m_pDatasetSon);
                }
                this.dockTree.Visibility = DockVisibility.Visible;
                this.dockTree.Text = "规则列表";
                this.dockTree.Width = 260;
                this.dockLegend.Visibility = DockVisibility.Visible;
            }
            catch (Exception ex)
            {
                progressbar.Hide();
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.Message);
                XtraMessageBox.Show("打开结果库失败！", "提示");
            }
            finally
            {
                progressbar.Hide(); //关闭打开结果库进度服务
            }
        }


        /// <summary>
        /// 释放uc中使用的资源
        /// </summary>
        public void UCDispose()
        {
            try
            {
                ////情况dock上的文本
                dockResults.Text = "";
                try
                {
                    this.dockTree.Visibility = DockVisibility.Hidden;
                }
                catch { }
                try
                {
                    this.dockResults.Visibility = DockVisibility.Hidden;
                }
                catch { }

                //try
                //{
                //    this.dockAttribute.Visibility = DockVisibility.Hidden;
                //}
                //catch { }

                //try
                //{
                //    this.dockLegend.Visibility = DockVisibility.Hidden;
                //}
                //catch { }

                this.TocControl.SetBuddyControl(null);
                //删除图上所有的图形要素
                this.UcMap.Map.ClearSelection();
                //删除地图上所有的绘制的要素
                IGraphicsContainer pGraphContainer = UcMap.ActiveView.GraphicsContainer;
                pGraphContainer.DeleteAllElements();

                //清楚所有的图层信息
                UcMap.ClearLayers();
                UcMap.ActiveView.Refresh();

                //清空结果库连接
                if (m_CurrentTask != null)
                {
                    AdoDbHelper.CloseDbConnection(m_CurrentTask.ResultConnection);
                    m_CurrentTask.Release();
                }

                //清空tree的结果
                //ucRulesTree.Dispose();

                //TocControl = null;
                GC.Collect();
            }
            catch
            {
            }
        }

        private void UcMap_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (MapMouseMove != null)
            {
                IPoint pt = new PointClass();
                pt.PutCoords(e.mapX, e.mapY);
                pt.SpatialReference = UcMap.Map.SpatialReference;
                MapMouseMove.Invoke(pt);
            }
        }

        private void UcMap_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            if (MapExtentChanged != null)
                MapExtentChanged.Invoke();
        }

        private void UcMap_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {
            if (e.keyCode == 27) //如果按取消键
            {
                if (UcMap.CurrentTool != null)
                {
                    UcMap.CurrentTool = null;
                }
            }
        }

        private void UcMap_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (MapExtentChanged != null)
            MapExtentChanged.Invoke();

            Control pControl = ParentForm.Controls.Find("ribbonStatusBar", true)[0];
            RibbonStatusBar ITEM = pControl as RibbonStatusBar;
            if (ITEM != null)
            {
                if (m_CurrentTask != null)
                {
                    for (int i = 0; i < ITEM.ItemLinks.Count; i++)
                    {
                        if (ITEM.ItemLinks[i].Item.Name.Equals("barStaticTask"))
                        {
                            ITEM.ItemLinks[i].Caption = string.Format("当前任务名：{0}, 任务路径：{1}",m_CurrentTask.Name,m_CurrentTask.Path);
                            break;
                        }
                    }
                }
            }
        }

        private void UcMap_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
                UcMap.Pan();
        }

        private void TocControl_OnBeginLabelEdit(object sender, ITOCControlEvents_OnBeginLabelEditEvent e)
        {
            e.canEdit = false;
        }

        private void TocControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            object other = null;
            object index = null;
            IBasicMap map = null;
            ILayer layer = null;


            CurTOCControl = (sender as AxTOCControl).Object as ITOCControl2;
            //返回点后的指定的对象
            CurTOCControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

            //如果点中的图例，则弹出符号设置窗口
            if ((e.button == 1) && (item == esriTOCControlItem.esriTOCControlItemLegendClass))
            {
                //暂不支持
            }
            else if ((e.button == 1) && (item == esriTOCControlItem.esriTOCControlItemLayer))
            {
                object obj = m_MapLayersController.GetLayerParent(layer);
                if (obj is ICompositeLayer)
                {
                    ILayer pParentLayer = (ILayer)obj;
                    bool b = layer.Visible;
                    if (b == false)
                    {
                        pParentLayer.Visible = true;
                    }
                }
            }
            else if (e.button == 2) //鼠标右键
            {
                switch (item)
                {
                    case esriTOCControlItem.esriTOCControlItemNone:
                        break;
                    case esriTOCControlItem.esriTOCControlItemMap:
                        CurTOCControl.SelectItem(map, null);
                        this.popMenuTOCMap.ShowPopup(MousePosition);
                        break;
                    case esriTOCControlItem.esriTOCControlItemLegendClass:
                        break;
                    case esriTOCControlItem.esriTOCControlItemLayer:
                        m_CurrentSelectLayer = layer;
                        CurTOCControl.SelectItem(layer, null);
                        popMenuTOCLayer.ShowPopup(MousePosition); // new System.Drawing.Point(e.x, e.y));
                        break;
                    case esriTOCControlItem.esriTOCControlItemHeading:
                        break;
                }

                CurActivityMapControl.CustomProperty = layer;
            }
            CurTOCControl = null;
        }
        private void ucRulesTree1_TreeListNodeIsSelected(string strRuleName, TreeRulesEventArgs e)
        {
            this.ucResult.ContentType = enumContentType.Rule;
            if (e.SubRules == null)
            {
                dockResults.Text = string.Format("{0}规则检查总项为{1}个", strRuleName, e.RulesCount); ;
                return;
            }
            else
            {
                dockResults.Text = string.Format("规则分类:{0} 规则总数{1}",strRuleName=="0"? e.RuleTypeName:strRuleName,e.RulesCount);
                if (dockResults.Visibility != DockVisibility.Visible)
                {
                    dockResults.Visibility = DockVisibility.Visible;
                    dockResults.Height = 260;
                }
            }

            if (e.BolHaveChildNode)
            {
                this.ucResult.SetTreeListNode((TreeListNode)e.SubRules);
               // this.ucResult.set = obj as DataTable;
            }
            else
            {
                this.ucResult.DataSource = e.SubRules as DataTable;
            }
        }

        private void ucResultsTree_TreeListNodeIsSelected(string strRuleName, TreeRulesEventArgs e)
        {

            this.ucResult.ContentType = enumContentType.Error;
            if (e.SubRules == null)
            {
                this.ucResult.PageCount = 0;
                dockResults.Text = string.Format("{0}错误总数:{1}个", strRuleName, e.RulesCount); 
                return;
            }
            else
            {
                dockResults.Text = string.Format("规则分类:{0} 错误总数:{1}                                                           双击错误信息可以对图形进行定位", strRuleName == "0" ? e.RuleTypeName : strRuleName, e.RulesCount);
                if (dockResults.Visibility != DockVisibility.Visible)
                {
                    dockResults.Visibility = DockVisibility.Visible;
                    dockResults.Height = 260;
                }
            }
            if (e.BolHaveChildNode)
            {
                this.ucResult.SetTreeListNode((TreeListNode)e.SubRules);
                // this.ucResult.set = obj as DataTable;
            }
            else
            {
                double dou = (double)e.RulesCount / COMMONCONST.MAX_ROWS;
                this.ucResult.PageCount =(int)Math.Ceiling(dou);
                m_DtSchema=e.SubRules as DataTable;
                errCount = e.RulesCount;

                this.ucResult.DataSource = GetError(0);
            }
        }

        private DataTable GetError(int pageIndex)
        {
            //2012-07-17 靳军杰 修改
            //如果没有错误时，依然要返回一个空间的datatable结构

            if (m_DtSchema == null || m_DtSchema.Rows.Count == 0)
            {
                return ErrorHelper.GenerateStandardErrorDt();
            }

            string strRuleIDs = "";
            for (int i = 0; i < m_DtSchema.Rows.Count; i++)
            {
                strRuleIDs += m_DtSchema.Rows[i]["RuleInstID"] as string + ",";
            }
            if (!string.IsNullOrEmpty(strRuleIDs))
            {
                strRuleIDs = strRuleIDs.Remove(strRuleIDs.Length - 1);
            }

            ErrorHelper errHelper = new ErrorHelper();
            errHelper.ResultConnection = this.m_CurrentTask.ResultConnection;           
            return errHelper.GetErrors((Hy.Check.Define.enumErrorType)m_DtSchema.Rows[0]["ErrorType"], strRuleIDs, COMMONCONST.MAX_ROWS,pageIndex, ref errCount);

        }

        private void barBtnCloseAllLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLayerVisible(false);
        }

        private void barBtnSetLayerTransparency_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_CurrentSelectLayer == null)
            {
                return;
            }
            FrmLayerTransparency frm = new FrmLayerTransparency();
            frm.InitForm(m_CurrentSelectLayer, UcMap.ActiveView);
            frm.ShowDialog();
        }

        private void barBtnOpenAllLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLayerVisible(true);
        }

        private void barBtnZoomLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_CurrentSelectLayer != null)
            {
                if (m_CurrentSelectLayer.Visible == false)
                {
                    m_CurrentSelectLayer.Visible = true;
                    object obj = m_MapLayersController.GetLayerParent(m_CurrentSelectLayer);
                    if (obj is ICompositeLayer)
                    {
                        ILayer pParentLayer = (ILayer)obj;
                        pParentLayer.Visible = true;
                    }
                }
                CurActivityMapControl.ActiveView.Extent = m_CurrentSelectLayer.AreaOfInterest;
                CurActivityMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        /// <summary>
        /// Sets the layer visible.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        private void SetLayerVisible(bool isVisible)
        {
            if (CurActivityMapControl != null)
            {
                for (int i = 0; i < CurActivityMapControl.LayerCount; i++)
                {
                    ILayer pLayer = CurActivityMapControl.get_Layer(i);
                    if (pLayer is ICompositeLayer)
                    {
                        m_MapLayersController.SetCompositeLayerVisible(pLayer, isVisible);
                    }
                    else
                    {
                        pLayer.Visible = isVisible;
                    }
                }

                CurActivityMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }
    }
}