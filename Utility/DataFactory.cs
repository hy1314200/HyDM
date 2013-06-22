using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate.Cfg;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;

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
        /// 根据app.config或web.config配置文件中的
        /// zgis.dbtype、zgis.connection两项来判定使用什么连接来获取系统数据
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection(string strType, string strConnection)
        {
            IDbConnection db = null;

            switch (strType)
            {
                case "ORACLE":
                    db = new OracleConnection(strConnection);
                    db.Open();
                    break;

                case "MSSQL":
                case "SQLSERVER":
                case "SQL SERVER":
                    db = new SqlConnection(strConnection);
                    db.Open();
                    break;

                case "MDB":
                case "ACCESS":
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
