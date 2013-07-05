using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Common.Utility.Data;
using Hy.Check.Utility;

namespace Hy.Check.Rule.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class StandardHelper
    {
        public StandardHelper(IDbConnection dbConnection)
        {
            this.m_DBConnection = dbConnection;
        }

        private IDbConnection m_DBConnection;

        /// <summary>
        /// 获取指定图层下的指定字段的数据类型
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="m_LayerName"></param>
        /// <returns></returns>
        public string GetLayerFieldType(string fieldName, string lyrName)
        {
          // DataTable dtSchema= (m_DBConnection as System.Data.Common.DbConnection).GetSchema("Column_Type",string[]{m_LayerName,fieldName});

            string strSQL = string.Format("select {0} from {1} where 1=2", fieldName, lyrName);
            DataTable dtSchema = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_DBConnection, strSQL);
            if (dtSchema == null)
                return null;

            return dtSchema.Columns[0].DataType.ToString();
        }

        /// <summary>
        /// 根据 图层别名 ，获取图层的要素类型代码
        /// </summary>
        /// <param name="aryFtCode"></param>
        /// <param name="strLayerName"></param>
        /// <returns></returns>
        public bool GetLayerCodes(ref List<string> aryFtCode, string strLayerName,int standardID)
        {
            try
            {
                string strSql;
                strSql = "select FeatureCode from LR_DicLayer where StandardID=" + standardID + " and LayerName='" +
                         strLayerName + "'";

                // 执行SQL命令得到标准名称		      
                DataTable ipRecordset = Common.Utility.Data.AdoDbHelper.GetDataTable(this.m_DBConnection, strSql);
                if (ipRecordset==null)
                {
                    aryFtCode = null;
                    return false;
                }

                string strFtCode = "";

                // 从DataTable中获取名称
                foreach (DataRow dr in ipRecordset.Rows)
                {
                    if (dr != null)
                    {
                        strFtCode = dr["FeatureCode"].ToString();
                        // 找到一条就退出
                        break;
                    }
                }

                if (strFtCode == "")
                {
                    aryFtCode = null;
                    return false;
                }
                //解析字符串
                string para_str = strFtCode;
                para_str.Trim();
                while (para_str.IndexOf('|') != -1)
                {
                    int left = para_str.IndexOf('|');

                    strFtCode = para_str.Substring(0, left);
                    aryFtCode.Add(strFtCode);

                    para_str = para_str.Substring(left, para_str.Length - 1 - left);
                }

                aryFtCode.Add(para_str);

                //关闭记录集
                ipRecordset.Dispose();
            }
            catch (Exception ex)
            {
                //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nLayerID">图层的ID</param>
        /// <param name="strStdName">建库标准名称</param>
        /// <param name="strAttrTableName">图层别名</param>
        /// <returns></returns>
        public bool GetLayerIDByTableName(ref int nLayerID, string  schemaID, string strAttrTableName)
        {
            try
            {
                int nStdID = SysDbHelper.GetStandardIDBySchemaID(schemaID);

                string strSql = "Select LayerID From LR_DicLayer Where StandardID = " + nStdID + "and AttrTableName = '" +
                                strAttrTableName + "'";
                DataTable dt = new DataTable();
                //	打开表LR_DicStandard
                dt = AdoDbHelper.GetDataTable(this.m_DBConnection, strSql);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                DataRow dr = dt.Rows[0];
                nLayerID = Convert.ToInt32(dr["LayerID"]);
            }
            catch (Exception ex)
            {
                //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                //显示错误信息; 
                //XtraMessageBox.Show("XStandardHelper::GetLayerIDByAlias()" + ex.Message);
                return false;
            }
            return true;
        }
    }
}
