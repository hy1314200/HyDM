using DIST.DGP.DataExchange.VCT.FileData;
using System.Data.OleDb;
using System.IO;
using System.Data.Common;
using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Windows.Forms;
namespace DIST.DGP.DataExchange.VCT.TempData 
{
    public class TempFile
    {
        /// <summary>
        /// 数据文件名称
        /// </summary>
        private string m_strFilePathName;

        /// <summary>
        /// 数据连接
        /// </summary>
        private OleDbConnection m_pOleDbConnection;
        public OleDbConnection DbConnection
        {
            get
            {
                return m_pOleDbConnection;
            }
        }

        private bool m_isFirst = false;
        public bool IsFirst
        {
            get
            {
                return m_isFirst;
            }
            set
            {
                m_isFirst = value;
            }
        }

        ///// <summary>
        ///// 面存储表
        ///// </summary>
        //private PolygonNodeTable m_pPolygonNodeTable;

        //public PolygonNodeTable PolygonNodes
        //{
        //    get
        //    {
        //        return m_pPolygonNodeTable;
        //    }
        //}

        ///// <summary>
        ///// 构面线存储表
        ///// </summary>
        //private LineNodeExTable m_pLineNodeExTable;

        //public LineNodeExTable LineNodeExs
        //{
        //    get
        //    {
        //        return m_pLineNodeExTable;
        //    }
        //}

        ///// <summary>
        ///// 线存储表
        ///// </summary>
        //private LineNodeTable m_pLineNodeTable;

        //public LineNodeTable LineNodes
        //{
        //    get
        //    {
        //        return m_pLineNodeTable;
        //    }
        //}

        public TempFile(string strFilePathName)
        {
            Connect(strFilePathName);
        }

        ///// <summary>
        ///// 析构函数
        ///// </summary>
        //~TempFile()
        //{

        //    Close();
        //}

        public void Close()
        {
            if (m_pOleDbConnection != null)
            {
                m_pOleDbConnection.Close();
                m_pOleDbConnection.Dispose();
                m_pOleDbConnection.ConnectionString = "";
                m_pOleDbConnection = null;
                try
                {

                    //if (File.Exists(m_strFilePathName))
                    //{
                    //    File.Delete(m_strFilePathName);
                    //}
                }
                catch (Exception ex)
                {
                    //Delete();
                }
            }
        }

        //private void Delete()
        //{
            
        //    Process proc = new Process();
        //    proc.StartInfo.CreateNoWindow = true;
        //    proc.StartInfo.FileName = "cmd.exe";
        //    proc.StartInfo.UseShellExecute = false;
        //    proc.StartInfo.RedirectStandardError = true;
        //    proc.StartInfo.RedirectStandardInput = true;
        //    proc.StartInfo.RedirectStandardOutput = true;
        //    proc.Start();
        //    proc.StandardInput.WriteLine("del " + m_strFilePathName.Substring(0, m_strFilePathName.Length - 3) + "ldb");
        //    proc.StandardInput.WriteLine("del " + m_strFilePathName);
        //    //proc.StandardInput.WriteLine("delete " + m_strPath + ".tmp.mdb");
        //    proc.Close();
            
        //}

        private bool Connect(string strFilePathName)
        {
            try
            {
                m_strFilePathName = strFilePathName;
                if (File.Exists(strFilePathName))
                {
                    try
                    {
                        File.Delete(strFilePathName);
                    }
                    catch (Exception ee)
                    {
                        //Delete();
                        strFilePathName += "_1.mdb";
                    }
                }

                try
                {
                    ADOX.Catalog catalog = new ADOX.Catalog();
                    catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePathName + ";Jet OLEDB:Engine Type=5");
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(catalog.ActiveConnection);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(catalog);
                }
                catch (Exception ee)
                {
                }

                DbConnectionStringBuilder dcsBuilder = new DbConnectionStringBuilder();

                dcsBuilder.Clear();
                dcsBuilder.Add("Provider", "Microsoft.Jet.Oledb.4.0");
                dcsBuilder.Add("User ID", "Admin");
                dcsBuilder.Add("Password", "");
                dcsBuilder.Add("Data Source", @strFilePathName);

                m_pOleDbConnection = new OleDbConnection(dcsBuilder.ConnectionString);

                m_pOleDbConnection.Open();


                //m_pPolygonNodeTable = new PolygonNodeTable(m_pOleDbConnection, true);
                //m_pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection, true);
                //m_pLineNodeTable = new LineNodeTable(m_pOleDbConnection, true);

                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            return false;
        }

        /// <summary>
        /// 处理面图层中构面线与线图层（与面图层关联）中的线对象之间的关系
        /// </summary>
        public void MatchPolygonToLine()
        {
            /*          string strCommand ="";
                      strCommand = "Update LineNodeEx,LineNode Set LineNodeEx.EntityID=LineNode.EntityID,LineNodeEx.IsFromLine='1' Where"
                      +" (LineNodeEx.X1-LineNode.X1)<0.000001 and (LineNodeEx.X1-LineNode.X1)>-0.000001 "
                      +"and (LineNodeEx.Y1-LineNode.Y1)<0.000001 and (LineNodeEx.Y1-LineNode.Y1)>-0.000001  "
                      +"and (LineNodeEx.X2-LineNode.X2)<0.000001 and (LineNodeEx.X2-LineNode.X2)>-0.000001  "
                      +"and (LineNodeEx.Y2-LineNode.Y2)<0.000001 and (LineNodeEx.Y2-LineNode.Y2)>-0.000001 and LineNodeEx.EntityID=-1";
                      pLineNodeExTable.ExecuteNonQuery(strCommand);

                      strCommand = "Update LineNodeEx,LineNode Set LineNodeEx.EntityID=LineNode.EntityID,LineNodeEx.IsFromLine='1' Where"
                       + "(LineNodeEx.X2-LineNode.X1)<0.000001 and (LineNodeEx.X2-LineNode.X1)>-0.000001 "
                      + "and (LineNodeEx.Y2-LineNode.Y1)<0.000001 and (LineNodeEx.Y2-LineNode.Y1)>-0.000001  "
                      + "and (LineNodeEx.X1-LineNode.X2)<0.000001 and (LineNodeEx.X1-LineNode.X2)>-0.000001  "
                      + "and (LineNodeEx.Y1-LineNode.Y2)<0.000001 and (LineNodeEx.Y1-LineNode.Y2)>-0.000001 and LineNodeEx.EntityID=-1"; 
            
                      pLineNodeExTable.ExecuteNonQuery(strCommand);

      */

            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                LineNodeTable pLineNodeTable = new LineNodeTable(m_pOleDbConnection);
                //pLineNodeTable.MaxRecordCount = 500000;
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);
                //pLineNodeExTable.MaxRecordCount = 500000;
                MatchLineNodeTable pMatchLineNodeTable = new MatchLineNodeTable(m_pOleDbConnection, true, this.IsFirst);

                string strLineNodeExWhere = pLineNodeExTable.FieldName_EntityID + "=-1";
                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_X1 + ","
                    + pLineNodeExTable.FieldName_Y1 + ","
                    + pLineNodeExTable.FieldName_X2 + ","
                    + pLineNodeExTable.FieldName_Y2 + ","
                    + pLineNodeExTable.FieldName_EntityID;
                string strLineNodeWhere = pLineNodeTable.FieldName_EntityID + ">0";
                string strLineNodeOrderBy = pLineNodeTable.FieldName_X1 + ","
                    + pLineNodeTable.FieldName_Y1 + ","
                    + pLineNodeTable.FieldName_X2 + ","
                    + pLineNodeTable.FieldName_Y2 + ","
                    + pLineNodeTable.FieldName_EntityID;

                DataTable dataTableLineNodeEx = pLineNodeExTable.GetRecords(strLineNodeExWhere, "", strLineNodeExOrderBy);
                DataTable dataTableLineNode = pLineNodeTable.GetRecords(strLineNodeWhere, "", strLineNodeOrderBy);

                if (dataTableLineNode.Rows.Count == 0 || dataTableLineNodeEx.Rows.Count == 0)
                    return;

                //更新构面线的标识码
                bool bNeedSave = false;
                int j = 0;
                int i = 0;
                int n = 0;
                while (dataTableLineNode.Rows.Count > 0)
                {
                    for (i = 0; i < dataTableLineNode.Rows.Count; i++)
                    {
                        if (j == dataTableLineNodeEx.Rows.Count)
                            break;

                        LineNodeSimple lineNode = new LineNodeSimple();
                        pLineNodeTable.GetLineNodeByDataRow(dataTableLineNode.Rows[i], ref lineNode, false);

                        while (dataTableLineNodeEx.Rows.Count > 0)
                        {
                            for (; j < dataTableLineNodeEx.Rows.Count; j++)
                            {
                                LineNodeSimple lineNodeEx = new LineNodeSimple();
                                pLineNodeExTable.GetLineNodeByDataRow(dataTableLineNodeEx.Rows[j], ref lineNodeEx, false);

                                if (lineNode == lineNodeEx)
                                {
                                    pMatchLineNodeTable.AddRow(lineNode.IndexID, lineNodeEx.IndexID, lineNode.EntityID);
                                    n++;
                                    //pLineNodeExTable.SetLineNodeFromLine(dataTableLineNodeEx.Rows[j], lineNode.EntityID, false);
                                    //if (bNeedSave == false)
                                    //    bNeedSave = true;
                                }
                                else if (lineNode > lineNodeEx)
                                {

                                }
                                else
                                {
                                    break;
                                }
                            }


                            if (j == dataTableLineNodeEx.Rows.Count)
                            {
                                if (dataTableLineNodeEx.Rows.Count < pLineNodeExTable.MaxRecordCount)
                                    break;
                                dataTableLineNodeEx = pLineNodeExTable.GetNextRecords();
                                j = 0;
                            }
                            else
                                break;
                        }
                    }

                    if (n >= pMatchLineNodeTable.MaxRecordCount)
                    {
                        bNeedSave = true;
                        pMatchLineNodeTable.Save(true);
                        n = 0;
                    }

                    if (i == dataTableLineNode.Rows.Count)
                    {
                        if (dataTableLineNode.Rows.Count < pLineNodeTable.MaxRecordCount)
                            break;
                        dataTableLineNode = pLineNodeTable.GetNextRecords();
                    }
                    else
                        break;
                }
                if (n > 0)
                {
                    bNeedSave = true;
                    pMatchLineNodeTable.Save(true);
                    n = 0;
                }

                if (bNeedSave == true)
                {
                    string strCommand = "Update " + pLineNodeExTable.TableName_TempTable + "," + pMatchLineNodeTable.TableName_TempTable + " Set "
                        + pLineNodeExTable.TableName_TempTable + "." + pLineNodeExTable.FieldName_EntityID + "=" + pMatchLineNodeTable.TableName_TempTable+"." + pMatchLineNodeTable.FieldName_EntityID + ","
                        + pLineNodeExTable.FieldName_IsReverse + "=-1,"
                        + pLineNodeExTable.FieldName_IsFromLine + "=1"
                        + " Where " + pLineNodeExTable.TableName_TempTable + "." + pLineNodeExTable.FieldName_LineNodeID + "=" + pMatchLineNodeTable.TableName_TempTable + "." + pMatchLineNodeTable.FieldName_LineExIndexID;
                    pLineNodeExTable.ExecuteNonQuery(strCommand);

                    bNeedSave = false;
                    pMatchLineNodeTable = new MatchLineNodeTable(m_pOleDbConnection, true, false);
                }


                //反向
                strLineNodeExOrderBy = pLineNodeExTable.FieldName_X2 + ","
                    + pLineNodeExTable.FieldName_Y2 + ","
                    + pLineNodeExTable.FieldName_X1 + ","
                    + pLineNodeExTable.FieldName_Y1 + ","
                    + pLineNodeExTable.FieldName_EntityID;

                //必须是未找到标识码的
                strLineNodeExWhere = pLineNodeExTable.FieldName_EntityID + "=-1";
                dataTableLineNodeEx = pLineNodeExTable.GetRecords(strLineNodeExWhere, "", strLineNodeExOrderBy);
                dataTableLineNode = pLineNodeTable.GetRecords(strLineNodeWhere, "", strLineNodeOrderBy);

                j = 0;
                while (dataTableLineNode.Rows.Count > 0)
                {
                    for (i = 0; i < dataTableLineNode.Rows.Count; i++)
                    {
                        if (j == dataTableLineNodeEx.Rows.Count)
                            break;

                        LineNodeSimple lineNode = new LineNodeSimple();
                        pLineNodeTable.GetLineNodeByDataRow(dataTableLineNode.Rows[i], ref lineNode, false);
                        while (dataTableLineNodeEx.Rows.Count > 0)
                        {

                            for (; j < dataTableLineNodeEx.Rows.Count; j++)
                            {
                                LineNodeSimple lineNodeEx = new LineNodeSimple();
                                pLineNodeExTable.GetLineNodeByDataRow(dataTableLineNodeEx.Rows[j], ref lineNodeEx, true);

                                if (lineNodeEx.EntityID != -1)
                                    continue;

                                if (lineNode == lineNodeEx)
                                {
                                    pMatchLineNodeTable.AddRow(lineNode.IndexID, lineNodeEx.IndexID, lineNode.EntityID);
                                    n++;
                                    //pLineNodeExTable.SetLineNodeEntityID(dataTableLineNodeEx.Rows[j], lineNode.EntityID, true);
                                    //if (bNeedSave == false)
                                    //    bNeedSave = true;
                                }
                                else if (lineNode > lineNodeEx)
                                {

                                }
                                else
                                {
                                    break;
                                }

                            }

                            if (j == dataTableLineNodeEx.Rows.Count)
                            {
                                //if (bNeedSave == true)
                                //{
                                //    pLineNodeExTable.Save(false);
                                //}
                                if (dataTableLineNodeEx.Rows.Count < pLineNodeExTable.MaxRecordCount)
                                    break;
                                //else
                                //    bNeedSave = false;
                                dataTableLineNodeEx = pLineNodeExTable.GetNextRecords();
                                j = 0;
                            }
                            else
                                break;
                        }
                    }

                    if (n >= pMatchLineNodeTable.MaxRecordCount)
                    {
                        bNeedSave = true;
                        pMatchLineNodeTable.Save(true);
                        n = 0;
                    }

                    if (i == dataTableLineNode.Rows.Count)
                    {
                        if (dataTableLineNode.Rows.Count < pLineNodeTable.MaxRecordCount)
                            break;
                        dataTableLineNode = pLineNodeTable.GetNextRecords();
                    }
                    else
                        break;
                }
                if (n > 0)
                {
                    bNeedSave = true;
                    pMatchLineNodeTable.Save(true);
                }


                if (bNeedSave == true)
                {
                    string strCommand = "Update " + pLineNodeExTable.TableName_TempTable + "," + pMatchLineNodeTable.TableName_TempTable + " Set "
                        + pLineNodeExTable.TableName_TempTable + "." + pLineNodeExTable.FieldName_EntityID + "=" + pMatchLineNodeTable.TableName_TempTable + "." + pMatchLineNodeTable.FieldName_EntityID + ","
                        + pLineNodeExTable.FieldName_IsReverse + "=1,"
                        + pLineNodeExTable.FieldName_IsFromLine + "=1"
                        + " Where " + pLineNodeExTable.TableName_TempTable + "." + pLineNodeExTable.FieldName_LineNodeID + "=" + pMatchLineNodeTable.TableName_TempTable + "." + pMatchLineNodeTable.FieldName_LineExIndexID;
                    pLineNodeExTable.ExecuteNonQuery(strCommand);
                }
                /*
                 * 匹配条件不正确，FieldName_EntityID重复
                //更新成对索引
                strCommand = "Update " + pLineNodeExTable.TableName_TempTable + " As a," + pLineNodeExTable.TableName_TempTable + " As b Set a." 
                    + pLineNodeExTable.FieldName_OrtherLineNodeID + "=b." + pLineNodeExTable.FieldName_LineNodeID
                    //+ ",b." + pLineNodeExTable.FieldName_OrtherLineNodeID + "=a." + pLineNodeExTable.FieldName_LineNodeID
                    + " Where a." + pLineNodeExTable.FieldName_EntityID + ">0 And a." + pLineNodeExTable.FieldName_EntityID + "=b." + pLineNodeExTable.FieldName_EntityID;
                pLineNodeExTable.ExecuteNonQuery(strCommand);
                 * */
            }

        }

        /// <summary>
        /// 处理面图层中构面线之间的关系
        /// </summary>
        public void MatchPolygonLine(/*ref int nNewEntityID*/)
        {
            /*Update LineNodeEx Set EntityID=nNewEntityID+LineNodeID  Where LineNodeEx.EntityID=-1
             * 
             * Update LineNodeEx a,LineNodeEx b Set a.EntityID=b.EntityID,a.IsReverse='1',b.OrtherIndexID=a.IndexID,a.OrtherIndexID=b.IndexID Where 
             * (a.X2-b.X1)*(a.X2-b.X1)<0.000001"
                + " and (a.Y2-b.Y1)*(a.Y2-b.Y1)<0.000001"
                + " and (a.X1-b.X2)*(a.X1-b.X2)<0.000001"
                + " and (a.Y1-b.Y2)*(a.Y1-b.Y2)<0.000001"//  and LineNodeEx.EntityID=-1
             * 
             * Select * from (Select IndexID as ID,X1 as PX1,Y1 as PY1,X2 as PX2,Y2 as PY2,0 LineNodeEx Where EntityID=-1 union Select IndexID as ID,X2 as PX1,Y2 as PY1,X1 as PX2,Y1 as PY2,1 LineNodeEx Where EntityID=-1)
             * Order By PX1,PY1,PX2,PY2
             * */
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                //查找匹配项，并写入临时表
                MatchLineNodeExTable matchLineNodeTable = new MatchLineNodeExTable(m_pOleDbConnection, true, this.IsFirst);

                LineNodeExView lineNodeExView = new LineNodeExView(m_pOleDbConnection);
                DataTable dataViewLineNodeEx = lineNodeExView.GetRecords("", "", "");

                TempLineNode tempLineNodeUp = null;
                TempLineNode tempLineNode = null;
                bool bNeedSave = false;
                int n = 0;
                while (dataViewLineNodeEx.Rows.Count > 0)
                {
                    for (int i = 0; i < dataViewLineNodeEx.Rows.Count; i++)
                    {
                        tempLineNode = new TempLineNode();
                        lineNodeExView.GetLineByDataRow(dataViewLineNodeEx.Rows[i], ref tempLineNode);

                        if (tempLineNode == tempLineNodeUp)
                        {
                            if (tempLineNodeUp.IsReverse == -1)
                            {
                                matchLineNodeTable.AddRow(tempLineNodeUp.LineNodeID, tempLineNode.LineNodeID, tempLineNodeUp.IsReverse, tempLineNode.IsReverse/*, nNewEntityID++*/);
                                    
                                n++;
                            }
                        }

                        tempLineNodeUp = tempLineNode;
                    }

                    if (n > matchLineNodeTable.MaxRecordCount)
                    {
                        matchLineNodeTable.Save(true);
                        bNeedSave = true;
                        n = 0;
                    }

                    if (dataViewLineNodeEx.Rows.Count < lineNodeExView.MaxRecordCount)
                        break;
                    dataViewLineNodeEx = lineNodeExView.GetNextRecords();

                }
                if (n > 0)
                {
                    matchLineNodeTable.Save(true);
                    bNeedSave = true;
                }

                if (bNeedSave == true)
                {
                    //更新匹配到的线索引
                    string strCommand = "";
                    strCommand = "Update LineNodeEx,MatchLineNodeEx Set LineNodeEx.OrtherLineNodeID=MatchLineNodeEx.Index2,LineNodeEx.IsReverse=Reverse1 Where LineNodeEx.LineNodeID=MatchLineNodeEx.Index1";//LineNodeEx.EntityID=MatchLineNodeEx.EntityID,
                    matchLineNodeTable.ExecuteNonQuery(strCommand);

                    strCommand = "Update LineNodeEx,MatchLineNodeEx Set LineNodeEx.OrtherLineNodeID=MatchLineNodeEx.Index1,LineNodeEx.IsReverse=Reverse2 Where LineNodeEx.LineNodeID=MatchLineNodeEx.Index2";//LineNodeEx.EntityID=MatchLineNodeEx.EntityID,
                    matchLineNodeTable.ExecuteNonQuery(strCommand);
                }

                ////给未匹配到的线实体分配标识码
                //LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection, false);

                //string strWhere = pLineNodeExTable.FieldName_EntityID + "<>0 And " + pLineNodeExTable.FieldName_IsFromLine + "='0'";
                //DataTable dataTableLineNodeEx = pLineNodeExTable.GetRecords(strWhere, "", "");
                //while (dataTableLineNodeEx.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dataTableLineNodeEx.Rows.Count; i++)
                //    {
                //        //必须是未找到标识码的
                //        if (Convert.ToInt32(dataTableLineNodeEx.Rows[i][pLineNodeExTable.FieldName_EntityID]) != -1)
                //            continue;
                //        //需要创建要素编码
                //        pLineNodeExTable.SetLineNodeEntityID(dataTableLineNodeEx.Rows[i], nNewEntityID++, false);
                //    }

                //    pLineNodeExTable.Save(false);


                //    if (dataTableLineNodeEx.Rows.Count < pLineNodeExTable.MaxRecordCount)
                //        break;
                //    dataTableLineNodeEx = pLineNodeExTable.GetNextRecords();
                //}
                /*
                string commandText = "Select * from "
                    + "(Select IndexID as ID,X1 as PX1,Y1 as PY1,X2 as PX2,Y2 as PY2,0 LineNodeEx Where EntityID=-1 "
                    + "union Select IndexID as ID,X2 as PX1,Y2 as PY1,X1 as PX2,Y1 as PY2,1 LineNodeEx Where EntityID=-1) " 
                    + "Order By PX1,PY1,PX2,PY2";

                OleDbDataAdapter pOleDbDataAdapter = new OleDbDataAdapter(commandText, m_pOleDbConnection);


                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(pOleDbDataAdapter);
                DataSet pDataSet = new DataSet();

                int nCurrentRowIndex = 0;
                nCurrentRowIndex += pOleDbDataAdapter.Fill(pDataSet, nCurrentRowIndex, 10000, "Table");
                if (pDataSet.Tables != null && pDataSet.Tables.Count > 0)
                {
                    DataTable pDataTable = pDataSet.Tables[0];

                    for (int i = 0; i < pDataTable.Rows.Count; i++)
                    {
                    }
               
                }
                 * */

            }

            /*
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection, false);

                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_X1 + ","
                    + pLineNodeExTable.FieldName_Y1 + ","
                    + pLineNodeExTable.FieldName_X2 + ","
                    + pLineNodeExTable.FieldName_Y2 + ","
                    + pLineNodeExTable.FieldName_EntityID;
                //必须是未找到标识码的
                string strWhere = pLineNodeExTable.FieldName_EntityID + "=-1";
                DataTable dataTableLineNodeEx = pLineNodeExTable.GetRecords(strWhere, "", strLineNodeExOrderBy);

                string strLineNodeExOrderByReverse = pLineNodeExTable.FieldName_X2 + ","
                   + pLineNodeExTable.FieldName_Y2 + ","
                   + pLineNodeExTable.FieldName_X1 + ","
                   + pLineNodeExTable.FieldName_Y1 + ","
                   + pLineNodeExTable.FieldName_EntityID;
                LineNodeExTable pLineNodeExTableReverse = new LineNodeExTable(m_pOleDbConnection, false);
                DataTable dataTableLineNodeExReverse = pLineNodeExTableReverse.GetRecords(strWhere, "", strLineNodeExOrderByReverse);

                //DataRow[] dataRows = dataTableLineNodeEx.Select("", strLineNodeExOrderBy2);

                int k = 0;
                int j = 0;
                for (int i = 0; i < dataTableLineNodeEx.Rows.Count; i++)
                {
                    LineNode lineNodeEx = new LineNodeEx();
                    pLineNodeExTable.GetLineNodeByDataRow(dataTableLineNodeEx.Rows[i], ref lineNodeEx, false);

                    //需要创建要素编码
                    lineNodeEx.EntityID = nNewEntityID++;
                    pLineNodeExTable.SetLineNodeEntityID(dataTableLineNodeEx.Rows[i], lineNodeEx.EntityID, false);

                    for (; j < dataTableLineNodeExReverse.Rows.Count; j++)
                    {
                        LineNode lineNodeExReverse = new LineNodeEx();
                        pLineNodeExTable.GetLineNodeByDataRow(dataTableLineNodeExReverse.Rows[j], ref lineNodeExReverse, true);
                        if (lineNodeExReverse.EntityID == -1)
                        {
                            if (lineNodeEx == lineNodeExReverse)
                            {
                                //arrLineNodeNewClone[j].InitiallyLineNode.EntityID = arrLineNodeExNew[i].EntityID;
                                //arrLineNodeNewClone[j].InitiallyLineNode.Reverse();
                                pLineNodeExTableReverse.SetLineNodeEntityID(dataTableLineNodeExReverse.Rows[j], lineNodeEx.EntityID, true);

                                //arrLineNodeNewClone[j].InitiallyLineNode.OtherPolygonLineNode = arrLineNodeExNew[i];
                                //arrLineNodeExNew[i].OtherPolygonLineNode = arrLineNodeNewClone[j].InitiallyLineNode;
                                pLineNodeExTableReverse.SetOtherPolygonLineNode(dataTableLineNodeExReverse.Rows[j], lineNodeEx.IndexID);
                                pLineNodeExTable.SetOtherPolygonLineNode(dataTableLineNodeEx.Rows[i], lineNodeExReverse.IndexID);
                                k++;
                            }
                            if (lineNodeEx > lineNodeExReverse)
                            {
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if ((k + 1) % 10000 == 0)
                    {
                        pLineNodeExTable.Save(false);
                        pLineNodeExTableReverse.Save(false);
                    }
                }
                if (k > 0)
                {
                    pLineNodeExTable.Save(true);
                    pLineNodeExTableReverse.Save(true);
                }
            }
             * */
        }

        /// <summary>
        /// 合并线节点标识码
        /// </summary>
        //public void MergePolygonLineEntityID(ref LineNodeEx lineNodeBegin, int nIndexBegin, int nIndexEnd)
        //{
        //    if (lineNodeBegin != null && nIndexEnd > 0)
        //    {
        //        bool bNeedFind = true;
        //        int k = nIndexEnd;
        //        //LineNode lineNodeTemp = null;
        //        while (bNeedFind == true)
        //        {
        //            if (k <= nIndexBegin)
        //                break;

        //            LineNodeEx lineNodeK = new LineNodeEx();
        //            //lineNodeTemp = lineNodeK as LineNode;
        //            pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[k - 1], ref lineNodeK, false);
        //            LineNodeEx lineNodeKOther = null;
        //            if (lineNodeK.OrtherIndexID > -1 && lineNodeK.OrtherIndexID < dataTableTemp.Rows.Count)
        //            {
        //                lineNodeKOther = new LineNodeEx();
        //                //lineNodeTemp = lineNodeKOther as LineNode;
        //                pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeK.OrtherIndexID], ref lineNodeKOther, false);
        //            }
        //            else
        //                lineNodeKOther = null;

        //            //不仅仅是最后一个
        //            if ((lineNodeBeginOther != null && lineNodeKOther != null && lineNodeBeginOther.PolygonID == lineNodeKOther.PolygonID)
        //                || (lineNodeBeginOther == null && lineNodeKOther == null && lineNodeBegin.IsFromLine == false && lineNodeK.IsFromLine == false))
        //            {
        //                //合并线
        //                lineNodeK.EntityID = lineNodeBegin.EntityID;
        //                pLineNodeExTable.SetLineNodeEntityID(dataTable.Rows[k - 1], lineNodeBegin.EntityID);

        //                if (lineNodeK.IsReverse != lineNodeBegin.IsReverse)
        //                {
        //                    //若方向不一致
        //                    //lineNodeK.Reverse();
        //                    pLineNodeExTable.ReverseLineNode(dataTable.Rows[k - 1]);
        //                    //在另一个面中的对象也反向
        //                    //if (lineNodeKOther != null)
        //                    //    lineNodeKOther.Reverse();
        //                    if (lineNodeKOther != null)
        //                        pLineNodeExTableTemp.ReverseLineNode(dataTableTemp.Rows[lineNodeK.OrtherIndexID]);

        //                }

        //                k--;
        //            }
        //            else
        //                bNeedFind = false;
        //        }
        //        lineNodeBegin = null;
        //    }
        //}
        //public void MergePolygonLineEntityID(LineNodeExTable pLineNodeExTable, ref LineNodeEx lineNodeBegin, LineNodeEx lineNodeBeginOther, int nIndexBegin, int nIndexEnd, DataTable dataTable, DataTable dataTableTemp, LineNodeExTable pLineNodeExTableTemp)
        //{
        //    if (lineNodeBegin != null && nIndexEnd > 0)
        //    {
        //        bool bNeedFind = true;
        //        int k = nIndexEnd;
        //        //LineNode lineNodeTemp = null;
        //        while (bNeedFind == true)
        //        {
        //            if (k <= nIndexBegin)
        //                break;

        //            LineNodeEx lineNodeK = new LineNodeEx();
        //            //lineNodeTemp = lineNodeK as LineNode;
        //            pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[k - 1], ref lineNodeK, false);
        //            LineNodeEx lineNodeKOther = null;
        //            if (lineNodeK.OrtherIndexID > -1 && lineNodeK.OrtherIndexID < dataTableTemp.Rows.Count)
        //            {
        //                lineNodeKOther = new LineNodeEx();
        //                //lineNodeTemp = lineNodeKOther as LineNode;
        //                pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeK.OrtherIndexID], ref lineNodeKOther, false);
        //            }
        //            else
        //                lineNodeKOther = null;

        //            //不仅仅是最后一个
        //            if ((lineNodeBeginOther != null && lineNodeKOther != null && lineNodeBeginOther.PolygonID == lineNodeKOther.PolygonID)
        //                || (lineNodeBeginOther == null && lineNodeKOther == null && lineNodeBegin.IsFromLine == false && lineNodeK.IsFromLine == false))
        //            {
        //                //合并线
        //                lineNodeK.EntityID = lineNodeBegin.EntityID;
        //                pLineNodeExTable.SetLineNodeEntityID(dataTable.Rows[k - 1], lineNodeBegin.EntityID);

        //                if (lineNodeK.IsReverse != lineNodeBegin.IsReverse)
        //                {
        //                    //若方向不一致
        //                    //lineNodeK.Reverse();
        //                    pLineNodeExTable.ReverseLineNode(dataTable.Rows[k - 1]);
        //                    //在另一个面中的对象也反向
        //                    //if (lineNodeKOther != null)
        //                    //    lineNodeKOther.Reverse();
        //                    if (lineNodeKOther != null)
        //                        pLineNodeExTableTemp.ReverseLineNode(dataTableTemp.Rows[lineNodeK.OrtherIndexID]);

        //                }

        //                k--;
        //            }
        //            else
        //                bNeedFind = false;
        //        }
        //        lineNodeBegin = null;
        //    }
        //}

        /// <summary>
        /// 合并线节点标识码（非引用线）
        /// </summary>
        public void MergeLineEntityID(ref int nNewEntityID)
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                MatchLineNodeExView matchLineNodeView = new MatchLineNodeExView(m_pOleDbConnection);
                ModifyEntityIDTable modifyEntityIDTable = new ModifyEntityIDTable(m_pOleDbConnection, true, this.IsFirst);
                ReverseLineNodeTable reverseLineNodeTable = new ReverseLineNodeTable(m_pOleDbConnection, true, this.IsFirst);

                DataTable dataViewLineNodeEx = matchLineNodeView.GetRecords("", "", "");

                bool bNeedSave = false;
                MatchLineNodeEx matchLineNodeUp = null;
                MatchLineNodeEx matchLineNode = null;
                MatchLineNodeEx matchLineBegin = null;
                int n = 0;
                bool bExistUp = false;
                List<MatchLineNodeEx> arrMatchLineNode = new List<MatchLineNodeEx>();
                List<int> arrLineNodeReverse = new List<int>();//记录已经反向的线节点
                while (dataViewLineNodeEx.Rows.Count > 0)
                {
                    for (int i = 0; i < dataViewLineNodeEx.Rows.Count; i++)
                    {
                        matchLineNode = new MatchLineNodeEx();
                        matchLineNodeView.GetLineByDataRow(dataViewLineNodeEx.Rows[i], ref matchLineNode);
                        
                        //if (arrLineNodeReverse.Contains(matchLineNode.LineNodeID))
                        //    matchLineNode.IsReverse = -matchLineNode.IsReverse;
                        
                        if (matchLineNodeUp != null)
                        {
                            //判断当前线节点与上一线节点是否属于同一个面
                            if (matchLineNode.PolygonID == matchLineNodeUp.PolygonID)
                            {
                                //属于同一个面
                                if (bExistUp == true)
                                {
                                    if (matchLineNode.EntityID == 0)//合并处理必须在同一个环内处理
                                    {
                                        //处理环内的首尾节点
                                        //MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, i, dataTable, dataTableTemp, pLineNodeExTableTemp);
                                        for (int j = 0; j < arrMatchLineNode.Count; j++)
                                        {
                                            modifyEntityIDTable.AddRow(arrMatchLineNode[j].LineNodeID, matchLineBegin.EntityID);

                                            if (arrMatchLineNode[j].IsReverse != matchLineBegin.IsReverse)
                                            {
                                                //若方向不一致

                                                //在另一个面中的对象也反向
                                                if (arrMatchLineNode[j].OtherLineNodeID != -1)
                                                {
                                                    if (arrMatchLineNode[j].OtherLineNodeID > arrMatchLineNode[j].LineNodeID)
                                                    {
                                                        //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                                                        reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                                        reverseLineNodeTable.AddRow(arrMatchLineNode[j].OtherLineNodeID);
                                                    }
                                                }
                                                else
                                                {
                                                    //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                                                    reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                                }
                                            }
                                            n++;
                                        }
                                        arrMatchLineNode.Clear();

                                        bExistUp = false;
                                    }
                                    else if (matchLineNode.LineIndex - matchLineNodeUp.LineIndex != 1)//(matchLineNode.IsFromLine == "1")//当前线节点是引用线
                                    {
                                        arrMatchLineNode.Clear();
                                        bExistUp = false;
                                    }
                                    else
                                    {
                                        //相邻线节点处理

                                        //线所属的面必须一致
                                        if ((matchLineNode.OtherLineNodeID != -1 && matchLineNodeUp.OtherLineNodeID != -1 && matchLineNode.OtherPolygonID == matchLineNodeUp.OtherPolygonID)
                                            || (matchLineNode.OtherLineNodeID == -1 && matchLineNodeUp.OtherLineNodeID == -1))
                                        {
                                            //合并线
                                            matchLineNode.EntityID = matchLineNodeUp.EntityID;
                                            modifyEntityIDTable.AddRow(matchLineNode.LineNodeID, matchLineNodeUp.EntityID);

                                            if (matchLineNode.IsReverse != matchLineNodeUp.IsReverse)
                                            {
                                                //若方向不一致
                                                
                                                //在另一个面中的对象也反向
                                                if (matchLineNode.OtherLineNodeID != -1)
                                                {
                                                    if (matchLineNode.OtherLineNodeID > matchLineNode.LineNodeID)
                                                    {
                                                        matchLineNode.IsReverse = -matchLineNode.IsReverse;
                                                        reverseLineNodeTable.AddRow(matchLineNode.LineNodeID);
                                                        reverseLineNodeTable.AddRow(matchLineNode.OtherLineNodeID);
                                                    }
                                                }
                                                else
                                                {
                                                    matchLineNode.IsReverse = -matchLineNode.IsReverse;
                                                    reverseLineNodeTable.AddRow(matchLineNode.LineNodeID);
                                                }

                                            }
                                            n++;
                                        }
                                        else
                                        {
                                            if (arrMatchLineNode.Count > 0)
                                                arrMatchLineNode.Clear();
                                        }

                                        if (matchLineNode.EntityID != matchLineBegin.EntityID)
                                        {
                                            if ((matchLineNode.OtherLineNodeID != -1 && matchLineBegin.OtherLineNodeID != -1 && matchLineNode.OtherPolygonID == matchLineBegin.OtherPolygonID)
                                                || (matchLineNode.OtherLineNodeID == -1 && matchLineBegin.OtherLineNodeID == -1))
                                            {
                                                arrMatchLineNode.Add(matchLineNode);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    arrMatchLineNode.Clear();
                                    matchLineBegin = matchLineNode;
                                    bExistUp = true;
                                }
                            }
                            else
                            {
                                //不属于同一个面（处理首尾节点）
                                //MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, i, dataTable, dataTableTemp, pLineNodeExTableTemp);
                                for (int j = 0; j < arrMatchLineNode.Count; j++)
                                {
                                    modifyEntityIDTable.AddRow(arrMatchLineNode[j].LineNodeID, matchLineBegin.EntityID);

                                    if (arrMatchLineNode[j].IsReverse != matchLineBegin.IsReverse)
                                    {
                                        //若方向不一致

                                        //在另一个面中的对象也反向
                                        if (arrMatchLineNode[j].OtherLineNodeID != -1)
                                        {
                                            if (arrMatchLineNode[j].OtherLineNodeID > arrMatchLineNode[j].LineNodeID)
                                            {
                                                //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                                                reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                                reverseLineNodeTable.AddRow(arrMatchLineNode[j].OtherLineNodeID);
                                            }
                                        }
                                        else
                                        {
                                            //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                                            reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                        }
                                    }
                                    n++;
                                }
                                arrMatchLineNode.Clear();
                                matchLineBegin = matchLineNode;
                                bExistUp = true;
                            }


                        }
                        else
                        {
                            matchLineBegin = matchLineNode;
                            bExistUp = true;
                        }

                        if (matchLineNode.EntityID == -1)
                        {
                            matchLineNode.EntityID = nNewEntityID++;
                            modifyEntityIDTable.AddRow(matchLineNode.LineNodeID, matchLineNode.EntityID);
                        }

                        matchLineNodeUp = matchLineNode;
                    }
                    if (n > modifyEntityIDTable.MaxRecordCount)
                    {
                        modifyEntityIDTable.Save(true);
                        reverseLineNodeTable.Save(true);
                        bNeedSave = true;
                        n = 0;
                    }

                    if (dataViewLineNodeEx.Rows.Count < matchLineNodeView.MaxRecordCount)
                        break;
                    dataViewLineNodeEx = matchLineNodeView.GetNextRecords();
                }

                //MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, dataTable.Rows.Count, dataTable, dataTableTemp, pLineNodeExTableTemp);
                for (int j = 0; j < arrMatchLineNode.Count; j++)
                {
                    modifyEntityIDTable.AddRow(arrMatchLineNode[j].LineNodeID, matchLineBegin.EntityID);

                    if (arrMatchLineNode[j].IsReverse != matchLineBegin.IsReverse)
                    {
                        //若方向不一致

                        //在另一个面中的对象也反向
                        if (arrMatchLineNode[j].OtherLineNodeID != -1)
                        {
                            if (arrMatchLineNode[j].OtherLineNodeID > arrMatchLineNode[j].LineNodeID)
                            {
                                //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                                reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                reverseLineNodeTable.AddRow(arrMatchLineNode[j].OtherLineNodeID);
                            }
                        }
                        else
                        {
                            //arrMatchLineNode[j].IsReverse = -arrMatchLineNode[j].IsReverse;
                            reverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                        }
                    }

                }
                arrMatchLineNode.Clear();

                if (n > 0)
                {
                    modifyEntityIDTable.Save(true);
                    reverseLineNodeTable.Save(true);
                    bNeedSave = true;
                }
                if (bNeedSave == true)
                {
                    //删除冗余记录
                    modifyEntityIDTable.DeleteSurplusRows();
                    reverseLineNodeTable.DeleteSurplusRows();

                    //更新标识码
                    string strCommand = "";
                    strCommand = "Update LineNodeEx,ModifyEntityID Set LineNodeEx.EntityID=ModifyEntityID.EntityID Where LineNodeEx.LineNodeID=ModifyEntityID.LineNodeID";
                    modifyEntityIDTable.ExecuteNonQuery(strCommand);

                    //更新反向标识
                    strCommand = "Update LineNodeEx Set IsReverse=-IsReverse Where LineNodeID In ( Select LineNodeID From ReverseLineNode )";//反向
                    reverseLineNodeTable.ExecuteNonQuery(strCommand);
                }
            }

            /*
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection, false);

                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                DataTable dataTable = pLineNodeExTable.GetRecords("", "", strLineNodeExOrderBy);

                string strLineNodeExOrderByTemp = pLineNodeExTable.FileName_LineNodeID;
                LineNodeExTable pLineNodeExTableTemp = new LineNodeExTable(m_pOleDbConnection, false);
                DataTable dataTableTemp = pLineNodeExTableTemp.GetRecords("", "", strLineNodeExOrderByTemp);

                LineNodeEx lineNodeUp = null;
                LineNodeEx lineNodeUpOther = null;
                LineNodeEx lineNodeCurrent = null;
                LineNodeEx lineNodeCurrentOther = null;
                LineNodeEx lineNodeBegin = null;
                LineNodeEx lineNodeBeginOther = null;
                bool bExistUp = false;
                int nIndexBegin = -1;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //获取当前线节点
                    lineNodeCurrent = new LineNodeEx();
                    //LineNode lineNodeTemp = lineNodeCurrent as LineNode;
                    pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);
                    if (lineNodeCurrent.OrtherIndexID > -1 && lineNodeCurrent.OrtherIndexID < dataTableTemp.Rows.Count)
                    {
                        lineNodeCurrentOther = new LineNodeEx();
                        //lineNodeTemp = lineNodeCurrentOther as LineNode;
                        pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeCurrent.OrtherIndexID], ref lineNodeCurrentOther, false);
                    }
                    else
                        lineNodeCurrentOther = null;


                    if (lineNodeBegin == null)
                    {
                        lineNodeBegin = lineNodeCurrent;
                        lineNodeBeginOther = lineNodeCurrentOther;
                        nIndexBegin = i;
                    }

                    if (i > 0)
                    {
                        //判断当前线节点与上一线节点是否属于同一个面
                        if (lineNodeCurrent.PolygonID == lineNodeUp.PolygonID)
                        {
                            //属于同一个面
                            if (bExistUp == true)
                            {
                                if (lineNodeCurrent.EntityID == 0)//合并处理必须在同一个环内处理
                                {
                                    //处理环内的首尾节点
                                    MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, i, dataTable, dataTableTemp, pLineNodeExTableTemp);

                                    bExistUp = false;
                                }
                                else if (lineNodeCurrent.IsFromLine == true)//当前线节点是引用线
                                {
                                    bExistUp = false;
                                }
                                else
                                {
                                    //相邻线节点处理

                                    //线所属的面必须一致
                                    if ((lineNodeCurrentOther != null && lineNodeUpOther != null && lineNodeCurrentOther.PolygonID == lineNodeUpOther.PolygonID)
                                        || (lineNodeCurrentOther == null && lineNodeUpOther == null && lineNodeCurrent.IsFromLine == false && lineNodeUp.IsFromLine == false))
                                    {
                                        //合并线
                                        lineNodeCurrent.EntityID = lineNodeUp.EntityID;
                                        pLineNodeExTable.SetLineNodeEntityID(dataTable.Rows[i], lineNodeUp.EntityID);

                                        if (lineNodeCurrent.IsReverse != lineNodeUp.IsReverse)
                                        {
                                            //若方向不一致
                                            //lineNodeCurrent.Reverse();
                                            pLineNodeExTable.ReverseLineNode(dataTable.Rows[i]);
                                            //在另一个面中的对象也反向
                                            //if (lineNodeCurrent.OtherPolygonLineNode != null)
                                            //    lineNodeCurrent.OtherPolygonLineNode.Reverse();
                                            if (lineNodeCurrentOther != null)
                                                pLineNodeExTableTemp.ReverseLineNode(dataTableTemp.Rows[lineNodeCurrent.OrtherIndexID]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bExistUp = true;
                            }


                        }
                        else
                        {
                            //不属于同一个面（处理首尾节点）
                            MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, i, dataTable, dataTableTemp, pLineNodeExTableTemp);


                            lineNodeBegin = lineNodeCurrent;
                            lineNodeBeginOther = lineNodeCurrentOther;
                            nIndexBegin = i;
                            bExistUp = true;
                        }
                    }
                    lineNodeUp = lineNodeCurrent;
                    lineNodeUpOther = lineNodeCurrentOther;

                    if ((i + 1) % 10000 == 0)
                    {
                        pLineNodeExTable.Save(false);
                        pLineNodeExTableTemp.Save(false);
                    }
                }

                MergePolygonLineEntityID(pLineNodeExTable, ref lineNodeBegin, lineNodeBeginOther, nIndexBegin, dataTable.Rows.Count, dataTable, dataTableTemp, pLineNodeExTableTemp);


                pLineNodeExTable.Save(true);
                pLineNodeExTableTemp.Save(true);
            }
             * */
        }

        /// <summary>
        /// 处理环内同一边界线分布在环的首尾的情况
        /// </summary>
        public void UpdatePolygonRing(ref List<LineNodeEx> arrLineNodeEx, int nIndexEnd, ModifyLineIndexTable pModifyLineIndexTable,ref int nMoveCount)
        {
            if (arrLineNodeEx.Count - nIndexEnd > 1 && 0 != nIndexEnd)
            {
                for (int j = arrLineNodeEx.Count - 1; j >= 0; j--)
                {
                    if (j > nIndexEnd)
                    {
                        //往前移动
                        //pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], -(nIndexEnd - nIndexBegin + 1));
                        pModifyLineIndexTable.AddRow(arrLineNodeEx[j].IndexID, arrLineNodeEx[j].LineIndex - (nIndexEnd + 1));
                    }
                    else
                    {
                        //往后移动
                        //pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], arrLineNodeEx.Count - 1 - nIndexEnd);
                        pModifyLineIndexTable.AddRow(arrLineNodeEx[j].IndexID, arrLineNodeEx[j].LineIndex + arrLineNodeEx.Count - 1 - nIndexEnd);
                    }
                }
                nMoveCount += arrLineNodeEx.Count;
            }

            arrLineNodeEx.Clear();
        }

        /// <summary>
        /// 处理环内同一边界线分布在环的首尾的情况
        /// </summary>
        public void UpdatePolygonRing(ref int nLineNodeExCount, int nLineNodeIDBegin, int nIndexEnd, ref List<string> arrWhere, ref List<string> arrValue)
        {
            if (nLineNodeExCount - nIndexEnd > 1 && 0 != nIndexEnd)
            {
                //for (int j = arrLineNodeEx.Count - 1; j >= 0; j--)
                //{
                //    if (j > nIndexEnd)
                //    {
                //        //往前移动
                //        //pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], -(nIndexEnd - nIndexBegin + 1));
                //        pModifyLineIndexTable.AddRow(arrLineNodeEx[j].IndexID, arrLineNodeEx[j].LineIndex - (nIndexEnd + 1));
                //    }
                //    else
                //    {
                //        //往后移动
                //        //pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], arrLineNodeEx.Count - 1 - nIndexEnd);
                //        pModifyLineIndexTable.AddRow(arrLineNodeEx[j].IndexID, arrLineNodeEx[j].LineIndex + arrLineNodeEx.Count - 1 - nIndexEnd);
                //    }
                //}
                //nMoveCount += arrLineNodeEx.Count;
                arrWhere.Add("LineNodeID>" + (nLineNodeIDBegin + nIndexEnd).ToString() + " And LineNodeID<" + (nLineNodeIDBegin + nLineNodeExCount).ToString());
                arrValue.Add("LineIndex-" + (1 + nIndexEnd).ToString());

                arrWhere.Add("LineNodeID<=" + (nLineNodeIDBegin + nIndexEnd).ToString() + " And LineNodeID>=" + nLineNodeIDBegin.ToString());
                arrValue.Add("LineIndex+" + (nLineNodeExCount - 1 - nIndexEnd).ToString());
            }
            nLineNodeExCount = 0;
            //arrLineNodeEx.Clear();
        }
        //public void UpdatePolygonRing(LineNodeExTable pLineNodeExTable, int nIndexBegin, int nIndexEnd, int nCount, DataTable dataTable)
        //{
        //    if (nCount - nIndexEnd > 1 && nIndexBegin != nIndexEnd)
        //    {
        //        for (int j = nCount - 1; j >= nIndexBegin; j--)
        //        {
        //            if (j > nIndexEnd)
        //            {
        //                pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], -(nIndexEnd - nIndexBegin + 1));
        //            }
        //            else
        //            {
        //                pLineNodeExTable.SetLineIndexChange(dataTable.Rows[j], nCount - 1 - nIndexEnd);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 处理环内同一边界线分布在环的首尾的情况
        /// </summary>
        public void UpdateRing()
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                //ModifyLineIndexTable pModifyLineIndexTable = new ModifyLineIndexTable(m_pOleDbConnection, true);
                //pModifyLineIndexTable.IsFirst = this.IsFirst;

                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);

                //LineNodeEx lineNodeBegin = null;//环的开始线节点
                int nLineNodeIDBegin = -1;
                int nEntityIDBegin = -1;
                //int nIndexBegin = -1;
                //bool bNeedSave = false;
                //bool bNeedMove = false;//是否需要处理
                int nIndexEnd = -1;    //环的结束索引（该索引以后的节点都要移到环的开始位置

                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                string strSelectFields = pLineNodeExTable.FieldName_PolygonID + ","
                    + pLineNodeExTable.FieldName_EntityID + ","
                    + pLineNodeExTable.FieldName_LineNodeID;
                DataTable dataTable = pLineNodeExTable.GetRecords(strSelectFields, "", "", strLineNodeExOrderBy);

                //LineNodeEx lineNodeUp = null;
                //LineNodeEx lineNodeCurrent = null;
                int nPolygonID = -1;
                int nLineNodeID = -1;
                int nEntityID = -1;
                int nPolygonIDUp = -1;
                int nLineNodeIDUp = -1;
                int nEntityIDUp = -1;

                List<string> arrWhere = new List<string>();
                List<string> arrValue = new List<string>();
                //List<LineNodeEx> arrLineNodeEx = new List<LineNodeEx>();
                int nLineNodeExCount = 0;
                //int n = 0;
                while (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //lineNodeCurrent = new LineNodeEx();
                        //pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);
                        nPolygonID = Convert.ToInt32(dataTable.Rows[i][0]);
                        nEntityID = Convert.ToInt32(dataTable.Rows[i][1]);
                        nLineNodeID = Convert.ToInt32(dataTable.Rows[i][2]);


                        if (nLineNodeIDBegin == -1)
                        {
                            nLineNodeIDBegin = nLineNodeID;
                            nEntityIDBegin = nEntityID;
                            //lineNodeBegin = lineNodeCurrent;
                            //nIndexBegin = 0;
                            nIndexEnd = 0;
                        }

                        if (nLineNodeIDUp != -1)
                        {
                            //判断当前线节点与上一线节点是否属于同一个面
                            if (nPolygonID == nPolygonIDUp)
                            {
                                if (nEntityID == 0)
                                {
                                    UpdatePolygonRing(ref nLineNodeExCount, nLineNodeIDBegin, nIndexEnd, ref arrWhere, ref arrValue);
                                    //UpdatePolygonRing(ref arrLineNodeEx, nIndexEnd, pModifyLineIndexTable, ref n);
                                    //arrLineNodeEx.Clear();
                                    nLineNodeIDBegin = -1;
                                }
                                else if (nEntityID != nEntityIDBegin)
                                {
                                    nIndexEnd = nLineNodeExCount;// arrLineNodeEx.Count;
                                }
                            }
                            else
                            {
                                UpdatePolygonRing(ref nLineNodeExCount, nLineNodeIDBegin, nIndexEnd, ref arrWhere, ref arrValue);
                                //UpdatePolygonRing(ref arrLineNodeEx, nIndexEnd, pModifyLineIndexTable, ref n);
                                //arrLineNodeEx.Clear();

                                nLineNodeIDBegin = nLineNodeID;
                                nEntityIDBegin = nEntityID;
                                //nIndexBegin = 0;
                                nIndexEnd = 0;
                            }
                        }
                        if (nEntityID != 0)
                        {
                            //arrLineNodeEx.Add(lineNodeCurrent);
                            nLineNodeExCount++;
                        }

                        nPolygonIDUp = nPolygonID;
                        nLineNodeIDUp = nLineNodeID;
                        nEntityIDUp = nEntityID;
                    }

                    //if (n >= pModifyLineIndexTable.MaxRecordCount)
                    //{
                    //    pModifyLineIndexTable.Save(true);
                    //    bNeedSave = true;
                    //    n = 0;
                    //}

                    if (dataTable.Rows.Count < pLineNodeExTable.MaxRecordCount)
                        break;
                    dataTable = pLineNodeExTable.GetNextRecords();

                }

                UpdatePolygonRing(ref nLineNodeExCount, nLineNodeIDBegin, nIndexEnd, ref arrWhere, ref arrValue);
                //UpdatePolygonRing(ref arrLineNodeEx, nIndexEnd, pModifyLineIndexTable, ref n);

                //if (n > 0)
                //{
                //    pModifyLineIndexTable.Save(true);
                //    bNeedSave = true;
                //}
                
                //更新线节点在面实体中的顺序号
                //if (bNeedSave == true)
                //{
                //    string strCommand = "Update LineNodeEx,ModifyLineIndex Set LineNodeEx.LineIndex=ModifyLineIndex.LineIndex Where LineNodeEx.LineNodeID=ModifyLineIndex.LineNodeID";
                //    pModifyLineIndexTable.ExecuteNonQuery(strCommand);
                //}
                string strCommand = "";
                for (int i = 0; i < arrWhere.Count; i++)
                {
                    strCommand = "Update LineNodeEx Set LineIndex=" + arrValue[i] + " Where " + arrWhere[i];
                    pLineNodeExTable.ExecuteNonQuery(strCommand);
                }
            }
        }

        /// <summary>
        /// 处理自闭合线问题（在不同面中的顺序不一致）
        /// </summary>
        public void UpdatePolygonClosedLineNode(ref List<LineNodeEx> arrLineNodeEx, ReverseLineNodeTable reverseLineNodeTable, ref int nReverseCount)
        {
            for (int i = 0; i < arrLineNodeEx.Count; i++)
            {
                reverseLineNodeTable.AddRow(arrLineNodeEx[i].IndexID);
                reverseLineNodeTable.AddRow(arrLineNodeEx[i].OrtherIndexID);
            }
            nReverseCount += arrLineNodeEx.Count * 2;
            arrLineNodeEx.Clear();
        }
        //public void UpdatePolygonClosedLineNode(LineNodeExTable pLineNodeExTable, int nBeginIndex, int nEndIndex/*, DataTable dataTable, DataTable dataTableTemp, LineNodeExTable pLineNodeExTableTemp*/)
        //{
            ////LineNode lineNodeTemp = null;
            //for (int j = nBeginIndex; j < nEndIndex; j++)
            //{
            //    LineNodeEx lineNode = new LineNodeEx();
            //    LineNodeEx lineNodeOther = null;
            //    //lineNodeTemp = lineNode as LineNode;
            //    pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[j], ref lineNode, false);
            //    if (lineNode.OrtherIndexID > -1 && lineNode.OrtherIndexID < dataTableTemp.Rows.Count)
            //    {
            //        lineNodeOther = new LineNodeEx();
            //        //lineNodeTemp = lineNodeOther as LineNode;
            //        pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNode.OrtherIndexID], ref lineNodeOther, false);
            //    }
            //    else
            //        lineNodeOther = null;

            //    pLineNodeExTable.ReverseLineNode(dataTable.Rows[j]);
            //    pLineNodeExTableTemp.ReverseLineNode(dataTableTemp.Rows[lineNode.OrtherIndexID]);
            //}
        //}

        /// <summary>
        /// 处理自闭合线问题（在不同面中的顺序不一致）
        /// </summary>
        public void UpdateClosedLineNode()
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                ReverseLineNodeTable reverseLineNodeTable = new ReverseLineNodeTable(m_pOleDbConnection, true, false);
                
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);
                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                string strWhere = pLineNodeExTable.FieldName_IsFromLine + "=-1 And " + pLineNodeExTable.FieldName_IsReverse + "=-1 And " + pLineNodeExTable.FieldName_OrtherLineNodeID + ">-1";
                DataTable dataTable = pLineNodeExTable.GetRecords(strWhere, "", strLineNodeExOrderBy);


                List<LineNodeEx> arrLineNodeEx = new List<LineNodeEx>();
                LineNodeEx lineNodeUp = null;
                LineNodeEx lineNodeCurrent = null;
                bool bNeedSave = false;
                int nReverseCount = 0;
                bool bAdded = false;
                while (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        lineNodeCurrent = new LineNodeEx();
                        pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);

                        bAdded = false;
                        //是否需要处理（方向未反向，标识码一致且同属于另一个面）
                        //if (lineNodeCurrent.IsReverse == false)
                        //{
                            //if (lineNodeCurrent.OrtherIndexID > -1)
                            //{
                                if (lineNodeUp == null || (lineNodeCurrent.EntityID == lineNodeUp.EntityID))
                                {
                                    arrLineNodeEx.Add(lineNodeCurrent);
                                    bAdded = true;
                                }
                            //}
                        //}

                        if (bAdded == true)
                        {
                            if (lineNodeUp != null)
                            {
                                //判断当前线节点与上一线节点是否属于同一个面
                                if (lineNodeCurrent.PolygonID != lineNodeUp.PolygonID)
                                {
                                    UpdatePolygonClosedLineNode(ref arrLineNodeEx, reverseLineNodeTable, ref nReverseCount);
                                }
                            }
                        }
                        else
                        {
                            arrLineNodeEx.Clear();
                        }
                        
                        lineNodeUp = lineNodeCurrent;
                    }

                    if (nReverseCount > reverseLineNodeTable.MaxRecordCount)
                    {
                        reverseLineNodeTable.Save(true);
                        bNeedSave = true;
                        nReverseCount = 0;
                    }

                    if (dataTable.Rows.Count < pLineNodeExTable.MaxRecordCount)
                        break;
                    dataTable = pLineNodeExTable.GetNextRecords();

                }

                UpdatePolygonClosedLineNode(ref arrLineNodeEx, reverseLineNodeTable, ref nReverseCount);
                if (nReverseCount > 0)
                {
                    reverseLineNodeTable.Save(true);
                    bNeedSave = true;
                }

                if (bNeedSave == true)
                {
                    //删除冗余记录
                    reverseLineNodeTable.DeleteSurplusRows();

                    //更新反向标识
                    string strCommand = "Update LineNodeEx Set IsReverse=0-IsReverse Where LineNodeID In ( Select LineNodeID From ReverseLineNode )";//反向
                    reverseLineNodeTable.ExecuteNonQuery(strCommand);
                }
            }
        }

        ///// <summary>
        ///// 处理线段在一个面中连续，另一个面中不连续的情况
        ///// </summary>
        //public void SplitPolygonLineNode(LineNodeExTable pLineNodeExTable,List<int> arrSplitEntityID, int nBeginPolygonIndex, int nEndPolygonIndex,
        //    DataTable dataTable, DataTable dataTableTemp, LineNodeExTable pLineNodeExTableTemp)
        //{

        //    LineNodeEx lineNodeK = null;
        //    LineNodeEx lineNodeKOther = null;
        //    //LineNode lineNodeTemp = null;
        //    for (int j = 0; j < arrSplitEntityID.Count; j++)
        //    {
        //        for (int k = nBeginPolygonIndex; k < nEndPolygonIndex; k++)
        //        {
        //            lineNodeK = new LineNodeEx();
        //            //lineNodeTemp = lineNodeK as LineNode;
        //            pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[k], ref lineNodeK, false);
        //            if (lineNodeK.OrtherIndexID > -1 && lineNodeK.OrtherIndexID < dataTableTemp.Rows.Count)
        //            {
        //                lineNodeKOther = new LineNodeEx();
        //                //lineNodeTemp = lineNodeKOther as LineNode;
        //                pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeK.OrtherIndexID], ref lineNodeKOther, false);
        //            }
        //            else
        //                lineNodeKOther = null;

        //            if (lineNodeKOther != null)
        //            {
        //                if (arrSplitEntityID[j] == lineNodeKOther.EntityID)
        //                {
        //                    if (lineNodeKOther.IsReverse == false)
        //                    {
        //                        //lineNodeKOther.Reverse();
        //                        pLineNodeExTableTemp.ReverseLineNode(dataTableTemp.Rows[lineNodeK.OrtherIndexID]);
        //                        //lineNodeK.Reverse();
        //                        pLineNodeExTable.ReverseLineNode(dataTable.Rows[k]);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //}

        /// <summary>
        /// 处理线段在一个面中连续，另一个面中不连续的情况
        /// </summary>
        public void SplitLineNode()
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                ReverseLineNodeTable pReverseLineNodeTable = new ReverseLineNodeTable(m_pOleDbConnection, true, false);

                MatchLineNodeExView pMatchLineNodeView = new MatchLineNodeExView(m_pOleDbConnection);
                DataTable dataViewLineNodeEx = pMatchLineNodeView.GetRecords("", "", "");

                MatchLineNodeEx matchLineNodeUp = null;
                MatchLineNodeEx matchLineNode = null;

                List<MatchLineNodeEx> arrMatchLineNode = new List<MatchLineNodeEx>();
                List<int> arrAllEntityID = new List<int>();
                List<int> arrSplitEntityID = new List<int>();
                int nReverseCount = 0;
                bool bNeedSave = false;
                while (dataViewLineNodeEx.Rows.Count > 0)
                {
                    for (int i = 0; i < dataViewLineNodeEx.Rows.Count; i++)
                    {
                        matchLineNode = new MatchLineNodeEx();
                        pMatchLineNodeView.GetLineByDataRow(dataViewLineNodeEx.Rows[i], ref matchLineNode);

                        if (matchLineNode.OtherEntityID > 0 && matchLineNode.IsReverse == -1)
                        {
                            arrMatchLineNode.Add(matchLineNode);
                        }

                        if (matchLineNodeUp != null)
                        {
                            //判断当前线节点与上一线节点是否属于同一个面
                            if (matchLineNode.PolygonID != matchLineNodeUp.PolygonID)
                            {
                                if (arrSplitEntityID.Count > 0)
                                {
                                    for (int j = 0; j < arrMatchLineNode.Count; j++)
                                    {
                                        if (arrSplitEntityID.Contains(matchLineNode.EntityID) == true)
                                        {
                                            pReverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                                            pReverseLineNodeTable.AddRow(arrMatchLineNode[j].OtherLineNodeID);
                                            nReverseCount++;
                                        }
                                    }
                                }
                                arrMatchLineNode.Clear();
                                arrAllEntityID.Clear();
                                arrSplitEntityID.Clear();
                            }
                            else
                            {
                                if (matchLineNode.OtherEntityID != matchLineNodeUp.OtherEntityID)
                                {
                                    if (matchLineNode.OtherEntityID > 0 && matchLineNode.IsReverse == -1)
                                    {
                                        if (arrAllEntityID.Contains(matchLineNode.OtherEntityID) == false)
                                        {
                                            arrAllEntityID.Add(matchLineNode.OtherEntityID);
                                        }
                                        else
                                        {
                                            arrSplitEntityID.Add(matchLineNode.OtherEntityID);
                                        }
                                    }
                                }
                            }
                        }

                        if (arrAllEntityID.Count == 0)
                            arrAllEntityID.Add(matchLineNode.OtherEntityID);
                       
                        matchLineNodeUp = matchLineNode;
                    }
                    if (nReverseCount > pReverseLineNodeTable.MaxRecordCount)
                    {
                        pReverseLineNodeTable.Save(true);
                        nReverseCount = 0;
                        bNeedSave = true;
                    }

                    if (dataViewLineNodeEx.Rows.Count < pMatchLineNodeView.MaxRecordCount)
                        break;
                    dataViewLineNodeEx = pMatchLineNodeView.GetNextRecords();
                }

                if (arrSplitEntityID.Count > 0)
                {
                    for (int j = 0; j < arrMatchLineNode.Count; j++)
                    {
                        if (arrSplitEntityID.Contains(matchLineNode.EntityID) == true)
                        {
                            pReverseLineNodeTable.AddRow(arrMatchLineNode[j].LineNodeID);
                            pReverseLineNodeTable.AddRow(arrMatchLineNode[j].OtherLineNodeID);
                            nReverseCount++;
                        }
                    }
                }
                arrMatchLineNode.Clear(); 
                arrAllEntityID.Clear();
                arrSplitEntityID.Clear();

                if (nReverseCount > 0)
                {
                    pReverseLineNodeTable.Save(true);
                    bNeedSave = true;
                }

                if (bNeedSave == true)
                {
                    //删除冗余记录
                    pReverseLineNodeTable.DeleteSurplusRows();

                    //更新反向标识
                    string strCommand = "Update LineNodeEx Set IsReverse=0-IsReverse Where LineNodeID In ( Select LineNodeID From ReverseLineNode )";//反向
                    pReverseLineNodeTable.ExecuteNonQuery(strCommand);
                }
                //LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection, false);

                //string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                //DataTable dataTable = pLineNodeExTable.GetRecords("", "", strLineNodeExOrderBy);

            //    //string strLineNodeExOrderByTemp = pLineNodeExTable.FileName_LineNodeID;
            //    //LineNodeExTable pLineNodeExTableTemp = new LineNodeExTable(m_pOleDbConnection, false);
            //    //DataTable dataTableTemp = pLineNodeExTableTemp.GetRecords("", "", strLineNodeExOrderByTemp);

            //    LineNodeEx lineNodeUp = null;
            //    //LineNodeEx lineNodeUpOther = null;
            //    LineNodeEx lineNodeCurrent = null;
            //    //LineNodeEx lineNodeCurrentOther = null;

            //    List<int> arrAllEntityID = new List<int>();
            //    List<int> arrSplitEntityID = new List<int>();
            //    int nBeginPolygonIndex = 0;
            //    for (int i = 0; i < dataTable.Rows.Count; i++)
            //    {
            //        lineNodeCurrent = new LineNodeEx();
            //        pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);
            //        //if (lineNodeCurrent.OrtherIndexID > -1 && lineNodeCurrent.OrtherIndexID < dataTableTemp.Rows.Count)
            //        //{
            //        //    lineNodeCurrentOther = new LineNodeEx();
            //        //    pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeCurrent.OrtherIndexID], ref lineNodeCurrentOther, false);
            //        //}
            //        //else
            //        //    lineNodeCurrentOther = null;

            //        if (i == 0)
            //        {
            //            //if(lineNodeCurrent.OrtherIndexID!=-1)
            //            //arrAllEntityID.Add(lineNodeCurrentOther.EntityID);
            //        }

            //        if (i > 0)
            //        {
            //            //判断当前线节点与上一线节点是否属于同一个面
            //            if (lineNodeCurrent.PolygonID != lineNodeUp.PolygonID)
            //            {
            //                SplitPolygonLineNode(pLineNodeExTable, arrSplitEntityID, nBeginPolygonIndex, i, dataTable, dataTableTemp, pLineNodeExTableTemp);

            //                nBeginPolygonIndex = i;
            //                arrAllEntityID.Clear();
            //                arrSplitEntityID.Clear();
            //            }
            //            else
            //            {
            //                if (lineNodeCurrent.OrtherIndexID != -1)
            //                {

            //                    if (lineNodeUp.OrtherIndexID == -1 ||
            //                        lineNodeCurrentOther.EntityID != lineNodeUpOther.EntityID)
            //                    {
            //                        if (arrAllEntityID.Contains(lineNodeCurrentOther.EntityID) == false)
            //                        {
            //                            arrAllEntityID.Add(lineNodeCurrentOther.EntityID);
            //                        }
            //                        else
            //                        {
            //                            arrSplitEntityID.Add(lineNodeCurrentOther.EntityID);
            //                        }
            //                    }
            //                }

            //            }
            //        }

            //        if ((i + 1) % 10000 == 0)
            //        {
            //            pLineNodeExTable.Save(false);
            //            pLineNodeExTableTemp.Save(false);
            //        }

            //        lineNodeUp = lineNodeCurrent;
            //        //lineNodeUpOther = lineNodeCurrentOther;
            //    }

            //    SplitPolygonLineNode(pLineNodeExTable, arrSplitEntityID, nBeginPolygonIndex, dataTable.Rows.Count, dataTable, dataTableTemp, pLineNodeExTableTemp);

            //    pLineNodeExTable.Save(true);
            //    pLineNodeExTableTemp.Save(true);
            }
        }

        /// <summary>
        /// 更新已反向的线节点的标识码（非引用线）
        /// </summary>
        public void UpdateReverseLineEntityID()
        {
            /*
             *  Update LineNodeEx a,LineNodeEx b Set a.EntityID=b.EntityID Where a.IsReverse='1' and b.OrtherIndexID=a.LineNodeID
             * */
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);
                string strCommand = "Update LineNodeEx a,LineNodeEx b Set a.EntityID=b.EntityID Where a.IsReverse=1 and b.OrtherLineNodeID=a.LineNodeID";// and a.OrtherLineNodeID=b.LineNodeID
                pLineNodeExTable.ExecuteNonQuery(strCommand);


                //string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                //DataTable dataTable = pLineNodeExTable.GetRecords("", "", strLineNodeExOrderBy);

                //string strLineNodeExOrderByTemp = pLineNodeExTable.FileName_LineNodeID;
                //LineNodeExTable pLineNodeExTableTemp = new LineNodeExTable(m_pOleDbConnection, false);
                //DataTable dataTableTemp = pLineNodeExTableTemp.GetRecords("", "", strLineNodeExOrderByTemp);

                //LineNodeEx lineNodeCurrent = null;
                //LineNodeEx lineNodeCurrentOther = null;

                //for (int i = 0; i < dataTable.Rows.Count; i++)
                //{
                //    lineNodeCurrent = new LineNodeEx();
                //    //LineNode lineNodeTemp = lineNodeCurrent as LineNode;
                //    pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);
                //    if (lineNodeCurrent.OrtherIndexID > -1 && lineNodeCurrent.OrtherIndexID < dataTableTemp.Rows.Count)
                //    {
                //        lineNodeCurrentOther = new LineNodeEx();
                //        //lineNodeTemp = lineNodeCurrentOther as LineNode;
                //        pLineNodeExTableTemp.GetLineNodeByDataRow(dataTableTemp.Rows[lineNodeCurrent.OrtherIndexID], ref lineNodeCurrentOther, false);
                //    }
                //    else
                //        lineNodeCurrentOther = null;

                //    if (lineNodeCurrent.IsReverse)
                //    {
                //        if (lineNodeCurrentOther != null)
                //        {
                //            pLineNodeExTable.SetLineNodeEntityID(dataTable.Rows[i], lineNodeCurrentOther.EntityID);
                //        }
                //    }

                //    if ((i + 1) % 10000 == 0)
                //        pLineNodeExTable.Save(false);
                //}

                //pLineNodeExTable.Save(true);
            }
        }

        /// <summary>
        /// 写入多边形线节点（非引用线图层）
        /// </summary>
        /// <param name="pVCTFile">VCT文件对象</param>
        public void WritePolygonLineNodes(VCTFile pVCTFile)
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);

                //只写入正向线节点
                string strWhere = pLineNodeExTable.FieldName_IsFromLine + "=-1 And " 
                    + pLineNodeExTable.FieldName_IsReverse + "=-1 And " + pLineNodeExTable.FieldName_EntityID + "<>0";
                //按标识码、在面的边界线集合中的索引排序
                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                DataTable dataTable = pLineNodeExTable.GetRecords(strWhere, "", strLineNodeExOrderBy);

                LineNodeEx lineNode = null;
                LineNodeEx lineNodeCurrent = null;
                LineNodeEx lineNodeUp = null;

                //for (int i = 0; i < dataTable.Rows.Count; i++)
                while (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {

                        lineNodeCurrent = new LineNodeEx();
                        //LineNode lineNodeTemp = lineNodeCurrent as LineNode;
                        pLineNodeExTable.GetLineNodeByDataRow(dataTable.Rows[i], ref lineNodeCurrent, false);

                        if (lineNode == null)
                        {
                            lineNode = lineNodeCurrent;
                        }
                        else
                        {
                            //判断当前线节点与上一线节点是否属于同一个面
                            if (lineNodeCurrent.PolygonID != lineNodeUp.PolygonID)
                            {
                                pVCTFile.WritePolygonLineNode(lineNode);
                                lineNode = lineNodeCurrent;
                            }
                            else
                            {
                                if (lineNodeCurrent.EntityID == lineNode.EntityID)
                                {
                                    lineNode.SegmentNodes.AddRange(lineNodeCurrent.SegmentNodes);
                                }
                                else
                                {
                                    pVCTFile.WritePolygonLineNode(lineNode);
                                    lineNode = lineNodeCurrent;
                                }
                            }
                        }
                        lineNodeUp = lineNodeCurrent;
                    }

                    if (dataTable.Rows.Count < pLineNodeExTable.MaxRecordCount)
                        break;
                    dataTable = pLineNodeExTable.GetNextRecords();

                }
                if (lineNode != null)
                    pVCTFile.WritePolygonLineNode(lineNode);

            }
        }

        /// <summary>
        /// 写入多边形节点
        /// </summary>
        /// <param name="pVCTFile">VCT文件对象</param>
        public void WritePolygonNodes(VCTFile pVCTFile)
        {
            if (this.m_pOleDbConnection != null && m_pOleDbConnection.State == ConnectionState.Open)
            {
                PolygonNodeTable pPolygonNodeTable = new PolygonNodeTable(m_pOleDbConnection);
                LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pOleDbConnection);

                //按面标识码
                string strPolygonNodeOrderBy = pPolygonNodeTable.FieldName_EntityID;
                DataTable dataTablePolygon = pPolygonNodeTable.GetRecords("", "", strPolygonNodeOrderBy);

                //按面标识码、在面的边界线集合中的索引排序
                string strLineNodeExOrderBy = pLineNodeExTable.FieldName_PolygonID + "," + pLineNodeExTable.FieldName_LineIndex;
                DataTable dataTableLine = pLineNodeExTable.GetRecords(pLineNodeExTable.FieldName_PolygonID + ",-" + pLineNodeExTable.FieldName_EntityID + "*" + pLineNodeExTable.FieldName_IsReverse, "", "", strLineNodeExOrderBy);

                PolygonNodeSimple polygonNode = null;
                //LineNodeEx lineNode = null;
                //LineNodeEx lineNodeUp = null;
                int nPolygonID = -1;
                int nEntityID = -1;
                int nEntityIDUp = -1;
                int j = 0;
                while (dataTablePolygon.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTablePolygon.Rows.Count; i++)
                    {
                        polygonNode = new PolygonNodeSimple();
                        pPolygonNodeTable.GetEntityNodeByDataRow(dataTablePolygon.Rows[i], ref polygonNode);

                        while (dataTableLine.Rows.Count > 0)
                        {
                            for (; j < dataTableLine.Rows.Count; j++)
                            {
                                //lineNodeUp = null;
                                //lineNode = new LineNodeEx();
                                //pLineNodeExTable.GetLineNodeByDataRow(dataTableLine.Rows[j], ref lineNode, false);
                                nPolygonID = Convert.ToInt32(dataTableLine.Rows[j][0]);
                                nEntityID = Convert.ToInt32(dataTableLine.Rows[j][1]);

                                if (nPolygonID == polygonNode.EntityID)
                                {
                                    if (nEntityIDUp != -1)
                                    {
                                        if (nEntityIDUp == nEntityID)
                                        {
                                            continue;
                                        }
                                    }
                                    polygonNode.LineNodes.Add(nEntityID);
                                }
                                else
                                {
                                    break;
                                }
                                nEntityIDUp = nEntityID;
                            }

                            if (j == dataTableLine.Rows.Count)
                            {
                                if (dataTableLine.Rows.Count < pLineNodeExTable.MaxRecordCount)
                                    break;
                                dataTableLine = pLineNodeExTable.GetNextRecords();
                                j = 0;
                            }
                            else
                                break;
                        }

                        pVCTFile.WritePolygonNode(polygonNode);
                    }
                    if (dataTablePolygon.Rows.Count < pPolygonNodeTable.MaxRecordCount)
                        break;
                    dataTablePolygon = pPolygonNodeTable.GetNextRecords();

                }

            }
        }
    }
}
