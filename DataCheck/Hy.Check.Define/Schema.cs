using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 方案
    /// </summary>
    public class Schema
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 评价模型标识
        /// </summary>
        public string EvaModelID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 标准标识
        /// </summary>
        public int StandardID { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
