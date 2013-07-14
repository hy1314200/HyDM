using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using Hy.Catalog.Define;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace HzGeoSpaceSys.Main.GISForm.DataManage
{
    internal class LayerCatalogAdapter
    {
        private TreeView  m_TreeList;
        private TreeNode m_Node3D;
        public event CatalogItemEventHandler SelectedCatalogItemChanged;
        public event CatalogItemEventHandler CatalogItemDoubleClicked;

        public void AdapterTreeView(TreeView treeHooked)
        {
            this.m_TreeList = treeHooked;
            this.m_Node3D= this.m_TreeList.Nodes[0].Nodes.Add("三维数据");
            this.m_Node3D.ImageIndex = 0;
            this.m_Node3D.SelectedImageIndex = 0;

            this.m_TreeList.NodeMouseDoubleClick    +=new TreeNodeMouseClickEventHandler(TreeList_NodeMouseDoubleClick);
            this.m_TreeList.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeList_NodeMouseClick);
        }

        void TreeList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Parent != m_Node3D)
                return;

            if (SelectedCatalogItemChanged != null)
                this.SelectedCatalogItemChanged.Invoke(e.Node.Tag as ICatalogItem);
        }

        void TreeList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == m_Node3D && m_Node3D.Nodes.Count==0)
            {
                //Load3DLayers();
                Load3DLayers2();
            }else
                if (e.Node != null && e.Node.Parent == m_Node3D)
                {
                    if (this.CatalogItemDoubleClicked != null)
                        this.CatalogItemDoubleClicked.Invoke(e.Node.Tag as ICatalogItem);
                }
        }

        private void Load3DLayers2()
        {
            IWorkspace wsSource = GISOpr.getInstance().WorkSpace;
            // 为了在Skyline下预览数据， 必须取得数据库的连接信息
            DBCore db = new DBCore(true);
            IList paramList = db.GetAll(typeof(Tbsysparams), "Paramid");
            IPropertySet workspaceProperySet = new PropertySetClass();
            foreach (Tbsysparams param in paramList)
            {
                if (param.Paramenname.ToUpper() == "SDESERVER")
                    workspaceProperySet.SetProperty("Server",param.Paramvalue);
                if (param.Paramenname.ToUpper() == "SDEINSTANCE")
                    workspaceProperySet.SetProperty("Instance", param.Paramvalue);
                if (param.Paramenname.ToUpper() == "SDEVERSION")
                     workspaceProperySet.SetProperty("Version", param.Paramvalue);
                if (param.Paramenname.ToUpper() == "SDEUSER")
                    workspaceProperySet.SetProperty("User", param.Paramvalue);
                if (param.Paramenname.ToUpper() == "SDEPASSWORD")
                    workspaceProperySet.SetProperty("Password", param.Paramvalue);
            }
            IWorkspaceCatalogItem itemWorkspace = new WorkspaceCatalogItem(workspaceProperySet,Hy.Catalog.Utility.enumWorkspaceType.SDE,null, "当前空间数据库");
            if ((wsSource as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, "ThreeDimenLayersCache"))
            {
                IFeatureWorkspace fwsSource=(wsSource as IFeatureWorkspace);
                ITable t3DLayers = fwsSource.OpenTable("ThreeDimenLayersCache");
                ICursor cursor = t3DLayers.Search(null, true);
                IRow rowLayer = cursor.NextRow();
                int fNameIndex = cursor.FindField("LayerName");
                int fTypeIndex = cursor.FindField("LayerType");
                int f3DType = (int)enumCatalogType.FeatureClass3D;
                while (rowLayer != null)
                {
                    if (f3DType == Convert.ToInt32(rowLayer.get_Value(fTypeIndex)))
                    {
                        IFeatureClass fClass3D = fwsSource.OpenFeatureClass(rowLayer.get_Value(fNameIndex) as string);

                        // 
                        ICatalogItem curItem = new FeatureClassCatalogItem((fClass3D as IDataset).FullName as IDatasetName, null);
                        curItem.WorkspaceItem = itemWorkspace;
                        TreeNode node3D = this.m_Node3D.Nodes.Add(curItem.Name);
                        node3D.ImageIndex = 19;
                        node3D.SelectedImageIndex = 19;

                        node3D.Tag = curItem;
                    }
                    rowLayer = cursor.NextRow();
                }
            }
            else
            {
                // ParentName和Desription（以及LayerType）都保留，到支持栅格数据时可能用得到
                string strSQL = @"Create Table ThreeDimenLayersCache(
                                    LayerName varchar2(256) not null,
                                    ParentName varchar2(256) ,
                                    Description varchar(4000),
                                    LayerType INTEGER default "
                    + ((int)enumCatalogType.FeatureClass3D).ToString()
                    + ")";

                wsSource.ExecuteSQL(strSQL);

                IEnumDatasetName enDatasetName = wsSource.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                IDatasetName dsName3D = enDatasetName.Next();
                while (dsName3D != null)
                {
                    if ((dsName3D as IFeatureClass).ShapeType == esriGeometryType.esriGeometryMultiPatch)
                    {
                        // 存入数据库缓存并加载到树上
                        strSQL = string.Format("Insert into ThreeDimenLayersCache(LayerName) values('{0}')", dsName3D.Name);
                        wsSource.ExecuteSQL(strSQL);

                        // 
                        ICatalogItem curItem = new FeatureClassCatalogItem(dsName3D, null);
                        curItem.WorkspaceItem = itemWorkspace;
                        TreeNode node3D = this.m_Node3D.Nodes.Add(curItem.Name);
                        node3D.ImageIndex = 19;
                        node3D.SelectedImageIndex = 19;

                        node3D.Tag = curItem;
                    }

                    dsName3D = enDatasetName.Next();
                }

                // FeatureDataset底下的3维FeatureClass
                enDatasetName = wsSource.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName dsNameContainer = enDatasetName.Next();
                while (dsNameContainer != null)
                {
                    IEnumDatasetName enDsName3D = dsNameContainer.SubsetNames;
                    dsName3D = enDsName3D.Next();
                    while (dsName3D != null)
                    {
                        if (dsName3D is IFeatureClass && (dsName3D as IFeatureClass).ShapeType == esriGeometryType.esriGeometryMultiPatch)
                        {

                            // 存入数据库缓存并加载到树上
                            strSQL = string.Format("Insert into ThreeDimenLayersCache(LayerName) values('{0}')", dsName3D.Name);
                            wsSource.ExecuteSQL(strSQL);

                            //
                            ICatalogItem curItem = new FeatureClassCatalogItem(dsName3D, null);
                            curItem.WorkspaceItem = itemWorkspace;
                            TreeNode node3D = this.m_Node3D.Nodes.Add(curItem.Name);
                            node3D.ImageIndex = 19;
                            node3D.SelectedImageIndex = 19;

                            node3D.Tag = curItem;
                        }

                        dsName3D = enDsName3D.Next();
                    }

                    dsNameContainer = enDatasetName.Next();
                }
            }

            m_Node3D.Expand();
        }
        private void Load3DLayers()
        {
            IWorkspace wsSource= GISOpr.getInstance().WorkSpace;
            // get 3D FeatureClass
            IEnumDatasetName enDatasetName = wsSource.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            IDatasetName dsName3D = enDatasetName.Next();
            while (dsName3D != null)
            {
                if((dsName3D as IFeatureClassName).ShapeType==esriGeometryType.esriGeometryMultiPatch)
                {
                    ICatalogItem curItem = new FeatureClassCatalogItem(dsName3D, null);
                    TreeNode node3D= this.m_Node3D.Nodes.Add(curItem.Name);
                    node3D.ImageIndex = 19;
                    node3D.SelectedImageIndex = 19;

                    node3D.Tag = curItem;
                }

                dsName3D = enDatasetName.Next();
            }

            // FeatureDataset底下的3维FeatureClass
            enDatasetName = wsSource.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            IDatasetName dsContainer = enDatasetName.Next();
            while (dsContainer != null)
            {
                IEnumDatasetName enDsName3D = dsContainer.SubsetNames;
                dsName3D = enDsName3D.Next();
                while (dsName3D != null)
                {
                    if ((dsName3D as IFeatureClass).ShapeType == esriGeometryType.esriGeometryMultiPatch)
                    {
                        ICatalogItem curItem = new FeatureClassCatalogItem(dsName3D, null);
                        TreeNode node3D = this.m_Node3D.Nodes.Add(curItem.Name);
                        node3D.ImageIndex = 19;
                        node3D.SelectedImageIndex = 19;

                        node3D.Tag = curItem;
                    }

                    dsName3D = enDsName3D.Next();
                }

                dsContainer = enDatasetName.Next();
            }

            m_Node3D.Expand();
        }
    }
}
