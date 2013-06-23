using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIST.DGP.DataExchange.VCT.Metadata;
using DIST.DGP.DataExchange.VCT.TempData;
using System.ComponentModel;
using System.Data;

namespace DIST.DGP.DataExchange.VCT
{
    //public delegate void WriteCommpleteEventHandler(bool IsSuccessful);

    public class WritePolygonNodes
    {
        //public event WriteCommpleteEventHandler WriteCommplete;

        private TempData.TempFile m_pTempFile;
        /// <summary>
        /// VCT数据文件对象
        /// </summary>
        private VCTFile m_VCTFile;

        /// <summary>
        /// 获取新的实体标识码
        /// </summary>
        private int m_nNewEntityID = 1;

        private int m_nLayerIndex = 0;

        private int m_nLayerCount = 0;

        private ESRIData.Dataset m_dataset;

        private bool m_bIsFirst = true;

        DateTime timeBeginPolygon;
        DateTime timeBeginLine;


        public WritePolygonNodes(ESRIData.Dataset dataset,TempData.TempFile pTempFile, VCTFile pVCTFile, int nNewEntityID)
        {
            m_dataset = dataset;
            m_pTempFile = pTempFile;
            m_VCTFile = pVCTFile;
            m_nNewEntityID = nNewEntityID;
            m_nLayerCount = m_dataset.GetLayerCount();

            //WriteCommplete = null;
        }



        /// <summary>
        /// 保存面图层对应的线图层中的实体到临时数据文件
        /// </summary>
        private void SaveLineNodesToTemp(LineNodeTable pLineNodeTable,string strPolygonTableName)
        {
            //暂时不考虑面与面之间的关联
            MetaTable metaTable = MetaDataFile.GetMetaTalbleByName(strPolygonTableName);
            List<string> arrFeatureCode = null;
            if (metaTable != null)
                arrFeatureCode = metaTable.LinkFeatureCode;
            if (arrFeatureCode != null && arrFeatureCode.Count > 0)
            {
                ESRIData.FeatureLayer featureLayer;
                int m = 1;
                for (int i = 0; i < m_nLayerCount; i++)
                {
                    //获取图层对象
                    featureLayer = m_dataset.GetFeatureLayerByIndex(i) as ESRIData.FeatureLayer;
                    ESRIData.LineLayer lineLayer = featureLayer as ESRIData.LineLayer;
                    if (lineLayer != null)
                    {
                        if (arrFeatureCode.Contains(lineLayer.FeatureCode) == true)
                        {
                            List<ESRIData.FeatureEntity> arrFeatureEntity = lineLayer.FeatureEntys;
                            if (arrFeatureEntity == null) return;
                            for (int j = 0; j < arrFeatureEntity.Count; j++)
                            {
                                FileData.LineNode lineNode = arrFeatureEntity[j].GetEntityNode() as FileData.LineNode;
                                if (lineNode != null)
                                {
                                    //拆分线：多点一线拆为两点一线
                                    for (int k = 0; k < lineNode.SegmentNodes.Count; k++)
                                    {
                                        FileData.BrokenLineNode brokenLineNode = lineNode.SegmentNodes[k] as FileData.BrokenLineNode;
                                        if (brokenLineNode != null)
                                        {
                                            for (int n = 1; n < brokenLineNode.PointInfoNodes.Count; n++)
                                            {
                                                FileData.PointInfoNodes pointInfoNodes = new FileData.PointInfoNodes();
                                                pointInfoNodes.Add(brokenLineNode.PointInfoNodes[n - 1]);
                                                pointInfoNodes.Add(brokenLineNode.PointInfoNodes[n]);

                                                FileData.BrokenLineNode brokenLineNodeNew = new FileData.BrokenLineNode();
                                                brokenLineNodeNew.PointInfoNodes = pointInfoNodes;

                                                FileData.SegmentNodes segmentNodes = new FileData.SegmentNodes();
                                                segmentNodes.Add(brokenLineNodeNew);

                                                FileData.LineNode lineNodeNew = new FileData.LineNode();
                                                lineNodeNew.SegmentNodes = segmentNodes;

                                                lineNodeNew.EntityID = lineNode.EntityID;
                                                lineNodeNew.FeatureCode = lineNode.FeatureCode;
                                                lineNodeNew.LineType = lineNode.LineType;
                                                lineNodeNew.Representation = lineNode.Representation;

                                                //arrLineNode.Add(lineNodeNew);
                                                //写入临时数据文件
                                                //if (m_pTempFile.LineNodeExs != null)
                                                //{
                                                pLineNodeTable.AddRow(lineNodeNew);
                                                //}

                                                if (m % pLineNodeTable.MaxRecordCount == 0)
                                                    pLineNodeTable.Save(true);
                                                m++;
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                }

                pLineNodeTable.Save(true);

            }
            //return arrLineNode;
        }

        //private bool IsFilishWritePolygon = false;
        //private void WritePolygonCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    TimeSpan span = DateTime.Now - this.timeBeginPolygon;
        //    LogAPI.WriteLog("写入构面线临时数据耗时" + span.ToString());

        //    if (IsFilishWritePolygon == false)
        //        IsFilishWritePolygon = true;
        //    if (IsFilishWriteLine == true)
        //        WritePolygonLineCompleted();

        //}

        //private bool IsFilishWriteLine = false;
        //private void WriteLineCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    TimeSpan span = DateTime.Now - this.timeBeginLine;
        //    LogAPI.WriteLog("写入引用线临时数据耗时" + span.ToString());
        //    if (IsFilishWriteLine == false)
        //        IsFilishWriteLine = true;
        //    if (IsFilishWritePolygon == true)
        //        WritePolygonLineCompleted();
        //}

        //写入面层和线层历史数据异步操作完成回调函数        
        private void WritePolygonLineCompleted()
        {
            //匹配面实体中的构面线和线图层中的线实体
            DateTime dateTime1 = DateTime.Now;
            m_pTempFile.MatchPolygonToLine();
            DateTime dateTime2 = DateTime.Now;
            TimeSpan span = dateTime2 - dateTime1;
            //MessageBox.Show(span.ToString());
            Logger.WriteLog("匹配面实体中的构面线和线图层中的线实体耗时" + span.ToString());



            //处理面图层中构面线之间的关系
            dateTime1 = DateTime.Now;
            int nMaxEntityID = this.m_nNewEntityID;
            m_pTempFile.MatchPolygonLine(/*ref this.m_nNewEntityID*/);
            dateTime2 = DateTime.Now;
            span = dateTime2 - dateTime1;
            Logger.WriteLog("处理面图层中构面线之间的关系耗时" + span.ToString());
            //合并线节点
            dateTime1 = DateTime.Now;
            m_pTempFile.MergeLineEntityID(ref this.m_nNewEntityID);
            dateTime2 = DateTime.Now;
            span = dateTime2 - dateTime1;
            Logger.WriteLog("合并线节点耗时" + span.ToString());
            //处理环线中首尾线段
            dateTime1 = DateTime.Now;
            m_pTempFile.UpdateRing();
            dateTime2 = DateTime.Now;
            span = dateTime2 - dateTime1;
            Logger.WriteLog("处理环线中首尾线段耗时" + span.ToString());

            //处理环线中的自闭合线
            dateTime1 = DateTime.Now;
            m_pTempFile.UpdateClosedLineNode();
            dateTime2 = DateTime.Now;
            span = dateTime2 - dateTime1;
            Logger.WriteLog("处理环线中的自闭合线耗时" + span.ToString());

            if (nMaxEntityID > 0)
            {
                //处理线段在一个面中连续，另一个面中不连续的情况
                dateTime1 = DateTime.Now;
                m_pTempFile.SplitLineNode();
                dateTime2 = DateTime.Now;
                span = dateTime2 - dateTime1;
                Logger.WriteLog("处理线段在一个面中连续，另一个面中不连续的情况耗时" + span.ToString());
                //更新反向线的索引
                dateTime1 = DateTime.Now;
                m_pTempFile.UpdateReverseLineEntityID();
                dateTime2 = DateTime.Now;
                span = dateTime2 - dateTime1;
                Logger.WriteLog("更新反向线的索引耗时" + span.ToString());
                //写入节点
                dateTime1 = DateTime.Now;
                m_pTempFile.WritePolygonLineNodes(m_VCTFile);
                dateTime2 = DateTime.Now;
                span = dateTime2 - dateTime1;
                Logger.WriteLog("写入面图层的线节点节点耗时" + span.ToString());
            }

            //写入面实体节点
            dateTime1 = DateTime.Now;
            m_pTempFile.WritePolygonNodes(m_VCTFile);
            dateTime2 = DateTime.Now;
            span = dateTime2 - dateTime1;
            Logger.WriteLog("写入面节点节点耗时" + span.ToString());


            this.m_nLayerIndex++;

            Write();
        }

        /// <summary>
        /// 写入面节点
        /// </summary>
        public void Write()
        {
            if (m_pTempFile.DbConnection != null && m_pTempFile.DbConnection.State == ConnectionState.Open)
            {
                ESRIData.FeatureLayer featureLayer;
                while (this.m_nLayerIndex < m_nLayerCount)
                {
                    //获取图层对象
                    featureLayer = m_dataset.GetFeatureLayerByIndex(this.m_nLayerIndex) as ESRIData.FeatureLayer;
                    ESRIData.PolygonLayer polygonLayer = featureLayer as ESRIData.PolygonLayer;
                    if (polygonLayer != null)
                    {
                        List<ESRIData.FeatureEntity> arrFeatureEntity = polygonLayer.FeatureEntys;
                        if (arrFeatureEntity!=null&&arrFeatureEntity.Count > 0)
                        {
                            m_pTempFile.IsFirst = m_bIsFirst;

                            ////清除临时数据
                            PolygonNodeTable pPolygonNodeTable = new PolygonNodeTable(m_pTempFile.DbConnection, true, m_bIsFirst);
                            LineNodeExTable pLineNodeExTable = new LineNodeExTable(m_pTempFile.DbConnection, true, m_bIsFirst);
                            LineNodeTable pLineNodeTable = new LineNodeTable(m_pTempFile.DbConnection, true, m_bIsFirst);


                            ///处理每个图层下的所有要素
                            //using (BackgroundWorker workerPolygon = new BackgroundWorker())
                            //{
                            //    workerPolygon.DoWork += delegate
                            //    {
                                    timeBeginPolygon = DateTime.Now;

                                    int j = 1, n = 1;
                                    FileData.PolygonNode polygonNode = null;
                                    while (arrFeatureEntity.Count > 0)
                                    {
                                        polygonNode = arrFeatureEntity[0].GetEntityNode() as FileData.PolygonNode;
                                        if (polygonNode != null)
                                        {
                                            pPolygonNodeTable.AddRow(polygonNode);

                                            for (int k = 0; k < polygonNode.LineNodes.Count; k++)
                                            {
                                                polygonNode.LineNodes[k].LineIndex = k;
                                                pLineNodeExTable.AddRow(polygonNode.LineNodes[k]);
                                                if (n % pLineNodeExTable.MaxRecordCount == 0)
                                                    pLineNodeExTable.Save(true);
                                                n++;
                                            }
                                        }

                                        //m_pTempFile.PolygonNodes.AddRow(polygonNode);
                                        if (j % pPolygonNodeTable.MaxRecordCount == 0 || 1 == arrFeatureEntity.Count)
                                        {
                                            //if (j == arrFeatureEntity.Count)
                                            pLineNodeExTable.Save(true);
                                            pPolygonNodeTable.Save(true);
                                        }
                                        arrFeatureEntity.RemoveAt(0);
                                        j++;
                                    }
                                    TimeSpan span = DateTime.Now - this.timeBeginPolygon;
                                    Logger.WriteLog("写入构面线临时数据耗时" + span.ToString());

                            //    };

                            //    workerPolygon.RunWorkerCompleted += WritePolygonCompleted;

                            //    workerPolygon.RunWorkerAsync();
                            //}


                            //保存关联的线图层中的线结点到临时图层/*
                            //using (BackgroundWorker workerLine = new BackgroundWorker())
                            //{
                            timeBeginLine = DateTime.Now;
                            //    workerLine.DoWork += delegate
                            //    {
                            SaveLineNodesToTemp(pLineNodeTable, featureLayer.StructureNode.TableName);
                            span = DateTime.Now - this.timeBeginLine;
                            Logger.WriteLog("写入引用线临时数据耗时" + span.ToString());

                            //    };

                            //    workerLine.RunWorkerCompleted += WriteLineCompleted;

                            //    workerLine.RunWorkerAsync();

                            //};

                            WritePolygonLineCompleted();

                            if (m_bIsFirst == true)
                                m_bIsFirst = false;
                        }
                        break;
                    }
                    else
                        this.m_nLayerIndex++;
                }
                //if (this.m_nLayerIndex == m_nLayerCount)
                //{
                //    m_pTempFile.Close();
                //    if (WriteCommplete != null)
                //        WriteCommplete(true);
                //}
            }
        }
    }
}
