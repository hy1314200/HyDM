using System.Data.OleDb;
using System;
using DIST.DGP.DataExchange.VCT;
using System.Data;
using DIST.DGP.DataExchange.VCT.FileData;

namespace DIST.DGP.DataExchange.VCT.TempData 
{
    public class LineNodeExTable : LineNodeTable
    {
        //public string FieldName_InitiallyLineNodeID = "InitiallyLineNodeID";
        public string FieldName_IsFromLine = "IsFromLine";
        public string FieldName_IsReverse = "IsReverse";
        public string FieldName_LineIndex = "LineIndex";
        public string FieldName_OrtherLineNodeID = "OrtherLineNodeID";
        public string FieldName_PolygonID = "PolygonID";
        //public string FieldName_OrtherPolygonID = "OrtherPolygonID";

        public LineNodeExTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "LineNodeEx", isCreateTable, bIsFirst)
        {
        }

        public LineNodeExTable(OleDbConnection pOleDbConnection)
            : base(pOleDbConnection, "LineNodeEx", false, false)
        {
        }

        public override bool CreateTable()
        {
            if (m_pOleDbConnection != null)
            {
                try
                {
                    string strCommand = "";
                    if (this.m_isFirst == false)
                    {
                        strCommand = "Drop Table " + TableName_TempTable;
                        this.ExecuteNonQuery(strCommand);
                    }

                    strCommand = "Create Table " + TableName_TempTable + "("
                        + FieldName_LineNodeID + " int primary key,"
                        + FieldName_EntityID + " int,"
                        + FieldName_FeatureCode + " VARCHAR(16),"
                        + FieldName_LineType + " int,"
                        + FieldName_Representation + " VARCHAR(16),"
                        + FieldName_X1 + " double,"
                        + FieldName_Y1 + " double,"
                        + FieldName_X2 + " double,"
                        + FieldName_Y2 + " double,"
                        + FieldName_IsFromLine + " int,"
                        + FieldName_IsReverse + " int,"
                        + FieldName_LineIndex + " int,"
                        + FieldName_OrtherLineNodeID + " int,"
                        + FieldName_PolygonID + " int)";
                        //+ FieldName_OrtherPolygonID + " int)";

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

        protected override DataRow CreateRow(EntityNode entityNode)
        {
            DataRow dataRow = base.CreateRow(entityNode);

            if (dataRow != null)
            {
                LineNodeEx lineNodeEx = entityNode as LineNodeEx;
                if (lineNodeEx != null)
                {
                    dataRow[FieldName_IsFromLine] = lineNodeEx.IsFromLine ? 1 : -1;
                    dataRow[FieldName_IsReverse] = lineNodeEx.IsReverse ? 1 : -1;
                    dataRow[FieldName_LineIndex] = lineNodeEx.LineIndex;
                    dataRow[FieldName_OrtherLineNodeID] = lineNodeEx.OrtherIndexID;
                    dataRow[FieldName_PolygonID] = lineNodeEx.PolygonID;
                    //dataRow[FieldName_OrtherPolygonID] = lineNodeEx.OtherPolygonID;

                    return dataRow;
                }
            }
            return null;
        }

        public void GetLineNodeByDataRow(DataRow dataRow, ref LineNodeEx lineNodeEx, bool bReverse)
        {
            if (dataRow != null)
            {
                LineNode lineNode = lineNodeEx as LineNode;
                //lineNodeEx.IsReverse = false;
                //if (dataRow[FieldName_IsReverse] != null)
                //{
                //    if (Convert.ToInt32(dataRow[FieldName_IsReverse]) == 1)
                //    {
                //        lineNodeEx.IsReverse = true;
                //        if (bReverse == true)
                //        {
                //            bReverse = false;
                //        }
                //        else
                //        {
                //            bReverse = true;
                //        }
                //    }
                //}
                base.GetLineNodeByDataRow(dataRow, ref lineNode, bReverse);

                lineNodeEx.IsFromLine = false;
                if (dataRow[FieldName_IsFromLine] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(dataRow[FieldName_IsFromLine]) == 1)
                        lineNodeEx.IsFromLine = true;
                }

                lineNodeEx.IsReverse = false;
                if (dataRow[FieldName_IsReverse] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(dataRow[FieldName_IsReverse]) == 1)
                        lineNodeEx.IsReverse = true;
                }
                if (bReverse == true)
                    lineNodeEx.IsReverse = true;

                lineNodeEx.LineIndex = dataRow[FieldName_LineIndex] == System.DBNull.Value ? 0 : Convert.ToInt32(dataRow[FieldName_LineIndex]);
                lineNodeEx.OrtherIndexID = dataRow[FieldName_OrtherLineNodeID] == System.DBNull.Value ? 0 : Convert.ToInt32(dataRow[FieldName_OrtherLineNodeID]);
                lineNodeEx.PolygonID = dataRow[FieldName_PolygonID] == System.DBNull.Value ? 0 : Convert.ToInt32(dataRow[FieldName_PolygonID]);
                //lineNodeEx.OtherPolygonID = dataRow[FieldName_OrtherPolygonID] == null ? 0 : Convert.ToInt32(dataRow[FieldName_OrtherPolygonID]);
            }
        }

        public void SetLineNodeFromLine(DataRow dataRow, int nEntityID, bool bReverse)
        {
            if (dataRow != null)
            {
                dataRow[FieldName_IsFromLine] = 1;
                SetLineNodeEntityID(dataRow, nEntityID, bReverse);
            }
        }

        public void SetLineNodeEntityID(DataRow dataRow, int nEntityID)
        {
            if (dataRow != null)
            {
                dataRow[FieldName_EntityID] = nEntityID;
            }
        }

        public void SetLineNodeEntityID(DataRow dataRow, int nEntityID, bool bReverse)
        {
            int nReverse = bReverse ? 1 : -1;
            SetLineNodeEntityID(dataRow, nEntityID, nReverse);
        }

        public void SetLineNodeEntityID(DataRow dataRow, int nEntityID, int nReverse)
        {
            if (dataRow != null)
            {
                dataRow[FieldName_EntityID] = nEntityID;
                dataRow[FieldName_IsReverse] = nReverse;
            }
        }

        public void SetOtherPolygonLineNode(DataRow dataRow, int nOrtherLineNodeID)
        {
            if (dataRow != null)
            {
                dataRow[FieldName_OrtherLineNodeID] = nOrtherLineNodeID;
            }
        }

        public void ReverseLineNode(DataRow dataRow)
        {
            if (dataRow != null)
            {
                bool bReverse = false;
                if (dataRow[FieldName_IsReverse] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(dataRow[FieldName_IsReverse]) == 1)
                        bReverse = true;
                }

                dataRow[FieldName_IsReverse] = bReverse ? -1 : 1;

                //if (bReverseOtherPolygonLineNode == true)
                //{
                //}
            }
        }

        public void SetLineIndexChange(DataRow dataRow, int nChange)
        {
            if (dataRow != null)
            {
                dataRow[FieldName_LineIndex] = dataRow[FieldName_LineIndex] == System.DBNull.Value ? 0 : Convert.ToInt32(dataRow[FieldName_LineIndex]) + nChange;
            }
        }

        //private DataRow GetOtherPolygonLineNode(LineNodeEx lineNodeEx)
        //{
        //    if (lineNodeEx != null)
        //    {
        //        if (lineNodeEx.OrtherIndexID != -1)
        //        {
        //        }
        //    }
        //    return null;
        //}
    }
}
