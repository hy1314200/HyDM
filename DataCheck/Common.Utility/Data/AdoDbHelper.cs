using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Common.Utility.Data
{
    public class AdoDbHelper
    {

        /// <summary>
        /// 指定当前连接类型，如果类型修改，直接改变此变量
        /// </summary>
        public static DatabaseType CurrentDatabaseType = DatabaseType.Access;

        private static DbProviderFactory m_Dbfactory = DBConnFactory.GetDbProviderFactory(CurrentDatabaseType);
        
        /// <summary>
        ///  打开MDB数据库
        /// </summary>
        /// <param name="strDBPath">mdb全路径</param>
        /// <returns></returns>
        public static IDbConnection GetDbConnection(string strDBPath)
        {
            IDbConnection pConnection = null;
            try
            {
                //连接数据库			
                //下面连接字串中的Provider是针对ACCESS2000环境的,
                //对于ACCESS97,需要改为:Provider=Microsoft.Jet.OLEDB.3.51; 
                string connStr = string.Format(" Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False",strDBPath);
                pConnection = m_Dbfactory.CreateConnection();
                pConnection.ConnectionString = connStr;
                pConnection.Open();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }

            return pConnection;
        }

        /// <summary>
        ///  关闭MDB数据库
        /// </summary>
        /// <param name="pAdoConn"></param>
        /// <returns></returns>
        public static bool CloseDbConnection(IDbConnection pAdoConn)
        {
            try
            {
                if (pAdoConn != null && pAdoConn.State != null)
                {
                    pAdoConn.Close();
                    pAdoConn.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        /// Opens the table.
        /// </summary>
        /// <param name="strTableName">Name of the STR table.</param>
        /// <param name="pRecordset">The p recordset.</param>
        /// <param name="pConnection">The p connection.</param>
        /// <returns></returns>
        public static bool OpenTable(string strTableName, ref DataTable pRecordset, IDbConnection pConnection)
        {
            if (pConnection == null) return false;

            try
            {
                string str = string.Format("select  *  from {0}", strTableName);

                pRecordset =GetDataTable(pConnection, str);
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            if (pRecordset == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 利用DataTable更新数据库里面的记录
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="pRecordset"></param>
        /// <param name="pConnection"></param>
        public static bool UpdateTable(string strTableName, DataTable pRecordset, IDbConnection pConnection)
        {
            DbDataAdapter oleDataAdapter = null;
            DbCommandBuilder cb = null;
            try
            {
                if (pConnection == null)
                {
                    return false;
                }

                if (pConnection.State == ConnectionState.Closed)
                {
                    pConnection.Open();
                }
                string str = string.Format("select  *  from {0}", strTableName);

                oleDataAdapter = GetDbDataAdapter(pConnection, str);

                cb =m_Dbfactory.CreateCommandBuilder();
                cb.DataAdapter=oleDataAdapter;

                oleDataAdapter.Update(pRecordset);

                return true;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            finally
            {
                if (oleDataAdapter != null)
                {
                    oleDataAdapter.Dispose();
                }

                if (cb != null)
                {
                    cb.Dispose();
                }
            }
        }


        /// <summary>
        /// 利用DataTable更新数据库里面的记录
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="pRecordset"></param>
        /// <param name="pConnection"></param>
        public static bool DeleteTable(string strTableName, IDbConnection pConnection)
        {
            try
            {
                if (pConnection == null)
                {
                    return false;
                }

                if (pConnection.State == ConnectionState.Closed)
                {
                    pConnection.Open();
                }
                string str = string.Format("Drop table  {0}", strTableName);

                if (!ExecuteSql(pConnection, str))
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return false;
            }
            return true;
        }

        /// <summary>
        ///  判断MDB中，表是否存在
        /// </summary>
        /// <param name="strTabName"></param>
        /// <param name="pConnection"></param>
        /// <returns></returns>
        public static bool TableExist(string strTabName, IDbConnection pConnection)
        {
            if (pConnection == null) return false;

            DataTable dt =null;

            try
            {
                string strSql = string.Format("select count(*) from {0}", strTabName);
                dt=GetDataTable(pConnection,strSql);
                if (dt==null)
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                // 表不存在
                return false;
            }
            finally
            {
                dt.Dispose();
            }
            // 表存在
            return true;
        }


        /// <summary>
        ///  执行sql语句,这些sql语句都是不用返回记录的,如delete,存储过程等
        /// </summary>
        /// <param name="pAdoConn"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool ExecuteSql2(IDbConnection pAdoConn, string strSql)
        {
            if (pAdoConn == null) return false;

            IDbTransaction sqlTran = null;
            IDbCommand command = m_Dbfactory.CreateCommand();
            try
            {
                sqlTran = pAdoConn.BeginTransaction();                
                command.Connection = pAdoConn;
                command.CommandText = strSql;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                sqlTran.Commit();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
                return false;
            }
            finally
            {
                if (sqlTran != null)
                {
                    sqlTran.Dispose();
                }
                command.Dispose();
            }
            return true;
        }

        /// <summary>
        ///  执行sql语句,这些sql语句都是不用返回记录的,如delete,存储过程等
        /// </summary>
        /// <param name="pAdoConn"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool ExecuteSql(IDbConnection pAdoConn, string strSql)
        {
            if (pAdoConn == null) return false;

            //IDbTransaction sqlTran = null;
            IDbCommand command = m_Dbfactory.CreateCommand();
            try
            {
                //sqlTran = pAdoConn.BeginTransaction();
                command.Connection = pAdoConn;
                command.CommandText = strSql;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
               // sqlTran.Commit();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());
                //if (sqlTran != null)
                //{
                //    sqlTran.Rollback();
                //}
                return false;
            }
            finally
            {
                //if (sqlTran != null)
                //{
                //    sqlTran.Dispose();
                //}
                command.Dispose();
            }
            return true;
        }

        /// <summary>
        /// 返回DbCommand
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static IDbCommand CreatCmd(IDbConnection pAdoConn,string strSql)
        {
            if (pAdoConn == null)
            {
                return null;
            }
            IDbCommand command = m_Dbfactory.CreateCommand();
            command.Connection = pAdoConn;
            command.CommandText = strSql;
            command.CommandType = CommandType.Text;
            return command;
        }

        /// <summary>
        /// 返回OleDbDataAdapter
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static DbDataAdapter GetDbDataAdapter(IDbConnection pAdoConn,string strSql)
        {
            if (pAdoConn == null)
            {
                return null;
            }

            DbDataAdapter oleDataAdapter = m_Dbfactory.CreateDataAdapter();

            oleDataAdapter.SelectCommand = CreatCmd(pAdoConn, strSql) as DbCommand;

            return oleDataAdapter;
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="SqlString"></param>    
        /// <returns></returns>
        public static DataSet GetDataSet(IDbConnection pAdoConn,string strSql)
        {
            if (pAdoConn == null)
            {
                return null;
            }
            DbDataAdapter oleDataAdapter = GetDbDataAdapter(pAdoConn,strSql);
            DataSet ds = new DataSet();
            oleDataAdapter.Fill(ds);
            oleDataAdapter.Dispose();
            return ds;
        }


        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <param name="pAdoConn">The ADO conn.</param>
        /// <param name="sqlString">The SQL string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="maxRow">The max row.</param>
        /// <returns></returns>
        public static DataSet GetDataSet(IDbConnection pAdoConn, string strSql,string tableName,int startRow,int maxRow)
        {
            if (pAdoConn == null)
            {
                return null;
            }
            DbDataAdapter dbDataAdapter = GetDbDataAdapter(pAdoConn, strSql);
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds, startRow, maxRow, tableName);
            dbDataAdapter.Dispose();
            return ds;
        }

        /// <summary>
        ///  执行SQL命令,返回一个Datatable集
        /// </summary>
        /// <param name="pConnection"></param>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(IDbConnection pConnection, string strSql)
        {
            if (pConnection == null) return null;
            try
            {
                DataSet ds =GetDataSet(pConnection,strSql);

                if (ds == null) return null;
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }


        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="pAdoConn">The p ADO conn.</param>
        /// <param name="sqlString">The SQL string.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="maxRow">The max row.</param>
        /// <returns></returns>
        public static DataTable GetDataTable(IDbConnection pAdoConn, string strSql, string tableName, int startRow, int maxRow)
        {
            if (pAdoConn == null) return null;
            try
            {
                DataSet ds = GetDataSet(pAdoConn, strSql,tableName,startRow,maxRow);

                if (ds == null) return null;
                DataTable dt = ds.Tables[tableName];
                return dt;
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
        }

        /// <summary>
        /// 获取一个数据流
        /// </summary>
        /// <param name="oleConn"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IDataReader GetQueryReader(IDbConnection dbConn, string sql)
        {
            IDataReader oleReader = null;
            IDbCommand command = null;
            try
            {
                if (dbConn == null)
                {
                    return null;
                }
                command = CreatCmd(dbConn, sql);

                oleReader = command.ExecuteReader();
            }
            catch (Exception exp)
            {
                Common.Utility.Log.OperationalLogManager.AppendMessage(exp.ToString());

                return null;
            }
            finally
            {
                command.Dispose();
            }
            return oleReader;
        }

        /// <summary>
        /// 根据规范中定义的字段枚举类型获取字段名称
        /// </summary>
        /// <param name="nFldType"></param>
        /// <returns></returns>
        public static string GetFieldTypeName(int nFldType)
        {
            string strFldType = "未知类型";
            switch (nFldType)
            {
                case 1:
                    {
                        strFldType = "唯一标志码类型";
                        break;
                    }
                case 2:
                    {
                        strFldType = "整形";
                        break;
                    }
                case 3:
                    {
                        strFldType = "单精度浮点型";
                        break;
                    }
                case 4:
                    {
                        strFldType = "双精度浮点型";
                        break;
                    }
                case 5:
                    {
                        strFldType = "字符型";
                        break;
                    }
                case 6:
                    {
                        strFldType = "日期型";
                        break;
                    }
                case 7:
                    {
                        strFldType = "布尔类型";
                        break;
                    }
                case 8:
                    {
                        strFldType = "大二进制类型";
                        break;
                    }
            }
            return strFldType;
        }
    }
}
