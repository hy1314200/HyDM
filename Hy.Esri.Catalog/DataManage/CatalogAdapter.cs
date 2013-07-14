using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ThreeDimenDataManage.Catalog;
using DevExpress.XtraTreeList.Columns;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using SunzSoft.Platform.MISBLL.LoadingFrm;
using System.Threading;

namespace HzGeoSpaceSys.Main.GISForm.DataManage
{
    internal class CatalogAdapter
    {
        private CatalogHookHelper m_HookHelper=new CatalogHookHelper();
        public CatalogHookHelper Hook
        {
            get
            {
                return m_HookHelper;
            }
        }

        private TreeList m_TreeList;
        public void AdapterTreeList(TreeList treeList)
        {
            this.m_TreeList = treeList;
            //treeList.Columns.Clear();
            //treeList.Columns.Add();
            //TreeListColumn tlColName = treeList.Columns.Add();

            treeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(treeList_FocusedNodeChanged);
           // treeList.MouseClick += new System.Windows.Forms.MouseEventHandler(treeList_MouseClick);
            treeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(treeList_MouseDoubleClick);
            treeList.MouseDown += new System.Windows.Forms.MouseEventHandler(treeList_MouseDown);

            ICatalogItem ItemRoot  = new RootCatalogItem();
            m_HookHelper.RootCatalogItem = ItemRoot;
            TreeListNode nodeRoot = treeList.AppendNode(new object[] { ItemRoot.Name }, null);
            nodeRoot.Tag = ItemRoot;
            nodeRoot.ImageIndex = 17;
            nodeRoot.SelectImageIndex = 18;
            BandEvent(nodeRoot, ItemRoot);
            ExpandNode(nodeRoot,true);
         }

        void treeList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_TreeList.FocusedNode = this.m_TreeList.CalcHitInfo(e.Location).Node;
        }
        void treeList_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TreeListNode nodeHit = m_TreeList.FocusedNode;

            ExpandNode(nodeHit,false);
        }
        void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
                return;

            m_HookHelper.CurrentCatalogItem = e.Node.Tag as ICatalogItem;
        }

        private void BandEvent(TreeListNode nodeTarget, ICatalogItem itemTarget)
        {
            itemTarget.OnOpen += delegate
            {
                ExpandNode(nodeTarget, true);
            };

            itemTarget.OnRefresh += delegate
            {
                nodeTarget.Nodes.Clear();
                if (itemTarget is IWorkspaceCatalogItem)
                {
                    nodeTarget.ImageIndex = 0;
                }
            };
        }
        private void ExpandNode(TreeListNode nodeTarget,bool refresh)
        {
            if (nodeTarget == null)
                return;

            if (!refresh && nodeTarget.HasChildren)
                return;

            nodeTarget.Nodes.Clear();
            ICatalogItem catalogItem = nodeTarget.Tag as ICatalogItem;
            if (!catalogItem.HasChild)
                return;

            ThreadLoad frmload = new ThreadLoad();
            Thread ui = frmload.CreatUIThread("正在加载，请稍候...");

            try
            {
                List<ICatalogItem> catalogItemList = catalogItem.Childrens;
                if (catalogItemList == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("打开失败");
                    return;
                }
                foreach (ICatalogItem subItem in catalogItemList)
                {
                    if (subItem == null)
                        continue;

                    TreeListNode nodeSub = m_TreeList.AppendNode(new object[] { subItem.Name }, nodeTarget);
                    nodeSub.Tag = subItem;
                    nodeSub.ImageIndex = GetImageIndex(subItem);
                    nodeSub.SelectImageIndex = 18;
                    BandEvent(nodeSub, subItem);
                    //subItem.OnOpen += delegate
                    //{
                    //    ExpandNode(nodeSub, true);
                    //};

                    //subItem.OnRefresh += delegate
                    //{
                    //    nodeSub.Nodes.Clear();
                    //    if (subItem is IWorkspaceCatalogItem)
                    //    {
                    //        nodeSub.ImageIndex = 0;
                    //    }
                    //};
                }
                nodeTarget.ExpandAll();
                // 如果是Workspace，改成连接的图标
                if (catalogItem is IWorkspaceCatalogItem)
                {
                    nodeTarget.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                ui.Abort();
            }
        }

        private int GetImageIndex(ICatalogItem item)
        {
  
//SDE
//Local
//FDs
//FC3D
//FCPoint
//FCLine
//FCArea
//FCAnno
//FCEmpty
//RasterCatalog
//RtasterSet
//NestRaster
//table
//Terrain
//Tin
//Topo

            if (item == null)
                return 8;

            switch (item.Type)
            {
                case enumCatalogType.Workpace:
                    return 0;
                case enumCatalogType.Undefine:
                    return -1;

                case enumCatalogType.Topology:
                    return 16;

                case enumCatalogType.Tin:
                    return 15;

                case enumCatalogType.Terrain:
                    return 14;

                case enumCatalogType.Table:
                    return 13;

                case enumCatalogType.RasterMosaic:
                    return 12;

                case enumCatalogType.RasterBand:
                    return 11;
                case enumCatalogType.RasterSet:
                    return 10;
                case enumCatalogType.RasterCatalog:
                    return 9;

                case enumCatalogType.FeatureDataset:
                    return 2;
                case enumCatalogType.FeatureClass3D:
                    return 3;

                case enumCatalogType.FeatureClassPoint:
                    return 4;
                case enumCatalogType.FeatureClassLine:
                    return 5;                   
                case enumCatalogType.FeatureClassArea:
                    return 6;
                case enumCatalogType.FeatureClassAnnotation:
                    return 7; 
                case enumCatalogType.FeatureClassEmpty:
                    return 8;                      

            }
            return -1;
        }
    }
}
