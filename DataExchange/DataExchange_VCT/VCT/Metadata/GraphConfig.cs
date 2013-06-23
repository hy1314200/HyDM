using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DIST.DGP.DataExchange.VCT.Metadata
{
    internal class GraphConfig
    {
       #region 属性
        private List<ConfigInfo> m_PointFeatureType;
        /// <summary>
        /// 点的特征类型
        /// </summary>
        public List<ConfigInfo> PointFeatureType
        {
            get
            {
                return m_PointFeatureType;
            }
        }

        private List<ConfigInfo> m_LineFeatureType;
        /// <summary>
        /// 线的特征类型
        /// </summary>
        public List<ConfigInfo> LineFeatureType
        {
            get
            {
                return m_LineFeatureType;
            }
        }

         private List<ConfigInfo> m_PolygonFeatureType;
        /// <summary>
        /// 面的特征类型
        /// </summary>
        public List<ConfigInfo> PolygonFeatureType
        {
            get
            {
                return m_PolygonFeatureType;
            }
        }

          private List<ConfigInfo> m_AnnotationFeatureType;
        /// <summary>
        /// 注记的特征类型
        /// </summary>
        public List<ConfigInfo> AnnotationFeatureType
        {
            get
            {
                return m_AnnotationFeatureType;
            }
        }

         private List<ConfigInfo> m_StructLineType;
        /// <summary>
        /// 线段的类型
        /// </summary>
        public List<ConfigInfo> StructLineType
        {
            get
            {
                return m_StructLineType;
            }
        }

          private List<ConfigInfo> m_StructPolygonType;
        /// <summary>
        /// 间接坐标面的构成类型
        /// </summary>
        public List<ConfigInfo> StructPolygonType
        {
            get
            {
                return m_StructPolygonType;
            }
        }

        private List<ConfigInfo> m_GraphType;
        /// <summary>
        /// 图形要素的类型（点线面注记栅格）
        /// </summary>
        public List<ConfigInfo> GraphType
        {
            get
            {
                return m_GraphType;
            }
        }
       #endregion
        /// <summary>
        /// 根据当前的图形要素xml节点初始化图形要素信息
        /// </summary>
        /// <param name="pNode"></param>
        /// <returns></returns>
        public bool Initial(XmlNode pNode)
        {
            if(pNode==null)
                return false;
             foreach (XmlNode pHeadInfo in pNode.ChildNodes)
            {
                ///点的特征类型
                if (pHeadInfo.Name == "POINTFEATURETYPE")
                {
                    m_PointFeatureType = new List<ConfigInfo>();
                    foreach (XmlNode pPointNode in pHeadInfo.ChildNodes)
                    {
                        m_PointFeatureType.Add(GetConfigInfo(pPointNode));
                    }
                }

                ///线的特征类型
                if (pHeadInfo.Name == "LINEFEATURETYPE")
                {
                    m_LineFeatureType = new List<ConfigInfo>();
                    foreach (XmlNode pLineNode in pHeadInfo.ChildNodes)
                    {
                        m_LineFeatureType.Add(GetConfigInfo(pLineNode));
                    }
                }

                ///线段类型
                if (pHeadInfo.Name == "LINETYPE")
                {
                    m_StructLineType = new List<ConfigInfo>();
                    foreach (XmlNode pStructLineNode in pHeadInfo.ChildNodes)
                    {
                        m_StructLineType.Add(GetConfigInfo(pStructLineNode));
                    }
                }

                 ///面的特征类型
                if (pHeadInfo.Name == "POLYGONFEATURETYPE")
                {
                    m_PolygonFeatureType = new List<ConfigInfo>();
                    foreach (XmlNode pPolygonNode in pHeadInfo.ChildNodes)
                    {
                        m_PolygonFeatureType.Add(GetConfigInfo(pPolygonNode));
                    }
                }

                ///注记的特征类型
                if (pHeadInfo.Name == "ANNOTATIONFEATURETYPE")
                {
                    m_AnnotationFeatureType = new List<ConfigInfo>();
                    foreach (XmlNode pAnotationNode in pHeadInfo.ChildNodes)
                    {
                        m_AnnotationFeatureType.Add(GetConfigInfo(pAnotationNode));
                    }
                }

                ///间接坐标面的构成类型
                if (pHeadInfo.Name == "POLYGONTYPE")
                {
                    m_StructPolygonType = new List<ConfigInfo>();
                    foreach (XmlNode pStructPolygon in pHeadInfo.ChildNodes)
                    {
                        m_StructPolygonType.Add(GetConfigInfo(pStructPolygon));
                    }
                }

                ///图形要素类型
                if (pHeadInfo.Name == "GRAPHSTYLE")
                {
                    m_GraphType = new List<ConfigInfo>();
                    foreach (XmlNode pGrahpStyleNode in pHeadInfo.ChildNodes)
                    {
                        m_GraphType.Add(GetConfigInfo(pGrahpStyleNode));
                    }
                }

            }

            return true;
        }
        /// <summary>
        /// 解析xml属性获取配置信息
        /// </summary>
        /// <param name="pInfoNode"></param>
        /// <returns></returns>
        private ConfigInfo GetConfigInfo(XmlNode pInfoNode)
        {
            string sSymbol = "";
            string sValue = "";
            string sMark = "";
            XmlAttributeCollection pAttributeCollection = pInfoNode.Attributes;

            ///读取特征属性
            XmlAttribute pAttribute = pAttributeCollection["Symbol"];
            if (pAttribute != null)
                sSymbol = pAttribute.Value;

            ///读取特征说明
            pAttribute = pAttributeCollection["AlisName"];
            if (pAttribute != null)
                sValue = pAttribute.Value;

            ///读取特征值
            pAttribute = pAttributeCollection["Mark"];
            if (pAttribute != null)
                sMark = pAttribute.Value;

            ConfigInfo info = new ConfigInfo(sSymbol, sValue, sMark);
            return info;
        }


        /// <summary>
        /// 根据配置表中图形结点设置的strMark值获取对应的vct命名
        /// </summary>
        /// <param name="sNodeName">所在标准文件的图形节点名称</param>
        /// <param name="strMark">程序中约定的标志值</param>
        /// <returns></returns>
        public  string GetGraphSymbol(string sNodeName, string strMark)
        {
            try
            {
                #region
                ///点的特征类型
                if (sNodeName == "POINTFEATURETYPE")
                {
                    for (int i = 0; i < this.PointFeatureType.Count; i++)
                    {
                        ConfigInfo pPointFeatureType = this.PointFeatureType[i];
                        if (strMark == pPointFeatureType.Mark)
                            return pPointFeatureType.Symbol;
                    }
                }///线的特征类型
                else if (sNodeName == "LINEFEATURETYPE")
                {
                    for (int i = 0; i < this.LineFeatureType.Count; i++)
                    {
                        ConfigInfo pLineFeatureType = this.LineFeatureType[i];
                        if (strMark == pLineFeatureType.Mark)
                            return pLineFeatureType.Symbol;
                    }
                }//线段类型
                else if (sNodeName == "LINETYPE")
                {
                    for (int i = 0; i < this.StructLineType.Count; i++)
                    {
                        ConfigInfo pStructLineType = this.StructLineType[i];
                        if (strMark == pStructLineType.Mark)
                            return pStructLineType.Symbol;
                    }
                }//面的特征类型
                else if (sNodeName == "POLYGONFEATURETYPE")
                {
                    for (int i = 0; i < this.PolygonFeatureType.Count; i++)
                    {
                        ConfigInfo pPolygonFeatureType = this.PolygonFeatureType[i];
                        if (strMark == pPolygonFeatureType.Mark)
                            return pPolygonFeatureType.Symbol;
                    }
                }///间接坐标面的构成类型
                else if (sNodeName == "POLYGONTYPE")
                {
                    for (int i = 0; i < this.StructPolygonType.Count; i++)
                    {
                        ConfigInfo pStructPolygonType = this.StructPolygonType[i];
                        if (strMark == pStructPolygonType.Mark)
                            return pStructPolygonType.Symbol;
                    }
                }///注记的特征类型
                else if (sNodeName == "ANNOTATIONFEATURETYPE")
                {
                    for (int i = 0; i < this.AnnotationFeatureType.Count; i++)
                    {
                        ConfigInfo pAnnotationFeatureType = this.AnnotationFeatureType[i];
                        if (strMark == pAnnotationFeatureType.Mark)
                            return pAnnotationFeatureType.Symbol;
                    }
                }
                return "";
                #endregion
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return "";
            }
        }

        /// <summary>
        /// 根据vct文件中的特征值获取标志
        /// </summary>
        /// <param name="sNodeName">所在标准文件的头文件节点名称</param>
        /// <param name="strSymbol">vct文件中的特征值</param>
        /// <returns></returns>
        public  string GetGraphMark(string sNodeName, string strSymbol)
        {
            try
            {
                #region
                ///点的特征类型
                if (sNodeName == "POINTFEATURETYPE")
                {
                    for (int i = 0; i < this.PointFeatureType.Count; i++)
                    {
                        ConfigInfo pPointFeatureType = this.PointFeatureType[i];
                        if (strSymbol == pPointFeatureType.Symbol)
                            return pPointFeatureType.Mark;
                    }
                }///线的特征类型
                else if (sNodeName == "LINEFEATURETYPE")
                {
                    for (int i = 0; i < this.LineFeatureType.Count; i++)
                    {
                        ConfigInfo pLineFeatureType = this.LineFeatureType[i];
                        if (strSymbol == pLineFeatureType.Symbol)
                            return pLineFeatureType.Mark;
                    }
                }//线段类型
                else if (sNodeName == "LINETYPE")
                {
                    for (int i = 0; i < this.StructLineType.Count; i++)
                    {
                        ConfigInfo pStructLineType = this.StructLineType[i];
                        if (strSymbol == pStructLineType.Symbol)
                            return pStructLineType.Mark;
                    }
                }//面的特征类型
                else if (sNodeName == "POLYGONFEATURETYPE")
                {
                    for (int i = 0; i < this.PolygonFeatureType.Count; i++)
                    {
                        ConfigInfo pPolygonFeatureType = this.PolygonFeatureType[i];
                        if (strSymbol == pPolygonFeatureType.Symbol)
                            return pPolygonFeatureType.Mark;
                    }
                }///间接坐标面的构成类型
                else if (sNodeName == "POLYGONTYPE")
                {
                    for (int i = 0; i < this.StructPolygonType.Count; i++)
                    {
                        ConfigInfo pStructPolygonType = this.StructPolygonType[i];
                        if (strSymbol == pStructPolygonType.Symbol)
                            return pStructPolygonType.Mark;
                    }
                }///注记的特征类型
                else if (sNodeName == "ANNOTATIONFEATURETYPE")
                {
                    for (int i = 0; i < this.AnnotationFeatureType.Count; i++)
                    {
                        ConfigInfo pAnnotationFeatureType = this.AnnotationFeatureType[i];
                        if (strSymbol == pAnnotationFeatureType.Symbol)
                            return pAnnotationFeatureType.Mark;
                    }
                }
                return "";
                #endregion
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
                return "";
            }
        }


        /// <summary>
        /// 获取程序中识别的图形类型
        /// </summary>
        /// <param name="sSymbol"></param>
        /// <returns></returns>
        public  string GetGraphTypeSymbol(string sSymbol)
        {
            foreach (ConfigInfo Configitem in this.GraphType)
            {
                if (Configitem.Symbol == sSymbol)
                    return Configitem.Mark;
            }
            return "";
        }

        /// <summary>
        /// 获取VCT文件中的图形类型
        /// </summary>
        /// <param name="sSymbol"></param>
        /// <returns></returns>
        public  string GetGraphTypeMark(string sMark)
        {
            foreach (ConfigInfo Configitem in this.GraphType)
            {
                if (Configitem.Mark == sMark)
                    return Configitem.Symbol;
            }
            return "";
        }
        
    }
}
