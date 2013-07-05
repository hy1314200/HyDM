using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Hy.Check.Define;

using System.Runtime.InteropServices;

namespace Hy.Check.Rule
{
    /// <summary>
    /// 该规则是判断满足某一条件的某一图层的要素是否和另一图层的要素重合
    /// </summary>
    public class RuleConditionCoincide : BaseRule
    {
        public override string Name
        {
            get { return "条件空间重合质检规则"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {
            m_structPara = new RuleExpression.CONDITIONCOINCIDEPARA();
        }

        public override bool Verify()
        {
            if (m_structPara.strFtName == "" || m_structPara.strFtName2 == "" || m_structPara.strWhereClause == "")
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的检查目标或检查表达式不存在，无法执行检查!");
                return false;
            }

            //先取得要进行空间关系查询的ILayer
            IFeatureWorkspace ipFtWS = (IFeatureWorkspace)m_BaseWorkspace;

            //得到目标图层和关系图层的featureclass
            try
            {
                pSrcFeatClass = ipFtWS.OpenFeatureClass(m_structPara.strFtName);
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的图层“" + m_structPara.strFtName + "”不存在,无法执行检查!");
                return false;
            }

            try
            {
                pRelFeatClass = ipFtWS.OpenFeatureClass(m_structPara.strFtName2);
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.VerifyError, "当前工作数据库的图层“" + m_structPara.strFtName2 + "”不存在,无法执行检查!");
                return false;
            }

            return true;
        }

        private IFeatureClass pSrcFeatClass = null;
        private IFeatureClass pRelFeatClass = null;
        public override bool Check(ref List<Error> checkResult)
        {
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = m_structPara.strWhereClause;
            //满足条件查询
            IFeatureCursor ipFeatCursor = pSrcFeatClass.Search(pQueryFilter, true);
            IFeature ipFeature = ipFeatCursor.NextFeature();
            IGeometryCollection pGeometryCollection = new GeometryBagClass();
            ///获取满足该条件的geometry
            while (ipFeature != null)
            {
                IGeometry ipGeometry = ipFeature.Shape;
                if (ipGeometry == null)
                {
                    ipFeature = ipFeatCursor.NextFeature();
                    continue;
                }
                object Missing = Type.Missing;
                pGeometryCollection.AddGeometry(ipGeometry, ref Missing, ref Missing);

                ipFeature = ipFeatCursor.NextFeature();
            }

            ISpatialIndex pSpatialIndex = (ISpatialIndex)pGeometryCollection;
            pSpatialIndex.AllowIndexing = true;
            pSpatialIndex.Invalidate();

            ///构建两个图层地物重叠的空间查询
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelOverlaps;
            ///将大的GeometryCollection放入spatialfilter
            pSpatialFilter.Geometry = (IGeometry)pGeometryCollection;
            string Fields = "OBJECTID,Shape";
            pSpatialFilter.SubFields = Fields;

            IFeatureCursor ipResultFtCur = pRelFeatClass.Search(pSpatialFilter, true);

            //保存数据 
            List<Error> pRuleResult = new List<Error>();
            AddResult(ref pRuleResult, ipResultFtCur);

            checkResult = pRuleResult;

            if (ipResultFtCur != null)
            {
                Marshal.ReleaseComObject(ipResultFtCur);
                ipResultFtCur = null;
            } if (pSrcFeatClass != null)
            {
                Marshal.ReleaseComObject(pSrcFeatClass);
                pSrcFeatClass = null;
            }
            if (pRelFeatClass != null)
            {
                Marshal.ReleaseComObject(pRelFeatClass);
                pRelFeatClass = null;
            }
            return true;

        }
     

        /// <summary>
        /// 条件重合质检规则结构体
        /// </summary>
        private RuleExpression.CONDITIONCOINCIDEPARA m_structPara = new RuleExpression.CONDITIONCOINCIDEPARA();

       

        /// <summary>
        ///  获取检查结果
        /// </summary>
        /// <param name="pRuleResult"></param>
        /// <param name="pFeatCursor"></param>
        private void AddResult(ref List<Error> pRuleResult, IFeatureCursor pFeatCursor)
        {
            if (pFeatCursor == null)
            {
                return;
            }

            try
            {
                IFeature ipFeature = pFeatCursor.NextFeature();
                while (ipFeature != null)
                {
                    // 添家结果记录
                    Error pResInfo = new Error();
                    pResInfo.DefectLevel = this.m_DefectLevel;
                    pResInfo.RuleID = this.InstanceID;

                    // OID
                    pResInfo.OID = ipFeature.OID;
                    // 目标图层
                    pResInfo.LayerName = m_structPara.strFtName2;
                    pResInfo.Description =m_structPara.strErrorReason;
                    pRuleResult.Add(pResInfo);

                    ipFeature = pFeatCursor.NextFeature();
                }
            }
            catch (Exception ex)
            {
                SendMessage(enumMessageType.Exception, ex.ToString());
                return;
            }
        }



    }
}