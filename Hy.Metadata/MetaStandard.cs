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
    }
}
