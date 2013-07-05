using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Runtime.InteropServices;
using Hy.Check.Rule;
using Hy.Check.Utility;
using Hy.Check.Define;

namespace Rule
{
    public class RuleSpatailOp : BaseRule
    {

        private LRSpatialOpPara m_pPara;

        private string m_strName;

        private string strRelLayer = null;

        private string strSrcLayer = null;

        public RuleSpatailOp()
        {
            m_pPara = new LRSpatialOpPara();
            m_strName = "空间操作质检规则";
        }

        // 获取检查结果
        private void AddResult(ref List<Hy.Check.Define.Error> pRuleResult, IFeatureCursor pFeatCursor)
        {
            if (pFeatCursor == null)
            {
                return;
            }
            try
            {
                IFeature ipFeature = pFeatCursor.NextFeature();

                string strErrMsg = null;
                int OID = -1;

                while (ipFeature != null)
                {
                    // 添家结果记录
                    Error pResInfo = new Error();

                    OID = ipFeature.OID;

                    // OID
                    pResInfo.OID = OID;

                    // 目标图层
                    pResInfo.LayerName = m_pPara.strTargetLayer;

                    // 错误信息
                    string strTemp = "";
                    if (m_pPara.strRemark.Trim() != strTemp && m_pPara.strRemark != null)
                    {
                        strErrMsg = m_pPara.strRemark;
                    }
                    else
                    {
                        strErrMsg = "(" + m_pPara.strTargetLayer + ")<" + m_pPara.strSpatialRel + ">(" +
                                    m_pPara.strRelLayer +
                                    ")";
                    }
                    pResInfo.Description = strErrMsg;

                    pRuleResult.Add(pResInfo);

                    ipFeature = pFeatCursor.NextFeature();
                }
            }
            catch (Exception ex)
            {
                //Hy.Check.Rule.Helper.LogAPI.CheckLog.AppendErrLogs(ex.ToString());
                //XtraMessageBox.Show("RuleSpatialOp:AddResult()"+ex.Message, "提示");
                return;
            }
        }

        public override string Name
        {
            get { return m_strName; }
        }

        public override Hy.Check.Define.IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {

            MemoryStream stream = new MemoryStream(objParamters);
            BinaryReader pParameter = new BinaryReader(stream);

            pParameter.BaseStream.Position = 0;

            int nCount1 = pParameter.ReadInt32();
            m_pPara.dBuffer = pParameter.ReadDouble();
            m_pPara.eSpatialRel = (esriSpatialRelEnum)pParameter.ReadInt32();
            m_pPara.bBuffer = Convert.ToBoolean(pParameter.ReadInt32());
            m_pPara.bCustomRel = Convert.ToBoolean(pParameter.ReadInt32());

            //解析字符串
            int nStrSize = sizeof(double) + 4 * sizeof(int);
            Byte[] bb = new byte[nCount1 - nStrSize];
            pParameter.Read(bb, 0, nCount1 - nStrSize);
            string para_str = Encoding.Default.GetString(bb);
            para_str.Trim();

            string[] strResult = para_str.Split('|');

            int i = 0;
            m_pPara.strAlias = strResult[i++];
            m_pPara.strRemark = strResult[i++];
            m_pPara.strStdName = strResult[i++];
            m_pPara.strTargetLayer = strResult[i++];
            m_pPara.strUnit = strResult[i++];
            m_pPara.strRelLayer = strResult[i++];
            m_pPara.strSpatialRel = strResult[i++];
            m_pPara.strWhereClause = strResult[i];

            return ;
        }

        public override bool Verify()
        {
            //根据别名取featureclass的名字
            int standardID = SysDbHelper.GetStandardIDBySchemaID(this.m_SchemaID);
            strSrcLayer = LayerReader.GetNameByAliasName(m_pPara.strTargetLayer, standardID);
            strRelLayer = LayerReader.GetNameByAliasName(m_pPara.strRelLayer, standardID);

            if (this.m_BaseWorkspace != null)
            {
                if (!(this.m_BaseWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strSrcLayer))
                {
                    string strLog = "当前工作数据库的关系图层" + strSrcLayer + "不存在,无法执行检查!";
                    SendMessage(enumMessageType.RuleError, strLog);
                    return false;
                }

                if (!(this.m_BaseWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strSrcLayer))
                {
                    string strLog = "当前工作数据库的目标图层" + strRelLayer + "不存在,无法执行检查!";
                    SendMessage(enumMessageType.RuleError, strLog);
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Check(ref List<Hy.Check.Define.Error> checkResult)
        {
            //得到目标图层和关系图层的featureclass
            IFeatureClass pSrcFeatClass = null;
            IFeatureClass pRelFeatClass = null;
            IFeatureCursor ipFeatCursor = null;
            checkResult = new List<Error>();
            try
            {

                string shapeFieldName;
                IFeature ipFeature;
                ISpatialFilter pSpatialFilter = new SpatialFilterClass(); //(CLSID_SpatialFilter);

                //先取得要进行空间关系查询的ILayer
                IFeatureWorkspace ipFtWS = null;

                ipFtWS = (IFeatureWorkspace)this.m_BaseWorkspace;
                    
                pSrcFeatClass = ipFtWS.OpenFeatureClass(strSrcLayer);
                pRelFeatClass = ipFtWS.OpenFeatureClass(strRelLayer);

                // 1. 设置空间关系
                if (!m_pPara.bCustomRel) // 使用engine预定义的空间关系
                {
                    pSpatialFilter.SpatialRel = m_pPara.eSpatialRel;
                }
                else //自定义空间关系
                {
                    pSpatialFilter.SpatialRelDescription = m_pPara.strSpatialRel;
                }

                // 2.设置过滤几何
                shapeFieldName = pSrcFeatClass.ShapeFieldName;
                pSpatialFilter.GeometryField = shapeFieldName;

                // 3.设置选择先后关系，虽然对PGDB不适用，但是我也放在这儿试试
                // Sets the order in which spatial searches are applied by the RDBMS (ArcSDE). 
                //pSpatialFilter.put_SearchOrder(esriSearchOrderSpatial);

                // 4.设置where语句
                if (m_pPara.strWhereClause.Length > 0)
                //hehy注释，2008年2月1日，将该判断条件改为pSpatialFilter.SpatialRel = m_pPara.eSpatialRel;
                {
                    pSpatialFilter.WhereClause = m_pPara.strWhereClause;
                }

                //pSpatialFilter.SpatialRel = m_pPara.eSpatialRel;
                // 5.目标层生成一个大的GeometryCollection
                IGeometryCollection pGeometryCollection = new GeometryBagClass(); //(CLSID_GeometryBag);
                IQueryFilter pQueryFilter = new QueryFilterClass(); //(CLSID_QueryFilter);
                string SubFields = "Shape";
                pQueryFilter.SubFields = SubFields;
                ipFeatCursor = pRelFeatClass.Search(pQueryFilter, true);

                ipFeature = ipFeatCursor.NextFeature();

                while (ipFeature != null)
                {
                    IGeometry ipGeometry = ipFeature.Shape;
                    if (ipGeometry == null)
                    {
                        ipFeature = ipFeatCursor.NextFeature();
                        continue;
                    }

                    object Missing = Type.Missing;

                    if (!(m_pPara.bBuffer)) //不用缓冲区
                    {
                        pGeometryCollection.AddGeometry(ipGeometry, ref Missing, ref Missing);
                    }
                    else //使用缓冲
                    {
                        ITopologicalOperator ipTopo = (ITopologicalOperator)ipGeometry;
                        ipTopo.Simplify();
                        IGeometry ipGeobuffer = ipTopo.Buffer(m_pPara.dBuffer);
                        pGeometryCollection.AddGeometry(ipGeobuffer, ref Missing, ref Missing);
                    }

                    ipFeature = ipFeatCursor.NextFeature();
                }

                ISpatialIndex pSpatialIndex = (ISpatialIndex)pGeometryCollection;
                pSpatialIndex.AllowIndexing = true;
                pSpatialIndex.Invalidate();

                // 6.将大的GeometryCollection放入spatialfilter
                pSpatialFilter.Geometry = (IGeometry)pGeometryCollection;

                // 7.目标图层中采用生成的spatialfilter进行查询
                IFeatureCursor ipResultFtCur;
                string Fields = "OBJECTID,Shape";
                pSpatialFilter.SubFields = Fields;

                //IQueryFilter queryFilter = new QueryFilterClass();
                //queryFilter = (IQueryFilter) pSpatialFilter;

                ipResultFtCur = pSrcFeatClass.Search(pSpatialFilter, true);

                // 8.保存数据 
                AddResult(ref checkResult, ipResultFtCur);
            }

            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (ipFeatCursor != null)
                {
                    Marshal.ReleaseComObject(ipFeatCursor);
                    ipFeatCursor = null;
                }
                if (pSrcFeatClass != null)
                {
                    Marshal.ReleaseComObject(pSrcFeatClass);
                    pSrcFeatClass = null;
                }
                if (pRelFeatClass != null)
                {
                    Marshal.ReleaseComObject(pRelFeatClass);
                    pRelFeatClass = null;
                }
            }
            return true;
        }
    }

    // 空间选择关系参数
    public class LRSpatialOpPara
    {
        public string strAlias;
        public string strRemark;

        // 标准名称
        public string strStdName;

        // 目标图层
        public string strTargetLayer;

        // 单位
        public string strUnit;

        // 关系图层
        public string strRelLayer;

        // 空间关系名称
        public string strSpatialRel;

        // sql语句where
        public string strWhereClause;

        // 空间关系
        public esriSpatialRelEnum eSpatialRel;

        // 是否使用缓冲区
        public bool bBuffer;

        // 是否自定义空间关系
        public bool bCustomRel;

        // 缓冲区
        public double dBuffer;
    }
}