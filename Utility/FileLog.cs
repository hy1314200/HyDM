using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 文件型日志类
    /// </summary>
    public class FileLog
    {
        private StringWriter m_LogWriter = null;

        public FileLog()
        {
        }

        public void SetLogFile(string logFile)
        {
            m_LogWriter = new StringWriter(logFile, true);
        }

        private void Validate()
        {
            if (m_LogWriter == null)
            {
                string strDefaultFile = System.Windows.Forms.Application.StartupPath + "//日志.log";
                SetLogFile(strDefaultFile);
            }
        }

        ~FileLog()
        {
            if (m_LogWriter != null)
                m_LogWriter.Close();
        }

        /// <summary>
        /// 设置是否操作日志自动写入磁盘
        /// 默认为是
        /// </summary>
        /// <param name="autoFlush"></param>
        public void SetAutoFlush(bool autoFlush)
        {
            Validate();
            m_LogWriter.AutoFlush = autoFlush;
        }

        /// <summary>
        /// 写入内容（并换行）
        /// </summary>
        /// <param name="strContents"></param>
        public void Append(string strContents)
        {
            Validate();
            m_LogWriter.WriteString(strContents);
        }

        /// <summary>
        /// 添加消息（并换行）
        /// @remark 所谓消息，会添加时间前缀
        /// </summary>
        /// <param name="strMsg"></param>
        public void AppendMessage(string strMsg)
        {
            Validate();
            strMsg = DateTime.Now.ToString() + ":" + strMsg;
            m_LogWriter.WriteString(strMsg);
        }

        /// <summary>
        /// 添加消息（并换行）
        /// 同时记录调用者（标识，可以是类名，方法名，或者是某种标识记号）
        /// 建议在异常记录时使用
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strSender"></param>
        public void AppendMessage(string strMsg, string strSender)
        {
            Validate();
            string strPrefix = DateTime.Now.ToString() + ":";
            strPrefix = strPrefix + strSender + "\r\n";
            strPrefix = strPrefix + "                   ";  // 19个空格
            m_LogWriter.WriteString(strPrefix + strMsg);
        }

        /// <summary>
        /// 添加分隔符（120个下划线）
        /// </summary>
        public void AppendSeparator()
        {
            Validate();
            m_LogWriter.WriteString("-----------------------------------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// 刷入磁盘
        /// </summary>
        public void Flush()
        {
            if (m_LogWriter != null)
                m_LogWriter.Flush();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (m_LogWriter != null)
                m_LogWriter.Close();
        }
    }
}
