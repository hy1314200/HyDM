using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class FeatureClassCatalogItem:CatalogItem 
    {

        //public FeatureClassCatalogItem(IDataset dsFeatureClass, ICatalogItem parent):base(dsFeatureClass,parent)
        //{
        //    if(!(dsFeatureClass is IFeatureClass ))
        //        throw new Exception("内部错误：FeatureClassCatalogItem构造参数必须为FeatueClass");
        //}

        public FeatureClassCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if (!(dsName is IFeatureClassName))
                throw new Exception("内部错误：FeatureClassCatalogItem构造参数必须为FeatueClassName");
        }

        public override string GetGpString()
        {
            IDatasetName dsNameParent = (m_DatasetName as IFeatureClassName).FeatureDatasetName;
            

            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName,(dsNameParent==null?null:dsNameParent.Name),m_DatasetName.Name);
        }

        public override string GetTELayerString()
        {
            if (WorkspaceItem == null)
                throw new Exception("内部错误：初始化错误，WorkspaceItem没有指定");

            return Utility.WorkspaceHelper.GetTELayerString(WorkspaceItem.WorkspacePropertySet,WorkspaceItem.WorkspaceType, m_DatasetName.Name);
        }

        public override bool HasChild
        {
            get
            {
                //if (m_Type == enumCatalogType.FeatureClass3D)
                //{
                //    return true;
                //}

                return false;
            }
        }
        public override List<ICatalogItem> Childrens
        {
            get
            {
                if (  m_Type == enumCatalogType.FeatureClass3D && m_Children==null)
                {
                    m_Children = new List<ICatalogItem>();
                    IFeatureClass fClass = this.Dataset as IFeatureClass;
                    IFeatureCursor fCursor = fClass.Search(null, false);
                    IFeature fModel = fCursor.NextFeature();
                    while (fModel != null)
                    {
                        m_Children.Add(new ThreeDimenModelCatalogItem(this.m_DatasetName, fModel.OID, this));

                        fModel = fCursor.NextFeature();
                    }
                }

                return m_Children;
            }
        }
    }
}
