using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataInteroperabilityTools;
using DIST.DGP.ArcEngine.Controls;


namespace DIST.DGP.DataExchange.E00Convertor
{
    public class E002MDB : DataConvertorBaseProgress
    {
        private List<string> m_strE00Files = null;
        private string m_strE00File = "";
        private string m_strMDBFile = "";

        /// <summary>
        /// E00转mdb
        /// </summary>
        /// <param name="strE00File">单个e00文件完整路径</param>
        /// <param name="strMDBPath">mdb文件路径</param>
        /// <returns></returns>
        public E002MDB(string strE00File, string strMDBPath)
        {
            m_strMDBFile = strMDBPath;
            m_strE00File = strE00File;
        }

        /// <summary>
        /// E00转mdb
        /// </summary>
        /// <param name="strE00Files">文件路径集合</param>
        /// <param name="strMDBPath">mdb文件路径</param>
        /// <returns></returns>
        public E002MDB(List<string> strE00Files, string strMDBPath)
        {
            m_strMDBFile = strMDBPath;
            m_strE00Files = strE00Files;
        }

        /// <summary>
        /// E00转mdb
        /// </summary>
        /// <param name="strE00Files">文件路径集合</param>
        /// <param name="strMDBPath">mdb文件路径</param>
        /// <returns></returns>
        private bool E00toMDB(string strE00File, string strMDBPath)
        {
            try
            {
                Geoprocessor geoprocessor = new Geoprocessor();
                QuickImport converion = new QuickImport();
                converion.Input = strE00File;
                converion.Output = strMDBPath;
                return RunTool(geoprocessor, converion, null);
            }
            catch (System.Exception e)
            {
                On_ProgressFinish(this, "转换过程中出现异常，转换结束，错误原因："+e.Message);
                return false;
            }
        }

        /// <summary>
        /// E00转mdb
        /// </summary>
        /// <param name="strE00Files">文件路径集合</param>
        /// <param name="strMDBPath">mdb文件路径</param>
        /// <returns></returns>
        private bool E00toMDB(List<string> strE00Files, string strMDBPath)
        {
            if (strE00Files == null || strE00Files.Count == 0)
                return false;
            if (strE00Files.Count == 1)
                return E00toMDB(strE00Files[0],strMDBPath);


            ///转换输入参数
            string strInput = "E00,"+strE00Files[0]+","+"\"RUNTIME_MACROS,\"\"_TEXT_CURVE,"
            +"FOLLOW,INCLUDE_BND,NO,INCLUDE_TIC,NO,GENERATE_NODE_FEATURES,YES,"
            +"_EXTRA_DATASETS,";

            ///构造源e00文件集合语句
            for (int i = 1; i < strE00Files.Count;i++ )
            {
                    strInput += strE00Files[i] + ",";
            }
            strInput += "_MERGE_SCHEMAS,YES\"\",META_MACROS,\"\"Source_TEXT_CURVE,"
            +"FOLLOW,SourceINCLUDE_BND,NO,SourceINCLUDE_TIC,NO,"
            +"SourceGENERATE_NODE_FEATURES,YES\"\",METAFILE,E00,COORDSYS,,"
            +"IDLIST,,__FME_DATASET_IS_SOURCE__,true\"";

            ///执行转换
            Geoprocessor geoprocessor = new Geoprocessor();
            QuickImport converion = new QuickImport();
            converion.Input = strInput;
            converion.Output = strMDBPath;
            return RunTool(geoprocessor, converion, null);
        }
        /// <summary>
        /// 获取指定目录下的e00文件，不包括子文件夹下的e00文件
        /// </summary>
        /// <returns></returns>
        public List<string> GetE00FilesFormFolder(string strFolderPath)
        {
            try
            {
                List<string> sFileList = new List<string>();
                if (!System.IO.Directory.Exists(strFolderPath))
                    return sFileList;
                //获取e00文件
                string[] pFiles = System.IO.Directory.GetFiles(strFolderPath);
                foreach (string sFile in pFiles)
                {
                    if(IsE00File(sFile))
                    sFileList.Add(sFile);
                }
                return sFileList;
            }
            catch (System.Exception e)
            {
                return null;
            }

        }
        /// <summary>
        /// 判断当前文件是否为e00格式
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool IsE00File(string strFileName)
        {
            try
            {
                string strExtent = System.IO.Path.GetExtension(strFileName);
                if (strExtent.ToUpper() == ".E00")
                    return true;
                else
                    return false;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        public override void Cancel()
        {
           // throw new NotImplementedException();
        }

        public override bool DoWork()
        {
            //throw new NotImplementedException();
            On_Start(this,"开始执行数据转换.....");
            bool bResult = false;
            if (m_strE00Files != null)
            {
                bResult =E00toMDB(m_strE00Files, m_strMDBFile);
            }
            else
            {
                bResult= E00toMDB(m_strE00File, m_strMDBFile);
            }
            if (bResult)
            {
                On_ProgressFinish(this, "转换成功！");
            }
            return bResult;
        }
    }
}
