using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;

using ESRI.ArcGIS.Geodatabase;

namespace Hy.Check.UI.UC
{
    public partial class UCAttribute : DevExpress.XtraEditors.XtraUserControl
    {
        public UCAttribute()
        {
            InitializeComponent();
        }

        public void SetErrorFeature(IFeature fSource, string strTitle)
        {
            treeListAttribute.ClearNodes();
            if (fSource == null)
                return;

            TreeListNode nodeRoot=null;
            if (!string.IsNullOrEmpty(strTitle))
            {
                nodeRoot = treeListAttribute.AppendNode(new object[] { strTitle }, null);
            }

            CreateNode(fSource, nodeRoot);
        }

        public void SetReferFeature(IFeature fRefer, string strTitle)
        {
            if (fRefer == null)
                return;

            TreeListNode nodeRoot = treeListAttribute.AppendNode(new object[] { strTitle }, null);
            CreateNode(fRefer, nodeRoot);
        }

        private void CreateNode(IFeature fSource,TreeListNode nodeParent)
        {
            string strShapeFieldName=null, strOidFieldName=null;
            IField fieldArea=null,fieldLength=null;
            if (!(fSource is ITopologyFeature) && fSource.Class is IFeatureClass)
            {
                IFeatureClass fClass=fSource.Class as IFeatureClass;
                strOidFieldName=fClass.OIDFieldName;
                strShapeFieldName=fClass.ShapeFieldName;
                fieldArea=fClass.AreaField;
                fieldLength=fClass.LengthField;
            }

            IFields fields = fSource.Fields;
            int count = fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField curField = fields.get_Field(i);
                if (curField.Name == strShapeFieldName || curField==fieldArea || curField==fieldLength)// || curField.Name == strOidFieldName)
                    continue;

                treeListAttribute.AppendNode(new object[] { curField.AliasName, fSource.get_Value(i) },nodeParent);
            }
        }
    }
}
