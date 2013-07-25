using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Hy.Check.Define;


namespace Hy.Check.Rule
{
    /// <summary>
    /// 接边检测
    /// </summary>
    public class RuleJoinSide : BaseRule
    {
        private string m_strBufferLayer;
        private RuleExpression.LRJoinSidePara m_pPara;
        private int m_nGeoType;
        private List<RuleExpression.JoinSideInfo> m_aryResult = new List<RuleExpression.JoinSideInfo>();
        private IArray m_aryInErrFeatures;
        private string strSrcLayer;
        private string strBoundLayer;

        public RuleJoinSide()
        {
        }

        public override string Name
        {
            get { return "接边检测"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            m_pPara = new RuleExpression.LRJoinSidePara();

            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();
            m_pPara.dLimit = pParameter.ReadDouble();

            //解析字符串
            int nStrSize = nCount1 - sizeof(double) - sizeof(int);
            Byte[] bb = new byte[nStrSize];
            pParameter.Read(bb, 0, nStrSize);
            string para_str = Encoding.Default.GetString(bb);

            para_str.Trim();
            string[] strResult = para_str.Split('|');

            m_pPara.arrayFieldName = new List<string>();
            for (int k = 0; k < strResult.Length; k++)
            {
                switch (k)
                {
                    case 0:
                        m_pPara.strAlias = strResult[k];
                        break;
                    case 1:
                        m_pPara.strRemark = strResult[k];
                        break;
                    case 2:
                        m_pPara.strFeatureLayer = strResult[k];
                        break;
                    case 3:
                        m_pPara.strBoundLayer = strResult[k];
                        break;
                    case 4:
                        m_pPara.strInciseField = strResult[k];
                        break;
                    case 5:
                        m_pPara.strStdName = strResult[k];
                        break;
                    default:
                        m_pPara.arrayFieldName.Add(strResult[k]);
                        break;
                }
            }
        }

        public override bool Verify()
        {
            if (m_pPara == null)
            {
                return false;
            }
            if (this.m_BaseWorkspace == null)
                return false;

            // 获取源图层名称
            strSrcLayer = base.GetLayerName(m_pPara.strFeatureLayer);
            strBoundLayer = base.GetLayerName(m_pPara.strBoundLayer);
            if (!(m_BaseWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strSrcLayer))
            {
                SendMessage(enumMessageType.RuleError, "当前工作数据库的接边图层" + strSrcLayer + "不存在!");
                return false;
            }

            // 打开范围图层
            if (!(m_BaseWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strBoundLayer))
            {
                SendMessage(enumMessageType.RuleError, "当前工作数据库的接边图层" + strBoundLayer + "不存在!");
                return false;
            }

            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            IFeatureClass ipSrcFeatClass=null; //接边图层要素
            IFeatureClass ipBoundFeatClass=null; //范围图层要素
            try
            {
                IFeatureWorkspace ipFtWS = (IFeatureWorkspace)m_BaseWorkspace; // 工作空间
                // 打开要素图层
                ipSrcFeatClass = ipFtWS.OpenFeatureClass(strSrcLayer);

                if (ipSrcFeatClass == null)
                {
                    return false;
                }

                // 打开范围图层
                ipBoundFeatClass = ipFtWS.OpenFeatureClass(strBoundLayer);

                if (ipBoundFeatClass == null)
                {
                    return false;
                }

                m_nGeoType = GetLayerGeoType(ipSrcFeatClass);


                int nTuFuFieldIndex = -1;
                List<string> sTuFuCodeArray = new List<string>();

                IEnvelope ipSrcFeatureEnvelop = GetEnvelopByFeatureClass(ipSrcFeatClass);

                IPointCollection ipPointCollection = new PolylineClass(); // (CLSID_Polyline);
                IEnvelope ipErrorEnvelop =
                    GetCutlineNumber(ipSrcFeatClass, ipBoundFeatClass, m_pPara.strInciseField, ipSrcFeatureEnvelop,
                                     ref nTuFuFieldIndex, ref sTuFuCodeArray, ipPointCollection);

                if (ipErrorEnvelop == null)
                {
                    return false;
                }
                if (nTuFuFieldIndex == -1)
                {
                    SendMessage(enumMessageType.RuleError, "接边字段在接边范围图层" + strBoundLayer + "不存在!");
                    return false;
                }

                int nTuFuCount = sTuFuCodeArray.Count;
                int nFieldCount = m_pPara.arrayFieldName.Count;

                if (nTuFuCount == 0)
                {
                    SendMessage(enumMessageType.RuleError, "至少选择一个图幅要素进行接边!");
                    return false;
                }
                if (nFieldCount == 0)
                {
                    SendMessage(enumMessageType.RuleError, "至少选择一个属性字段作为接边条件!");
                    return false;
                }

                IArray aryGriddingLine = GetTuFuGriddingLine(ipPointCollection, ipErrorEnvelop);

                string sWhereClause;

                int nMatchEdge = 0;
                int lGriddingLineCount = aryGriddingLine.Count;


                string sProgressMessage, sCurrentTuFuCode;
                string sTuFuName;

                sTuFuName = m_pPara.strInciseField;

                List<string> aryOIDs = new List<string>();
                List<string> aryCheckInfos = new List<string>();

                esriGeometryType eGeometryType = ipSrcFeatClass.ShapeType;

                List<int> aryFieldIndex = new List<int>(); //CUIntArray aryFieldIndex;
                for (int t = 0; t < nFieldCount; t++)
                {
                    string str = m_pPara.arrayFieldName[t];
                    int nFIndex = -1;
                    GetFieldIndexByFieldName(ipSrcFeatClass, str, ref nFIndex);
                    if (nFIndex != -1)
                    {
                        aryFieldIndex.Add(nFIndex);
                    }
                }

                IFields ipFields = ipBoundFeatClass.Fields;

                IField ipField = ipFields.get_Field(nTuFuFieldIndex);

                esriFieldType eFieldType = ipField.Type;

                string strSql = "";

                //设置接边标志
                for (int i = 0; i < lGriddingLineCount; i++)
                {
                    object obj = aryGriddingLine.get_Element(i);
                    IPolyline ipPolyline = (IPolyline)obj;

                    if (eGeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        MatchEdgeCheckPolygon(ipSrcFeatClass, strSql, ipPolyline,
                                              aryFieldIndex, m_pPara.dLimit,
                                              ipErrorEnvelop, ref aryOIDs, ref aryCheckInfos);
                    }
                    else
                    {
                        MatchEdgeCheckLine(ipSrcFeatClass, strSql,
                                           ipPolyline, aryFieldIndex, m_pPara.dLimit,
                                           ipErrorEnvelop, ref aryOIDs, ref aryCheckInfos);
                    }
                }


                if (aryOIDs.Count > 0)
                {
                    long nErrCount = aryOIDs.Count;
                    for (int i = 0; i < nErrCount; i++)
                    {
                        RuleExpression.JoinSideInfo jsInfo;
                        jsInfo.OID1 = Convert.ToInt32(aryOIDs[i]);
                        jsInfo.strError = aryCheckInfos[i];
                        m_aryResult.Add(jsInfo);
                    }
                }

                List<Error> pResult = new List<Error>();
                if (!SaveData(ref pResult))
                {
                    pResult = null;
                    return false;
                }
                checkResult = pResult;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (ipSrcFeatClass != null)
                {
                    Marshal.ReleaseComObject(ipSrcFeatClass);
                }
                if (ipBoundFeatClass != null)
                {
                    Marshal.ReleaseComObject(ipBoundFeatClass);
                }
            }
        }

        /// <summary>
        /// 获取图层的几何类型
        /// </summary>
        /// <param name="ipSrcFeaCls">要素类</param>
        /// <returns>正确返回类型值，否则返回0</returns>
        private int GetLayerGeoType(IFeatureClass ipSrcFeaCls)
        {
            int nGeoType = 0;

            esriGeometryType Type = ipSrcFeaCls.ShapeType;

            switch (Type)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        nGeoType = 1;
                        break;
                    }
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    {
                        nGeoType = 2;
                        break;
                    }
                case esriGeometryType.esriGeometryRing:
                case esriGeometryType.esriGeometryPolygon:
                case esriGeometryType.esriGeometryEnvelope:
                    {
                        nGeoType = 3;
                        break;
                    }
                default:
                    break;
            }

            return nGeoType;
        }

        /// <summary>
        /// 获取要素集的最大外矩
        /// </summary>
        /// <param name="ipFeatureClass">要素集</param>
        /// <returns>正确返回要素集最大外矩</returns>
        private IEnvelope GetEnvelopByFeatureClass(IFeatureClass ipFeatureClass)
        {
            try
            {
                IEnvelope ipMaxEnvelop = new EnvelopeClass();

                double dMinX = 0.0;
                double dMaxX = 0.0;
                double dMinY = 0.0;
                double dMaxY = 0.0;


                ISelectionSet ipSelectionSet =
                    ipFeatureClass.Select(null, esriSelectionType.esriSelectionTypeHybrid,
                                          esriSelectionOption.esriSelectionOptionNormal, null);
                int nFeatureCount = ipSelectionSet.Count;


                if (nFeatureCount < 5000)
                {
                    ICursor ipCursor = null;
                    ipSelectionSet.Search(null, false, out ipCursor);
                    IFeatureCursor ipFeatureCursor = (IFeatureCursor) ipCursor;


                    if (ipFeatureCursor == null)
                    {
                        return null;
                    }

                    IFeature ipFeature = ipFeatureCursor.NextFeature();
                    IEnvelope ipEnvelop = ipFeature.Extent;
                    dMinX = ipEnvelop.XMin;
                    dMaxX = ipEnvelop.XMax;
                    dMinY = ipEnvelop.YMin;
                    dMaxY = ipEnvelop.YMax;


                    ipFeature = ipFeatureCursor.NextFeature();

                    double dtMinX = 0.0;
                    double dtMaxX = 0.0;
                    double dtMinY = 0.0;
                    double dtMaxY = 0.0;
                    while (ipFeature != null)
                    {
                        ipEnvelop = ipFeature.Extent;
                        dtMinX = ipEnvelop.XMin;
                        dtMaxX = ipEnvelop.XMax;
                        dtMinY = ipEnvelop.YMin;
                        dtMaxY = ipEnvelop.YMax;

                        if (dtMinX < dMinX)
                            dMinX = dtMinX;
                        if (dtMaxX > dMaxX)
                            dMaxX = dtMaxX;
                        if (dtMinY < dMinY)
                            dMinY = dtMinY;
                        if (dtMaxY > dMaxY)
                            dMaxY = dtMaxY;

                        ipFeature = ipFeatureCursor.NextFeature();
                    }
                }
                else
                {
                    IFeatureLayer ipFeatureLayer = new FeatureLayerClass();
                    ipFeatureLayer.FeatureClass = ipFeatureClass;

                    ipMaxEnvelop = ipFeatureLayer.AreaOfInterest;

                    //将其范围向类缩一点，避免不必要的图符被选进来
                    dMinX = ipMaxEnvelop.XMin;
                    dMaxX = ipMaxEnvelop.XMax;
                    dMinY = ipMaxEnvelop.YMin;
                    dMaxY = ipMaxEnvelop.YMax;
                }

                double dHeight = (dMaxY - dMinY)/500;
                double dWidth = (dMaxX - dMinX)/500;
                double dTempTol = dHeight > dWidth ? dWidth : dHeight;

                dMinX += dTempTol;
                dMaxX -= dTempTol;
                dMinY += dTempTol;
                dMaxY -= dTempTol;

                ipMaxEnvelop.XMin = dMinX;
                ipMaxEnvelop.XMax = dMaxX;
                ipMaxEnvelop.YMin = dMinY;
                ipMaxEnvelop.YMax = dMaxY;

                return ipMaxEnvelop;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取分幅图层中指定目标图层的范围大小的所有图符号字段的值
        /// </summary>
        /// <param name="ipFeatrueClass">接边要素类</param>
        /// <param name="ipBoundFeatureClass">范围要素类</param>
        /// <param name="strTuFuNumFieldName">图符号字段名</param>
        /// <param name="ipEnvelope"></param>
        /// <param name="nTuFuFieldIndex"></param>
        /// <param name="aryNumber">图符号数组</param>
        /// <param name="ipPointCollection"></param>
        /// <returns>成功返回true,否则返回false</returns>
        private IEnvelope GetCutlineNumber(IFeatureClass ipFeatrueClass,
                                           IFeatureClass ipBoundFeatureClass,
                                           string strTuFuNumFieldName,
                                           IEnvelope ipEnvelope,
                                           ref int nTuFuFieldIndex, ref List<string> aryNumber,
                                           IPointCollection ipPointCollection)
        {
            try
            {
                if (ipEnvelope == null) return null;

                // //测试代码
                // BSTR bstrTuFuNum = strTuFuNumFieldName.AllocSysString();
                //ipTable.FindField(bstrTuFuNum,&nIndex);
                //查出图符号字段的索引
                ITable ipTable = (ITable) ipBoundFeatureClass;
                int nIndex = ipTable.FindField(strTuFuNumFieldName);


                if (nIndex == -1)
                {
                    return null;
                }

                IEnvelope ipTuFuMaxEnvelope = new EnvelopeClass();
                nTuFuFieldIndex = nIndex;

                //设置空间条件
                ISpatialFilter ipSpatialFilter = new SpatialFilterClass();
                ipSpatialFilter.AddField(strTuFuNumFieldName);
                IGeometry ipGeo = ipEnvelope;
                ipSpatialFilter.Geometry = ipGeo;
                string bstrSharpFieldName = ipBoundFeatureClass.ShapeFieldName;
                ipSpatialFilter.GeometryField = bstrSharpFieldName;
                ipSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                //进行空间查询,并提取出图幅号字段的值
                IFeatureCursor ipFeatureCursor = ipBoundFeatureClass.Search(ipSpatialFilter, false);

                if (ipFeatureCursor == null)
                {
                    return null;
                }

                IGeometry ipGeometry;
                IGeometry ipPolygonGeometry;
                IPolygon2 ipPolygon;
                IRing ipRing;


                IFeature ipFeature = ipFeatureCursor.NextFeature();
                IEnvelope ipTuFuEnvelope = ipFeature.Extent;

                double dMinX = 0.0;
                double dMaxX = 0.0;
                double dMinY = 0.0;
                double dMaxY = 0.0;

                double dTMinX = 0.0;
                double dTMaxX = 0.0;
                double dTMinY = 0.0;
                double dTMaxY = 0.0;

                dMinX = ipTuFuEnvelope.XMin;
                dMaxX = ipTuFuEnvelope.XMax;
                dMinY = ipTuFuEnvelope.YMin;
                dMaxY = ipTuFuEnvelope.YMax;


                IPoint ipPoint = new PointClass();

                int nPointCount = 0;
                while (ipFeature != null)
                {
                    object varTuFu = ipFeature.get_Value(nIndex);
                    string str = varTuFu.ToString();
                    aryNumber.Add(str);

                    ipTuFuEnvelope = ipFeature.Extent;
                    dTMinX = ipTuFuEnvelope.XMin;
                    dTMaxX = ipTuFuEnvelope.XMax;
                    dTMinY = ipTuFuEnvelope.YMin;
                    dTMaxY = ipTuFuEnvelope.YMax;

                    if (dTMinX < dMinX)
                        dMinX = dTMinX;
                    if (dTMaxX > dMaxX)
                        dMaxX = dTMaxX;
                    if (dTMinY < dMinY)
                        dMinY = dTMinY;
                    if (dTMaxY > dMaxY)
                        dMaxY = dTMaxY;

                    object obj = Type.Missing;


                    ipPoint.X = dTMinX;
                    ipPoint.Y = dTMinY;
                    ipPointCollection.AddPoint(ipPoint, ref obj, ref obj);
                    ipPoint.X = dTMinX;
                    ipPoint.Y = dTMaxY;
                    ipPointCollection.AddPoint(ipPoint, ref obj, ref obj);
                    ipPoint.X = dTMaxX;
                    ipPoint.Y = dTMinY;
                    ipPointCollection.AddPoint(ipPoint, ref obj, ref obj);
                    ipPoint.X = dTMaxX;
                    ipPoint.Y = dTMaxY;
                    ipPointCollection.AddPoint(ipPoint, ref obj, ref obj);

                    ipFeature = ipFeatureCursor.NextFeature();
                }

                ipTuFuMaxEnvelope.XMin = dMinX;
                ipTuFuMaxEnvelope.XMax = dMaxX;
                ipTuFuMaxEnvelope.YMin = dMinY;
                ipTuFuMaxEnvelope.YMax = dMaxY;
                return ipTuFuMaxEnvelope;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return null;
            }
        }

        /// <summary>
        ///  获取图符网格线
        /// </summary>
        /// <param name="ipPointCollection">网格点集合</param>
        /// <param name="ipTuFuEnvelope">图幅集最大外矩</param>
        /// <returns></returns>
        private IArray GetTuFuGriddingLine(IPointCollection ipPointCollection, IEnvelope ipTuFuEnvelope)
        {
            try
            {
                IArray aryGriddingLine = new ArrayClass();
                double dMinX = 0.0;
                double dMaxX = 0.0;
                double dMinY = 0.0;
                double dMaxY = 0.0;

                dMinX = ipTuFuEnvelope.XMin;
                dMaxX = ipTuFuEnvelope.XMax;
                dMinY = ipTuFuEnvelope.YMin;
                dMaxY = ipTuFuEnvelope.YMax;


                int nPointCount = ipPointCollection.PointCount;

                IPoint ipPoint1 = null;
                IPoint ipPoint2 = null;

                double dX1 = 0.0;
                double dY1 = 0.0;
                double dX2 = 0.0;
                double dY2 = 0.0;

                double dHeight = (dMaxY - dMinY)/1000;
                double dWidth = (dMaxX - dMinX)/1000;
                double dTempTol = dHeight > dWidth ? dWidth : dHeight;

                List<int> aryVerticalIndex = new List<int>(); //CArray<long,long> aryVerticalIndex;//所有垂直网格线的起点索引
                List<int> aryHorizontal = new List<int>(); //CArray<long,long> aryHorizontal;//所有水平网格线的起点索引

                int nVerCount = 0;
                int nHorCount = 0;
                int i, j;

                //保留边框上的点,中间的点去掉
                for (i = 0; i < nPointCount;)
                {
                    ipPoint1 = ipPointCollection.get_Point(i);
                    if (!PointBIsOverBoundary(ipPoint1, ipTuFuEnvelope, dTempTol))
                    {
                        ipPointCollection.RemovePoints(i, 1);
                        nPointCount--;
                    }
                    else
                    {
                        i++;
                    }
                }

                nPointCount = ipPointCollection.PointCount;
                for (i = 0; i < nPointCount; i++)
                {
                    ipPoint1 = ipPointCollection.get_Point(i);

                    dX1 = ipPoint1.X;
                    dY1 = ipPoint1.Y;

                    nVerCount = aryVerticalIndex.Count;
                    if (nVerCount == 0 && Math.Abs(dY1 - dMinY) < dTempTol
                        && Math.Abs(dX1 - dMinX) > dTempTol
                        && Math.Abs(dX1 - dMaxX) > dTempTol)
                    {
                        nVerCount++;
                        aryVerticalIndex.Add(i);
                    }

                    nHorCount = aryHorizontal.Count;
                    if (nHorCount == 0 && Math.Abs(dX1 - dMinX) < dTempTol
                        && Math.Abs(dY1 - dMinY) > dTempTol
                        && Math.Abs(dY1 - dMaxY) > dTempTol)
                    {
                        nHorCount++;
                        aryHorizontal.Add(i);
                    }

                    for (j = 0; j < nVerCount; j++)
                    {
                        ipPoint2 = ipPointCollection.get_Point(aryVerticalIndex[j]);
                        dX2 = ipPoint2.X;
                        dY2 = ipPoint2.Y;

                        if (Math.Abs(dX1 - dX2) < dTempTol && Math.Abs(dY1 - dY2) < dTempTol)
                        {
                            break;
                        }
                    }

                    if (j >= nVerCount)
                    {
                        //取底部去掉左右两侧的点为起点
                        if (Math.Abs(dY1 - dMinY) < dTempTol
                            && Math.Abs(dX1 - dMinX) > dTempTol
                            && Math.Abs(dX1 - dMaxX) > dTempTol)
                        {
                            aryVerticalIndex.Add(i);
                        }
                    }

                    for (j = 0; j < nHorCount; j++)
                    {
                        ipPoint2 = ipPointCollection.get_Point(aryHorizontal[j]);
                        dX2 = ipPoint2.X;
                        dY2 = ipPoint2.Y;
                        if (Math.Abs(dX1 - dX2) < dTempTol && Math.Abs(dY1 - dY2) < dTempTol)
                        {
                            break;
                        }
                    }

                    if (j >= nHorCount)
                    {
                        //取左侧去掉上下两侧的点为起点
                        if (Math.Abs(dX1 - dMinX) < dTempTol
                            && Math.Abs(dY1 - dMinY) > dTempTol
                            && Math.Abs(dY1 - dMaxY) > dTempTol)
                        {
                            aryHorizontal.Add(i);
                        }
                    }
                }

                nPointCount = ipPointCollection.PointCount;

                //组织垂直网格线
                nVerCount = aryVerticalIndex.Count;
                for (i = 0; i < nVerCount; i++)
                {
                    ipPoint1 = ipPointCollection.get_Point(aryVerticalIndex[i]);
                    dX1 = ipPoint1.X;
                    dY1 = ipPoint1.Y;

                    //添加起点
                    IPolyline ipPolyline = new PolylineClass();
                    ipPolyline.FromPoint = ipPoint1;

                    for (j = 0; j < nPointCount; j++)
                    {
                        ipPoint2 = ipPointCollection.get_Point(j);
                        dX2 = ipPoint2.X;
                        dY2 = ipPoint2.Y;
                        if (Math.Abs(dX1 - dX2) < dTempTol && Math.Abs(dY1 - dY2) > dTempTol)
                        {
                            //添加终点
                            ipPolyline.ToPoint = ipPoint2;
                            break;
                        }
                    }
                    aryGriddingLine.Add(ipPolyline);
                }

                //组织水平网格线
                nHorCount = aryHorizontal.Count;
                for (i = 0; i < nHorCount; i++)
                {
                    ipPoint1 = ipPointCollection.get_Point(aryHorizontal[i]);
                    dX1 = ipPoint1.X;
                    dY1 = ipPoint1.Y;

                    //添加起点
                    IPolyline ipPolyline = new PolylineClass();
                    ipPolyline.FromPoint = ipPoint1;

                    for (j = 0; j < nPointCount; j++)
                    {
                        ipPoint2 = ipPointCollection.get_Point(j);
                        dX2 = ipPoint2.X;
                        dY2 = ipPoint2.Y;
                        if (Math.Abs(dY1 - dY2) < dTempTol && Math.Abs(dX1 - dX2) > dTempTol)
                        {
                            //添加终点
                            ipPolyline.ToPoint = ipPoint2;
                            break;
                        }
                    }
                    aryGriddingLine.Add(ipPolyline);
                }
                return aryGriddingLine;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// 判断一个点是否在外矩上
        /// </summary>
        /// <param name="ipPoint">点</param>
        /// <param name="ipEnvelope">外矩</param>
        /// <param name="dblTolerance">容错值</param>
        /// <returns></returns>
        private bool PointBIsOverBoundary(IPoint ipPoint, IEnvelope ipEnvelope, double dblTolerance)
        {
            double dX = 0.0;
            double dY = 0.0;

            double dMinX = ipEnvelope.XMin;
            double dMaxX = ipEnvelope.XMax;
            double dMinY = ipEnvelope.YMin;
            double dMaxY = ipEnvelope.YMax;

            dX = ipPoint.X;
            dY = ipPoint.Y;


            double dTempTol = dblTolerance;

            if (Math.Abs(dX - dMinX) < dTempTol ||
                Math.Abs(dX - dMaxX) < dTempTol ||
                Math.Abs(dY - dMinY) < dTempTol ||
                Math.Abs(dY - dMaxY) < dTempTol)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取字段的索引
        /// </summary>
        /// <param name="ipFeatrueClass">要素类</param>
        /// <param name="strFieldName">字段名称</param>
        /// <param name="nIndex">索引</param>
        /// <returns>正确返回true，否则返回false</returns>
        private bool GetFieldIndexByFieldName(IFeatureClass ipFeatrueClass, string strFieldName, ref int nIndex)
        {
            try
            {
                nIndex = ipFeatrueClass.FindField(strFieldName);

                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// 线接边检测
        /// </summary>
        /// <param name="pMatchEdgeFeatureClass">要进行接边检测的要素</param>
        /// <param name="szSql">属性过滤条件</param>
        /// <param name="ipGriddingline"></param>
        /// <param name="nFieldArray">属性匹配字段索引列表</param>
        /// <param name="dblTolerance">容错值</param>
        /// <param name="ipErrorFilterEnvelop">对于在边界附近的要素没有接边对象的不属于错误,因此设一个过滤的对象</param>
        /// <param name="sOIDs">接边检测的对象OID</param>
        /// <param name="sCheckInfos">接边检测每组信息</param>
        /// <returns></returns>
        private bool MatchEdgeCheckLine(IFeatureClass pMatchEdgeFeatureClass,
                                        string szSql,
                                        IPolyline ipGriddingline,
                                        List<int> nFieldArray,
                                        double dblTolerance,
                                        IEnvelope ipErrorFilterEnvelop,
                                        ref List<string> sOIDs,
                                        ref List<string> sCheckInfos)
        {
            try
            {
                //使用网格线进行空间查询
                IGeometry ipLineBufGeoInSide, ipLineBufGeoOutSide;


                //遍历ipSourceSelectionSet中的要素(Buffer处理)
                //然后在ipTargetSelectionSet获取与其相交的要素
                bool bIsInToOut = true; //就否由内到外

                for (int k = 0; k < 2; k++)
                {
                    //获取内缓冲区域要素集
                    IPolyline ipPolyline = LineBuffer(ipGriddingline, dblTolerance);

                    ISpatialFilter ipSpatialFilter = new SpatialFilterClass();
                    ipLineBufGeoInSide = ipPolyline;

                    ipSpatialFilter.Geometry = ipLineBufGeoInSide;
                    ipSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    //ipSpatialFilter.WhereClause = szSql;

                    ISelectionSet ipInSideSelectionSet = null;
                    ISelectionSet ipOutSideSelectionSet = null;
                    try
                    {
                        ipInSideSelectionSet =
                            pMatchEdgeFeatureClass.Select(ipSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                          esriSelectionOption.esriSelectionOptionNormal, null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        return false;
                    }
                    int lInSideSelectionCount = ipInSideSelectionSet.Count;


                    //内缓冲区没有对象，那么外缓冲区的对象也不用检测
                    if (lInSideSelectionCount < 1)
                    {
                        return true;
                    }

                    //获取外缓冲区域的要素集
                    ipPolyline = LineBuffer(ipGriddingline, -dblTolerance);
                    ipLineBufGeoOutSide = ipPolyline;

                    ipSpatialFilter.Geometry = ipLineBufGeoOutSide;

                    try
                    {
                        ipOutSideSelectionSet =
                            pMatchEdgeFeatureClass.Select(ipSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                          esriSelectionOption.esriSelectionOptionNormal, null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        return false;
                    }
                    int lOutSideSelectionCount = ipOutSideSelectionSet.Count;

                    IGeometry ipGriddinglineBufferGeo = ((ITopologicalOperator) ipGriddingline).Buffer(dblTolerance);


                    ISelectionSet ipSourceSelectionSet = null;
                    ISelectionSet ipTargetSelectionSet = null;
                    ICursor ipSourceCursor = null;
                    if (bIsInToOut)
                    {
                        ipSourceSelectionSet = ipInSideSelectionSet;
                        ipTargetSelectionSet = ipOutSideSelectionSet;
                    }
                    else
                    {
                        ipSourceSelectionSet = ipOutSideSelectionSet;
                        ipTargetSelectionSet = ipInSideSelectionSet;
                    }
                    try
                    {
                        ipSourceSelectionSet.Search(null, false, out ipSourceCursor);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        return false;
                    }
                    if (ipSourceCursor == null)
                    {
                        return false;
                    }
                    IRow ipSourceRow = ipSourceCursor.NextRow();
                    IFields ipMatchEdgeFields = pMatchEdgeFeatureClass.Fields;

                    int nProcessingFeature = 0;
                    string sProgressMessage;
                    while (ipSourceRow != null)
                    {
                        nProcessingFeature++;
                        //根据验证字段得到ipSourceRow的对应字段值
                        int nFieldIndex;
                        int nFieldCount = nFieldArray.Count;
                        //当前是第几组接边检测
                        int lSourceRowOID = ipSourceRow.OID;
                        if (lSourceRowOID == 30)
                        {
                        }
                        string sSourceRowOID = lSourceRowOID.ToString();
                        string sMatchEdgeWhereClause = GetCondition(ipSourceRow, ipMatchEdgeFields, nFieldArray);

                        //这里增加先判断是否有空间上的接边对象，如果有，再进一步判断是否有属性接边
                        IGeometry ipSourceGeometry, ipFromBufferGeometry, ipToBufferGeometry;
                        ipSourceGeometry = ((IFeature) ipSourceRow).Shape;

                        //这里要把ipSourceGeometry的起点和终点拿到，分别判断其起点和终点是否需要有接边对象
                        IPoint ipFromPoint = ((ICurve) ipSourceGeometry).FromPoint;
                        IPoint ipToPoint = ((ICurve) ipSourceGeometry).ToPoint;

                        bool bIsFromOver = PointBIsOverBoundary(ipFromPoint, ipErrorFilterEnvelop, dblTolerance);
                        bool bIsToOver = PointBIsOverBoundary(ipToPoint, ipErrorFilterEnvelop, dblTolerance);
                        if (bIsFromOver && bIsToOver)
                        {
                            Marshal.ReleaseComObject(ipSourceRow);
                            ipSourceRow = ipSourceCursor.NextRow();
                            continue;
                        }

                        //分别查找起点和终点是否在图幅缓冲带中
                        bool vtContainsFrom, vtContainsTo;
                        ipFromBufferGeometry = ((ITopologicalOperator) ipFromPoint).Buffer(dblTolerance*0.1);
                        ipToBufferGeometry = ((ITopologicalOperator) ipToPoint).Buffer(dblTolerance*0.1);


                        ((ITopologicalOperator) ipFromBufferGeometry).Simplify();
                        ((ITopologicalOperator) ipToBufferGeometry).Simplify();


                        vtContainsFrom = ((IRelationalOperator) ipGriddinglineBufferGeo).Contains(ipFromBufferGeometry);
                        vtContainsTo = ((IRelationalOperator) ipGriddinglineBufferGeo).Contains(ipToBufferGeometry);

                        if (vtContainsFrom == true || vtContainsTo == true)
                        {
                            //具体查找起点或终点在Tolerance范围内的目标接边对象
                            ipFromBufferGeometry = ((ITopologicalOperator) ipFromPoint).Buffer(dblTolerance);
                            ipToBufferGeometry = ((ITopologicalOperator) ipToPoint).Buffer(dblTolerance);
                            IGeometry ipFromBoundaryGeometry, ipToBoundaryGeometry;
                            ipFromBoundaryGeometry = ((ITopologicalOperator) ipFromBufferGeometry).Boundary;
                            ipToBoundaryGeometry = ((ITopologicalOperator) ipToBufferGeometry).Boundary;

                            ISpatialFilter ipFromSpatialFilter = new SpatialFilterClass();
                            ISpatialFilter ipToSpatialFilter = new SpatialFilterClass();

                            ipFromSpatialFilter.Geometry = ipFromBoundaryGeometry;
                            ipFromSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                            ipToSpatialFilter.Geometry = ipToBoundaryGeometry;
                            ipToSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                            //起点和终点都需要接边
                            if (vtContainsFrom == true && vtContainsTo == true)
                            {
                                if (
                                    !JointFromToPoints(bIsFromOver, bIsToOver, ipToSpatialFilter, ipFromSpatialFilter,
                                                       ref ipSourceRow, ipSourceCursor, ipTargetSelectionSet,
                                                       sMatchEdgeWhereClause, lSourceRowOID, sSourceRowOID, ref sOIDs,
                                                       ref sCheckInfos))
                                {
                                    continue;
                                }
                            }
                            else if (vtContainsFrom == true)
                            {
                                //只有起点需要接边对象
                                if (!JoinFromPoint(bIsFromOver, bIsToOver, ipToSpatialFilter, ipFromSpatialFilter,
                                                   ref ipSourceRow, ipSourceCursor, ipTargetSelectionSet,
                                                   sMatchEdgeWhereClause, lSourceRowOID, sSourceRowOID, ref sOIDs,
                                                   ref sCheckInfos))
                                {
                                    continue;
                                }
                            }
                            else if (vtContainsTo == true)
                            {
                                //只有终点需要接边对象
                                if (!JoinToPoint(bIsFromOver, bIsToOver, ipToSpatialFilter, ipFromSpatialFilter,
                                                 ref ipSourceRow, ipSourceCursor, ipTargetSelectionSet,
                                                 sMatchEdgeWhereClause,
                                                 lSourceRowOID, sSourceRowOID, ref sOIDs, ref sCheckInfos))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                sOIDs.Add(sSourceRowOID);
                                //起点和终点都没有接边对象,可能是起点和终点不在图幅缓冲带中
                                string sInfo = sSourceRowOID + "要素缺少对应的接边对象";
                                sCheckInfos.Add(sInfo);
                            }
                        }
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                    }
                    bIsInToOut = false;
                    Marshal.ReleaseComObject(ipSourceCursor);
                    if (ipInSideSelectionSet != null)
                    {
                        Marshal.ReleaseComObject(ipInSideSelectionSet);
                    }
                    if (ipOutSideSelectionSet != null)
                    {
                        Marshal.ReleaseComObject(ipOutSideSelectionSet);
                    }
                    if (ipSourceSelectionSet != null)
                    {
                        Marshal.ReleaseComObject(ipSourceSelectionSet);

                        if (ipTargetSelectionSet != null)
                        {
                            Marshal.ReleaseComObject(ipTargetSelectionSet);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 面接边检测
        /// </summary>
        /// <param name="pMatchEdgeFeatureClass">要进行接边检测的要素</param>
        /// <param name="szSql">属性过滤条件</param>
        /// <param name="ipGriddingline"></param>
        /// <param name="nFieldArray">属性匹配字段索引列表</param>
        /// <param name="dblTolerance">容错值</param>
        /// <param name="ipErrorFilterEnvelop"></param>
        /// <param name="sOIDs">接边检测的对象OID</param>
        /// <param name="sCheckInfos">接边检测每组信息</param>
        /// <returns></returns>
        private bool MatchEdgeCheckPolygon(IFeatureClass pMatchEdgeFeatureClass, string szSql, IPolyline ipGriddingline,
                                           List<int> nFieldArray, double dblTolerance, IEnvelope ipErrorFilterEnvelop,
                                           ref List<string> sOIDs, ref List<string> sCheckInfos)
        {
            //测试代码

            IPoint ipTestFromPoint = ipGriddingline.FromPoint;
            IPoint ipTestToPoint = ipGriddingline.ToPoint;

            double dFromTestX = ipTestFromPoint.X;
            double dFromTestY = ipTestFromPoint.Y;
            double dToTestX = ipTestToPoint.X;
            double dToTestY = ipTestToPoint.Y;
            try
            {
                //

                //使用网格线进行空间查询
                ISpatialFilter ipSpatialFilter = new SpatialFilterClass();

                IGeometry ipLineBufGeoInSide, ipLineBufGeoOutSide;

                ISelectionSet ipInSideSelectionSet = null;
                ISelectionSet ipOutSideSelectionSet = null;

                //获取内缓冲区域要素集
                //IPolygon ipPolygon = LineBuffer(ipGriddingline,dblTolerance);
                IPolyline ipPolyline = LineBuffer(ipGriddingline, dblTolerance);

                ipLineBufGeoInSide = ipPolyline;

                ipSpatialFilter.Geometry = ipLineBufGeoInSide;
                ipSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                ipSpatialFilter.WhereClause = szSql;

                ipInSideSelectionSet =
                    pMatchEdgeFeatureClass.Select(ipSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                  esriSelectionOption.esriSelectionOptionNormal, null);
                int lInSideSelectionCount = ipInSideSelectionSet.Count;

                //内缓冲区没有对象，那么外缓冲区的对象也不用检测
                if (lInSideSelectionCount < 1)
                {
                    return true;
                }

                //获取外缓冲区域的要素集
                ipPolyline = LineBuffer(ipGriddingline, -dblTolerance);
                ipLineBufGeoOutSide = ipPolyline;

                ipSpatialFilter.Geometry = (IGeometry) ipLineBufGeoOutSide;

                ipOutSideSelectionSet =
                    pMatchEdgeFeatureClass.Select(ipSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                  esriSelectionOption.esriSelectionOptionNormal, null);

                int lOutSideSelectionCount = ipOutSideSelectionSet.Count;

                IScratchWorkspaceFactory ipSwf = new ScratchWorkspaceFactoryClass();
                IWorkspace ipScratchWorkspace = ipSwf.CreateNewScratchWorkspace();

                bool bIsInToOut = true; //就否由内到外
                for (int k = 0; k < 2; k++)
                {
                    ISelectionSet ipSourceSelectionSet = null;
                    ISelectionSet ipTargetSelectionSet = null;
                    if (bIsInToOut)
                    {
                        ipSourceSelectionSet = ipInSideSelectionSet;
                        ipTargetSelectionSet = ipOutSideSelectionSet;
                    }
                    else
                    {
                        ipSourceSelectionSet = ipOutSideSelectionSet;
                        ipTargetSelectionSet = ipInSideSelectionSet;
                    }

                    //遍历ipSourceSelectionSet中的要素(Buffer处理)
                    //然后在ipTargetSelectionSet获取与其相交的要素
                    ICursor ipSourceCursor;
                    ipSourceSelectionSet.Search(null, false, out ipSourceCursor);
                    IRow ipSourceRow = ipSourceCursor.NextRow();
                    IFields ipMatchEdgeFields = pMatchEdgeFeatureClass.Fields;
                    IField ipMatchEdgeField;

                    int nProcessingFeature = 0;
                    string sProgressMessage;
                    while (ipSourceRow != null)
                    {
                        nProcessingFeature++;

                        //根据验证字段得到ipSourceRow的对应字段值
                        int nFieldIndex;
                        string sMatchEdgeWhereClause;
                        int nFieldCount = nFieldArray.Count;
                        esriFieldType eMatchEdgeFieldType;
                        string sMatchEdgeFieldValue;

                        //ipSourceRow的OID
                        int lSourceRowOID = ipSourceRow.OID;
                        string sSourceRowOID = lSourceRowOID.ToString();

                        //构建属性查询条件
                        sMatchEdgeWhereClause = GetCondition(ipSourceRow, ipMatchEdgeFields, nFieldArray);

                        //这里增加先判断是否有空间上的接边对象，如果有，再进一步判断是否有属性接边

                        IGeometry ipSourceGeometry = ((IFeature) ipSourceRow).Shape;

                        ISpatialFilter ipSourceSpatialFilter = new SpatialFilterClass();

                        ipSourceSpatialFilter.Geometry = ipSourceGeometry;
                        ipSourceSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                        ISelectionSet ipSourceSelectionSet1 =
                            ipTargetSelectionSet.Select(ipSourceSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                        esriSelectionOption.esriSelectionOptionNormal,
                                                        ipScratchWorkspace);

                        int lSourceSelectionCount = ipSourceSelectionSet1.Count;

                        if (lSourceSelectionCount > 0)
                        {
                            //有图形接边
                            ipSourceSpatialFilter.WhereClause = sMatchEdgeWhereClause;

                            ISelectionSet ipSourceAttrSelectionSet1 =
                                ipSourceSelectionSet1.Select(ipSourceSpatialFilter,
                                                             esriSelectionType.esriSelectionTypeHybrid,
                                                             esriSelectionOption.esriSelectionOptionNormal,
                                                             ipScratchWorkspace);
                            int lSourceAttrSelectionCount = ipSourceAttrSelectionSet1.Count;

                            if (lSourceAttrSelectionCount > 0)
                            {
                                //有属性接边
                                //判断接边对象是否多于一个
                                ICursor ipSourceAttrCursor;
                                ipSourceAttrSelectionSet1.Search(null, false, out ipSourceAttrCursor);

                                IRow ipSourceAttrRow = ipSourceAttrCursor.NextRow();
                                int nSourceAttr = 0;
                                while (ipSourceAttrRow != null)
                                {
                                    int lSourceAttrOID = ipSourceAttrRow.OID;


                                    if (lSourceAttrOID != lSourceRowOID)
                                    {
                                        nSourceAttr++;
                                    }
                                    ipSourceAttrRow = ipSourceAttrCursor.NextRow();
                                }

                                string sInfo;
                                if (nSourceAttr > 1)
                                {
                                    sOIDs.Add(sSourceRowOID);
                                    sInfo = sSourceRowOID + "要素处有" + nSourceAttr + "个接边对象";
                                    sCheckInfos.Add(sInfo);
                                }
                            }
                            else
                            {
                                sOIDs.Add(sSourceRowOID);
                                //没有属性接边对象
                                string sInfo = sSourceRowOID + "要素有接边对象,但属性不同";
                                sCheckInfos.Add(sInfo);
                            }
                        }
                        else
                        {
                            sOIDs.Add(sSourceRowOID);
                            //没有接边对象
                            string sInfo = sSourceRowOID + "要素缺少对应的接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                    }
                    bIsInToOut = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// 获取线的缓冲区几何对象
        /// </summary>
        /// <param name="ipPolyline">线</param>
        /// <param name="tolerance">缓冲值</param>
        /// <returns>正确返回线几何对象，否则返回null</returns>
        private IPolyline LineBuffer(IPolyline ipPolyline, double tolerance)
        {
            if (ipPolyline == null)
            {
                return null;
            }

            ISegmentCollection ipSegCollRes = (ISegmentCollection) ipPolyline;

            long nSegmentCount = ipSegCollRes.SegmentCount;

            ISegment ipSegment = null;
            IPointCollection ipPtColl = new PolylineClass();

            for (int i = 0; i < nSegmentCount; i++)
            {
                ipSegment = ipSegCollRes.get_Segment(i);

                ILine ipline1 = new LineClass();
                ILine ipline2 = new LineClass();

                ipSegment.QueryNormal(esriSegmentExtension.esriExtendTangentAtFrom, 0, false, -tolerance, ipline1);
                double dLength = ipSegment.Length;

                ipSegment.QueryNormal(esriSegmentExtension.esriExtendTangentAtFrom, dLength, false, -tolerance, ipline2);

                object obj = Type.Missing;
                IPoint ipFromPoint = ipline2.FromPoint;
                ipPtColl.AddPoint(ipFromPoint, ref obj, ref obj);

                IPoint ipPoint = ipline1.ToPoint;
                ipPtColl.AddPoint(ipPoint, ref obj, ref obj);

                double dFromX = ipFromPoint.X;
                double dFromY = ipFromPoint.Y;
                double dToX = ipPoint.X;
                double dToY = ipPoint.Y;
            }
            IPolyline ipResultPolyline = (IPolyline) ipPtColl;
            return ipResultPolyline;
        }


        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="ipSourceRow">行集</param>
        /// <param name="ipMatchEdgeFields">字段集</param>
        /// <param name="nFieldArray">要过滤的字段索引</param>
        /// <returns></returns>
        private string GetCondition(IRow ipSourceRow, IFields ipMatchEdgeFields, List<int> nFieldArray)
        {
            //构建属性查询条件
            string sMatchEdgeFieldValue = "";
            string sMatchEdgeWhereClause = "";
            esriFieldType eMatchEdgeFieldType;
            object vtFieldValue;
            IField ipMatchEdgeField = null;
            Int64 nFieldCount = nFieldArray.Count;

            string strFieldName;

            int nFieldIndex = 0;

            for (int i = 0; i < nFieldCount; i++)
            {
                nFieldIndex = nFieldArray[i];
                vtFieldValue = ipSourceRow.get_Value(nFieldIndex);
                ipMatchEdgeField = ipMatchEdgeFields.get_Field(nFieldIndex);
                string bstrFieldName = ipMatchEdgeField.Name;

                strFieldName = bstrFieldName;
                eMatchEdgeFieldType = ipMatchEdgeField.Type;
                switch (eMatchEdgeFieldType)
                {
                    case esriFieldType.esriFieldTypeSmallInteger:
                        sMatchEdgeFieldValue = vtFieldValue.ToString();
                        sMatchEdgeWhereClause += strFieldName + " = " + sMatchEdgeFieldValue;
                        break;
                    case esriFieldType.esriFieldTypeInteger:
                        sMatchEdgeFieldValue = vtFieldValue.ToString();
                        sMatchEdgeWhereClause += strFieldName + " = " + sMatchEdgeFieldValue;
                        break;
                    case esriFieldType.esriFieldTypeSingle:
                        sMatchEdgeFieldValue = vtFieldValue.ToString();
                        sMatchEdgeWhereClause += strFieldName + " = " + sMatchEdgeFieldValue;
                        break;
                    case esriFieldType.esriFieldTypeDouble:
                        sMatchEdgeFieldValue = vtFieldValue.ToString();
                        sMatchEdgeWhereClause += strFieldName + " = " + sMatchEdgeFieldValue;
                        break;
                    case esriFieldType.esriFieldTypeString:
                        sMatchEdgeFieldValue = vtFieldValue.ToString();
                        sMatchEdgeWhereClause += strFieldName + " = '" + sMatchEdgeFieldValue + "'";
                        break;
                    default:
                        break;
                }
                //	sMatchEdgeWhereClause += string(bsFieldName) + " = '" + sMatchEdgeFieldValue + "'";
                if (i != (nFieldCount - 1))
                {
                    sMatchEdgeWhereClause += " and ";
                }
            }

            return sMatchEdgeWhereClause;
        }

        /// <summary>
        /// 保存检测结果
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private bool SaveData(ref List<Error> pResult)
        {
            try
            {
                string strSql;

                // 在入口写入目标表名称
                if (m_nGeoType == 1) // 点
                {
                    strSql = "update LR_ResultEntryRule set TargetFeatClass2='" + m_pPara.strFeatureLayer +
                             "|' where RuleInstID='" + this.m_SchemaID + "'";

                    Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, strSql);
                }
                else if (m_nGeoType == 2) // 线
                {
                    strSql = "update LR_ResultEntryRule set TargetFeatClass1='" + m_pPara.strFeatureLayer +
                             "',TargetFeatClass2='" + m_pPara.strBoundLayer + "|" + m_pPara.strBoundLayer +
                             "'where RuleInstID='" + this.m_SchemaID + "'";

                    Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, strSql);
                }
                else if (m_nGeoType == 3) // 面
                {
                    strSql = "update LR_ResultEntryRule set TargetFeatClass1='" + m_pPara.strFeatureLayer +
                             "',TargetFeatClass2='" +
                             m_pPara.strBoundLayer + "|" + m_pPara.strBoundLayer + "' where RuleInstID='" +
                             this.m_SchemaID + "'";

                    Hy.Common.Utility.Data.AdoDbHelper.ExecuteSql(this.m_ResultConnection, strSql);
                }

                int size = m_aryResult.Count;

                for (int i = 0; i < size; i++)
                {
                    Error pResInfo = new Error();
                    pResInfo.DefectLevel = this.m_DefectLevel;
                    pResInfo.RuleID = this.InstanceID;

                    pResInfo.OID = m_aryResult[i].OID1;
                    pResInfo.Description = m_aryResult[i].strError;
                    pResult.Add(pResInfo);
                }
                //关闭记录集
                //		ipRecordset.Close();
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 起点、终点均需要接边对象
        /// </summary>
        /// <param name="bIsFromOver"></param>
        /// <param name="bIsToOver"></param>
        /// <param name="ipToSpatialFilter"></param>
        /// <param name="ipFromSpatialFilter"></param>
        /// <param name="ipSourceRow"></param>
        /// <param name="ipSourceCursor"></param>
        /// <param name="ipTargetSelectionSet"></param>
        /// <param name="sMatchEdgeWhereClause"></param>
        /// <param name="lSourceRowOID"></param>
        /// <param name="sSourceRowOID"></param>
        /// <param name="sOIDs"></param>
        /// <param name="sCheckInfos"></param>
        /// <returns></returns>
        private bool JointFromToPoints(bool bIsFromOver, bool bIsToOver, ISpatialFilter ipToSpatialFilter,
                                       ISpatialFilter ipFromSpatialFilter,
                                       ref IRow ipSourceRow, ICursor ipSourceCursor, ISelectionSet ipTargetSelectionSet,
                                       string sMatchEdgeWhereClause, int lSourceRowOID, string sSourceRowOID,
                                       ref List<string> sOIDs, ref List<string> sCheckInfos)
        {
            try
            {
                int lFromSelectionCount, lToSelectionCount;
                ISelectionSet ipFromSelectionSet = null;
                ISelectionSet ipToSelectionSet = null;
                if (bIsFromOver == false)
                {
                    try
                    {
                        ipFromSelectionSet = ipTargetSelectionSet.Select(ipFromSpatialFilter,
                                                                         esriSelectionType.esriSelectionTypeHybrid,
                                                                         esriSelectionOption.esriSelectionOptionNormal,
                                                                         null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                        return false;
                    }

                    lFromSelectionCount = ipFromSelectionSet.Count;
                }
                else
                {
                    lFromSelectionCount = 0;
                }

                if (bIsToOver == false)
                {
                    try
                    {
                        ipToSelectionSet =
                            ipTargetSelectionSet.Select(ipToSpatialFilter,
                                                        esriSelectionType.esriSelectionTypeHybrid,
                                                        esriSelectionOption.esriSelectionOptionNormal,
                                                        null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                        return false;
                    }
                    lToSelectionCount = ipToSelectionSet.Count;
                }
                else
                {
                    lToSelectionCount = 0;
                }

                if (lFromSelectionCount > 0 && lToSelectionCount > 0)
                {
                    //有起点和终点的图形图形接边
                    ipFromSpatialFilter.WhereClause = sMatchEdgeWhereClause;
                    ipToSpatialFilter.WhereClause = sMatchEdgeWhereClause;

                    ISelectionSet ipFromAttrSelectionSet, ipToAttrSelectionSet;
                    ipFromAttrSelectionSet =
                        ipFromSelectionSet.Select(ipFromSpatialFilter,
                                                  esriSelectionType.esriSelectionTypeHybrid,
                                                  esriSelectionOption.esriSelectionOptionNormal, null);

                    ipToAttrSelectionSet =
                        ipToSelectionSet.Select(ipToSpatialFilter,
                                                esriSelectionType.esriSelectionTypeHybrid,
                                                esriSelectionOption.esriSelectionOptionNormal, null);
                    int lFromAttrSelectionCount, lToAttrSelectionCount;
                    lFromAttrSelectionCount = ipFromAttrSelectionSet.Count;
                    lToAttrSelectionCount = ipToAttrSelectionSet.Count;

                    if (lFromAttrSelectionCount > 0 && lToAttrSelectionCount > 0)
                    {
                        //起点和终点有属性接边
                        //判断起点和终点的接边对象是否多于一个
                        ICursor ipFromAttrCursor, ipToAttrCursor;
                        ipFromAttrSelectionSet.Search(null, false, out ipFromAttrCursor);
                        ipToAttrSelectionSet.Search(null, false, out ipToAttrCursor);
                        //判断起点

                        IRow ipFromAttrRow = ipFromAttrCursor.NextRow();
                        int nFromAttr = 0;
                        while (ipFromAttrRow != null)
                        {
                            int lFromAttrOID = ipFromAttrRow.OID;
                            if (lFromAttrOID != lSourceRowOID)
                            {
                                nFromAttr++;
                            }
                            ipFromAttrRow = ipFromAttrCursor.NextRow();
                        }

                        //判断终点
                        IRow ipToAttrRow = ipToAttrCursor.NextRow();
                        int nToAttr = 0;
                        while (ipToAttrRow != null)
                        {
                            int lToAttrOID = ipToAttrRow.OID;

                            if (lToAttrOID != lSourceRowOID)
                            {
                                nToAttr++;
                            }
                            ipToAttrRow = ipToAttrCursor.NextRow();
                        }

                        Marshal.ReleaseComObject(ipFromAttrCursor);
                        Marshal.ReleaseComObject(ipToAttrCursor);

                        string sInfo = null;
                        if (nFromAttr > 1 && nToAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            sInfo = sSourceRowOID + "要素起点处有" + nFromAttr + "个接边对象;终点处有" + nToAttr +
                                    "个接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        else if (nFromAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            sInfo = sSourceRowOID + "要素起点处有" + nFromAttr + "个接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        else if (nToAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            sInfo = sSourceRowOID + "要素终点处有" + nToAttr + "个接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                    }
                    else if (lFromAttrSelectionCount > 0)
                    {
                        //起点有属性接边对象,终点没有属性接边对象
                        ICursor ipFromAttrCursor = null;
                        ipFromAttrSelectionSet.Search(null, false, out ipFromAttrCursor);
                        //判断起点
                        IRow ipFromAttrRow = ipFromAttrCursor.NextRow();
                        int nFromAttr = 0;
                        while (ipFromAttrRow != null)
                        {
                            int lFromAttrOID = ipFromAttrRow.OID;

                            if (lFromAttrOID != lSourceRowOID)
                            {
                                nFromAttr++;
                            }
                            ipFromAttrRow = ipFromAttrCursor.NextRow();
                        }

                        Marshal.ReleaseComObject(ipFromAttrCursor);

                        if (nFromAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素起点处有" + nFromAttr + "个接边对象,而且终点处接边对象的属性不同";
                            sCheckInfos.Add(sInfo);
                        }
                        else
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素终点处接边对象的属性不同";
                            sCheckInfos.Add(sInfo);
                        }
                    }
                    else if (lToAttrSelectionCount > 0)
                    {
                        //终点有属性接边对象,起点没有属性接边对象
                        //判断终点
                        ICursor ipToAttrCursor = null;
                        ipToAttrSelectionSet.Search(null, false, out ipToAttrCursor);
                        IRow ipToAttrRow = ipToAttrCursor.NextRow();
                        int nToAttr = 0;
                        while (ipToAttrRow != null)
                        {
                            int lToAttrOID = ipToAttrRow.OID;

                            if (lToAttrOID != lSourceRowOID)
                            {
                                nToAttr++;
                            }
                            ipToAttrRow = ipToAttrCursor.NextRow();
                        }


                        Marshal.ReleaseComObject(ipToAttrCursor);
                        if (nToAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素终点处有" + nToAttr + "个接边对象,而且起点处接边对象的属性不同";
                            sCheckInfos.Add(sInfo);
                        }
                        else
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素起点处接边对象的属性不同";
                            sCheckInfos.Add(sInfo);
                        }
                    }
                    else
                    {
                        sOIDs.Add(sSourceRowOID);
                        //起点和终点都没有属性接边对象
                        string sInfo = sSourceRowOID + "要素起点和终点处有接边对象,但属性不同";
                        sCheckInfos.Add(sInfo);
                    }
                }
                else if (lFromSelectionCount > 0)
                {
                    ipFromSpatialFilter.WhereClause = sMatchEdgeWhereClause;

                    ISelectionSet ipFromAttrSelectionSet =
                        ipFromSelectionSet.Select(ipFromSpatialFilter,
                                                  esriSelectionType.esriSelectionTypeHybrid,
                                                  esriSelectionOption.esriSelectionOptionNormal, null);

                    int lFromAttrSelectionCount = ipFromAttrSelectionSet.Count;

                    if (lFromAttrSelectionCount > 0)
                    {
                        //起点有属性接边对象
                        ICursor ipFromAttrCursor = null;
                        ipFromAttrSelectionSet.Search(null, false, out ipFromAttrCursor);
                        //判断起点
                        IRow ipFromAttrRow = ipFromAttrCursor.NextRow();
                        int nFromAttr = 0;
                        while (ipFromAttrRow != null)
                        {
                            int lFromAttrOID = ipFromAttrRow.OID;


                            if (lFromAttrOID != lSourceRowOID)
                            {
                                nFromAttr++;
                            }
                            ipFromAttrRow = ipFromAttrCursor.NextRow();
                        }

                        Marshal.ReleaseComObject(ipFromAttrCursor);

                        if (nFromAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素起点处有" + nFromAttr + "个接边对象,而且终点处缺少接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        else
                        {
                            if (nFromAttr < 1)
                            {
                                sOIDs.Add(sSourceRowOID);
                                string sInfo = sSourceRowOID + "要素终点处缺少接边对象";
                                sCheckInfos.Add(sInfo);
                            }
                        }
                    }
                    else
                    {
                        sOIDs.Add(sSourceRowOID);
                        string sInfo = sSourceRowOID + "要素起点处有接边对象,但属性不同;而且终点处缺少接边对象";
                        sCheckInfos.Add(sInfo);
                    }
                }
                else if (lToSelectionCount > 0)
                {
                    ipToSpatialFilter.WhereClause = sMatchEdgeWhereClause;

                    ISelectionSet ipToAttrSelectionSet =
                        ipToSelectionSet.Select(ipToSpatialFilter,
                                                esriSelectionType.esriSelectionTypeHybrid,
                                                esriSelectionOption.esriSelectionOptionNormal, null);

                    int lToAttrSelectionCount = ipToAttrSelectionSet.Count;

                    if (lToAttrSelectionCount > 0)
                    {
                        //终点有属性接边对象
                        ICursor ipToAttrCursor = null;
                        ipToAttrSelectionSet.Search(null, false, out ipToAttrCursor);
                        //判断终点
                        IRow ipToAttrRow = ipToAttrCursor.NextRow();
                        int nToAttr = 0;
                        while (ipToAttrRow != null)
                        {
                            int lToAttrOID = ipToAttrRow.OID;

                            if (lToAttrOID != lSourceRowOID)
                            {
                                nToAttr++;
                            }
                            ipToAttrRow = ipToAttrCursor.NextRow();
                        }


                        Marshal.ReleaseComObject(ipToAttrCursor);

                        if (nToAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素终点处有" + nToAttr + "个接边对象,而且起点处缺少接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        else
                        {
                            if (nToAttr < 1)
                            {
                                sOIDs.Add(sSourceRowOID);
                                string sInfo = sSourceRowOID + "%s要素起点处缺少接边对象";
                                sCheckInfos.Add(sInfo);
                            }
                        }
                    }
                    else
                    {
                        sOIDs.Add(sSourceRowOID);
                        string sInfo = sSourceRowOID + "%s要素终点处有接边对象,但属性不同;而且起点处缺少接边对象";
                        sCheckInfos.Add(sInfo);
                    }
                }
                else
                {
                    if (bIsFromOver == false && bIsToOver == false)
                    {
                        sOIDs.Add(sSourceRowOID);
                        //没有图形接边
                        string sInfo = sSourceRowOID + "要素起点和终点处都缺少对应的接边对象";
                        sCheckInfos.Add(sInfo);
                    }
                }
                if (ipFromSelectionSet != null)
                {
                    Marshal.ReleaseComObject(ipFromSelectionSet);
                }
                if (ipToSelectionSet != null)
                {
                    Marshal.ReleaseComObject(ipToSelectionSet);
                }
                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                Marshal.ReleaseComObject(ipSourceRow);
                ipSourceRow = ipSourceCursor.NextRow();
                return false;
            }
        }

        /// <summary>
        /// 只有起点需要接边对象
        /// </summary>
        /// <param name="bIsFromOver"></param>
        /// <param name="bIsToOver"></param>
        /// <param name="ipToSpatialFilter"></param>
        /// <param name="ipFromSpatialFilter"></param>
        /// <param name="ipSourceRow"></param>
        /// <param name="ipSourceCursor"></param>
        /// <param name="ipTargetSelectionSet"></param>
        /// <param name="sMatchEdgeWhereClause"></param>
        /// <param name="lSourceRowOID"></param>
        /// <param name="sSourceRowOID"></param>
        /// <param name="sOIDs"></param>
        /// <param name="sCheckInfos"></param>
        /// <returns></returns>
        private bool JoinFromPoint(bool bIsFromOver, bool bIsToOver, ISpatialFilter ipToSpatialFilter,
                                   ISpatialFilter ipFromSpatialFilter,
                                   ref IRow ipSourceRow, ICursor ipSourceCursor, ISelectionSet ipTargetSelectionSet,
                                   string sMatchEdgeWhereClause, int lSourceRowOID, string sSourceRowOID,
                                   ref List<string> sOIDs, ref List<string> sCheckInfos)
        {
            try
            {
                //只有起点需要接边对象
                ISelectionSet ipFromSelectionSet = null;
                int lFromSelectionCount;
                if (bIsFromOver == false)
                {
                    try
                    {
                        ipFromSelectionSet =
                            ipTargetSelectionSet.Select(ipFromSpatialFilter, esriSelectionType.esriSelectionTypeHybrid,
                                                        esriSelectionOption.esriSelectionOptionNormal, null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                        return false;
                    }
                    lFromSelectionCount = ipFromSelectionSet.Count;
                }
                else
                {
                    lFromSelectionCount = 0;
                }

                if (lFromSelectionCount > 0)
                {
                    //属性接边
                    ipFromSpatialFilter.WhereClause = sMatchEdgeWhereClause;
                    ISelectionSet ipFromAttrSelectionSet;

                    ipFromAttrSelectionSet =
                        ipFromSelectionSet.Select(ipFromSpatialFilter,
                                                  esriSelectionType.esriSelectionTypeHybrid,
                                                  esriSelectionOption.esriSelectionOptionNormal, null);

                    int lFromAttrSelectionCount = ipFromAttrSelectionSet.Count;

                    if (lFromAttrSelectionCount > 0)
                    {
                        //起点有属性接边对象
                        ICursor ipFromAttrCursor;
                        ipFromAttrSelectionSet.Search(null, false, out ipFromAttrCursor);
                        //判断起点
                        IRow ipFromAttrRow = ipFromAttrCursor.NextRow();
                        int nFromAttr = 0;
                        while (ipFromAttrRow != null)
                        {
                            int lFromAttrOID = ipFromAttrRow.OID;


                            if (lFromAttrOID != lSourceRowOID)
                            {
                                nFromAttr++;
                            }
                            ipFromAttrRow = ipFromAttrCursor.NextRow();
                        }

                        if (ipFromAttrCursor != null)
                        {
                            Marshal.ReleaseComObject(ipFromAttrCursor);
                        }
                        if (nFromAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素起点处有" + nFromAttr + "个接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                        else
                        {
                            if (nFromAttr < 1)
                            {
                                sOIDs.Add(sSourceRowOID);
                                string sInfo = sSourceRowOID + "要素起点处缺少接边对象";
                                sCheckInfos.Add(sInfo);
                            }
                        }
                    }
                    else
                    {
                        sOIDs.Add(sSourceRowOID);
                        //起点没有属性接边对象
                        string sInfo = sSourceRowOID + "要素起点处有接边对象,但属性不同";
                        sCheckInfos.Add(sInfo);
                    }
                }
                else
                {
                    if (bIsFromOver == false)
                    {
                        sOIDs.Add(sSourceRowOID);
                        //
                        string sInfo = sSourceRowOID + "要素起点处没有接边对象";
                        sCheckInfos.Add(sInfo);
                    }
                }
                if (ipFromSelectionSet != null)
                {
                    Marshal.ReleaseComObject(ipFromSelectionSet);
                }

                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                Marshal.ReleaseComObject(ipSourceRow);
                ipSourceRow = ipSourceCursor.NextRow();
                return false;
            }
        }

        /// <summary>
        /// 只有终点需要接边对象
        /// </summary>
        /// <param name="bIsFromOver"></param>
        /// <param name="bIsToOver"></param>
        /// <param name="ipToSpatialFilter"></param>
        /// <param name="ipFromSpatialFilter"></param>
        /// <param name="ipSourceRow"></param>
        /// <param name="ipSourceCursor"></param>
        /// <param name="ipTargetSelectionSet"></param>
        /// <param name="sMatchEdgeWhereClause"></param>
        /// <param name="lSourceRowOID"></param>
        /// <param name="sSourceRowOID"></param>
        /// <param name="sOIDs"></param>
        /// <param name="sCheckInfos"></param>
        /// <returns></returns>
        private bool JoinToPoint(bool bIsFromOver, bool bIsToOver, ISpatialFilter ipToSpatialFilter,
                                 ISpatialFilter ipFromSpatialFilter,
                                 ref IRow ipSourceRow, ICursor ipSourceCursor, ISelectionSet ipTargetSelectionSet,
                                 string sMatchEdgeWhereClause, int lSourceRowOID, string sSourceRowOID,
                                 ref List<string> sOIDs, ref List<string> sCheckInfos)
        {
            try
            {
                int lToSelectionCount;
                ISelectionSet ipToSelectionSet = null;
                if (bIsToOver == false)
                {
                    try
                    {
                        ipToSelectionSet =
                            ipTargetSelectionSet.Select(ipToSpatialFilter,
                                                        esriSelectionType.esriSelectionTypeHybrid,
                                                        esriSelectionOption.esriSelectionOptionNormal, null);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, ex.ToString());
                        Marshal.ReleaseComObject(ipSourceRow);
                        ipSourceRow = ipSourceCursor.NextRow();
                        return false;
                    }
                    lToSelectionCount = ipToSelectionSet.Count;
                }
                else
                {
                    lToSelectionCount = 0;
                }
                if (lToSelectionCount > 0)
                {
                    //属性接边

                    ipToSpatialFilter.WhereClause = sMatchEdgeWhereClause;

                    ISelectionSet ipToAttrSelectionSet =
                        ipToSelectionSet.Select(ipToSpatialFilter,
                                                esriSelectionType.esriSelectionTypeHybrid,
                                                esriSelectionOption.esriSelectionOptionNormal, null);
                    int lToAttrSelectionCount = ipToAttrSelectionSet.Count;

                    if (lToAttrSelectionCount > 0)
                    {
                        //终点有属性接边对象
                        ICursor ipToAttrCursor;
                        ipToAttrSelectionSet.Search(null, false, out ipToAttrCursor);
                        //判断起点
                        IRow ipToAttrRow = ipToAttrCursor.NextRow();
                        int nToAttr = 0;
                        while (ipToAttrRow != null)
                        {
                            int lToAttrOID = ipToAttrRow.OID;


                            if (lToAttrOID != lSourceRowOID)
                            {
                                nToAttr++;
                            }
                            ipToAttrRow = ipToAttrCursor.NextRow();
                        }


                        Marshal.ReleaseComObject(ipToAttrCursor);

                        if (nToAttr > 1)
                        {
                            sOIDs.Add(sSourceRowOID);
                            string sInfo = sSourceRowOID + "要素终点处有" + nToAttr + "个接边对象";
                            sCheckInfos.Add(sInfo);
                        }
                    }
                    else
                    {
                        sOIDs.Add(sSourceRowOID);
                        //终点没有属性接边对象
                        string sInfo = sSourceRowOID + "要素终点处有接边对象,但属性不同";
                        sCheckInfos.Add(sInfo);
                    }
                }
                else
                {
                    if (bIsToOver == false)
                    {
                        sOIDs.Add(sSourceRowOID);
                        //
                        string sInfo = sSourceRowOID + "要素终点处没有接边对象";
                        sCheckInfos.Add(sInfo);
                    }
                }
                if (ipToSelectionSet != null)
                {
                    Marshal.ReleaseComObject(ipToSelectionSet);
                }
                return true;
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                Marshal.ReleaseComObject(ipSourceRow);
                ipSourceRow = ipSourceCursor.NextRow();
                return false;
            }
        }



    }
}