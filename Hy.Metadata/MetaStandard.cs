using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    /// <summary>
    /// 元数据标准
    /// </summary>
    public class MetaStandard
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 标准名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段名映射表的字典项
        /// 解决对非标准命名的数据项入库容错
        /// </summary>
        public string MappingDict { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 字段信息
        /// </summary>
        public IList<FieldInfo> FieldsInfo { get; set; }

        private System.Collections.IEnumerable m_FieldsEnumerator;
        private System.Collections.IEnumerable FieldsEnumerator
        {
            set
            {
                FieldsInfo = null;
                if (value == null)
                    return;

                m_FieldsEnumerator = value;
                System.Collections.IEnumerator en = value.GetEnumerator();
                FieldsInfo = new List<FieldInfo>();
                while (en.MoveNext())
                {
                    FieldsInfo.Add(en.Current as FieldInfo);
                }
            }

            get
            {
                return m_FieldsEnumerator;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        private Dictionary<string, string> m_FieldNameDictionary;
        public Dictionary<string, string> GetFieldNameDictionary()
        {
            m_FieldNameDictionary = new Dictionary<string, string>();
            m_FieldNameDictionary["ID"] = "标识符";

            if (this.FieldsInfo != null)
            {
                foreach (FieldInfo fInfo in this.FieldsInfo)
                {
                    m_FieldNameDictionary[fInfo.Name] = fInfo.AliasName;
                }
            }

            return m_FieldNameDictionary;
        }
    }
}
