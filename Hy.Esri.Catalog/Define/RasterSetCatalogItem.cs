using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

namespace Hy.Esri.Catalog.Define
{
    public class RasterSetCatalogItem:CatalogItem
    {
        //public RasterSetCatalogItem(IDataset dsRaster, ICatalogItem parent)
        //    : base(dsRaster, parent)
        //{
        //    if (!(dsRaster is IRasterDataset))
        //        throw new Exception("内部错误：RasterSetCatalogItem构造参数必须为Raster");
        //}  
        public RasterSetCatalogItem(IDatasetName dsName, ICatalogItem parent)
            : base(dsName, parent)
        {
            if (!(dsName is IRasterDatasetName))
                throw new Exception("内部错误：RasterSetCatalogItem构造参数必须为Raster");
        }

        public override List<ICatalogItem> Childrens
        {
            get
            {
                if (m_Children == null)
                {
                    //IRasterBandCollection colRasterBand = m_Dataset as IRasterBandCollection;
                    //int subCount = colRasterBand.Count;
                    //m_Children = new List<ICatalogItem>();
                    //for (int i = 0; i < subCount; i++)
                    //{
                    //    IDataset dsRasterBand = colRasterBand.Item(i) as IDataset;
                    //    this.m_Children.Add(CatalogItemFactory.CreateCatalog(dsRasterBand, this,this.WorkspaceItem));
                    //}

                    IEnumDatasetName enDatasetName = this.m_DatasetName.SubsetNames;
                    IDatasetName dsNameSub = enDatasetName.Next();
                    m_Children = new List<ICatalogItem>();
                    while (dsNameSub != null)
                    {
                        this.m_Children.Add(CatalogItemFactory.CreateCatalog(dsNameSub, this, this.WorkspaceItem));

                        dsNameSub = enDatasetName.Next();
                    }
                }

                return m_Children;
            }

        }

        public override string GetGpString()
        {
            return Utility.WorkspaceHelper.GetGpString(m_DatasetName.WorkspaceName, m_DatasetName.Name, null);
        }

        public override string GetTELayerString()
        {
            if (WorkspaceItem == null)
                throw new Exception("内部错误：初始化错误，WorkspaceItem没有指定");

            return Utility.WorkspaceHelper.GetTELayerString(WorkspaceItem.WorkspacePropertySet, WorkspaceItem.WorkspaceType, m_DatasetName.Name);
        }
    }
}
