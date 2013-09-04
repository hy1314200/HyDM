using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class RasterCatalogCatalogItem:FeatureClassCatalogItem
    {
        //public RasterCatalogCatalogItem(IDataset dsRasterCatalog, ICatalogItem parent)
        //    : base(dsRasterCatalog, parent)
        //{
        //    if (!(dsRasterCatalog is IRasterCatalog))
        //        throw new Exception("内部错误：RaterCatalogCatalogItem构造参数必须为RasterCatalog");
        //} 
        public RasterCatalogCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if (!(dsName is IRasterCatalogName))
                throw new Exception("内部错误：RaterCatalogCatalogItem构造参数必须为RasterCatalog");
        }

        public override enumCatalogType Type
        {
            get
            {
                return enumCatalogType.RasterCatalog;
            }
        }
    }
}
