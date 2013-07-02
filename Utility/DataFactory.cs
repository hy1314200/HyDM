using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate.Cfg;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.Common;

namespace Utility
{
    /// <summary>
    /// 提供Nhibernate的创建
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 创建Nhibernate实例
        /// 允许指定多个程序集进行
        /// </summary>
        /// <param name="hibernateConnection"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static NhibernateHelper GetNhibernateHelper(string hibernateConnection, List<string> assemblys)
        {
            if (string.IsNullOrWhiteSpace(hibernateConnection))
                throw new Exception("NhibernateHelper创建错误：数据库连接为null");

            if (assemblys == null || assemblys.Count == 0)
            {
                throw new Exception("NhibernateHelper创建错误：没有指定程序集");
            }
            
            Configuration config = new Configuration();
            foreach (string strAssembly in assemblys)
            {
                config.AddAssembly(strAssembly);
            }
            //config.SetProperty("ConnectionString", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\项目\龙岩\Out\Bin\SysWorkspace.mdb;Persist Security Info=False");
            config.Properties["connection.connection_string"] = hibernateConnection;
            NHibernate.ISessionFactory sFactory = config.BuildSessionFactory();
            NHibernate.ISession session = sFactory.OpenSession();

            return new NhibernateHelper(session);
        }

        /// <summary>
        /// 根据数据类型描述和连接字符串获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection(string strType, string strConnection )
        {
            System.Data.Common.DbProviderFactory dbFactory;
            IDbConnection db = null;

            switch (strType.ToUpper())
            {
                case "ORACLE":
                    db = new OracleConnection(strConnection);
                    db.Open();
                    dbFactory = System.Data.OracleClient.OracleClientFactory.Instance;
                    break;

                case "MSSQL":
                case "SQLSERVER":
                case "SQL SERVER":
                    db = new SqlConnection(strConnection);
                    db.Open();
                    dbFactory = System.Data.SqlClient.SqlClientFactory.Instance;
                    break;

                case "MDB":
                case "ACCESS":
                    dbFactory = System.Data.OleDb.OleDbFactory.Instance;
                    db = new OleDbConnection(strConnection);
                    db.Open();
                    break;

                default:
                    throw new Exception("配置的ADO数据库类型不被支持，应该在ORACLE、SQLSERVER、ACCESS当中");


            }

            return db;
        }

    }
}
