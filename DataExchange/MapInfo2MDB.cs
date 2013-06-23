using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using OSGeo.OGR;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
//using OSGeo.GDAL;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using stdole;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataInteroperabilityTools;
using DIST.DGP.ArcEngine.Controls;



namespace DIST.DGP.DataExchange.MapInfoConvertor
{
    public class MapInfo2MDB : DataConvertorBaseProgress
    {

        private string m_strMDBFile = "";
        private string m_strMapInfoFile = "";
        /// <summary>
        /// mapinfo格式转mdb（包括mif，tab）
        /// </summary>
        /// <param name="strMapInfoFilePath">mapinfo数据所在的文件夹（批量转换）或者mapinfo数据文件路径（单个转换）</param>
        /// <param name="strMDBPath">输出mdb的完整路径</param>
        public MapInfo2MDB(string strMapInfoFile,string strMDBFile )
        {
            m_strMDBFile = strMDBFile;
            m_strMapInfoFile = strMapInfoFile;
        }
     
        /// <summary>
        /// mapinfo格式转mdb（包括mif，tab）
        /// </summary>
        /// <param name="strMapInfoFilePath">mapinfo数据所在的文件夹（批量转换）或者mapinfo数据文件路径（单个转换）</param>
        /// <param name="strMDBPath">输出mdb的完整路径</param>
        /// <returns></returns>
        private bool MapInfoToMDB(string strMapInfoFilePath,string strMDBPath)
        {
            try
            {
                if (strMapInfoFilePath == "" || strMDBPath == "")
                    return false;

                Geoprocessor geoprocessor = new Geoprocessor();
                QuickImport conversion = new QuickImport();
                //有扩展名，表示为单个文件
                if (System.IO.Path.HasExtension(strMapInfoFilePath))
                {
                    conversion.Input = strMapInfoFilePath;
                }
                else
                {
                    //否则为批量文件
                    conversion.Input = "MAPINFO," + strMapInfoFilePath + "\\*.*,\"RUNTIME_MACROS,\"\"FME_TABLE_PASSWORD,,_MERGE_SCHEMAS,YES\"\",META_MACROS,\"\"SourceFME_TABLE_PASSWORD,\"\",METAFILE,MAPINFO,COORDSYS,,IDLIST,,__FME_DATASET_IS_SOURCE__,true\"";
                }

                conversion.Output = strMDBPath;
                return RunTool(geoprocessor, conversion, null);
            }
            catch (Exception ex)
            {
                On_ProgressFinish(this, "转换过程中出现异常，转换结束，错误原因：" + ex.Message);
            }
            return false;
        }

        public override void Cancel()
        {
            //throw new NotImplementedException();
        }

        public override bool DoWork()
        {
            On_Start(this,"开始转换MapInfo格式数据.....");
            bool bResult = MapInfoToMDB(m_strMapInfoFile, m_strMDBFile);
            if (bResult)
            {
                On_ProgressFinish(this,"转换成功！");
            }
            return true;
        }
    }
}
