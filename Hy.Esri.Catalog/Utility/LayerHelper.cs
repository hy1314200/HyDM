using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;
using System.IO;

namespace Hy.Esri.Catalog.Utility
{
    /// <summary>
    /// 图层服务类
    /// </summary>
    public class LayerHelper
    {
        /// <summary>
        /// 从文件获取栅格图层
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static ILayer GetRasterLayer(string strFile)
        {
            if (string.IsNullOrEmpty(strFile) || !File.Exists(strFile))
                return null;

            //IWorkspaceFactory wsfRaster = new RasterWorkspaceFactoryClass();
            //IRasterWorkspace rwsSource = wsfRaster.OpenFromFile(Path.GetDirectoryName(strFile), 0) as IRasterWorkspace;

            //if (rwsSource == null)
            //    return null;

            //IRasterDataset rDataset = rwsSource.OpenRasterDataset(Path.GetFileName(strFile));
            //IRasterLayer rasterLayer = new RasterLayerClass();
            //rasterLayer.CreateFromDataset(rDataset);

            //return rasterLayer;


            IRasterLayer rasterLayer = new RasterLayerClass();
            rasterLayer.CreateFromFilePath(strFile);

            return rasterLayer;
        }

        /// <summary>
        /// 从文件夹获取Tin图层
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static ILayer GetTinLayer(string strPath)
        {
            if (string.IsNullOrEmpty(strPath) || !Directory.Exists(strPath))
                return null;

            //DirectoryInfo dirInfo = new DirectoryInfo(strPath);
            //string strParent=dirInfo.Parent.FullName;
            //string strName=dirInfo.Name;
            string strParent = Path.GetDirectoryName(strPath);
            string strName = Path.GetFileName(strPath);
            IWorkspaceFactory wsfTin = new TinWorkspaceFactoryClass();
            if (!wsfTin.IsWorkspace(strParent))
                return null;

            try
            {
                IWorkspace wsTin = wsfTin.OpenFromFile(strParent, 0);
                ITin tinTarget = (wsTin as ITinWorkspace).OpenTin(strName);
                ITinLayer lyrTin = new TinLayerClass();
                lyrTin.Dataset = tinTarget;

                return lyrTin;
            }
            catch
            {
                return null;
            }
        }
    }
}
