using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;


namespace Check.UI.Forms
{
    public partial class FrmFtAttr : XtraForm
    {

        private IMap m_pMap;
        private bool m_bFtAttr;
        private IActiveView m_pActiveView;
        private AxMapControl m_pMapControl;

        public FrmFtAttr(IActiveView pActiveView, AxMapControl pMapControl)
        {
            InitializeComponent();
            m_pActiveView = pActiveView;
            m_pMapControl = pMapControl;
        }

        private void frmFtAttr_Load(object sender, EventArgs e)
        {
            m_pMap = null;
            m_bFtAttr = true ;

            TreeListColumn col = this.FTtreeList.Columns.Add();
            col.Caption = "要素名";
            col.Name = "FtName";
            col.FieldName = "FtName";
            col.Visible = true;
            col.OptionsColumn.AllowEdit = false;

            col = FTAttrTreeList.Columns.Add();
            col.Caption = "字段名";
            col.Name = "FieldName";
            col.FieldName = "FieldName";
            col.Visible = true;
            col.OptionsColumn.AllowEdit = false;

            col = FTAttrTreeList.Columns.Add();
            col.Caption = "字段值";
            col.Name = "FieldValue";
            col.FieldName = "FieldValue";
            col.Visible = true;
            col.OptionsColumn.AllowEdit = false;

        }

        public void UpdateFeature( IMap pMap ,ILayer pCurrentLayer,IPoint pPoint )
        {
            //处理具体数据
            if (pMap == null || pPoint == null )
                return;
            m_pMap = pMap;

            //如果没有地图图层 ，则不处理
            long nLayerCount = pMap.LayerCount;
            if( nLayerCount == 0 )
                return;

            //删除树结点
            FtTreeView.Nodes.Clear();

            this.FTtreeList.Nodes.Clear();


            //开始选择
            IGeometry geom = pPoint;

            ISpatialReference spatialReference = m_pMap.SpatialReference;
			geom.SpatialReference = spatialReference;

			//Refresh the active view
            IActiveView activeView = (IActiveView)m_pMap;

            ISelectionEnvironment selEnv =  new SelectionEnvironment();
            selEnv.PointSelectionMethod = esriSpatialRelEnum.esriSpatialRelWithin;
            m_pMap.SelectByShape(geom, selEnv, false);
			activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);



            //如果没有设置 当前图层pCurrentLayer ，则不处理 

            ESRI.ArcGIS.esriSystem.IPersist pPersist = new FeatureLayer() as ESRI.ArcGIS.esriSystem.IPersist;
            UID uid = pPersist as UID ;
            
            IEnumLayer pEnumlayer = pMap.get_Layers(uid,true );

            pEnumlayer.Reset();
            ILayer pLayer = pEnumlayer.Next();
            while( pLayer != null )
            {

                if( pLayer.Visible == false )
                {
                    pLayer = pEnumlayer.Next();
                    continue;
                }

                IFeatureLayer2 pFtLayer = (IFeatureLayer2)pLayer;
                IFeatureSelection pFtSelect = (IFeatureSelection)pFtLayer;
                ISelectionSet pSelectset = (ISelectionSet)pFtSelect.SelectionSet;

                InitTreeView(pSelectset, pLayer);

                pLayer = pEnumlayer.Next();
            }


            
        }

        /// <summary>
        /// 更新显示属性信息
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pGeo"></param>
        public void UpdateFeature(IMap pMap, IGeometry pGeo )
        {
            //处理具体数据
            if (pMap == null || pGeo == null)
                return;
            m_pMap = pMap;


            //删除树结点

            FTAttrTreeList.Nodes.Clear();
            FTtreeList.Nodes.Clear();

            //如果没有设置 当前图层pCurrentLayer ，则不处理 
            ESRI.ArcGIS.esriSystem.IPersist pPersist = new FeatureLayer() as ESRI.ArcGIS.esriSystem.IPersist;
            UID uid = pPersist as UID;

            IEnumLayer pEnumlayer = pMap.get_Layers(uid, true);
            
            pEnumlayer.Reset();
            ILayer pLayer = pEnumlayer.Next();
            while (pLayer != null)
            {

                if (pLayer.Visible == false)
                {
                    pLayer = pEnumlayer.Next();
                    continue;
                }
                try
                {
                    if (pLayer.SupportedDrawPhases == 5) //如果图层为拓扑图层
                    {
                        pLayer = pEnumlayer.Next();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                    pLayer = pEnumlayer.Next();
                    continue;
                }
                if(pLayer is ICompositeLayer)
                {
                    pLayer = pEnumlayer.Next();
                    continue;
                }

                IFeatureLayer2 pFtLayer2 = (IFeatureLayer2)pLayer;
                IFeatureSelection pFtSelect = (IFeatureSelection)pFtLayer2;
                ISelectionSet pSelectset = (ISelectionSet)pFtSelect.SelectionSet;
                InitTreeView(pSelectset, pLayer);
                
                pLayer = pEnumlayer.Next();
            }

            FTtreeList.ExpandAll();

            #region 在右侧面板显示第一个结点的详细信息
            TreeListNode treelistNode = FTtreeList.Nodes.FirstNode;

            if (treelistNode == null)
            {
                return;
            
            }
            TreeListNode nodeChild = treelistNode.FirstNode;

            if (nodeChild.ParentNode == null)
            {
                return;
            }

            TreeListNode parentNode = nodeChild.ParentNode;
            try
            {

                object st = treelistNode[0];
                string strLayerName1 = nodeChild.ParentNode.GetDisplayText("FtName").ToString();
            }
            catch (Exception ex)
            {
                //GT_CONST.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                string s = ex.Message;
            }
            string strLayerName = nodeChild.ParentNode.GetDisplayText("FtName").ToString();
            ILayer pLayer1 = Common.Utility.Esri.MapOperAPI.GetLayerFromMapByName(m_pMap, strLayerName);
            if(pLayer1==null)
            {
                return;
            }
            IFeatureLayer pFtLayer = (IFeatureLayer)pLayer1;
            IFeatureClass pFtCls = pFtLayer.FeatureClass;

            //IQueryFilter pFilter = new QueryFilterClass();

            IFields pFields = pFtCls.Fields;
            int nIndex = pFields.FindField("BSM");
            if (nIndex == -1)
            {
                return;

            }

            IQueryFilter pQueryFilter = new QueryFilterClass();
            IField pField = pFields.get_Field(nIndex);


            bool bTest = GetTypeByEsriField(pField.Type);

            if (bTest == false)  //整形
            {
                pQueryFilter.WhereClause = "BSM = " + nodeChild.GetDisplayText("FtName").ToString();
                //pQueryFilter.WhereClause = "BSM = " + treenodeChild.Text;
            }
            else
            {
                pQueryFilter.WhereClause = "BSM = " + nodeChild.GetDisplayText("FtName").ToString();
                
                //pQueryFilter.WhereClause = "BSM = '" + treenodeChild.Text + "'";
            }

            //string strOIDField = pFtCls.OIDFieldName;

            //pFilter.WhereClause = "BSM =" + treenodeChild.Text;
            IFeatureCursor pFtCursor = pFtCls.Search(pQueryFilter, true);

            IFeature pFt = pFtCursor.NextFeature();
            while (pFt != null)
            {
                int nOID = pFt.OID;
                IGeometry geom = pFt.Shape;
                InitAttrTreeList(pFtCls, pFt);

                //InitAttrListView(pFtCls, pFt);
                //ArcGISAPI.ZoomToFeature(m_pActiveView, pFtLayer, nOID);
                //m_pMap.SelectByShape(geom, null, false);
                
                pFt = pFtCursor.NextFeature();
            }
            //m_pActiveView.Refresh();

            #endregion

        }

        private void frmFtAttr_FormClosed(object sender, FormClosedEventArgs e)
        {
            //m_bFtAttr = false;

            ////ArcGISService.UserClass.Identify
            //Dispose();

           
        }

        public void Attach( ref bool bFtAttr )
        {
            
            m_bFtAttr = bFtAttr;
            m_bFtAttr = true;
        }

        private void InitTreeView( ISelectionSet pSelectSet,ILayer pLayer )
        {
            if( pSelectSet==null || pLayer==null )
                return;

            IFeatureClass pFtCls = ((IFeatureLayer)pLayer).FeatureClass;

            if (pFtCls == null)
                return;
            IFields pFields = pFtCls.Fields;

            //这里如果数据没有bsm怎么办
            int nIndex = pFields.FindField("BSM");
            if (nIndex == -1)
            {
                return;

            }
           

            string strId;

            ICursor pCursor;

            pSelectSet.Search( null,false,out pCursor );
            IRow pRow = pCursor.NextRow();
            if (pRow == null)
            {
                return;
            }

            TreeListNode noderoot =
           this.FTtreeList.AppendNode(new object[] { pLayer.Name }, null);
          
            while( pRow != null )
            {
                //strId = pRow.OID.ToString();
                if (pRow.get_Value(nIndex) != null)
                {
                    strId = pRow.get_Value(nIndex).ToString();
                    //NodeRoot.Nodes.Add(strId);
                    TreeListNode node =
           this.FTtreeList.AppendNode(new object[] { strId }, noderoot);                    

                }
               
                pRow = pCursor.NextRow();
            }

           
        }

        /// <summary>
        /// 加载图层树信息
        /// </summary>
        /// <param name="pGeo"></param>
        /// <param name="pLayer"></param>
        /// <param name="pGeoColResult"></param>
        private void InitTreeView(IGeometry pGeo, ILayer pLayer, ref IGeometryCollection pGeoColResult)
        {
            if (pGeo == null || pLayer == null)
                return;

            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pGeo;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureClass pFtCls = ((IFeatureLayer) pLayer).FeatureClass;
            IFeatureCursor FtCur = pFtCls.Search(spatialFilter, false);
            IFeature pFt = FtCur.NextFeature();
            if(pFt==null)
            {
                return;
            }


            IFields pFields = pFtCls.Fields;
            int nIndex = pFields.FindField("BSM");
            if (nIndex == -1)
            {
                return;

            }
            //TreeNode NodeRoot;
            //NodeRoot = FtTreeView.Nodes.Add(pLayer.Name);

            TreeListNode noderoot =
            this.FTtreeList.AppendNode(new object[] { pLayer.Name }, null);

            object obj = Type.Missing;
            while (pFt != null)
            {
                //strId = pRow.OID.ToString();
                if (pFt.get_Value(nIndex) != null)
                {
                    string strId = pFt.get_Value(nIndex).ToString();
                    //NodeRoot.Nodes.Add(strId);
                    TreeListNode node =
            this.FTtreeList.AppendNode(new object[] { strId }, noderoot);
                    //pGeoColResult.AddGeometry(pFt.ShapeCopy, ref obj, ref obj);
                }

                pFt = FtCur.NextFeature();
            }

            //NodeRoot.ExpandAll();
        }

        //选择项目
        private void FtTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void InitAttrListView( IFeatureClass pFtCls, IFeature pFt )
        {
            FtAttrView.Items.Clear();
            FtAttrView.Columns.Clear();

            FtAttrView.Columns.Add( "字段名",100, HorizontalAlignment.Left );
            FtAttrView.Columns.Add("字段值", 100, HorizontalAlignment.Left );

            IFields pFlds = pFtCls.Fields;
            int nFldCount = pFlds.FieldCount;
            int i;
            IField pField;
            for (i = 0; i < nFldCount;i++ )
            {
                pField = pFlds.get_Field(i);

                ListViewItem subItem = new ListViewItem();
                subItem.SubItems[0].Text  = pField.AliasName;
                string str = "";
                if (pField.AliasName == "几何类型")
                {
                    str = pFt.Shape.GeometryType.ToString();
                }
                else
                {
                    str = pFt.get_Value(i).ToString();
                }                
                subItem.SubItems.Add ( str );

                FtAttrView.Items.Add(subItem);

            }
        }

        /// <summary>
        /// 加载右侧字段信息树信息
        /// </summary>
        /// <param name="pFtCls"></param>
        /// <param name="pFt"></param>
        private void InitAttrTreeList(IFeatureClass pFtCls, IFeature pFt)
        {
            this.FTAttrTreeList.Nodes.Clear();

           

            IFields pFlds = pFtCls.Fields;
            int nFldCount = pFlds.FieldCount;
            int i;
            IField pField;
            for (i = 0; i < nFldCount; i++)
            {
                pField = pFlds.get_Field(i);
                string str = "";
                if (pField.Name.ToLower().Contains("shape") || pField.Name.ToUpper().Contains("OBJECTID"))
                {
                    continue;
                }
                else
                {
                    if (pFt.get_Value(i)==null)
                    {
                    }
                    else
                    {
                        str = pFt.get_Value(i).ToString();
                        TreeListNode node = FTAttrTreeList.AppendNode(new object[] { pField.AliasName, str }, null);

                    }
                    
                }

                
            }
        }

        private void FtTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (FtTreeView.SelectedNode == null)
            {
                FtAttrView.Clear();
                return;
            }
            int nLevel = FtTreeView.SelectedNode.Level;

            if (nLevel != 1)
                return;
            //m_pMap.ClearSelection();
            //m_pActiveView.Refresh();

            TreeNode Nodeparent = FtTreeView.SelectedNode.Parent;

            ILayer pLayer = Common.Utility.Esri.MapOperAPI.GetLayerFromMapByName(m_pMap, Nodeparent.Text);
            if (pLayer == null)
            {
                return;
            }
            IFeatureLayer pFtLayer = (IFeatureLayer)pLayer;
            IFeatureClass pFtCls = pFtLayer.FeatureClass;

            IFields pFields = pFtCls.Fields;
            int nIndex = pFields.FindField("BSM");
            if (nIndex == -1)
            {
                return;
            }

            IQueryFilter pQueryFilter = new QueryFilterClass();
            IField pField = pFields.get_Field(nIndex);


            bool bTest = GetTypeByEsriField(pField.Type);

            if (bTest == false)  //整形
            {
                pQueryFilter.WhereClause = "BSM = " + FtTreeView.SelectedNode.Text;
            }
            else
            {
                pQueryFilter.WhereClause = "BSM = '" + FtTreeView.SelectedNode.Text + "'";
            }

            IFeatureCursor FtCur = pFtCls.Search(pQueryFilter, true);
            IFeature pFt = FtCur.NextFeature();
            if (pFt != null)
            {
                InitAttrListView(pFtCls, pFt);
                //m_pMap.SelectFeature(pLayer, pFt);
                m_pMapControl.ActiveView.Extent = pFt.Shape.Envelope;
                m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                m_pActiveView.ScreenDisplay.UpdateWindow();
                Common.Utility.Esri.MapOperAPI.FlashGeometry(m_pMapControl.Object as IMapControl4, pFt.Shape);
                //m_pMapControl.FlashShape(pFt.SHP, 2, 150, null);
                //Engine_API.ZoomToFeature(m_pMapControl, pFt);
                return;

            }
            //IFeatureSelection FtSel = (IFeatureSelection) pLayer;
            //FtSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
            ////int nOID = Convert.ToInt32(FtTreeView.SelectedNode.Text);
            ////IFeature pFeature = pFtCls.GetFeature(nOID);
            ////InitAttrListView(pFtCls, pFeature);
            ////m_pMap.SelectFeature(pLayer, pFeature);
            //m_pActiveView.Refresh();
        }

        private void FtTreeView_Click(object sender, EventArgs e)
        {
        }

        private void FTtreeList_Click(object sender, EventArgs e)
        {
           
            DevExpress.XtraTreeList.TreeListMultiSelection treelistSelect =this.FTtreeList.Selection;

            if (treelistSelect.Count == 0)
            {
                FtAttrView.Clear();
                return;
            }
            TreeListNode node = treelistSelect[0];

            if(node.ParentNode==null)
            {
                FtAttrView.Clear();
                return;
            }

            m_pMap.ClearSelection();
            //m_pActiveView.Refresh();

            string LayerName = node.ParentNode.GetDisplayText("FtName").ToString();

            ILayer pLayer = Common.Utility.Esri.MapOperAPI.GetLayerFromMapByName(m_pMap, LayerName);
            if (pLayer == null)
            {
                return;
            }
            IFeatureLayer pFtLayer = (IFeatureLayer)pLayer;
            IFeatureClass pFtCls = pFtLayer.FeatureClass;

            IFields pFields = pFtCls.Fields;
            int nIndex = pFields.FindField("BSM");
            if (nIndex == -1)
            {
                return;

            }

            IQueryFilter pQueryFilter = new QueryFilterClass();
            IField pField = pFields.get_Field(nIndex);


            bool bTest = GetTypeByEsriField(pField.Type);

            if (bTest == false)  //整形
            {
                pQueryFilter.WhereClause = "BSM = " + node.GetDisplayText("FtName").ToString();
            }
            else
            {
                pQueryFilter.WhereClause = "BSM = '" + node.GetDisplayText("FtName").ToString() + "'";
            }

            IFeatureCursor FtCur = pFtCls.Search(pQueryFilter, true);
            IFeature pFt = FtCur.NextFeature();
            if (pFt != null)
            {
                InitAttrTreeList(pFtCls, pFt);
                m_pMap.SelectFeature(pLayer, pFt);
                //m_pMapControl.ActiveView.Extent = pFt.SHP.Envelope;
                //m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                m_pActiveView.Refresh();
                
                //m_pActiveView.ScreenDisplay.UpdateWindow();
                //EngineAPI.en_FlashGeometry(m_pMapControl, pFt.SHP);
                //m_pMapControl.FlashShape(pFt.SHP, 2, 150, null);
                //Engine_API.ZoomToFeature(m_pMapControl, pFt);
                return;

            }

            //IFeatureSelection FtSel = (IFeatureSelection) pLayer;
            //FtSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultAdd, false);
            ////int nOID = Convert.ToInt32(FtTreeView.SelectedNode.Text);
            ////IFeature pFeature = pFtCls.GetFeature(nOID);
            ////InitAttrListView(pFtCls, pFeature);
            ////m_pMap.SelectFeature(pLayer, pFeature);
            //m_pActiveView.Refresh();
        }

        private void FTAttrTreeList_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Right)
            {
                return;
            }
            TreeListMultiSelection multiSelection =  this.FTAttrTreeList.Selection;
            if(multiSelection.Count>0)
            {
                System.Drawing.Point pPoint = this.FTAttrTreeList.PointToScreen(e.Location);
                this.popupMenu1.ShowPopup(pPoint);
            }
            
        }

        private void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListMultiSelection multiSelection = this.FTAttrTreeList.Selection;
            TreeListNode node = multiSelection[0];
            string strTemp = node.GetDisplayText("FieldValue").ToString();
            if (strTemp != "" && strTemp != null)
                Clipboard.SetText(strTemp);
        }

        /// <summary>
        /// 根据esri字段类型，判断sql语句查询是否需要''
        /// </summary>
        /// <param name="esriFldType"></param>
        /// <returns>返回true,则说明是string型，需要''</returns>
        private  static bool GetTypeByEsriField(esriFieldType esriFldType)
        {
            bool bol= true;

            switch (esriFldType)
            {
                case esriFieldType.esriFieldTypeOID:
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                    {
                        bol = false;
                        break;
                    }
                case esriFieldType.esriFieldTypeString:
                case esriFieldType.esriFieldTypeDate:
                case esriFieldType.esriFieldTypeBlob:
                    {
                        bol = true;
                        break;
                    }
            }
            return bol;
        }
    }
}