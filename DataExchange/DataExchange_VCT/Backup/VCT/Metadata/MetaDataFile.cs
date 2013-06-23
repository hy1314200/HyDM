using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DIST.DGP.DataExchange.VCT;
using System.Collections;
using System.IO;

namespace DIST.DGP.DataExchange.VCT.Metadata
{
    internal static class MetaDataFile
    {
        private static string m_strPath = Application.StartupPath + "\\StandardDBofLandUsePlan.xml";
        private static XmlDocument m_XMLDoc;

        /// <summary>
        /// 分隔符
        /// </summary>
        public static string Sparator
        {
            get
            {
                return HeadConfig.Sparator.Symbol;
            }
        }

        private static HeadConfig m_HeadConfig;
        /// <summary>
        /// 头文件配置信息
        /// </summary>
        public static HeadConfig HeadConfig
        {
            get 
            {
                return m_HeadConfig;
            }
        }

        private static GraphConfig m_GraphConfig;
        /// <summary>
        /// 图形表现配置信息
        /// </summary>
        public static GraphConfig GraphConfig
        {
            get
            {
                return m_GraphConfig;
            }
        }

        /// <summary>
        /// 初始化xml文档对象
        /// </summary>
        /// <returns></returns>
        public static bool Initial(EnumDBStandard pEnumDBStandard)
        {
            try
            {
                if (m_XMLDoc == null)
                    m_XMLDoc = new XmlDocument();
                m_XMLDoc.Load(m_strPath);
                if (!InitialConfig(m_XMLDoc, pEnumDBStandard))
                    return false;
                m_pMetaTables = GetMetaTablesByName(m_XMLDoc, pEnumDBStandard,null);
                if (m_pMetaTables == null)
                    return false;
                return true;
            }
            catch(Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return false;
            }
        }
       
        /// <summary>
        /// 初始化xml文档对象
        /// </summary>
        /// <param name="pEnumDBStandard">vct数据文件类型</param>
        /// <param name="pFilterList">过滤层集合</param>
        /// <returns></returns>
        public static bool Initial(EnumDBStandard pEnumDBStandard,List<string> pFilterList)
        {
            try
            {
                if (m_XMLDoc == null)
                    m_XMLDoc = new XmlDocument();
                m_XMLDoc.Load(m_strPath);
                if (!InitialConfig(m_XMLDoc, pEnumDBStandard))
                    return false;
                m_pMetaTables = GetMetaTablesByName(m_XMLDoc, pEnumDBStandard, pFilterList);
                if (m_pMetaTables == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return false;
            }
        }
        private static bool InitialConfig(XmlDocument pDoc,EnumDBStandard pEnumDBStandard)
        {

            XmlNode pHeadNode = null;///获取头文件集合节点
            for (int i = 0; i < pDoc.DocumentElement.ChildNodes.Count; i++)
            {
                XmlNode pNode = pDoc.DocumentElement.ChildNodes[i];
                if (pNode.Name == "Description")
                {
                    pHeadNode = pNode;
                    break;
                }
            }
            if (pHeadNode != null)
            {
                foreach (XmlNode pChildNode in pHeadNode.ChildNodes)
                {
                    ///获取头文件配置信息
                    if (pChildNode.Name == "HEAD")
                    {
                        m_HeadConfig = new HeadConfig(pEnumDBStandard);
                        if (!m_HeadConfig.Initial(pChildNode))
                            return false;
                    }
                    ///获取图形配置信息
                    if (pChildNode.Name == "GRAPH")
                    {
                        m_GraphConfig = new GraphConfig();
                        if (!m_GraphConfig.Initial(pChildNode))
                            return false;
                    }
                }
            }
            return true;
        }

        private static Hashtable m_pMetaTables;
        public static Hashtable MetaTabls
        {
            get
            {
                return m_pMetaTables;
            }
        }

        /// <summary>
        /// 根据标准名称获取该标准下的元数据表集合
        /// </summary>
        private static Hashtable GetMetaTablesByName(XmlDocument pDoc, EnumDBStandard pStandard,List<string> pList)
        {
            try
            {
                XmlNode pTablesNode =null;///获取表集合节点
                Hashtable pHashMetaTalbes = new Hashtable();
                for (int i = 0; i < pDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    XmlNode pNode = pDoc.DocumentElement.ChildNodes[i];
                    if (pNode.Name == "TABLES")
                    {
                        pTablesNode = pNode;
                        break;
                    }
                }

                if (pTablesNode != null)
                {

                    string sStandardName = "";
                    switch (pStandard)
                    {
                        case EnumDBStandard.None:
                            break;
                        case EnumDBStandard.XJBZ:
                            sStandardName = "XJBZ";
                            break;
                        case EnumDBStandard.SJBZ:
                            sStandardName = "SJBZ";
                            break;
                        case EnumDBStandard.XZJBZ:
                            sStandardName = "XZJBZ";
                            break;
                        case EnumDBStandard.ALL:
                            break;
                        default:
                            break;
                    }
                    ///遍历配置数据表
                    foreach (XmlNode pTable in pTablesNode.ChildNodes)
                    {
                        ///读表格属性信息构造配置表对象
                        XmlAttribute pVersion =pTable.Attributes["Versions"];
                        if (pVersion != null && !pVersion.Value.ToString().Contains(sStandardName))
                        {
                            ////如果该表不属于当前级别的数据库标准
                            continue;
                        }
                        else
                        {
                            MetaTable pMetaTable = new MetaTable();
                            ///构造成功则添加到集合中
                            if (pMetaTable.StructMetaTableByXML(pTable, pStandard,pList))
                            {
                                pHashMetaTalbes.Add(pMetaTable.AtrTableName,pMetaTable);
                            }
                        }
                    }
                }
                return pHashMetaTalbes;
            }
            catch(Exception ex)
            {
                LogAPI.WriteErrorLog(ex);
                return null;
            }
        }

        
        /// <summary>
        /// 根据配置文件获取标识码字段
        /// </summary>
        /// <returns></returns>
        public static string GetEntityIDFieldName(string sTalbleName)
        {
           MetaTable pMetaTalble= GetMetaTalbleByName(sTalbleName);
           if (pMetaTalble != null)
               return pMetaTalble.EntityIDFiledName;
           else
               return "";
        }

        /// <summary>
        /// 根据配置文件获取标识码字段
        /// </summary>
        /// <returns></returns>
        public static string GetYSDMFieldName(string sTalbleName)
        {
            MetaTable pMetaTalble = GetMetaTalbleByName(sTalbleName);
            if (pMetaTalble != null)
                return pMetaTalble.YSDMFiledName;
            else
                return "";
        }

        /// <summary>
        /// 根据表名称获取配置表结构
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static MetaTable GetMetaTalbleByName(string sTableName)
        {
            try
            {
                return m_pMetaTables[sTableName] as MetaTable;
            }
            catch(Exception ex)
            {
                LogAPI.WriteLog("未能获取【" + sTableName + "】的配置表结构，请检查该表是否在配置表中！");
                return null;
            }
        }

        /// <summary>
        /// 根据表名称获取要素代码
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static string GetFeatureCodeByName(string sTableName)
        {
            try
            {
                return (m_pMetaTables[sTableName] as MetaTable).FeatureCode;
            }
            catch(Exception ex)
            {
                LogAPI.WriteLog("未能获取【" + sTableName + "】的要素代码，请检查该表是否在配置表中！");
                return "";
            }
        }

        /// <summary>
        /// 设置配置文件的路径
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public static bool SetConfigFilePath(string sFilePath)
        {
            if (File.Exists(sFilePath))
            {
                m_strPath = sFilePath;
                return true;
            }
            else
                return false;
        }

        public static void Dispose()
        {
             m_XMLDoc = null;
             m_GraphConfig = null;
             m_HeadConfig = null;
            if(m_pMetaTables!=null)
             m_pMetaTables.Clear();
        }
    }

    /// <summary>
    /// 配置信息结构
    /// </summary>
    public class ConfigInfo
    {
        private string m_strSymbol;
        /// <summary>
        /// 配置特征
        /// </summary>
        public string Symbol
        {
            get
            {
                return m_strSymbol;
            }
        }
        private string m_strValue;
        /// <summary>
        /// 配置用于读取vct的值
        /// </summary>
        public string Value
        {
            get
            {
                return m_strValue;
            }
        }
        public ConfigInfo(string sSymbol, string sValue, string sMark)
        {
            this.m_strSymbol = sSymbol;
            this.m_strValue = sValue;
            this.m_strMark = sMark;
        }

        private string m_strMark;
        /// <summary>
        /// 配置中用于程序识别的值
        /// </summary>
        public string Mark
        {
            get
            {
                return m_strMark;
            }
        }
    }
}
