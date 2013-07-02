using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 规则执行状态
    /// </summary>
    public enum enumRuleState
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        UnKnown = -1,

        /// <summary>
        /// 未执行
        /// </summary>
        UnExecute = 0,

        /// <summary>
        /// 失败
        /// </summary>
        ExecuteFailed = 1,

        /// <summary>
        /// 执行成功
        /// </summary>
        ExecuteSucceed = 2,
    }
}
