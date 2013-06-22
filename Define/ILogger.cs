using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger 
    { 

        /// <summary>
        /// 写入内容（并换行）
        /// </summary>
        /// <param name="strContents"></param>
        void Append(enumLogType logType, string strContents);

        /// <summary>
        /// 添加消息（并换行）
        /// @remark 所谓消息，会添加时间前缀
        /// </summary>
        /// <param name="strMsg"></param>
        void AppendMessage(enumLogType logType, string strMsg);      

    }
}
