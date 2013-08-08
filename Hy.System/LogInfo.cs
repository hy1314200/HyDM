using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.System
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public global::Define.enumLogType Type { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
