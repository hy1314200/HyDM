using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Task
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum enumTaskState
    {
        /// <summary>
        /// 已创建
        /// </summary>
        Created = 0,
        /// <summary>
        /// 部分执行（抽检）
        /// </summary>
        PartlyExcuted = 1,
        /// <summary>
        /// 全部执行（全检）
        /// </summary>
        WhollyExcuted = 2
    }

}
