using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class FeatureDatasetCatalogItem:CatalogItem
    {

        //public FeatureDatasetCatalogItem(IDataset dsFeatureDataset, ICatalogItem parent):base(dsFeatureDataset,parent)
        //{
        //    if (!(dsFeatureDataset is IFeatureDataset))
        //        throw new Exception("内部错误：FeatureClassCatalogItem构造参数必须为FeatueDataset");
        //}
           public FeatureDatasetCatalogItem(IDatasetName dsName, ICatalogItem parent):base(dsName,parent)
        {
            if (!(dsName is IFeatureDatasetName))
                throw new Exception("内部错误：FeatureClassCatalogItem构造参数必须为FeatueDataset");
        }
        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(this.m_DatasetName.WorkspaceName, m_DatasetName.Name, null);
        }

        public override List<ICatalogItem> Childrens
        {
            get {
                if (m_Children == null)
                {
                    m_Children = new List<ICatalogItem>();
                    IEnumDatasetName enDatasetName = m_DatasetName.SubsetNames;
                    IDatasetName dsNameSub = enDatasetName.Next();
                    while (dsNameSub != null)
                    {
                        ICatalogItem subItem = CatalogItemFactory.CreateCatalog(dsNameSub, this,this.WorkspaceItem);
                        if (subItem != null)
                            m_Children.Add(subItem); 

                        dsNameSub = enDatasetName.Next();
                    }
                }

                return m_Children;
            }
        }
    }
}
