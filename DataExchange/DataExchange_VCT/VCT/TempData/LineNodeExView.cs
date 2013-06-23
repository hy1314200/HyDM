using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DIST.DGP.DataExchange.VCT.TempData
{
    public class LineNodeExView : BaseTable
    {
        public LineNodeExView(OleDbConnection pOleDbConnection)
            : base(pOleDbConnection, "LineNodeEx", false, false)
        {

        }

        public override DataTable GetRecords(string strWhere, string strGroupBy, string strOrderBy)
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    m_nCurrentRowIndex = 0;

                    string commandText = "Select * from "
                        + "(Select LineNodeID as LineID,X1 as PX1,Y1 as PY1,X2 as PX2,Y2 as PY2,-1 from LineNodeEx Where EntityID=-1 "
                        + "union Select LineNodeID as LineID,X2 as PX1,Y2 as PY1,X1 as PX2,Y1 as PY2,1 from LineNodeEx Where EntityID=-1) "
                        + "Order By PX1,PY1,PX2,PY2,LineID";

                    m_pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);


                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(m_pOleDbDataAdapter);
                    

                    return GetNextRecords();
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
            return null;
        }

        public virtual void GetLineByDataRow(DataRow dataRow, ref TempLineNode line)
        {
            if (dataRow != null)
            {
                line.LineNodeID = dataRow["LineID"] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow["LineID"]);
                line.X1 = dataRow["PX1"] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow["PX1"]);
                line.Y1 = dataRow["PY1"] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow["PY1"]);
                line.X2 = dataRow["PX2"] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow["PX2"]);
                line.Y2 = dataRow["PY2"] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow["PY2"]);
                line.IsReverse = dataRow[5] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[5]);
            }
        }
    }

    public class TempLineNode
    {
        public int LineNodeID = -1;
        public double X1 = 0.0;
        public double Y1 = 0.0;
        public double X2 = 0.0;
        public double Y2 = 0.0;
        public int IsReverse = -1;

        /// <summary>
        /// 操作符“==”
        /// </summary>
        /// <param name="xLine">线实体节点</param>
        /// <param name="yLine">线实体节点</param>
        public static bool operator ==(TempLineNode xLine, TempLineNode yLine)
        {
            if (object.Equals(xLine, null))
            {
                if (object.Equals(yLine, null))
                    return true;
                else
                    return false;
            }
            else
            {
                if (object.Equals(yLine, null))
                    return false;
            }

            if (xLine.X1 - yLine.X1 > -0.000001 && xLine.X1 - yLine.X1 < 0.000001
                && xLine.Y1 - yLine.Y1 > -0.000001 && xLine.Y1 - yLine.Y1 < 0.000001
                && xLine.X2 - yLine.X2 > -0.000001 && xLine.X2 - yLine.X2 < 0.000001
                && xLine.Y2 - yLine.Y2 > -0.000001 && xLine.Y2 - yLine.Y2 < 0.000001)
                return true;
            return false;
        }

        /// <summary>
        /// 操作符“!=”
        /// </summary>
        /// <param name="xLine">线实体节点</param>
        /// <param name="yLine">线实体节点</param>
        public static bool operator !=(TempLineNode xLine, TempLineNode yLine)
        {
            if (object.Equals(xLine, null))
            {
                if (object.Equals(yLine, null))
                    return false;
                else
                    return true;
            }
            else
            {
                if (object.Equals(yLine, null))
                    return true;

            }
            if (xLine.X1 - yLine.X1 > -0.000001 && xLine.X1 - yLine.X1 < 0.000001
                && xLine.Y1 - yLine.Y1 > -0.000001 && xLine.Y1 - yLine.Y1 < 0.000001
                && xLine.X2 - yLine.X2 > -0.000001 && xLine.X2 - yLine.X2 < 0.000001
                && xLine.Y2 - yLine.Y2 > -0.000001 && xLine.Y2 - yLine.Y2 < 0.000001)
                return false;
            return true;
        }

        /// <summary>
        /// 重写方法，GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 重写方法，Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return this == obj as TempLineNode ? true : false;
        }
    }
}
