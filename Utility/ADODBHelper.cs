using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Utility
{
    public class ADODBHelper:Define.IADODBHelper
    {
        private IDbConnection m_DbConnection = null;
        private IDbTransaction m_Transaction = null;
        private DbProviderFactory m_ProviderFactory = null;

        internal ADODBHelper(IDbConnection dbConnection,DbProviderFactory dbFactory)
        {
            this.m_DbConnection = dbConnection;
            this.m_ProviderFactory = dbFactory;

            if (m_DbConnection == null)
                throw new Exception("内部错误：ADO连接没有初始化");
        }

        private void Verify()
        {
            if (m_DbConnection.State == ConnectionState.Closed)
            {
                m_DbConnection.Open();
                
            }
        }
        public void BeginTrans()
        {
            Verify();
            m_Transaction = m_DbConnection.BeginTransaction();
        }

        public void Commit()
        {
            if (m_Transaction != null)
                m_Transaction.Commit();

            m_Transaction.Dispose();
            m_Transaction = null;
        }

        public void Rollback()
        {
            if (m_Transaction != null)
                m_Transaction.Rollback();

            m_Transaction.Dispose();
            m_Transaction = null;
        }

        private IDbCommand m_Command = null;
        public int ExecuteSQL(string strSql)
        {
            Verify();

            m_Command = m_DbConnection.CreateCommand();
            m_Command.CommandText = strSql;
            return m_Command.ExecuteNonQuery();
        }

        public System.Data.DataSet ExecuteDataset(string strSql)
        {
            Verify();

            m_Command = m_DbConnection.CreateCommand();
            m_Command.CommandText = strSql;

            throw new NotImplementedException(); 
        }

        public System.Data.DataTable ExecuteDataTable(string strSql)
        {
            DbDataAdapter dataAdapter = this.m_ProviderFactory.CreateDataAdapter();

            m_Command = m_DbConnection.CreateCommand();
            m_Command.CommandText = strSql;
            dataAdapter.SelectCommand = m_Command as DbCommand;

            DataTable dtResult=new DataTable();
            dataAdapter.Fill(dtResult);

            return dtResult;
        }

        public object ExecuteScalar(string strSql)
        {
            Verify();

            m_Command = m_DbConnection.CreateCommand();
            m_Command.CommandText = strSql;
            return m_Command.ExecuteScalar();
        }

        public System.Data.IDataReader ExecuteReader(string strSql)
        {

            Verify();

            m_Command = m_DbConnection.CreateCommand();
            m_Command.CommandText = strSql;
            return m_Command.ExecuteReader();
        }


        public System.Data.DataTable OpenTable(string strTable)
        {
            return this.ExecuteDataTable(string.Format("select * from {0}", strTable)); 
        }

        public bool TableExists(string strTable)
        {
            Verify();

            return (this.m_DbConnection as DbConnection).GetSchema("Tables", new string[] { strTable }).Rows.Count > 0;
        }

        public System.Data.IDbConnection ADOConnection
        {
            get { return this.m_DbConnection; }
        }
    }
}
