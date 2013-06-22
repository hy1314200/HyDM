using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 可提供给框架使用的日志类
    /// </summary>
    public class Log
    {
        private static FileLog m_OperationalLogger;
        private static FileLog m_ExceptionLogger;

        static Log()
        {
        }


        private static void Validate()
        {
            if (m_OperationalLogger == null || m_ExceptionLogger == null)
            {
                string operateFile = System.Windows.Forms.Application.StartupPath + "//操作日志.log";
                string exceptionFile = System.Windows.Forms.Application.StartupPath + "//异常日志.log";
                SetLogFile(operateFile, exceptionFile);
                SetAutoFlush(true);
            }
        }

        /// <summary>
        /// 设置日志（操作、异常和调试两种）路径
        /// </summary>
        /// <param name="operationalFile"></param>
        /// <param name="exceptionFile"></param>
        public static void SetLogFile(string operationalFile, string exceptionFile)
        {
            string strSplit = string.Format("------------------------------------------------------{0}----------------------------------------------------", DateTime.Today.ToString("yyyy年MM月dd日"));
            string strOperate = strSplit;
            if (!System.IO.File.Exists(operationalFile))
            {
                strOperate = string.Format((@"
---------------------------------------------------------------------------------------------------------------------
* Copyright (c) 2013-06-17  Hy.
{0}

"), strSplit);
            }
            m_OperationalLogger = new FileLog();
            m_OperationalLogger.SetLogFile(operationalFile);
            m_OperationalLogger.Append(strOperate);

            string strException = strSplit;
            if (!System.IO.File.Exists(exceptionFile))
            {
                strException = string.Format((@"
---------------------------------------------------------------------------------------------------------------------
* Copyright (c) 2013-02-27  Hy.
{0}

"), strSplit);
            }
            m_ExceptionLogger = new FileLog();
            m_ExceptionLogger.SetLogFile(exceptionFile);
            m_ExceptionLogger.Append(strException);
        }

        /// <summary>
        /// 是否自动刷入
        /// </summary>
        /// <param name="autoFlush"></param>
        public static void SetAutoFlush(bool autoFlush)
        {
            Validate();
            m_OperationalLogger.SetAutoFlush(autoFlush);
            m_ExceptionLogger.SetAutoFlush(autoFlush);
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="strContents"></param>
        public static void Append(Define.enumLogType logType, string strContents)
        {
            Validate();
            if (logType == Define.enumLogType.Operate)
            {
                m_OperationalLogger.Append(strContents);
            }
            else
            {
                m_ExceptionLogger.Append("[" + logType.ToString() + "]" + strContents);
            }
        }

        /// <summary>
        /// 添加“消息”
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="strMsg"></param>
        public static void AppendMessage(Define.enumLogType logType, string strMsg)
        {
            Validate();
            if (logType == Define.enumLogType.Operate)
            {
                m_OperationalLogger.AppendMessage(strMsg);
            }
            else
            {
                m_ExceptionLogger.AppendMessage("[" + logType.ToString() + "]" + strMsg);
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        public static void Flush()
        {
            if (m_OperationalLogger != null)
                m_OperationalLogger.Flush();

            if (m_ExceptionLogger != null)
                m_ExceptionLogger.Flush();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Close()
        {
            if (m_OperationalLogger != null)
                m_OperationalLogger.Close();

            if (m_ExceptionLogger != null)
                m_ExceptionLogger.Close();
        }

        ~Log()
        {
            if (m_ExceptionLogger != null)
                m_ExceptionLogger.Close();

            if (m_OperationalLogger != null)
                m_OperationalLogger.Close();
        }
    }
}
