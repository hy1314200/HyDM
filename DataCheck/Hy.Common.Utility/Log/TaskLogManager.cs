using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Common.Utility.Log
{
    /// <summary>
    /// 任务（执行）日志管理类
    /// 使用前先设置日志文件路径@see ::SetLogFile
    /// </summary>
    public class TaskLogManager
    {

        private static LogWriter m_LogWriter =null;// new LogWriter(System.Windows.Forms.Application.StartupPath + "//HyDC.log",false);

        /// <summary>
        /// 设置任务日志路径
        /// </summary>
        /// <param name="strFile"></param>
        public static void SetLogFile(string strFile)
        {
            m_LogWriter = new LogWriter(strFile, false);
        }

        /// <summary>
        /// 设置是否操作日志自动写入磁盘
        /// 默认为否
        /// </summary>
        /// <param name="autoFlush"></param>
        public static void SetAutoFlush(bool autoFlush)
        {
            m_LogWriter.AutoFlush = autoFlush;
        }

        /// <summary>
        /// 写入内容（并换行）
        /// </summary>
        /// <param name="strContents"></param>
        public static void Append(string strContents)
        {
            m_LogWriter.WriteString(strContents);
        }

        /// <summary>
        /// 添加消息（并换行）
        /// @remark 所谓消息，会添加时间前缀
        /// </summary>
        /// <param name="strMsg"></param>
        public static void AppendMessage(string strMsg)
        {
            strMsg = DateTime.Now.ToString() + ":" + strMsg;
            m_LogWriter.WriteString(strMsg);
        }

        /// <summary>
        /// 添加分隔符（120个下划线）
        /// </summary>
        public static void AppendSeparator()
        {
            m_LogWriter.WriteString("-----------------------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// 刷入磁盘
        /// </summary>
        public static void Flush()
        {
            m_LogWriter.Flush();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Close()
        {
            m_LogWriter.Close();
        }
    }
}
