using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class TinCatalogItem:CatalogItem
    {
        //public TinCatalogItem(IDataset dsTin, ICatalogItem parent)
        //    : base(dsTin, parent)
        //{
        //} 
        
        public TinCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
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
                return enumCatalogType.Tin;
            }
        }
        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName, null, m_DatasetName.Name);
        }
    }
}
