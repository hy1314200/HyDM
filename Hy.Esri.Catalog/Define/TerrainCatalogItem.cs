using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class TerrainCatalogItem:CatalogItem
    {
        //public TerrainCatalogItem(IDataset dsTerrain, ICatalogItem parent)
        //    : base(dsTerrain, parent)
        //{
        //}
        public TerrainCatalogItem(IDatasetName dsName, ICatalogItem parent)
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
                return enumCatalogType.Terrain;
            }
        }

        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName, null, m_DatasetName.Name);
        }
    }
}
