using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Skyline.Core.Helper
{
	class DBConnectionFactory
	{
	  private static DBConnectionFactory factory = null;

        private DBConnectionFactory()
        {
        }
        public static DBConnectionFactory ins()
        {
            if (factory == null)
                factory = new DBConnectionFactory();
            return factory;
        }

        /// <summary>
        /// 根据app.config或web.config配置文件中的
        /// zgis.dbtype、zgis.connection两项来判定使用什么连接来获取系统数据
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            IDbConnection db = null;
            string sDBType = ConfigurationManager.AppSettings["Type"].ToUpper();
            string sConnection = ADODBHelper.ConfigConnectionString;

            switch (sDBType)
            {
                case "ORACLE":
                    db = new OracleConnection(sConnection);
                    db.Open();
                    break;

                case "MSSQL":
                case "SQLSERVER":
                case "SQL SERVER":
                    db = new SqlConnection(sConnection);
                    db.Open();
                    break;

                case "MDB":
                case "ACCESS":
                    db = new OleDbConnection(sConnection);
                    db.Open();
                    break;

                default:
                    throw new Exception("配置的ADO数据库类型不被支持，应该在ORACLE、SQLSERVER、ACCESS当中");


            }
            
            return db;
        }
    }
}
