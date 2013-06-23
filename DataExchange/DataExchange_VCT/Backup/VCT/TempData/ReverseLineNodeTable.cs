using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    /*
     * 合并线节点标识码（不包括设置标识码）
     * 处理自闭合线问题（在不同面中的顺序不一致）
     * 处理线段在一个面中连续，另一个面中不连续的情况
     * */
    public class ReverseLineNodeTable:BaseTable
    {
        public string FieldName_IndexID = "IndexID";
        public string FieldName_LineNodeID = "LineNodeID";

        private int nIndex = 0;

        public ReverseLineNodeTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool isFirst)
            : base(pOleDbConnection, "ReverseLineNode", isCreateTable, isFirst)
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
                        + FieldName_LineNodeID + " int)";

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

        public void AddRow(int nLineNodeID)
        {
            DataRow dataRow = CreateRow(nLineNodeID);
            m_pDataTable.Rows.Add(dataRow);
        }

        protected virtual DataRow CreateRow(int nLineNodeID)
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
                    //删除反向次数为偶数的记录记录
                    string strCommand = "Delete * From " + TableName_TempTable + " Where " + FieldName_LineNodeID
                        + " in ( Select " + FieldName_LineNodeID + " From ("
                        + " Select " + FieldName_LineNodeID + ",Count(*) as RowCount From " + TableName_TempTable + " Group by "+FieldName_LineNodeID
                        +" ) Where RowCount Mod 2=0 )";

                    OleDbCommand oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
                    oleDbCommand.ExecuteNonQuery();

                    //删除重复记录（只保留一个）
                    strCommand = "Delete * From " + TableName_TempTable + " Where " + FieldName_IndexID
                        + " in( Select a." + FieldName_IndexID + " From " + TableName_TempTable + " As a," + TableName_TempTable + " As b Where a." + FieldName_LineNodeID + "=b." + FieldName_LineNodeID + " And a." + FieldName_IndexID + "<b." + FieldName_IndexID + ")";

                    oleDbCommand = new OleDbCommand(strCommand, m_pOleDbConnection);
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
