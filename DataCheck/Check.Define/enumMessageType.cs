using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Check.Define
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum enumMessageType
    {
        /// <summary>
        /// 数据导入错误
        /// </summary>
        DataImportError,
        /// <summary>
        /// 预处理错误消息
        /// </summary>
        PretreatmentError,
        /// <summary>
        /// 验证错误消息
        /// </summary>
        VerifyError,
        /// <summary>
        /// 规则（执行抛出的）消息
        /// </summary>
        RuleError,
        /// <summary>
        /// 操作日志
        /// </summary>
        OperationalLog,
        /// <summary>
        /// 异常
        /// </summary>
        Exception
    }
}
