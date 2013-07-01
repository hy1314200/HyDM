using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Check.Define;

namespace Check.Rule
{
    /// <summary>
    /// 2012-02-20 张航宇 添加注释
    /// 当“容差”尾数不为1（如0.02）时，本类的处理结果将会少报错误
    /// 另外，当本规则作用于不同空间参考（或者当空间参考的单位不同时）的数据时，请修改系统库的“容差”值
    /// </summary>
    public class RulePointDist : BaseRule
    {

        /// <summary>
        /// OID与Feature对照哈希表
        /// </summary>
        private Hashtable m_MapOIDFeature1 = new Hashtable();

        /// <summary>
        /// OID与Feature对照哈希表
        /// </summary>
        private Hashtable m_MapOIDFeature2 = new Hashtable();

        /// <summary>
        /// 坐标值与OID的对照哈希表，坐标值采用“x_y”的方式，x、y均在整数位截断
        /// </summary>
        private Hashtable m_PointtoOID = new Hashtable();

        /// <summary>
        /// 重复点对之间的键值哈希表,key值为“x_y”，value值为x_y重复的oid字符串
        /// </summary>
        private Hashtable m_RepeatOIDtoOID = new Hashtable();

        ///// <summary>
        ///// 源图层的几何类型 
        ///// </summary>
        //private int m_nGeoType;

        ///// <summary>
        ///// 由线和面打散的点图层的名称
        ///// </summary>
        //private string m_PointLayerName;

        /// <summary>
        ///  缓冲区图层 
        /// </summary>
        private IFeatureClass m_ipBufferFeaCls;

        /// <summary>
        /// 结果
        /// </summary>
        private List<PointDistInfo> m_aryResult; //C++中为CArray<PointDistInfo,PointDistInfo> m_aryResult        


        ///// <summary>
        ///// 缓冲区图层名
        ///// </summary>
        //private string m_strBufferLayer;

        private RuleExpression.LRPointDistPara m_pPara;

        private string m_strSrcLayer = null;
        private string m_strName;
        private IFeatureClass pSrcFeatClass = null;

        public struct PointDistInfo
        {
            public int OID1;
            public int OID2;
            public double dDistance;
        } ;

        public struct PointXY
        {
            public double dx;
            public double dy;
        } ;


        /// <summary>
        /// Initializes a new instance of the <see cref="RulePointDist"/> class.
        /// </summary>
        public RulePointDist()
        {
            m_pPara = null;
            m_strName = "两点距离质检规则";
        }

        /// <summary>
        /// 获取图层的几何类型
        /// </summary>
        /// <param name="ipSrcFeaCls"></param>
        /// <returns></returns>
        private int GetLayerGeoType(IFeatureClass ipSrcFeaCls)
        {
            int nGeoType = 0;

            esriGeometryType Type = ipSrcFeaCls.ShapeType;
            switch (Type)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    nGeoType = 1;
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    nGeoType = 2;
                    break;
                case esriGeometryType.esriGeometryRing:
                case esriGeometryType.esriGeometryPolygon:
                case esriGeometryType.esriGeometryEnvelope:
                    nGeoType = 3;
                    break;
                default:
                    break;
            }

            return nGeoType;
        }


    
        /// <summary>
        /// 搜索位于缓冲区中的点要素- 使用ISpatialFilter
        /// </summary>
        /// <param name="ipPtFeaCls">点图层</param>
        /// <param name="ipBufferFeaCls">缓冲区图层</param>
        /// <returns></returns>
        private bool SearchSpatialFilter(IFeatureClass ipPtFeaCls,
                                         IFeatureClass ipBufferFeaCls)
        {
            if (ipBufferFeaCls == null || ipPtFeaCls == null) return false;

            ISpatialFilter pSpatialFilter = new SpatialFilterClass(); //(CLSID_SpatialFilter);

            // 1. 设置空间关系
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;


            // 2.设置过滤几何
            string shapeFieldName = ipPtFeaCls.ShapeFieldName;
            pSpatialFilter.GeometryField = shapeFieldName;


            // 结果cursor
            IFeatureCursor ipResultFtCur = null;

            // 遍历源图层要素
            IFeatureCursor ipFeatCursor;
            IFeature ipFeature;

            IQueryFilter pQueryFilter = new QueryFilterClass(); //(CLSID_QueryFilter);
            string SubFields = "OID,Shape";
            pQueryFilter.SubFields = SubFields;
            ipFeatCursor = ipBufferFeaCls.Search(pQueryFilter, true);
            ipFeature = ipFeatCursor.NextFeature();

            m_aryResult = new List<PointDistInfo>();
            while (ipFeature != null)
            {
                IGeometry ipGeometry1, ipGeometry2;
                int OID1, OID2;
                ipGeometry1 = ipFeature.Shape;
                OID1 = ipFeature.OID;

                if (ipGeometry1 == null)
                {
                    ipFeature = ipFeatCursor.NextFeature();
                    continue;
                }

                pSpatialFilter.Geometry = ipGeometry1;

                if (ipResultFtCur != null)
                {
                    Marshal.ReleaseComObject(ipResultFtCur);
                }
                ipResultFtCur = ipPtFeaCls.Search(pSpatialFilter, true);


                // 添加到结果数组
                if (ipResultFtCur == null)
                {
                    ipFeature = ipFeatCursor.NextFeature();
                    continue;
                }

                IFeature ipResFeature = ipResultFtCur.NextFeature();


                while (ipResFeature != null)
                {
                    OID2 = ipResFeature.OID;

                    // ID相同，说明是同一个点。ID1:缓冲区；ID2:包含在缓冲区中的点
                    if (OID1 == OID2)
                    {
                        ipResFeature = ipResultFtCur.NextFeature();
                        continue;
                    }

                    // 添家结果记录
                    PointDistInfo PtInfo;
                    // OID1
                    PtInfo.OID1 = OID1;
                    // OID2
                    PtInfo.OID2 = OID2;

                    IFeature ipFeat = null;
                    //if (OID1 == 58)
                    //{
                    //   //XtraMessageBox.Show("RulePointDist:SearchSpatialFiler :OID为58的地物无法获取，请检查原因！");
                    //   break;

                    //}

                    try
                    {
                        ipFeat = ipPtFeaCls.GetFeature(OID1);
                    }
                    catch (Exception ex)
                    {
                        SendMessage(enumMessageType.Exception, "获取要素出错，"+ex.ToString());
                        break;
                    }

                    if (ipFeat != null)
                    {
                        IGeometry ipPtGeo1 = ipFeat.Shape;
                        ipGeometry2 = ipResFeature.Shape;

                        // 距离
                        PtInfo.dDistance = CalDist(ipPtGeo1, ipGeometry2);

                        // 是否添加相同点，即是否添加距离为0的点
                        if (m_pPara.bSearchSamePt ||
                            (!m_pPara.bSearchSamePt && PtInfo.dDistance != 0))
                        {
                            m_aryResult.Add(PtInfo);
                        }
                    }

                    ipResFeature = ipResultFtCur.NextFeature();
                }

                ipFeature = ipFeatCursor.NextFeature();
            }

            return true;
        }


        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="ipGeo1"></param>
        /// <param name="ipGeo2"></param>
        /// <returns></returns>
        private double CalDist(IGeometry ipGeo1, IGeometry ipGeo2)
        {
            if (ipGeo1 == null || ipGeo1 == null)
                return -1;

            IPoint ipPt1 = (IPoint)ipGeo1;
            IPoint ipPt2 = (IPoint)ipGeo2;

            if (ipPt1 == null || ipPt2 == null)
                return -1;

            double x1, y1, x2, y2;
            x1 = ipPt1.X;
            y1 = ipPt1.Y;
            x2 = ipPt2.X;
            y2 = ipPt2.Y;

            double dDist = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);

            return Math.Sqrt(dDist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipPtFeaCls"></param>
        /// <returns></returns>
        private bool SearchPoints(IFeatureClass ipPtFeaCls)
        {
            if (ipPtFeaCls == null) return false;
            try
            {
                // 遍历源图层要素
                IQueryFilter ipQuery = new QueryFilterClass();
                ipQuery.SubFields = "OBJECTID,Shape,BSM";
                IFeatureCursor ipFeatCursor = ipPtFeaCls.Search(ipQuery, true);
                IFeature ipFeature = ipFeatCursor.NextFeature();

                IGeometry ipGeometry = null;
                int OID1 = -1;
                m_aryResult = new List<PointDistInfo>();
                int iDecimal = 0;

                int iParmer = GetParameter(m_pPara.dPointDist, out iDecimal);
                while (ipFeature != null)
                {
                    ipGeometry = ipFeature.Shape;
                    OID1 = ipFeature.OID;
                    if (ipGeometry == null)
                    {
                        ipFeature = ipFeatCursor.NextFeature();
                        continue;
                    }
                    else
                    {
                        if (ipGeometry.GeometryType == esriGeometryType.esriGeometryPoint ||
                            ipGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                        {
                            IPoint ipT = (IPoint)ipGeometry;
                            int dx = Convert.ToInt32(Math.Round(ipT.X, iDecimal) * iParmer);
                            int dy = Convert.ToInt32(Math.Round(ipT.Y, iDecimal) * iParmer);
                            string str = dx.ToString() + "_" + dy.ToString();
                            if (!m_PointtoOID.Contains(str))
                            //构建一hashtable，以点的“x_y”坐标组成的字符串为key键，其中，x、y均取一位小数，仅针对两点距离的参数设置"<=0.1"有效,如果“x_y”key值不同，则两点间距离肯定>=0.1
                            {
                                m_PointtoOID.Add(str, OID1);
                            }
                            else
                            {
                                object OID = m_PointtoOID[str];
                                IFeature OriginFeature = ipPtFeaCls.GetFeature((int)OID);
                                IPoint OriginPoint = (IPoint)OriginFeature.Shape;
                                // 两点间距离
                                double dDist = (ipT.X - OriginPoint.X) * (ipT.X - OriginPoint.X) +
                                               (ipT.Y - OriginPoint.Y) * (ipT.Y - OriginPoint.Y);
                                ///查找错误结果集
                                if ((int)OID != OID1 && Math.Round(Math.Sqrt(dDist), 2) < m_pPara.dPointDist)
                                {
                                    //m_OIDtoOID.Add(OID1, OID);

                                    if (!m_RepeatOIDtoOID.Contains(str))
                                    {
                                        string strTemp = OID1.ToString();
                                        m_RepeatOIDtoOID.Add(str, strTemp);
                                    }
                                    else
                                    {
                                        string strTemp = m_RepeatOIDtoOID[str].ToString() + "," + OID1;
                                        m_RepeatOIDtoOID[str] = strTemp;
                                    }

                                    //// 添家结果记录
                                    //PointDistInfo PtInfo = new PointDistInfo();

                                    //PtInfo.dDistance = Math.Round(Math.Sqrt(dDist), 2);

                                    //PtInfo.OID1 = Convert.ToInt32(OID1);
                                    //PtInfo.OID2 = Convert.ToInt32(OID);

                                    //m_aryResult.Add(PtInfo);
                                }
                            }
                        }
                    }
                    ipFeature = ipFeatCursor.NextFeature();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //根据经纬度坐标和地理坐标的不同设置不同的扩大容限
        //2009-714
        private int GetParameter(double par1, out int iDecimal)
        {
            int iReturn = 0;
            int n = 1;
            while (par1 * 10 < 1)
            {
                par1 *= 10;
                n++;
            }
            iDecimal = n;
            iReturn = Convert.ToInt32(Math.Pow(10, iDecimal));
            return iReturn;
        }

        /// <summary>
        /// 直接使用嵌套循环的方法进行搜索，所以说是“笨搜索”
        /// </summary>
        /// <param name="ipPtFeaCls">点图层</param>
        /// <returns></returns>
        private bool SearchStupid(IFeatureClass ipPtFeaCls)
        {
            if (ipPtFeaCls == null) return false;

            // 遍历源图层要素

            IQueryFilter ipQuery = new QueryFilterClass();
            ipQuery.SubFields = "OBJECTID,Shape";
            IFeatureCursor ipFeatCursor = ipPtFeaCls.Search(ipQuery, true);
            IFeature ipFeature = ipFeatCursor.NextFeature();

            IGeometry ipGeometry = null;
            int OID1 = -1;
            string str;

            while (ipFeature != null)
            {
                ipGeometry = ipFeature.Shape;
                OID1 = ipFeature.OID;
                if (ipGeometry == null)
                {
                    ipFeature = ipFeatCursor.NextFeature();
                    continue;
                }
                else
                {
                    if (ipGeometry.GeometryType == esriGeometryType.esriGeometryPoint ||
                        ipGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
                    {
                        PointXY pt = new PointXY();
                        IPoint ipT = (IPoint)ipGeometry;
                        pt.dx = ipT.X;
                        pt.dy = ipT.Y;
                        str = OID1.ToString();
                        m_MapOIDFeature1.Add(str, pt);
                        m_MapOIDFeature2.Add(str, pt);
                    }
                }
                Marshal.ReleaseComObject(ipFeature);
                ipFeature = ipFeatCursor.NextFeature();
            }


            string rsOID, rsOID2;
            PointXY rsGeometry;
            PointXY rsGeometry2;
            m_aryResult = new List<PointDistInfo>();
            foreach (DictionaryEntry dn in m_MapOIDFeature1)
            {
                rsOID = (string)dn.Key;
                rsGeometry = (PointXY)dn.Value;
                foreach (DictionaryEntry dn2 in m_MapOIDFeature2)
                {
                    rsOID2 = (string)dn2.Key;
                    rsGeometry2 = (PointXY)dn2.Value;

                    // 两点间距离
                    double dDist = (rsGeometry2.dx - rsGeometry.dx) * (rsGeometry2.dx - rsGeometry.dx) +
                                   (rsGeometry2.dy - rsGeometry.dy) * (rsGeometry2.dy - rsGeometry.dy);

                    if (rsOID != rsOID2 && Math.Sqrt(dDist) < m_pPara.dPointDist)
                    {
                        // 添家结果记录
                        PointDistInfo PtInfo = new PointDistInfo();

                        PtInfo.dDistance = Math.Round(Math.Sqrt(dDist), 2);

                        PtInfo.OID1 = Convert.ToInt32(rsOID);
                        PtInfo.OID2 = Convert.ToInt32(rsOID2);
                        m_aryResult.Add(PtInfo);
                        break;
                    }
                }
            }


            ClearMap();

            return true;
        }

        /// <summary>
        ///  保存数据
        /// </summary>
        /// <returns></returns>
        private bool SaveData(ref List<Error> pResult)
        {
            try
            {
                string strSql;
                // 判断是图层的几何类型：点、线、面
                int nGeoType = GetLayerGeoType(pSrcFeatClass);
                // 在入口写入目标表名称
                if (nGeoType == 1) // 点
                {
                    strSql = "update LR_ResultEntryRule set TargetFeatClass2='" + m_pPara.strTargetLayer +
                             "|' where RuleInstID='" +
                             this.m_InstanceID + "'";
                    Common.Utility.Data.AdoDbHelper.ExecuteSql(null, strSql);
                }
                //else if (nGeoType == 2) // 线
                //{
                //    strSql = "update LR_ResultEntryRule set TargetFeatClass1='" + m_PointLayerName +
                //             "',TargetFeatClass2='" + m_PointLayerName + "|" + m_pPara.strTargetLayer +
                //             "' where RuleInstID='" + m_RuleInfo.strID + "'";
                //    GT_CARTO.CommonAPI.ado_ExecuteSQL(m_pResultAdoConn, strSql);
                //}
                //else if (nGeoType == 3) // 面
                //{
                //    strSql = "update LR_ResultEntryRule set TargetFeatClass1='" + m_PointLayerName +
                //             "',TargetFeatClass2='" + m_PointLayerName + "|" + m_pPara.strTargetLayer +
                //             "' where RuleInstID='" + m_RuleInfo.strID + "'";
                //    GT_CARTO.CommonAPI.ado_ExecuteSQL(m_pResultAdoConn, strSql);
                //}

                IFields pFields = pSrcFeatClass.Fields;
                int nIndex = pFields.FindField("BSM");

                foreach (DictionaryEntry dn in m_RepeatOIDtoOID)
                {
                    string key = dn.Key.ToString();
                    Error err = new Error();
                    err.DefectLevel = this.m_DefectLevel;
                    err.RuleID = this.InstanceID;
                    err.LayerName = this.m_strSrcLayer;

                    RuleExpression.LRResultInfo pResInfo = new RuleExpression.LRResultInfo();
                    err.OID = Convert.ToInt32(m_PointtoOID[key]);
                    err.ReferOID = dn.Value.ToString();
                    if (nIndex >= 0)
                    {
                        IFeature pFt = pSrcFeatClass.GetFeature(pResInfo.OID);
                        err.BSM =pFt.get_Value(nIndex).ToString();
                        string[] listOID = err.ReferOID.Split(',');
                        string listBSM2 = "";
                        for (int i = 0; i < listOID.Length; i++)
                        {
                            int nOIDTemp = Convert.ToInt32(listOID[i]);
                            IFeature pFt1 = pSrcFeatClass.GetFeature(nOIDTemp);
                            listBSM2 += pFt1.get_Value(nIndex).ToString() + ",";
                        }
                        listBSM2 = listBSM2.Substring(0, listBSM2.Length - 1);
                        err.ReferBSM = listBSM2;
                    }
                    err.Description = string.Format("'{0}'中标识码为'{1}'与标识码为'{2}'的地物相互重叠，实地两相邻点距离不应小于{3}。", m_pPara.strTargetLayer, pResInfo.BSM, pResInfo.BSM2, m_pPara.dPointDist);
                    pResult.Add(err);
                }
            }
            catch (Exception ex)
            {
                //Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                //显示错误信息; 
                //XtraMessageBox.Show("RulePointDist:SaveData():" + ex.Message + " ");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查两个数据点是否重合
        /// </summary>
        /// <param name="srcPt"></param>
        /// <param name="tarPt"></param>
        /// <returns></returns>
        private bool PointIsEqual(PointXY srcPt, PointXY tarPt)
        {
            if (Math.Abs(srcPt.dx - tarPt.dx) < m_pPara.dPointDist && Math.Abs(srcPt.dy - tarPt.dy) < m_pPara.dPointDist)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 清空OID与Feature对照表
        /// </summary>
        private void ClearMap()
        {
            m_MapOIDFeature1.Clear();
            m_MapOIDFeature2.Clear();
        }

        public override string Name
        {
            get { return "两点距离质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {

            m_pPara = new RuleExpression.LRPointDistPara();
            BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();
            m_pPara.dPointDist = pParameter.ReadDouble();
            m_pPara.nSearchType = pParameter.ReadInt32();
            m_pPara.bSearchSamePt = Convert.ToBoolean(pParameter.ReadInt32());


            int nStrSize = sizeof(double) + 3 * sizeof(int);
            Byte[] bb = new byte[nCount1 - nStrSize];
            pParameter.Read(bb, 0, nCount1 - nStrSize);
            string para_str = Encoding.Default.GetString(bb);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_pPara.strAlias = strResult[i++];
            m_pPara.strRemark = strResult[i++];
            m_pPara.strTargetLayer = strResult[i++];
            m_pPara.strStdName = strResult[i++];
            m_pPara.strBufferLayer = strResult[i];

        }

        public override bool Verify()
        {
            IFeatureWorkspace ipFtWS;
            try
            {
                if (m_pPara == null)
                {
                    return false;
                }
                // 工作空间
                ipFtWS = base.m_BaseWorkspace as IFeatureWorkspace;                

                // 获取源图层名称
                m_strSrcLayer=base.GetLayerName(m_pPara.strTargetLayer);
                
                // 打开源图层
                if (!(ipFtWS as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, m_strSrcLayer))
                {
                    return false;
                }

                pSrcFeatClass = ipFtWS.OpenFeatureClass(m_strSrcLayer);
  
                if (pSrcFeatClass == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                return false;
            }
            return true;
        }

        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                SearchPoints(pSrcFeatClass);
                //SearchStupid(pSrcFeatClass);
                checkResult = new List<Error>();

                if (!SaveData(ref checkResult))
                {
                    checkResult = null;
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (pSrcFeatClass != null)
                {
                    Marshal.ReleaseComObject(pSrcFeatClass);
                    pSrcFeatClass = null;
                }
            }
        }
    }
}