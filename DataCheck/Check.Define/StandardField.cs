using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 标准下的字段对象
    /// </summary>
    public class StandardField
    {
        /// <summary>
        /// 图层对象ID
        /// </summary>
        public int LayerID { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段别名
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 字段精度
        /// </summary>
        public int Precision{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// （若为编码字段）编码类型
        /// </summary>
        public int CodeType { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool NullAble { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可选描述
        /// </summary>
        public string Option { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
