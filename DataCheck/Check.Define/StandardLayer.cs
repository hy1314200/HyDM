using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 标准下的图层对象 
    /// </summary>
    public class StandardLayer
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int ID { get;  set; }

        /// <summary>
        /// Layer（FeautreClass/Table等）名
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get;  set; }

        /// <summary>
        /// 要素类型代码
        /// </summary>
        public string FeatrueCode { get;  set; }

        /// <summary>
        /// 属性表名称
        /// </summary>
        public string AttributeTableName { get;  set; }

        /// <summary>
        /// 图层几何类型
        /// </summary>
        public enumLayerType Type { get;  set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int OrderIndex { get;  set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get;  set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
