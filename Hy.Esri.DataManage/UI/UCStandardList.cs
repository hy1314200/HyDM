using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard;
using Hy.Esri.DataManage.Standard.Helper;
using DevExpress.XtraTreeList.Nodes;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.DataManage.UI
{
    public delegate void StandardItemEventHandler(StandardItem sItem);
    public partial class UCStandardList : DevExpress.XtraEditors.XtraUserControl
    {
        public UCStandardList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            tlCatalog.Nodes.Clear();
            IList<StandardItem> rootList = StandardHelper.GetAllStandard();
            TreeListNode nodeRoot = tlCatalog.AppendNode(new object[] { "所有标准", null }, null);
            nodeRoot.SelectImageIndex = 18;
            foreach (StandardItem sItem in rootList)
            {
                StandardHelper.InitItemDetial(sItem);
                BoundItem(sItem, nodeRoot);
            }

            nodeRoot.Expanded = true;
        }


        private int GetImageIndex(StandardItem sItem)
        {
            switch (sItem.Type)
            {
                case enumItemType.Standard:
                    return 1;

                case enumItemType.FeatureDataset:
                    return 2;

                case enumItemType.FeatureClass:
                    FeatureClassInfo fcInfo = sItem.Details as FeatureClassInfo;
                    switch (fcInfo.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            return 4;

                        case esriGeometryType.esriGeometryPolyline:
                            return 5;

                        case esriGeometryType.esriGeometryPolygon:
                            return 6;
                    }
                    break;

                case enumItemType.Table:
                    return 13;
            }
            return 8;
        }

        private void BoundItem(StandardItem sItem, TreeListNode nodeParent)
        {
            if (sItem == null)
                return;

            StandardHelper.InitItemDetial(sItem);
            TreeListNode nodeItem = tlCatalog.AppendNode(new object[] { sItem.Name, sItem.Type }, nodeParent, sItem);
            nodeItem.ImageIndex = GetImageIndex(sItem);
            nodeItem.SelectImageIndex = 18;
            if (sItem.SubItems != null)
            {
                foreach (StandardItem diSub in sItem.SubItems)
                {
                    BoundItem(diSub, nodeItem);
                }
            }
        }

        public StandardItem SelectedStandardItem { get; private set; }

        public event StandardItemEventHandler SelectedStandardItemChanged;

        private void tlCatalog_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.SelectedStandardItem =tlCatalog.FocusedNode==null?null: tlCatalog.FocusedNode.Tag as StandardItem;
            if (this.SelectedStandardItemChanged != null)
                this.SelectedStandardItemChanged.Invoke(this.SelectedStandardItem);
        }

    }
}
