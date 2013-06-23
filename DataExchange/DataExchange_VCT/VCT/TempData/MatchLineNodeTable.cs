using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    class MatchLineNodeTable: BaseTable
    {
        public string FieldName_LineIndexID = "LineIndexID";
        public string FieldName_LineExIndexID = "LineExIndexID";
        public string FieldName_EntityID = "EntityID";
        //public string FieldName_Reverse = "Reverse";

        public MatchLineNodeTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "MatchLineNode", isCreateTable, bIsFirst)
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
                        + FieldName_LineIndexID + " int,"
                        + FieldName_LineExIndexID + " int primary key,"
                        + FieldName_EntityID + " int)";
                        //+ FieldName_EntityID + " int primary key)";

                    OleDbCommand oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
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

        public void AddRow(int nLineIndexID, int nLineExIndexID, int nEntityID/*, int nReverse*/)
        {
            DataRow dataRow = CreateRow(nLineIndexID, nLineExIndexID, nEntityID/*, nReverse*/);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(int nLineIndexID, int nLineExIndexID, int nEntityID/*, int nReverse*/)
        {
            if (m_pDataTable == null)
            {
                GetTableForAdd();
            }
            if (m_pDataTable != null)
            {
                DataRow dataRow = m_pDataTable.NewRow();

                dataRow[FieldName_LineIndexID] = nLineIndexID;
                dataRow[FieldName_LineExIndexID] = nLineExIndexID;
                dataRow[FieldName_EntityID] = nEntityID;
                //dataRow[FieldName_Reverse] = nReverse;

                return dataRow;
            }
            return null;
        }
    }
}
