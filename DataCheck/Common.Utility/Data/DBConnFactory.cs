using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Common.Utility.Data
{

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        Access,

        SqlServer,

        Oracle
    }

    /// <summary>
    /// 此类用于将来扩展sqlserver、oracle的ado操作连接
    /// </summary>
    public class DBConnFactory
    {

        /// <summary>
        /// 获取连接操作符
        /// </summary>
        /// <param name="DbType"></param>
        /// <returns></returns>
        public static string GetProviderName(DatabaseType DbType)
        {
            string sProviderName = "";
            switch (DbType)
            {
                case DatabaseType.Oracle:
                    sProviderName = "System.Data.OracleClient";
                    break;
                case DatabaseType.SqlServer:
                    sProviderName = "System.Data.SqlClient";
                    break;
                case DatabaseType.Access:
                    sProviderName = "System.Data.OleDb";
                    break;
                default:
                    break;
            }
            return sProviderName;
        }

        public static DbProviderFactory GetDbProviderFactory(DatabaseType DbType)
        {
            DbProviderFactory factory = null;
            factory =DbProviderFactories.GetFactory(GetProviderName(DbType));
            return factory;
        }
    }
}
