using System.Data.OleDb;
using System.Data;
using System;
using DIST.DGP.DataExchange.VCT;
using DIST.DGP.DataExchange.VCT.FileData;


namespace DIST.DGP.DataExchange.VCT.TempData 
{
    public class LineNodeTable : EntityNodeTable
    {
        public string FieldName_LineNodeID = "LineNodeID";
        //public string FieldName_EntityID = "EntityID";
        //public string FieldName_FeatureCode = "FeatureCode";
        public string FieldName_LineType = "LineType";
        //public string FieldName_Representation = "Representation";
        public string FieldName_X1 = "X1";
        public string FieldName_Y1 = "Y1";
        public string FieldName_X2 = "X2";
        public string FieldName_Y2 = "Y2";

        public LineNodeTable(OleDbConnection pOleDbConnection, string strTableName, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, strTableName, isCreateTable, bIsFirst)
        {

        }

        public LineNodeTable(OleDbConnection pOleDbConnection, string strTableName)
            : base(pOleDbConnection, strTableName, false, false)
        {

        }

        public LineNodeTable(OleDbConnection pOleDbConnection, bool isCreateTable, bool bIsFirst)
            : base(pOleDbConnection, "LineNode", isCreateTable, bIsFirst)
        {
      
        }

        public LineNodeTable(OleDbConnection pOleDbConnection)
            : base(pOleDbConnection, "LineNode", false, false)
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
                        + FieldName_EntityID + " int,"
                        + FieldName_FeatureCode + " VARCHAR(16),"
                        + FieldName_LineType + " int,"
                        + FieldName_Representation + " VARCHAR(16),"
                        + FieldName_X1 + " double,"
                        + FieldName_Y1 + " double,"
                        + FieldName_X2 + " double,"
                        + FieldName_Y2 + " double)";

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

        protected override DataRow CreateRow(EntityNode entityNode)
        {
            DataRow dataRow = base.CreateRow(entityNode);

            if (dataRow != null)
            {
                LineNode lineNode = entityNode as LineNode;
                if (lineNode != null)
                {
                    dataRow[FieldName_LineNodeID] = m_nNewIndexID++;
                    dataRow[FieldName_LineType] = lineNode.LineType;

                    if (lineNode.SegmentNodes != null)
                    {
                        if (lineNode.SegmentNodes.Count > 0)
                        {
                            BrokenLineNode line = lineNode.SegmentNodes[0] as BrokenLineNode;
                            if (line != null && line.PointInfoNodes.Count >= 2)
                            {
                                dataRow[FieldName_X1] = line.PointInfoNodes[0].X;
                                dataRow[FieldName_Y1] = line.PointInfoNodes[0].Y;
                                dataRow[FieldName_X2] = line.PointInfoNodes[1].X;
                                dataRow[FieldName_Y2] = line.PointInfoNodes[1].Y;
                            }
                        }
                    }
                    return dataRow;
                }
            }
            return null;
        }

        //public override void GetEntityNodeByDataRow(DataRow dataRow, ref EntityNode entityNode)
        //{
        //    GetLineNodeByDataRow(dataRow, entityNode, false);
        //}

        public void GetLineNodeByDataRow(DataRow dataRow, ref LineNode lineNode, bool bReverse)
        {
            if (dataRow != null)
            {
                EntityNode entityNode = lineNode as EntityNode;
                base.GetEntityNodeByDataRow(dataRow, ref entityNode);

                lineNode.IndexID = dataRow[FieldName_LineNodeID] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_LineNodeID]);
                //lineNode.EntityID = dataRow[FieldName_EntityID] == null ? -1 : Convert.ToInt32(dataRow[FieldName_EntityID]);
                //lineNode.FeatureCode = dataRow[FieldName_FeatureCode] == null ? "" : dataRow[FieldName_FeatureCode].ToString();
                lineNode.LineType = dataRow[FieldName_LineType] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_LineType]);
                //lineNode.Representation = dataRow[FieldName_Representation] == null ? "" : dataRow[FieldName_Representation].ToString();

                if (entityNode.EntityID != 0)
                {
                    SegmentNodes segmentNodes = new SegmentNodes();
                    BrokenLineNode brokenLineNode = new BrokenLineNode();
                    PointInfoNodes pointInfoNodes = new PointInfoNodes();
                    double dX1 = dataRow[FieldName_X1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X1]);
                    double dY1 = dataRow[FieldName_Y1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y1]);
                    PointInfoNode pointInfoNode1 = new PointInfoNode(dX1, dY1);

                    double dX2 = dataRow[FieldName_X2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X2]);
                    double dY2 = dataRow[FieldName_Y2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y2]);
                    PointInfoNode pointInfoNode2 = new PointInfoNode(dX2, dY2);

                    if (bReverse == true)
                    {
                        pointInfoNodes.Add(pointInfoNode2);
                        pointInfoNodes.Add(pointInfoNode1);
                    }
                    else
                    {
                        pointInfoNodes.Add(pointInfoNode1);
                        pointInfoNodes.Add(pointInfoNode2);
                    }
                    brokenLineNode.PointInfoNodes = pointInfoNodes;
                    segmentNodes.Add(brokenLineNode);
                    lineNode.SegmentNodes = segmentNodes;
                }
            }
        }

        public void GetLineNodeByDataRow(DataRow dataRow, ref LineNodeSimple lineNode, bool bReverse)
        {
            if (dataRow != null)
            {
                EntityNode entityNode = lineNode as EntityNode;
                base.GetEntityNodeByDataRow(dataRow, ref entityNode);

                lineNode.IndexID = dataRow[FieldName_LineNodeID] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_LineNodeID]);
                //lineNode.EntityID = dataRow[FieldName_EntityID] == null ? -1 : Convert.ToInt32(dataRow[FieldName_EntityID]);
                //lineNode.FeatureCode = dataRow[FieldName_FeatureCode] == null ? "" : dataRow[FieldName_FeatureCode].ToString();
                lineNode.LineType = dataRow[FieldName_LineType] == System.DBNull.Value ? -1 : Convert.ToInt32(dataRow[FieldName_LineType]);
                //lineNode.Representation = dataRow[FieldName_Representation] == null ? "" : dataRow[FieldName_Representation].ToString();

                if (entityNode.EntityID != 0)
                {
                    //SegmentNodes segmentNodes = new SegmentNodes();
                    //BrokenLineNode brokenLineNode = new BrokenLineNode();
                    //PointInfoNodes pointInfoNodes = new PointInfoNodes();
                    //double dX1 = 
                    //double dY1 = 
                    //PointInfoNode pointInfoNode1 = new PointInfoNode(dX1, dY1);

                    //double dX2 = dataRow[FieldName_X2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X2]);
                    //double dY2 = dataRow[FieldName_Y2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y2]);
                    //PointInfoNode pointInfoNode2 = new PointInfoNode(dX2, dY2);

                    if (bReverse == true)
                    {
                        lineNode.X2 = dataRow[FieldName_X1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X1]);
                        lineNode.Y2 = dataRow[FieldName_Y1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y1]);
                        lineNode.X1 = dataRow[FieldName_X2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X2]);
                        lineNode.Y1 = dataRow[FieldName_Y2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y2]);

                        //pointInfoNodes.Add(pointInfoNode2);
                        //pointInfoNodes.Add(pointInfoNode1);
                    }
                    else
                    {
                        lineNode.X1 = dataRow[FieldName_X1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X1]);
                        lineNode.Y1 = dataRow[FieldName_Y1] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y1]);
                        lineNode.X2 = dataRow[FieldName_X2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_X2]);
                        lineNode.Y2 = dataRow[FieldName_Y2] == DBNull.Value ? 0.0 : Convert.ToDouble(dataRow[FieldName_Y2]);

                        //pointInfoNodes.Add(pointInfoNode1);
                        //pointInfoNodes.Add(pointInfoNode2);
                    }
                    //brokenLineNode.PointInfoNodes = pointInfoNodes;
                    //segmentNodes.Add(brokenLineNode);
                    //lineNode.SegmentNodes = segmentNodes;
                }
            }
        }
    }
}
