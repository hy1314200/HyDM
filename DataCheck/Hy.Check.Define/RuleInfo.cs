using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 规则信息类（对应HyDC模型中的模板）
    /// </summary>
    public class RuleInfo
    {
        /// <summary>
        /// 标识 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 规则名
        /// </summary>
        public string Name { get; set; }


        // 缺陷级别对质检本身来说非必须的（考虑系统库无评价模型，此属性删除）
        ///// <summary>
        ///// 缺陷级别
        ///// </summary>
        //public enumDefectLevel DefectLevel { get; set; }

        /// <summary>
        /// 规则类信息
        /// </summary>
        public RuleDllInfo RuleClassInfo { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public byte[] Paramters { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
