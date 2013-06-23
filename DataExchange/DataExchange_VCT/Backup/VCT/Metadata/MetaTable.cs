using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DIST.DGP.DataExchange.VCT;
using System.Collections;

namespace DIST.DGP.DataExchange.VCT.Metadata
{
    /// <summary>
    /// 元数据属性部分数据表
    /// </summary>
    internal class MetaTable
    {
        #region 属性

        private List<string> m_LinkFeatureCode =new List<string>();
        public List<string> LinkFeatureCode
        {
            get
            {
                return m_LinkFeatureCode;
            }
         }

        private string m_strTableName = "";
        ///表名
        public string TableName
        {
            get
            {
                return m_strTableName;
            }
        }

        private string m_strFeatureCode = "";
        //要素代码
        public string FeatureCode
        {
            get
            {
                return m_strFeatureCode;
            }

        }

        private string m_strType;
        /// <summary>
        /// 要素图形类型
        /// </summary>
        public string Type
        {
            get
            {
                return m_strType;
            }

        }

        private string m_strAtrTableName;
        /// <summary>
        /// 属性表名
        /// </summary>
        public string AtrTableName
        {
            get
            {
                return m_strAtrTableName;
            }
        }

        private string m_strLimit;
        /// <summary>
        /// 约束条件
        /// </summary>
        public string Limit
        {
            get
            {
                return m_strLimit;
            }
        }

        private string m_strClass;
        /// <summary>
        /// 要素分类
        /// </summary>
        public string ClassType
        {
            get
            {
               return m_strClass;
            }
        }

        private string m_strEntityIDFiledName;
        /// <summary>
        /// 标识码字段名称(BSM)
        /// </summary>
        public string EntityIDFiledName
        {
            get
            {
                return m_strEntityIDFiledName;
            }
        }
        private string m_strYSDMFiledName;
        /// <summary>
        /// 要素代码字段名称(SYDM)
        /// </summary>
        public string YSDMFiledName
        {
            get
            {
                return m_strYSDMFiledName;
            }
        }

        private string m_strGraPerformance;
        /// <summary>
        /// 图形表现编码
        /// </summary>
        public string GraphPerformance
        {
            get
            {
             return   m_strGraPerformance;
            }
            set
            {
                m_strGraPerformance = value;
            }
        }
        private List<MetaDataField> m_pMetaDataFilds;
        /// <summary>
        /// 
        /// </summary>
        public List<MetaDataField> MetaDataFields
        {
            get
            {
                if (m_pMetaDataFilds == null)
                    m_pMetaDataFilds = new List<MetaDataField>();
                return m_pMetaDataFilds;
            }
            set
            {
                m_pMetaDataFilds = value;
            }
        }
        #endregion

        public bool StructMetaTableByXML(XmlNode pTable, EnumDBStandard pDBStandard,List<string> pList)
        {
            string sDBStandard = "";
            switch (pDBStandard)
            {
                case EnumDBStandard.XJBZ:
                    sDBStandard = "XJBZ";
                    break;
                case EnumDBStandard.SJBZ:
                    sDBStandard = "SJBZ";
                    break;
                case EnumDBStandard.XZJBZ:
                    sDBStandard = "XZJBZ";
                    break;
                case EnumDBStandard.ALL:
                default:
                    break;
            }
            XmlAttributeCollection pAttributeCollection = pTable.Attributes;
            m_pMetaDataFilds = new List<MetaDataField>();
            ///如果没有属性集合则返回
            if (pAttributeCollection == null || pAttributeCollection.Count <= 0)
                return false;

            ///获取表名称
            XmlAttribute pTableAttribute = pAttributeCollection["Name"];
            if (pTableAttribute != null)
                this.m_strTableName = pTableAttribute.Value;

            ///获取要素代码
            pTableAttribute = pAttributeCollection["Code"];
            if (pTableAttribute != null)
                this.m_strFeatureCode = pTableAttribute.Value;

            ///获取要素类型
            pTableAttribute = pAttributeCollection["GeometryType"];
            if (pTableAttribute != null)
                this.m_strType = pTableAttribute.Value;

            ///获取属性表名称
            pTableAttribute = pAttributeCollection["AtrTBName"];
            if (pTableAttribute != null)
                this.m_strAtrTableName = pTableAttribute.Value;
            ///获取约束条件 与分类 暂时不需要

            ///2011-9-11添加过滤条件
            if (pList != null && !pList.Contains(m_strAtrTableName))
            {
                return false;
            }
            ///获取图形表现编码
            pTableAttribute = pAttributeCollection["GraphPerformance"];
            if (pTableAttribute != null)
                this.m_strGraPerformance = pTableAttribute.Value;

            for (int i=0;i<pTable.ChildNodes.Count;i++)
            {
                XmlNode pTableSubNode =pTable.ChildNodes[i];
                //处理字段信息
                if (pTableSubNode.Name == "FIELDS")
                {
                    ///读表格属性信息构造配置表对象
                    foreach (XmlNode pFiled in pTableSubNode.ChildNodes)
                    {
                        XmlAttribute pVersion = pFiled.Attributes["Versions"];
                        if (pVersion != null && !pVersion.Value.ToString().Contains(sDBStandard))
                        {
                            ////如果该表不属于当前级别的数据库标准
                            continue;
                        }
                        else
                        {
                            ///获取字段信息
                            MetaDataField pMetaDataField = GetMetaTableFieldByXMLAtr(pFiled.Attributes);
                            if (pMetaDataField != null)
                            {
                                this.m_pMetaDataFilds.Add(pMetaDataField);
                                if (pMetaDataField.FiledType == EnumFieldType.EntityID)
                                    m_strEntityIDFiledName = pMetaDataField.Code;
                                else if (pMetaDataField.FiledType == EnumFieldType.YSDM)
                                    m_strYSDMFiledName = pMetaDataField.Code;
                            }
                        }
                    }
                }
                 //处理关联要素信息
                else if (pTableSubNode.Name == "TABLELINKS")
                {
                    foreach (XmlNode pTableLinkItem in pTableSubNode.ChildNodes)
                    {
                       XmlAttribute pTableLinkNameAttr = pTableLinkItem.Attributes["TableName"];
                       XmlAttribute pTableLinkCodeAttr = pTableLinkItem.Attributes["Code"];
                       if (pTableLinkNameAttr != null && pTableLinkCodeAttr != null)
                            {
                                m_LinkFeatureCode.Add(pTableLinkCodeAttr.Value);
                       }
                    }
                }
            }

            return true;
        }

        public string GetFiledALisNameByCode(string strCode)
        {
            try
            {
                foreach (MetaDataField item in MetaDataFields)
                {
                    if (item.Code == strCode)
                    {
                        return item.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.WriteLog("未能获取" + m_strTableName+"下的字段【"+strCode+"】的字段名称！");
            }
            return "";
        }

        /// <summary>
        /// 获取表中的字段信息（没有做版本判断）
        /// </summary>
        /// <param name="pFieldAtrrCollection"></param>
        /// <returns></returns>
        private MetaDataField GetMetaTableFieldByXMLAtr(XmlAttributeCollection pFieldAtrrCollection)
        {
            if (pFieldAtrrCollection == null || pFieldAtrrCollection.Count <= 0)
                return null;
            MetaDataField pMetaDataField = new MetaDataField();

            ///获取表名称
            XmlAttribute pAttribute = pFieldAtrrCollection["Name"];
            if (pAttribute != null)
                pMetaDataField.Name = pAttribute.Value;

            ///获取要素代码
            pAttribute = pFieldAtrrCollection["Code"];
            if (pAttribute != null)
                pMetaDataField.Code = pAttribute.Value;

            ///获取字段类型
            pAttribute = pFieldAtrrCollection["Type"];
            if (pAttribute != null)
                pMetaDataField.Type = pAttribute.Value;

            ///获取字段长度
            pAttribute = pFieldAtrrCollection["Length"];
            if (pAttribute != null)
            {
                if (pAttribute.Value != "" && pAttribute.Value != null)
                {
                    int nLength=-1;
                    if (VCTFile.ConvertToInt32(pAttribute.Value, out nLength))
                        pMetaDataField.Length = nLength;
                }
            }

            ///获取字段精度
            pAttribute = pFieldAtrrCollection["Precision"];
            if (pAttribute != null)
            {
                int nPresion = -1;
                if (pAttribute.Value != "" && pAttribute.Value != null)
                {
                    if (VCTFile.ConvertToInt32(pAttribute.Value,out nPresion))
                        pMetaDataField.Presion = nPresion;
                }
            }

            ///获取要素代码字段
            pAttribute = pFieldAtrrCollection["FieldType"];
            if (pAttribute != null)
            {
                EnumFieldType pEnumType = EnumFieldType.Other;
                switch (pAttribute.Value)
                {
                    case "bsm":
                        pEnumType = EnumFieldType.EntityID;
                        break;
                    case "ysdm":
                        pEnumType = EnumFieldType.YSDM;
                        break;
                    case "other":
                        pEnumType = EnumFieldType.Other;
                        break;
                    default:
                        break;
                }
                pMetaDataField.FiledType = pEnumType;
            }

            ///获取约束条件 与值域 对照表 暂时不需要
            return pMetaDataField;
        }
    }
    /// <summary>
    /// 数据标准类型
    /// </summary>
    public enum EnumDBStandard
    {
        /// <summary>
        /// 初始空值
        /// </summary>
        None,

        /// <summary>
        /// 县级标准
        /// </summary>
        XJBZ,

        /// <summary>
        ///市级标准 
        /// </summary>
        SJBZ,

        /// <summary>
        /// 乡镇级标准
        /// </summary>
        XZJBZ,

        /// <summary>
        ///各级标准都适用 
        /// </summary>
        ALL
    }
}