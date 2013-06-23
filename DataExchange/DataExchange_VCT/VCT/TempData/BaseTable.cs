using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    public class BaseTable
    {
        /// <summary>
        /// 索引
        /// </summary>
        protected int m_nNewIndexID;

        /// <summary>
        /// 数据连接
        /// </summary>
        protected OleDbConnection m_pOleDbConnection;

        /// <summary>
        /// 数据适配器
        /// </summary>
        protected OleDbDataAdapter m_pOleDbDataAdapter;

        ///// <summary>
        ///// 数据集
        ///// </summary>
        //protected DataSet m_pDataSet;

        /// <summary>
        /// 活动数据表
        /// </summary>
        protected DataTable m_pDataTable;

        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName_TempTable = "TempTable";

        protected int m_nCurrentRowIndex = 0;

        private int m_nMaxRecordCount = 10000;
        public int MaxRecordCount
        {
            get
            {
                return m_nMaxRecordCount;
            }
            set
            {
                m_nMaxRecordCount = value;
            }
        }

        protected bool m_isFirst = false;
        //public bool IsFirst
        //{
        //    get
        //    {
        //        return m_isFirst;
        //    }
        //    set
        //    {
        //        m_isFirst = value;
        //    }
        //}


        public BaseTable(OleDbConnection pOleDbConnection, string strTableName, bool isCreateTable,bool isFirst)
        {
            m_pOleDbConnection = pOleDbConnection;
            TableName_TempTable = strTableName;
            m_nNewIndexID = 0;
            m_isFirst = isFirst;

            if (isCreateTable == true)
                CreateTable();
        }

        public bool ExecuteNonQuery(string commandText)
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    OleDbCommand oleDbCommand = new OleDbCommand(commandText, m_pOleDbConnection);

                    oleDbCommand.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
            return false;
        }

        public bool RemoveAllRecord()
        {
            m_nNewIndexID = 0;
            return ExecuteNonQuery("Delete * From " + TableName_TempTable);
        }

        public DataTable GetTableForAdd()
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    string commandText = "Select * From " + TableName_TempTable + " Where 1>2";
                    m_pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);

                    DataSet m_pDataSet = new DataSet();
                    m_pOleDbDataAdapter.Fill(m_pDataSet);
                    if (m_pDataSet.Tables != null && m_pDataSet.Tables.Count > 0)
                    {
                        m_pDataTable = m_pDataSet.Tables[0];

                        return m_pDataTable;
                    }
                    
                    return m_pDataTable;
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
            return null;
        }

        public virtual DataTable GetRecords(string strWhere, string strGroupBy, string strOrderBy)
        {

            return GetRecords("*", strWhere, strGroupBy, strOrderBy);
        }

        public virtual DataTable GetRecords(string strSelectFields, string strWhere, string strGroupBy, string strOrderBy)
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    m_nCurrentRowIndex = 0;

                    string commandText = "Select " + strSelectFields + " From " + TableName_TempTable;
                    if (strWhere.Length > 0)
                        commandText += " Where " + strWhere;
                    if (strGroupBy.Length > 0)
                        commandText += " Group By " + strGroupBy;
                    if (strOrderBy.Length > 0)
                        commandText += " Order By " + strOrderBy;
                    m_pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);


                    //OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(m_pOleDbDataAdapter);

                    //m_pDataSet = new DataSet();

                    return GetNextRecords();
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
            return null;
        }

        public DataTable GetNextRecords()
        {
            DataSet m_pDataSet = new DataSet();
            m_nCurrentRowIndex += m_pOleDbDataAdapter.Fill(m_pDataSet, m_nCurrentRowIndex, this.MaxRecordCount, "Table");
            
            if (m_pDataSet.Tables != null && m_pDataSet.Tables.Count > 0)
            {
                m_pDataTable = m_pDataSet.Tables[0];

                return m_pDataTable;
            }
            return null;
        }

        public void Save(bool bRelease)
        {
            if (m_pOleDbDataAdapter != null && m_pDataTable != null)
            {
                try
                {
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(m_pOleDbDataAdapter);
                    m_pOleDbDataAdapter.Update(m_pDataTable);
                    if (bRelease == true)
                    {
                        m_pDataTable.Rows.Clear();
                        m_pDataTable.Dispose();
                        //m_pDataSet.Tables.Clear();
                        //m_pDataSet.Dispose();
                        m_pOleDbDataAdapter.Dispose();
                        m_pDataTable = null;
                        //m_pDataSet = null;
                        m_pOleDbDataAdapter = null;
                        //m_pDataTable.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
        }

        public virtual bool CreateTable()
        {
            if (this.m_isFirst == false)
            {
                string strCommand = "Drop Table " + TableName_TempTable;
                this.ExecuteNonQuery(strCommand);
            }
            return true;
        }
    }
}
