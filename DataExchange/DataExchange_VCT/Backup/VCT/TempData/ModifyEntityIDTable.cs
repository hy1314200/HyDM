using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    class ModifyEntityIDTable : BaseTable
    {
        public string FieldName_IndexID = "IndexID";
        public string FieldName_LineNodeID = "LineNodeID";
        public string FieldName_EntityID = "EntityID";

        private int nIndex = 0;

        public ModifyEntityIDTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "ModifyEntityID", isCreateTable, bIsFirst)
        {
        }

        public override bool CreateTable()
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    base.CreateTable();

                    string strCommand = "Create Table " + TableName_TempTable + "("
                        + FieldName_IndexID + " int primary key,"
                        + FieldName_LineNodeID + " int,"
                        + FieldName_EntityID + " int)";

                    OleDbCommand oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
                    oleDbCommand.ExecuteNonQuery();

                    nIndex = 0;
                    return true;
                }
                catch (Exception ex)
                {
                    LogAPI.WriteErrorLog(ex);
                }
            }
            return false;
        }

        public void AddRow(int nLineNodeID, int nEntityID)
        {
            DataRow dataRow = CreateRow(nLineNodeID, nEntityID);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(int nLineNodeID, int nEntityID)
        {
            if (m_pDataTable == null)
            {
                GetTableForAdd();
            }
            if (m_pDataTable != null)
            {
                DataRow dataRow = m_pDataTable.NewRow();

                dataRow[FieldName_IndexID] = nIndex++;
                dataRow[FieldName_LineNodeID] = nLineNodeID;
                dataRow[FieldName_EntityID] = nEntityID;

                return dataRow;
            }
            return null;
        }

        public bool DeleteSurplusRows()
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    //删除重复记录（只保留一个）
                    string strCommand = "Delete * From " + TableName_TempTable + " Where " + FieldName_IndexID 
                        + " in( Select a." + FieldName_IndexID + " From " + TableName_TempTable + " As a," + TableName_TempTable + " As b Where a." + FieldName_LineNodeID + "=b." + FieldName_LineNodeID + " And a." + FieldName_IndexID + "<b." + FieldName_IndexID + ")";

                    OleDbCommand oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
                    oleDbCommand.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    LogAPI.WriteErrorLog(ex);
                }
            }
            return false;
        }
    }
}
