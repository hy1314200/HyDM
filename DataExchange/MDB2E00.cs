using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataInteroperabilityTools;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using DIST.DGP.ArcEngine.Controls;


namespace DIST.DGP.DataExchange.E00Convertor
{
    public class MDB2E00 : DataConvertorBaseProgress
    {
        private string m_strMDBFiles="";
        private string m_strE00Path="";
        /// <summary>
        /// 转换mdb到e00
        /// </summary>
        /// <param name="strMDBPath">mdb文件路径或mdb中的feature路径集合,如：(D:\\1.mdb\\D_JC_FD_X;D:\\1.mdb\\D_JC_ZT_X)</param>
        /// <param name="strE00Path"></param>
        /// <returns></returns>
        public  MDB2E00(string strMDBPath,string strE00Path)
        {
            m_strMDBFiles = strMDBPath;
            m_strE00Path = strE00Path;
        }
        public override void Cancel()
        {
            //throw new NotImplementedException();
        }

        public override bool DoWork()
        {
            //throw new NotImplementedException if (m_strMapInfoFile == "" || m_strMDBFile == "")
            On_Start(this, "数据转换开始....");
            Geoprocessor geoprocessor = new Geoprocessor();
            QuickExport conversion = new QuickExport();

            //设置输入参数
            conversion.Output = "E00, " + m_strE00Path + ",\"RUNTIME_MACROS,\"\"_PRECISION,"
            +"double,_COMPRESSION,NONE,_LINEAR_TOPO,"
            +"no\"\",META_MACROS,\"\"Dest_PRECISION,double,Dest_COMPRESSION,"
            +"NONE,Dest_LINEAR_TOPO,no\"\",METAFILE,E00,COORDSYS,,"
            +"__FME_DATASET_IS_SOURCE__,false\"";

            //设置输入路径
            conversion.Input =m_strMDBFiles;
            bool bResult = RunTool(geoprocessor, conversion, null);
            if (bResult)
            {
                On_ProgressFinish(this,"转换成功！");
            }
            return bResult;
        }
    }
}
