using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    public class ModifyLineIndexTable : BaseTable
    {
        public string FieldName_LineNodeID = "LineNodeID";
        public string FieldName_LineIndex = "LineIndex";

        public ModifyLineIndexTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool isFirst)
            : base(pOleDbConnection, "ModifyLineIndex", isCreateTable, isFirst)
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
                        + FieldName_LineNodeID + " int primary key,"
                        + FieldName_LineIndex + " int)";

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

        public void AddRow(int nLineNodeID, int nLineIndex)
        {
            DataRow dataRow = CreateRow(nLineNodeID, nLineIndex);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(int nLineNodeID, int nLineIndex)
        {
            if (m_pDataTable == null)
            {
                GetTableForAdd();
            }
            if (m_pDataTable != null)
            {
                DataRow dataRow = m_pDataTable.NewRow();

                dataRow[FieldName_LineNodeID] = nLineNodeID;
                dataRow[FieldName_LineIndex] = nLineIndex;

                return dataRow;
            }
            return null;
        }
    }
}
