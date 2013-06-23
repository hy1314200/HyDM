using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIST.DGP.DataExchange.VCT.Metadata
{
    /// <summary>
    /// 元数据表中的字段
    /// </summary>
    internal class MetaDataField
    {
        private string m_strName;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }
        private string m_strCode;
        /// <summary>
        /// 字段代码
        /// </summary>
        public string Code
        {
            get
            {
                return m_strCode;
            }
            set
            {
                m_strCode = value;
            }
        }

        private string m_strType;
        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type
        {
            get
            {
                return m_strType;
            }
            set
            {
                m_strType = value;
            }
        }
        private int m_nLength;
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        {
            get
            {
                return m_nLength;
            }
            set
            {
                m_nLength = value;
            }
        }
        private int m_nPresion;
        /// <summary>
        /// 字段精度
        /// </summary>
        public int Presion
        {
            get
            {
                return m_nPresion;
            }
            set
            {
                m_nPresion = value;
            }
        }
        private string m_strValueRange;
        /// <summary>
        /// 取值范围i
        /// </summary>
        public string ValueRange
        {
            get
            {
                return m_strValueRange;
            }
            set
            {
                m_strValueRange = value;
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
            set
            {
                m_strLimit = value;
            }
        }
        private string m_strRemark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return m_strRemark;
            }
            set
            {
                m_strRemark = value;
            }
        }

        private EnumFieldType m_pFieldType = EnumFieldType.Other;
        /// <summary>
        /// 字段类型（一般字段，标识码字段，要素代码字段）
        /// </summary>
        public EnumFieldType FiledType
        {
            get
            {
               return m_pFieldType;
            }
            set
            {
                m_pFieldType = value;
            }
        }
    }
    public enum EnumFieldType
    {
        /// <summary>
        /// 一般字段类型
        /// </summary>
        Other,

        /// <summary>
        /// 标识码字段
        /// </summary>
        EntityID,

        /// <summary>
        /// 要素代码字段
        /// </summary>
        YSDM
    }
}
