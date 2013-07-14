using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class ThreeDimenModelCatalogItem:CatalogItem,I3DModelCatalogItem
    {
        private string m_Caption;
        private int m_FeatureOID = -1;
        private string m_ModelPath;
        private int m_AttributeOID = -1;

        //public ThreeDimenModelCatalogItem(IDataset dsFeatureClass, int featureOID, ICatalogItem parent)
        //    : base(dsFeatureClass, parent)
        //{
        //    m_FeatureOID = featureOID;
        //}
        public ThreeDimenModelCatalogItem(IDatasetName dsName, int featureOID, ICatalogItem parent)
            : base(dsName, parent)
        {
            m_FeatureOID = featureOID;
        }

        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.ThreeDimenModel;
            }
        }
        public override string Name
        {
            get
            {
                if (string.IsNullOrEmpty(m_Caption))
                {
                    GetModelInfo();
                }
                return m_Caption;
            }
        }
        public override List<ICatalogItem> Childrens
        {
            get
            {
                return null;
            }
        }
        public override bool HasChild
        {
            get
            {
                return false;
            }
        }
        public override string GetGpString()
        {
            return null;
        }


        public string ClassName
        {
            get { return m_DatasetName.Name; }
        }
        public int FeatureOID
        {
            get { return m_FeatureOID; }
        }
        public string DataID
        {
            get
            {
                return string.Format("{0}-{1}", ClassName, FeatureOID);
            }
        }
        public string ModelPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_ModelPath))
                {
                    GetModelInfo();
                }
                return m_ModelPath;
            }
        }
        public int AttributeOID
        {
            get
            {
                if (m_AttributeOID < 0)
                {
                    GetModelInfo();
                }
                return m_AttributeOID;
            }
        }
        /// <summary>
        /// 获取Name，ModelPath，和AttributeOID
        /// </summary>
        private void GetModelInfo()
        {
            Utility.File3DHelper.GetDataInfo(this.DataID, ref m_AttributeOID, ref m_ModelPath, ref m_Caption);
        }
    }
}
