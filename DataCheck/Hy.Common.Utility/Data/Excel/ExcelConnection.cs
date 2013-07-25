using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Hy.Common.Utility.Data.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelConnection
    {

        /// <summary>
        ///创建excel数据源连接 
        /// </summary>
        /// <param name="strFileName">文件路径名</param>
        /// <param name="DbConn">返回数据源连接</param>
        /// <returns></returns>
        public static bool CreateConnection(string strFileName, ref IDbConnection DbConn)
        {
            bool bolReturn = false;
            string strConnectString = string.Empty;

            if (string.IsNullOrEmpty(strFileName))
            {
                return false;
            }
            if (!System.IO.File.Exists(strFileName))
            {
                return false;
            }
            strConnectString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1\";Data Source={0};", strFileName);
            try
            {
                //实例化连接
                DbConn = new System.Data.OleDb.OleDbConnection();
                DbConn.ConnectionString = strConnectString;
                DbConn.Open();
                bolReturn = true;
            }
            catch (Exception exp)
            {
                Hy.Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

            }
            return bolReturn;
        }
    }
}
