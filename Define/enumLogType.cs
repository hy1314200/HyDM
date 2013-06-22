using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum enumLogType
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        Operate,

        /// <summary>
        /// 致命性错误
        /// </summary>
        Fatal,

        /// <summary>
        /// 程序中发生的错误
        /// </summary>
        Error,

        /// <summary>
        /// 警告
        /// </summary>
        Warn,

        /// <summary>
        /// 信息
        /// </summary>
        Info,
    
        /// <summary>
        /// 调试
        /// </summary>
        Debug
    }
}
