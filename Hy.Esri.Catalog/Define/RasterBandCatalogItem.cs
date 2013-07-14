using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Define
{
    public class RasterBandCatalogItem:CatalogItem
    {
        //public RasterBandCatalogItem(IDataset dsRasterBand, ICatalogItem parent)
        //    : base(dsRasterBand, parent)
        //{
        //    if (!(dsRasterBand is IRasterBand))
        //        throw new Exception("内部错误：RasterSetCatalogItem构造参数必须为RasterBand");
        //}
        public RasterBandCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if (!(dsName is IRasterBandName))
                throw new Exception("内部错误：RasterSetCatalogItem构造参数必须为RasterBand");
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
        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName, (m_DatasetName as IRasterBandName).RasterDatasetName.Name, m_DatasetName.Name);
        }
    }
}
