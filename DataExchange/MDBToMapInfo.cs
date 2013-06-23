using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
//using OSGeo.OGR;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
//using OSGeo.GDAL;
using System.IO;
//using DIST.DGP.DataExchange.ShapeConvertor;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataInteroperabilityTools;
using System.Windows.Forms;
using DIST.DGP.ArcEngine.Controls;


namespace DIST.DGP.DataExchange.MapInfoConvertor
{
    public class MDBToMapInfo : DataConvertorBaseProgress
    {
        /// <summary>
        /// mdb转mapinfo构造函数
        /// </summary>
        /// <param name="strFeatures">要素路径集合</param>
        /// <param name="strMapInfo">目标文件路径</param>
        /// <param name="pType">目标mapinfo数据格式</param>
        public MDBToMapInfo(string strFeatures,string strMapInfo,EnumMapInfoType pType)
        {
            m_strMapInfoFile = strMapInfo;
            m_strMDBFile = strFeatures;
            m_pType = pType;
        }
        private string m_strMDBFile = "";
        private string m_strMapInfoFile = "";
        private EnumMapInfoType m_pType = EnumMapInfoType.tab;
        
        /*
        /// <summary>
        /// 将shape格式数据转为Mapinfo数据(使用org的方法)
        /// </summary>
        /// <param name="strShapeFilePath"></param>
        /// /// <param name="sMapInfoFilePath"></param>
        /// <returns></returns>
        private bool ConvertShapeToMapInfo(string strShapeFilePath,string sMapInfoFilePath)
        {
            Ogr.RegisterAll();
            Gdal.AllRegister();

            //设置源数据集
            OSGeo.OGR.Driver sourceDriver = Ogr.GetDriverByName("ESRI Shapefile");
            DataSource sourceSource = sourceDriver.Open(strShapeFilePath, 0);
            //设置目标数据集
            OSGeo.OGR.Driver destDriver = Ogr.GetDriverByName("MapInfo File");
            DataSource destSource = destDriver.CreateDataSource(sMapInfoFilePath, new string[] { });
            int layerCount = sourceSource.GetLayerCount();

            int featureCount = 0;
            for (int i = 0; i < layerCount; i++)
            {
                Layer layer = sourceSource.GetLayerByIndex(i);
                featureCount = layer.GetFeatureCount(0);//GetFeatureCount()参数0对ShapeFile文件适用,对MAPINFO文件不限;
                if (featureCount <= 0) featureCount = layer.GetFeatureCount(1);
                Layer destLayer = null;

                for (int k = 0; k < featureCount; k++)
                {
                    OSGeo.OGR.Feature feature = null;
                    try
                    {
                        feature = layer.GetFeature(k);
                    }
                    catch (Exception ex)
                    {//MAPINFO的文件不是从O开始
                        continue;
                    }
                    if (feature != null)
                    {
                        try
                        {
                            OSGeo.OGR.wkbGeometryType geoType = feature.GetGeometryRef().GetGeometryType();
                            if (destLayer == null)
                            {
                                //创建图层
                                destLayer = destSource.CreateLayer(
                                    layer.GetName(),
                                    layer.GetSpatialRef(),
                                    geoType,
                                    new string[] { });

                                //创建字段
                                FeatureDefn featureDefn = layer.GetLayerDefn();
                                for (int m = 0; m < featureDefn.GetFieldCount(); m++)
                                {
                                    destLayer.CreateField(featureDefn.GetFieldDefn(m), 0);
                                }
                            }
                            //写入要素
                            OSGeo.OGR.Feature cloneFeature = feature.Clone();
                            destLayer.CreateFeature(cloneFeature);

                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                //保存到本地
                destLayer.SyncToDisk();
                destLayer.Dispose();
            }

            sourceSource.Dispose();
            destSource.Dispose();
            destDriver.Dispose();

            return true;
        }
         * */

        public override void Cancel()
        {
            //throw new NotImplementedException();
        }

        public override bool DoWork()
        {
            if (m_strMapInfoFile == "" || m_strMDBFile == "")
                return false;
            string strType = "";
            if (m_pType == EnumMapInfoType.tab)
            {
                strType = "MAPINFO";
            }
            else
            {
                strType = "MIF";
            }
            On_Start(this, "转换开始....目标数据类型为【"+strType+"】");
            Geoprocessor geoprocessor = new Geoprocessor();
            QuickExport conversion = new QuickExport();
            //D:\1.mdb\ok\DZZHYFQ;D:\1.mdb\ok\DZJCSS MIF,D:\test,"RUNTIME_MACROS,,META_MACROS,,METAFILE,MIF,COORDSYS,,__FME_DATASET_IS_SOURCE__,false"

            conversion.Input = m_strMDBFile;
            conversion.Output = strType + "," + m_strMapInfoFile + "," + "\"RUNTIME_MACROS,,META_MACROS,,METAFILE,"+strType+",COORDSYS,,__FME_DATASET_IS_SOURCE__,false\"";
            bool bResult = RunTool(geoprocessor, conversion, null);
            if (bResult)
            {
                On_ProgressFinish(this,"转换成功！");
            }
            return bResult;
        }
    }

    /// <summary>
    /// mapinfo数据格式枚举
    /// </summary>
    public enum EnumMapInfoType
    {
        tab,
        mif
    }
}
