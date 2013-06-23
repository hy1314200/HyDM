using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Skyline.Core.Helper
{
    public class SqlHelper
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private OleDbConnection oledbConn;
        /// <summary>
        /// 数据库连接命令
        /// </summary>
        private OleDbCommand oledbCom;
        /// <summary>
        /// 数据库连接桥梁
        /// </summary>
        private OleDbDataAdapter oledbDap;
        /// <summary>
        /// 查询结果的内存数据表
        /// </summary>
        private DataSet ds;


        /// <summary>
        /// 查询全部信息
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public DataSet selectAll(string tablename, string conditions)
        {
            using (oledbConn = SqlConn.getOleConn())
            {
                try
                {
                    oledbCom = new OleDbCommand("select * from " + tablename + " where " + conditions, oledbConn);
                    oledbDap = new OleDbDataAdapter(oledbCom);
                    ds = new DataSet();
                    oledbDap.Fill(ds, tablename);
                }
                catch (Exception e)
                { 
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return ds;
            }
            
            
        }
        /// <summary>
        /// 查询全部信息
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public DataSet selectSQL(string tablename, string SQL)
        {
            using (oledbConn = SqlConn.getOleConn())
            {
                try
                {
                    oledbCom = new OleDbCommand(SQL, oledbConn);
                    oledbDap = new OleDbDataAdapter(oledbCom);
                    ds = new DataSet();
                    oledbDap.Fill(ds, tablename);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return ds;
            }


        }
        /// <summary>
        /// 查询全部信息
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public DataSet selectTableAll(string tablename)
        {
            using (oledbConn = SqlConn.getOleConn())
            {
                try
                {
                    oledbCom = new OleDbCommand("select * from " + tablename + " ", oledbConn);
                    oledbDap = new OleDbDataAdapter(oledbCom);
                    ds = new DataSet();
                    oledbDap.Fill(ds, tablename);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return ds;
            }


        }
        /// <summary>
        /// 插入方法
        /// </summary>
        /// <param name="commStr">一个完整的insert语句sql的</param>
        /// <returns></returns>
        public bool Insert(string commStr)
        {
            using (oledbConn = SqlConn.getOleConn())
            {
                try
                {
                    oledbCom = new OleDbCommand(commStr, oledbConn);
                    if (oledbCom.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                     System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                     return false;
                }
            }
        }

        /// <summary>
        /// 判断建筑是否存在
        /// </summary>
        /// <param name="objectid">Object主键</param>
        /// <returns></returns>
        public bool JudgeObjectID(string objectid)
        {
            oledbConn = SqlConn.getOleConn();
            string sql = "select * from builderObject where buildObjectID = '" + objectid + "'";
            oledbCom = new OleDbCommand(sql, oledbConn);
            if (oledbCom.ExecuteScalar() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
