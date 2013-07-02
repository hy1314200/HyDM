using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 缺陷级别
    /// </summary>
    public enum enumDefectLevel
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown=-1,
        /// <summary>
        /// 轻
        /// </summary>
        Lower = 0,
        /// <summary>
        /// 重
        /// </summary>
        High = 1,
        /// <summary>
        /// 严重
        /// </summary>
        Serious = 2
    }
}
