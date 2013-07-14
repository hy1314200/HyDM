using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class TopologyCatalogItem:CatalogItem 
    {
         //public TopologyCatalogItem(IDataset dsTopology, ICatalogItem parent)
         //    : base(dsTopology, parent)
         //{
         //    if (!(dsTopology is ITopology))
         //        throw new Exception("内部错误：TopologyCatalogItem构造参数必须为Topology");
         //}
  public TopologyCatalogItem(IDatasetName dsName, ICatalogItem parent)
             : base(dsName, parent)
         {
             if (!(dsName is ITopologyName))
                 throw new Exception("内部错误：TopologyCatalogItem构造参数必须为Topology");
         }
        public override List<ICatalogItem> Childrens
        {
            get { return null; }
        }

        public override bool HasChild
        {
            get
            {
                return false;
            }
        }
        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.Topology;
            }
        }

        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName, (m_DatasetName as ITopologyName).FeatureDatasetName.Name, m_DatasetName.Name);
        }
    }
}
