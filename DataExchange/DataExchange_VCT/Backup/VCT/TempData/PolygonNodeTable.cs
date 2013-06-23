using System.Data.OleDb;
using System;
using DIST.DGP.DataExchange.VCT;
using System.Data;
using DIST.DGP.DataExchange.VCT.FileData;

namespace DIST.DGP.DataExchange.VCT.TempData 
{
    public class PolygonNodeTable : EntityNodeTable
    {
        public string FieldName_PolygonType = "PolygonType";
        public string FieldName_X = "X";
        public string FieldName_Y = "Y";
        public string FieldName_ComposeType = "ComposeType";

        public PolygonNodeTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "PolygonNode", isCreateTable, bIsFirst)
        {
        }

        public PolygonNodeTable(OleDbConnection pOleDbConnection)
            : base(pOleDbConnection, "PolygonNode", false, false)
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
                        + FieldName_EntityID + " int primary key,"
                        + FieldName_FeatureCode + " VARCHAR(16),"
                        + FieldName_Representation + " VARCHAR(16),"
                        + FieldName_PolygonType + " int,"
                        + FieldName_X + " double,"
                        + FieldName_Y + " double,"
                        + FieldName_ComposeType + " int)";

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
                PolygonNode polygonNode = entityNode as PolygonNode;
                if (polygonNode != null)
                {
                    dataRow[FieldName_PolygonType] = polygonNode.PolygonType;
                    dataRow[FieldName_X] = polygonNode.LablePointInfoNode.X;
                    dataRow[FieldName_Y] = polygonNode.LablePointInfoNode.Y;
                    dataRow[FieldName_ComposeType] = polygonNode.ComposeType;
                    return dataRow;
                }
            }
            return null;
        }

        public void GetEntityNodeByDataRow(DataRow dataRow, ref PolygonNode polygonNode)
        {
            if (dataRow != null)
            {
                if (polygonNode != null)
                {
                    EntityNode entityNode = polygonNode as EntityNode;

                    base.GetEntityNodeByDataRow(dataRow, ref entityNode);

                    polygonNode.PolygonType = dataRow[FieldName_PolygonType] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_PolygonType]);

                    double dX = dataRow[FieldName_X] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X]);
                    double dY = dataRow[FieldName_Y] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y]);
                    PointInfoNode pointInfoNode = new PointInfoNode(dX, dY);
                    polygonNode.LablePointInfoNode = pointInfoNode;

                    polygonNode.LineNodes = new System.Collections.Generic.List<LineNodeEx>();
                    //polygonNode.ComposeType = dataRow[FieldName_ComposeType] == null ? -1 : Convert.ToInt32(dataRow[FieldName_ComposeType]);
                }
            }
        }

        public void GetEntityNodeByDataRow(DataRow dataRow, ref PolygonNodeSimple polygonNode)
        {
            if (dataRow != null)
            {
                if (polygonNode != null)
                {
                    EntityNode entityNode = polygonNode as EntityNode;

                    base.GetEntityNodeByDataRow(dataRow, ref entityNode);

                    polygonNode.PolygonType = dataRow[FieldName_PolygonType] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_PolygonType]);

                    double dX = dataRow[FieldName_X] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X]);
                    double dY = dataRow[FieldName_Y] == System.DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y]);
                    PointInfoNode pointInfoNode = new PointInfoNode(dX, dY);
                    polygonNode.LablePointInfoNode = pointInfoNode;

                    polygonNode.LineNodes = new System.Collections.Generic.List<int>();
                    //polygonNode.ComposeType = dataRow[FieldName_ComposeType] == null ? -1 : Convert.ToInt32(dataRow[FieldName_ComposeType]);
                }
            }
        }
    }
}
