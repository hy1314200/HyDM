using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    class MatchLineNodeView:BaseTable
    {
        public MatchLineNodeView(OleDbConnection pOleDbConnection)
            : base(pOleDbConnection, "LineNodeEx", false)
        {

        }

        public override DataTable GetRecords(string strWhere, string strGroupBy, string strOrderBy)
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    m_nCurrentRowIndex = 0;
                    /*
                     * Select a.LineNodeID,b.LineNodeID,a.PolygonID,b.PolygonID,a.IsReverse,b.IsReverse,a.EntityID,a.IsFromLine From LineNodeEx as a left join LineNodeEx as b on a.OrtherIndexID=b.LineNodeID Order By a.PolygonID,a.LineIndex
                     * */

                    string commandText = "Select a.LineNodeID as LineNodeID,b.LineNodeID as OtherLineNodeID,a.PolygonID as PolygonID,b.PolygonID as OtherPolygonID,a.IsReverse as IsReverse,b.IsReverse as OtherIsReverse,a.EntityID as EntityID,b.EntityID as OtherEntityID,a.LineIndex as LineIndex,b.LineIndex as OtherLineIndex"//,a.IsFromLine as IsFromLine
                        + " From LineNodeEx as a left join LineNodeEx as b on a.OrtherLineNodeID=b.LineNodeID Where a.IsFromLine=-1"
                        + " Order By a.PolygonID,a.LineIndex";

                    m_pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);


                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(m_pOleDbDataAdapter);


                    return GetNextRecords();
                }
                catch (Exception ex)
                {
                    LogAPI.WriteErrorLog(ex);
                }
            }
            return null;
        }

        public DataTable GetRecordsEx(string strWhere, string strGroupBy, string strOrderBy)
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    m_nCurrentRowIndex = 0;
                    /*
                     * Select a.LineNodeID,b.LineNodeID,a.PolygonID,b.PolygonID,a.IsReverse,b.IsReverse,a.EntityID,a.IsFromLine From LineNodeEx as a left join LineNodeEx as b on a.OrtherIndexID=b.LineNodeID Order By a.PolygonID,a.LineIndex
                     * */

                    string commandText = "Select a.LineNodeID as LineNodeID,b.LineNodeID as OtherLineNodeID,a.PolygonID as PolygonID,b.PolygonID as OtherPolygonID,a.IsReverse as IsReverse,b.IsReverse as OtherIsReverse,a.EntityID EntityID,b.EntityID as OtherEntityID,a.LineIndex as LineIndex,b.LineIndex as OtherLineIndex"//,a.IsFromLine as IsFromLine
                        + " From LineNodeEx as a left join LineNodeEx as b on a.OrtherIndexID=b.LineNodeID"
                        + " Order By a.PolygonID,a.LineIndex";

                    m_pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);


                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(m_pOleDbDataAdapter);


                    return GetNextRecords();
                }
                catch (Exception ex)
                {
                    LogAPI.WriteErrorLog(ex);
                }
            }
            return null;
        }

        public virtual void GetLineByDataRow(DataRow dataRow, ref MatchLineNode matchLineNode)
        {
            if (dataRow != null)
            {
                matchLineNode.LineNodeID = dataRow["LineNodeID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["LineNodeID"]);
                matchLineNode.OtherLineNodeID = dataRow["OtherLineNodeID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["OtherLineNodeID"]);
                matchLineNode.PolygonID = dataRow["PolygonID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["PolygonID"]);
                matchLineNode.OtherPolygonID = dataRow["OtherPolygonID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["OtherPolygonID"]);
                matchLineNode.IsReverse = dataRow["IsReverse"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["IsReverse"]);
                matchLineNode.OtherIsReverse = dataRow["OtherIsReverse"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["OtherIsReverse"]);
                matchLineNode.EntityID = dataRow["EntityID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["EntityID"]);
                matchLineNode.OtherEntityID = dataRow["OtherEntityID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["OtherEntityID"]);
                matchLineNode.LineIndex = dataRow["LineIndex"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["LineIndex"]);
                matchLineNode.OtherLineIndex = dataRow["OtherLineIndex"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["OtherLineIndex"]);
            }
        }
    }

    public class MatchLineNode
    {
        public int LineNodeID = -1;
        public int OtherLineNodeID = -1;
        public int PolygonID = -1;
        public int OtherPolygonID = -1;
        public int IsReverse = -1;
        public int OtherIsReverse = -1;
        public int EntityID = -1;
        public int OtherEntityID = -1;
        public int LineIndex = -1;
        public int OtherLineIndex = -1;
        //public string IsFromLine = "";
    }
}
