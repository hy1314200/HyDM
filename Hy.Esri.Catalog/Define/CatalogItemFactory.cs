using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.GeoDatabaseExtensions;
//using ESRI.ArcGIS.GeoDatabaseExtensions;

namespace Hy.Esri.Catalog.Define
{
    public class CatalogItemFactory
    {
        public static ICatalogItem CreateCatalog(IDatasetName dsNameSource,ICatalogItem parent,IWorkspaceCatalogItem workspaceItem)
        {
            if (dsNameSource == null)
                return null;

            if (dsNameSource is IWorkspaceName)
            {
                return new WorkspaceCatalogItem(dsNameSource, parent);
            }

            ICatalogItem catalogItem = null;
            switch (dsNameSource.Type)
            {
                case esriDatasetType.esriDTFeatureDataset:
                    catalogItem = new FeatureDatasetCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTFeatureClass:
                    catalogItem = new FeatureClassCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTTable:
                    catalogItem = new TableCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTTerrain:
                    catalogItem = new TerrainCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTTin:
                    catalogItem = new TinCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTTopology:
                    catalogItem = new TopologyCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTRasterCatalog:
                    catalogItem = new RasterCatalogCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTRasterDataset:
                case esriDatasetType.esriDTMosaicDataset:
                    catalogItem= new RasterSetCatalogItem(dsNameSource, parent);
                    break;

                case esriDatasetType.esriDTRasterBand:
                    catalogItem= new RasterBandCatalogItem(dsNameSource, parent);
                    break;

                default:
                    return null;
            }
            if (catalogItem != null)
                catalogItem.WorkspaceItem = workspaceItem;

            return catalogItem;
        }

        public static ILayer CreateLayer(ICatalogItem catalogItem)
        {
            if (catalogItem == null || catalogItem.Dataset==null)
                return null;

            ILayer lyrNew = null;
            enumCatalogType catalogType = catalogItem.Type;
            switch(catalogType)
            {
                case enumCatalogType.Workpace:
                case enumCatalogType.FeatureDataset:
                case enumCatalogType.Table:
                    return null;

                case enumCatalogType.FeatureClassPoint:
                case enumCatalogType.FeatureClassLine:
                case enumCatalogType.FeatureClassArea:
                case enumCatalogType.FeatureClassAnnotation:
                case enumCatalogType.FeatureClassEmpty:
                case enumCatalogType.FeatureClass3D:
                case enumCatalogType.RasterCatalog:
                    IFeatureLayer lyrFeature= new FeatureLayerClass();
                    lyrFeature.FeatureClass = catalogItem.Dataset as IFeatureClass;
                    lyrNew = lyrFeature;
                    break;

                case enumCatalogType.RasterMosaic:
                case enumCatalogType.RasterSet:
                    IRasterLayer lyrRaster = new RasterLayerClass();
                    lyrRaster.CreateFromDataset(catalogItem.Dataset as IRasterDataset);
                    lyrNew = lyrRaster;
                    break;

                case enumCatalogType.RasterBand:
                    IRasterLayer lyrRasterBand = new RasterLayerClass();
                    IRasterBand rasterBand = catalogItem.Dataset as IRasterBand;
                    IRasterBandCollection colRasterBand = new RasterClass();
                    colRasterBand.Add(rasterBand, 0);
                    lyrRasterBand.CreateFromRaster(colRasterBand as IRaster);

                    lyrNew = lyrRasterBand;
                    break;

                case enumCatalogType.Tin:
                    ITinLayer lyrTin = new TinLayerClass();
                    lyrTin.Dataset = catalogItem.Dataset as ITin;
                    lyrNew = lyrTin;
                    break;

                case enumCatalogType.Terrain:
                    ITerrainLayer lyrTerrain = new TerrainLayerClass();
                    lyrTerrain.Terrain = catalogItem.Dataset as ITerrain;
                    lyrNew = lyrTerrain;
                    break;
                    
                case enumCatalogType.Topology:
                    ITopologyLayer lyrTopology = new TopologyLayerClass();
                    lyrTopology.Topology = catalogItem.Dataset as ITopology;
                    lyrNew = lyrTopology as ILayer;
                    break;
            }

            return lyrNew;
        }
    }
}
