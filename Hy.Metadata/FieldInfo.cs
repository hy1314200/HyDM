using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    /// <summary>
    /// 字段信息
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 所在图层（或表）
        /// </summary>
        public string Layer { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public enumFieldType Type { get; set; }

        /// <summary>
        /// 数据精度（只有当字段类型为Decimal时有效）
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 数据长度（字符串类型，另可扩展数字类型）
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool NullAble { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// 联动项（可跟字典项关联）
        /// </summary>
        public string Linkage { get; set; }
    }
}
