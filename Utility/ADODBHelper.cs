using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Utility
{
    public class AdodbHelper:Define.IAdodbHelper
    {
        private IDbConnection m_DbConnection = null;
        private IDbTransaction m_Transaction = null;
        private DbProviderFactory m_ProviderFactory = null;

        public AdodbHelper(IDbConnection dbConnection,DbProviderFactory dbFactory)
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
            Verify();
            if (m_Transaction != null)
                m_Transaction.Commit();

            m_Transaction.Dispose();
            m_Transaction = null;
        }

        public void Rollback()
        {
            Verify();
            if (m_Transaction != null)
                m_Transaction.Rollback();

            m_Transaction.Dispose();
            m_Transaction = null;
        }

        public int ExecuteSQL(string strSql)
        {
            Verify();

            return CreateCommand(strSql).ExecuteNonQuery();
        }

        public System.Data.DataSet ExecuteDataset(string strSql)
        {
            Verify();

            DbDataAdapter dataAdapter = CreateAdapter(strSql);

            DataSet dsResult = new DataSet();
            dataAdapter.Fill(dsResult);

            return dsResult;
        }

        private IDbCommand CreateCommand(string strSql)
        {
            IDbCommand cmd = m_ProviderFactory.CreateCommand();// m_DbConnection.CreateCommand();
            cmd.Connection = m_DbConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;

            return cmd;
        }
        private DbDataAdapter CreateAdapter(string strSql)
        {            
            DbDataAdapter dataAdapter = this.m_ProviderFactory.CreateDataAdapter();
            dataAdapter.SelectCommand = CreateCommand(strSql) as DbCommand;

            return dataAdapter;
        }

        public System.Data.DataTable ExecuteDataTable(string strSql)
        {
            Verify();
            DbDataAdapter dataAdapter = CreateAdapter(strSql);

            DataTable dtResult=new DataTable();
            dataAdapter.Fill(dtResult);

            return dtResult;
        }

        public object ExecuteScalar(string strSql)
        {
            Verify();
            return CreateCommand(strSql).ExecuteScalar();
        }

        public System.Data.IDataReader ExecuteReader(string strSql)
        {
            Verify();

            return  CreateCommand(strSql).ExecuteReader();
        }


        public System.Data.DataTable OpenTable(string strTable)
        {
            return this.ExecuteDataTable(string.Format("select * from {0}", strTable)); 
        }

        public bool TableExists(string strTable)
        {
            Verify();

            return (this.m_DbConnection as DbConnection).GetSchema("Tables", new string[] {null,null, strTable }).Rows.Count > 0;
        }

        public System.Data.IDbConnection ADOConnection
        {
            get { return this.m_DbConnection; }
        }

        private static string m_GetServerTimeSQL = null;
        public DateTime GetServerTime()
        {
            if (m_GetServerTimeSQL == null)
            {
                m_GetServerTimeSQL = this.ExecuteScalar(string.Format("select ItemValue from {0} where ItemKey='GetServerTimeSQL'",Properties.Settings.Default.ParamenterTableName)) as string;
            }
            return Convert.ToDateTime(this.ExecuteScalar(m_GetServerTimeSQL));
        }


        public bool UpdateTable(string strTable, DataTable dtData)
        {
            Verify();
            DbDataAdapter dataAdapter = null;
            DbCommandBuilder cmdBuilder = null;
            try
            {
                dataAdapter = CreateAdapter(string.Format("select * from {0}", strTable));
                //DataTable dtTemp=new DataTable();
                //oleDataAdapter.Fill(dtTemp);
                //dtTemp.Merge(dtData);
                cmdBuilder = m_ProviderFactory.CreateCommandBuilder();
                cmdBuilder.DataAdapter = dataAdapter;
                dataAdapter.InsertCommand= cmdBuilder.GetInsertCommand(true);
                dataAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand(true);
                dataAdapter.Update(dtData);

                return true;
            }
            catch //(Exception exp)
            {
                return false;
            }
            finally
            {
                if (dataAdapter != null)
                {
                    dataAdapter.Dispose();
                }

                if (cmdBuilder != null)
                {
                    cmdBuilder.Dispose();
                }
            }
        }
     
    }
}
