using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using Hy.Check.Define;

using Hy.Check.Rule.Helper;
using Hy.Check.Utility;

namespace Hy.Check.Rule
{
    public class RuleTopo : BaseRule,ITopologicalRule
    {       

        // 参数数组
        private RuleExpression.LRTopoParas m_psRuleParas;

        public override enumErrorType ErrorType
        {
            get
            {
                return enumErrorType.Topology;
            }
        }

        public override string Name
        {
            get { return "拓扑检查"; }
        }

        public override IParameterSetter GetParameterSetter()
        {
            return null;
        }

        public override void SetParamters(byte[] objParamters)
        {

            try
            {
                BinaryReader pParameter = new BinaryReader(new MemoryStream(objParamters));
                BinaryReader pBinaryReader = pParameter;

                m_psRuleParas = new RuleExpression.LRTopoParas();
                pBinaryReader.BaseStream.Position = 0;

                int nStrSize = pParameter.ReadInt32(); // 字符串总长度
                int origT_size = pParameter.ReadInt32(); // Layer : 原始图层几何类型总长度
                int selT_size = pParameter.ReadInt32(); // Layer : 选中图层几何类型
                int rank_size = pParameter.ReadInt32(); // Rank : 图层的优先级
                int rule_count = pParameter.ReadInt32(); // 规则结构体的个数

                List<int> rule_size = new List<int>(); // 每个规则结构体的长度

                for (int i = 0; i < rule_count; i++)
                {
                    int nRuleSize = pParameter.ReadInt32();
                    rule_size.Add(nRuleSize);
                }

                // LRTopoParas的参数
                m_psRuleParas.dTolerance = pParameter.ReadDouble(); //	容差 

                //解析字符串
                Byte[] bb = new byte[nStrSize];
                pParameter.Read(bb, 0, nStrSize);
                string para_str = Encoding.Default.GetString(bb);
                para_str.Trim();

                // 解析字符串
                ParseTopoPara(para_str, m_psRuleParas);


                // Layer : 原始图层几何类型
                m_psRuleParas.arrayOrigGeoT = new List<int>();
                for (int k = 0; k < origT_size / 4; k++)
                {
                    int orig_geoT = pParameter.ReadInt32();
                    m_psRuleParas.arrayOrigGeoT.Add(orig_geoT);
                }

                int j = 0;

                // Layer : 选中图层几何类型
                m_psRuleParas.arraySeledGeoT = new List<int>();
                for (j = 0; j < (selT_size / 4); j++)
                {
                    int sel_geoT = pParameter.ReadInt32();
                    m_psRuleParas.arraySeledGeoT.Add(sel_geoT);
                }
                // Rank : 图层的优先级
                m_psRuleParas.arrayRanks = new List<int>();
                for (j = 0; j < (rank_size / 4); j++)
                {
                    int rank = pParameter.ReadInt32();
                    m_psRuleParas.arrayRanks.Add(rank);
                }

                // ---------------------------规则------------------------------------
                m_psRuleParas.arrayRules = new List<RuleExpression.LRTopoRule>();
                for (j = 0; j < rule_count; j++)
                {
                    int nRuleSize = rule_size[j];

                    RuleExpression.LRTopoRule pRule = new RuleExpression.LRTopoRule();
                    pRule.lPicID = pParameter.ReadInt32(); // 规则描述图片ID
                    pRule.nGeoTSrc = pParameter.ReadInt32(); // 源图层几何类型
                    pRule.nGeoTTarget = pParameter.ReadInt32(); // 目标图标几何类型
                    pRule.nRankSrc = pParameter.ReadInt32(); // 源图层Rank		
                    pRule.nRankTarget = pParameter.ReadInt32(); // 目标图标Rank
                    pRule.bShowError = Convert.ToBoolean(pParameter.ReadInt32()); // 是否显示错误

                    //解析字符串
                    Byte[] bb1 = new byte[nRuleSize - sizeof(int) * 6];
                    pParameter.Read(bb1, 0, nRuleSize - sizeof(int) * 6);
                    string para_str1 = Encoding.Default.GetString(bb1);
                    para_str1.Trim();

                    string[] strResult1 = para_str1.Split('|');

                    int k = 0;

                    pRule.strSourceLayerName = strResult1[k++];
                    pRule.strTopoRuleName = strResult1[k++];
                    pRule.strTargetLayerName = strResult1[k++];
                    pRule.strRuleDesc = strResult1[k++];
                    pRule.strRuleAliasName = strResult1[k];

                    // 插入规则数组
                    m_psRuleParas.arrayRules.Add(pRule);
                }

                pParameter = pBinaryReader;

                this.m_InstanceName = m_psRuleParas.arrayRules[0].strRuleAliasName;
            }
            catch
            {
            }
        }


        public override bool Verify()
        {
            try
            {
                IWorkspace2 ws2 = m_TopoWorkspace as IWorkspace2;
                m_LayerName = this.m_psRuleParas.arraySeledLayers[0];
                m_LayerName = this.GetLayerName(m_LayerName);

                if (!ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, m_LayerName))
                {
                    Common.Utility.Esri.GPTool gpTool = new Common.Utility.Esri.GPTool();
                    gpTool.CopyFeatureClass(m_BaseWorkspace.PathName + "\\" + m_LayerName, m_TopoWorkspace.PathName + "\\" + this.m_Topology.FeatureDataset.Name + "\\" + m_LayerName);
                }

                if (this.m_psRuleParas.arraySeledLayers.Count > 1)
                {
                    m_ReferLayerName = this.m_psRuleParas.arraySeledLayers[1];
                    m_ReferLayerName = this.GetLayerName(m_ReferLayerName);

                    if (!ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, m_ReferLayerName))
                    {
                        Common.Utility.Esri.GPTool gpTool = new Common.Utility.Esri.GPTool();
                        gpTool.CopyFeatureClass(m_BaseWorkspace.PathName + "\\" + m_ReferLayerName, m_TopoWorkspace.PathName + "\\" + this.m_Topology.FeatureDataset.Name + "\\" + m_ReferLayerName);
                    }
                }
            }
            catch
            {
                SendMessage(enumMessageType.VerifyError, string.Format("图层“{0}”不存在", this.m_psRuleParas.arraySeledLayers[0]));
                return false;
            }

            return true;
        }

        public override bool Pretreat()
        {
            try
            {
                //SendMessage(enumMessageType.RuleError, "正在准备数据");

                IFeatureWorkspace fws = m_TopoWorkspace as IFeatureWorkspace;
                IWorkspace2 ws2 = m_TopoWorkspace as IWorkspace2;

                IFeatureClassContainer fClassContainer = this.m_Topology as IFeatureClassContainer;
                IEnumFeatureClass enFeatureClass = fClassContainer.Classes;
                IFeatureClass fClass = enFeatureClass.Next();
                IFeatureClass fClassRefer = null;
                bool isAdded = false;
                while (fClass != null)
                {
                    IDataset ds = fClass as IDataset;
                    if (ds.Name == m_LayerName)
                    {
                        isAdded = true;
                        break;
                    }
                    fClass = enFeatureClass.Next();
                }
                if (!isAdded)
                {
                    if (!ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, m_LayerName))
                    {
                        SendMessage(enumMessageType.RuleError, string.Format("图层{0}导入拓扑库失败，无法检查", this.m_psRuleParas.arraySeledLayers[0]));
                        return false;
                    }
                    fClass = fws.OpenFeatureClass(m_LayerName);
                    this.m_Topology.AddClass(fClass as IClass, 1, m_psRuleParas.arrayRanks[0], 1, false);
                }

                if (this.m_psRuleParas.arraySeledLayers.Count > 1)
                {

                    enFeatureClass.Reset();
                    fClassRefer = enFeatureClass.Next();
                    isAdded = false;
                    while (fClass != null)
                    {
                        IDataset ds = fClassRefer as IDataset;
                        if (ds.Name == m_ReferLayerName)
                        {
                            isAdded = true;
                            break;
                        }
                        fClassRefer = enFeatureClass.Next();
                    }
                    if (!isAdded)
                    {
                        if (!ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, m_ReferLayerName))
                        {
                            SendMessage(enumMessageType.RuleError, string.Format("图层{0}不存在，无法检查", this.m_psRuleParas.arraySeledLayers[1]));
                            return false;
                        }
                        fClassRefer = fws.OpenFeatureClass(m_ReferLayerName);
                        this.m_Topology.AddClass(fClass as IClass, 1, m_psRuleParas.arrayRanks[1], 1, false);
                    }
                }

                m_TopologylRule = new TopologyRuleClass();
                m_TopologylRule.Name = m_psRuleParas.arrayRules[0].strRuleAliasName;
                m_TopologylRule.OriginClassID = fClass.ObjectClassID;
                if (fClassRefer != null)
                    m_TopologylRule.DestinationClassID = fClassRefer.ObjectClassID;

                m_TopologylRule.AllOriginSubtypes = true; 
                m_TopologylRule.AllDestinationSubtypes = true;
                m_TopologylRule.TopologyRuleType = GetTopologyTypeByName(m_psRuleParas.arrayRules[0].strTopoRuleName);


                (this.m_Topology as ITopologyRuleContainer).AddRule(m_TopologylRule);

                return true;

            }
            catch (Exception exp)
            {
                SendMessage(enumMessageType.PretreatmentError, "加入到拓扑时出错，信息："+exp.Message);
                SendMessage(enumMessageType.Exception, exp.ToString());
                return false;
            }
        }

        private ITopologyRule m_TopologylRule;
        private string m_LayerName;
        private string m_ReferLayerName;

        /// <summary>
        /// 根据拓扑名称获取拓扑类型
        /// </summary>
        /// <param name="strRuleName">拓扑名称</param>
        /// <returns>拓扑类型</returns>
        private esriTopologyRuleType GetTopologyTypeByName(string strRuleName)
        {
            //---------------点要素拓扑规则----------
            if (strRuleName == TopoHelper.TopoName_PointCoveredByLineEndpoint) //1. "点要素必须被线的端点覆盖";
            {
                return esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint;
            }
            else if (strRuleName == TopoHelper.TopoName_PointCoveredByLine) //2. "点必须被线覆盖";
            {
                return esriTopologyRuleType.esriTRTPointCoveredByLine;
            }
            else if (strRuleName == TopoHelper.TopoName_PointProperlyInsideArea) //3. "点必须完全位于面内部";
            {
                return esriTopologyRuleType.esriTRTPointProperlyInsideArea;
            }
            else if (strRuleName == TopoHelper.TopoName_PointCoveredByAreaBoundary) //4. "点必须被面边界覆盖";
            {
                return esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary;
            }

                //---------------线要素拓扑规则---------
            else if (strRuleName == TopoHelper.TopoName_LineNoOverlap) //1. "线层内要素不互相重叠";
            {
                return esriTopologyRuleType.esriTRTLineNoOverlap;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoIntersection) //2. "线层内要素不互相相交";
            {
                return esriTopologyRuleType.esriTRTLineNoIntersection;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoDangles) //3. "线层内要素没有悬挂节点";
            {
                return esriTopologyRuleType.esriTRTLineNoDangles;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoPseudos) //4. "线层内要素没有伪节点";
            {
                return esriTopologyRuleType.esriTRTLineNoPseudos;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoIntersectOrInteriorTouch) //5. "线层内要素不能相交或内部相切";
            {
                return esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoOverlapLine) //6. "线层与线层间要素不相互重叠";
            {
                return esriTopologyRuleType.esriTRTLineNoOverlapLine;
            }
            else if (strRuleName == TopoHelper.TopoName_LineCoveredByLineClass) //7. "线层要素必须被另一线层要素覆盖";
            {
                return esriTopologyRuleType.esriTRTLineCoveredByLineClass;
            }
            else if (strRuleName == TopoHelper.TopoName_LineCoveredByAreaBoundary) //8. "线层必须被面层边界覆盖";
            {
                return esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary;
            }
            else if (strRuleName == TopoHelper.TopoName_LineEndpointCoveredByPoint) //9. "线层要素端点必须被点层覆盖";
            {
                return esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoSelfOverlap) //10. "线层内要素不自重叠";
            {
                return esriTopologyRuleType.esriTRTLineNoSelfOverlap;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoSelfIntersect) //11. "线层内要素不自相交";
            {
                return esriTopologyRuleType.esriTRTLineNoSelfIntersect;
            }
            else if (strRuleName == TopoHelper.TopoName_LineNoMultipart) //12. "线层内要素必须为单部分";
            {
                return esriTopologyRuleType.esriTRTLineNoMultipart;
            }

                //---------------面要素拓扑规则---------
            else if (strRuleName == TopoHelper.TopoName_AreaNoOverlap) //1. "面层内要素不相互重叠";
            {
                return esriTopologyRuleType.esriTRTAreaNoOverlap;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaNoGaps) //2. "面层内要素之间没有缝隙";
            {
                return esriTopologyRuleType.esriTRTAreaNoGaps;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaNoOverlapArea) //3. "面层不和另一面层重叠";
            {
                return esriTopologyRuleType.esriTRTAreaNoOverlapArea;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaCoveredByAreaClass) //6. "面层内要素被另一个面层要素覆盖";
            {
                return esriTopologyRuleType.esriTRTAreaCoveredByAreaClass;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaAreaCoverEachOther) //5. "面层和另一个面层相互覆盖";
            {
                return esriTopologyRuleType.esriTRTAreaAreaCoverEachOther;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaCoveredByArea) //4. "面层必须被另一个面层覆盖";
            {
                return esriTopologyRuleType.esriTRTAreaCoveredByArea;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaBoundaryCoveredByLine) //7. "面层边界被另一个线层覆盖";
            {
                return esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaBoundaryCoveredByAreaBoundary) //8. "面层和另一面层边界一致";
            {
                return esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary;
            }
            else if (strRuleName == TopoHelper.TopoName_AreaContainPoint) //9. "面层包含点层要素";
            {
                return esriTopologyRuleType.esriTRTAreaContainPoint;
            }
            else
            {
                return esriTopologyRuleType.esriTRTAny;
            }
        }

        public override bool Check(ref List<Error> checkResult)
        {
            try
            {
                if (m_TopologylRule == null)
                {
                    SendMessage(enumMessageType.RuleError, "拓扑规则未创建成功");
                    return false;
                }

                checkResult = new List<Error>();
                IEnumTopologyErrorFeature enErrorFeature = (m_Topology as IErrorFeatureContainer).get_ErrorFeatures((m_Topology.FeatureDataset as IGeoDataset).SpatialReference, this.m_TopologylRule, (m_Topology.FeatureDataset as IGeoDataset).Extent, true, true);
                ITopologyErrorFeature errFeature= enErrorFeature.Next();
                while (errFeature != null)
                {
                    TopoError err = new TopoError();
                    err.DefectLevel = this.DefectLevel;
                    err.ErrorType = enumErrorType.Topology;
                    err.LayerName = this.m_psRuleParas.arraySeledLayers[0];
                    err.LayerID= m_TopologylRule.OriginClassID;
                    if (this.m_psRuleParas.arraySeledLayers.Count > 1)
                        err.ReferLayerName = this.m_psRuleParas.arraySeledLayers[1];

                    err.ReferLayerID = m_TopologylRule.DestinationClassID;
                    err.RuleType = (int)m_TopologylRule.TopologyRuleType;
                    err.RuleID = this.m_InstanceID;
                    err.Description = this.InstanceName;
                    err.OID = errFeature.OriginOID;
                    err.ReferOID = errFeature.DestinationOID.ToString();
                    err.JHLX = errFeature.ShapeType;

                    checkResult.Add(err);

                    errFeature = enErrorFeature.Next();
                }

                return true;
            }
            catch(Exception exp)
            {
                SendMessage(enumMessageType.RuleError, "获取拓扑结果失败");
                SendMessage(enumMessageType.OperationalLog, exp.ToString());
                return false;
            }
        }


        private ITopology m_Topology;
        public ITopology Topology
        {
            set { this.m_Topology = value; }
        }

        private Dictionary<string, int> m_DictRank;
        public Dictionary<string, int> RankDictionary
        {
            set
            {
                this.m_DictRank = value;
            }
        }

        

        

        //public override bool Check(ref ICheckResult ppResult)
        //{
        //    return true;

        //    // 将拓扑图层的别名转换为真实名称
        //    InitTopoLayerArray();

        //    CTopoConstruct topoObj = new CTopoConstruct();

        //    // 初始化
        //    string strTopoName = "" + m_strID + "_Topology";

        //    if (!topoObj.Init(ref m_psRuleParas, strTopoName, m_pSonEngineWks))
        //    {
        //        return false;
        //    }

        //    // 构建拓扑
        //    bool state = topoObj.ConstructTopo();
        //    if (state != true)
        //    {
        //        return state;
        //    }

        //    ppResult = null;

        //    return true;
        //}

          

        // 解析拓扑结构里面的参数
        private bool ParseTopoPara(string strTopoPara, RuleExpression.LRTopoParas psRulePara)
        {
            if (strTopoPara == "")
            {
                return false;
            }
            string para_str = strTopoPara;
            m_psRuleParas.arraySeledLayers = new List<string>();
            m_psRuleParas.arrayOrigLayers = new List<string>();

            string para_tmp1, para_tmp2, para_tmp3;

            string[] strList = para_str.Split('#');
            para_tmp1 = strList[0];
            para_tmp2 = strList[1];
            para_tmp3 = strList[2];

            int j = 0;
            string[] strResult = para_tmp1.Split('|');
            m_psRuleParas.strTopoName = strResult[j++]; //拓扑名称
            m_psRuleParas.strStandardName = strResult[j++]; //标准名称
            m_psRuleParas.strSourceLayer = strResult[j]; //待检查图层,即目标图层


            string[] strResult1 = para_tmp2.Split('|');
            for (j = 0; j < strResult1.Length; j++)
            {
                m_psRuleParas.arrayOrigLayers.Add(strResult1[j]);
            }

            string[] strResult2 = para_tmp3.Split('|');
            for (j = 0; j < strResult2.Length; j++)
            {
                if (strResult2[j] != "")
                {
                    m_psRuleParas.arraySeledLayers.Add(strResult2[j]);
                }
            }


            return true;
        }

        private class TopoError : Error
        {
            /// <summary>
            /// SourceLayerID
            /// </summary>
            public int LayerID { get; set; }

            /// <summary>
            /// TargetLayerID
            /// </summary>
            public int ReferLayerID { get; set; }

            /// <summary>
            /// 规则类型（指Esri的拓扑规则）
            /// </summary>
            public int RuleType { get; set; }

            /// <summary>
            /// 几何类型
            /// </summary>
            public ESRI.ArcGIS.Geometry.esriGeometryType JHLX { get; set; }

            public override string ToSQLString()
            {
                StringBuilder strBuilder=new StringBuilder("Insert Into");
                // Fields
                strBuilder.Append(" LR_ResAutoTopo (CheckType,RuleInstID,DefectLevel,IsException,Reason,YSTC,MBTC,TPTC,SourceLayerID,TargetLayerID,SourceOID,TargetOID,JHLX,ArcGisRule) Values(");

                // Values
                strBuilder.Append("'','");
                strBuilder.Append(this.RuleID); strBuilder.Append("',");
                strBuilder.Append((int)this.DefectLevel); strBuilder.Append(",");
                strBuilder.Append(this.IsException); strBuilder.Append(",'");
                strBuilder.Append(this.Description.Replace("'", "''")); strBuilder.Append("','");

                strBuilder.Append(this.LayerName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(this.ReferLayerName.Replace("'", "''")); strBuilder.Append("','");
                strBuilder.Append(COMMONCONST.Topology_Name);strBuilder.Append("',");
                strBuilder.Append(this.LayerID); strBuilder.Append(",");
                strBuilder.Append(this.ReferLayerID); strBuilder.Append(",");
                strBuilder.Append(this.OID); strBuilder.Append(",'");
                strBuilder.Append(this.ReferOID.Replace("'", "''")); strBuilder.Append("',");
                strBuilder.Append((int)this.JHLX);strBuilder.Append(",'");
                strBuilder.Append((int)this.RuleType); strBuilder.Append("'");
                strBuilder.Append(")");

                return strBuilder.ToString();

            }
        }
    }
}