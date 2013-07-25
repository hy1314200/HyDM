using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Common.Utility.Log
{
    /// <summary>
    /// 软件的操作及异常日志管理类
    /// </summary>
    public class OperationalLogManager
    {
        
        private static LogWriter m_LogWriter = new LogWriter(System.Windows.Forms.Application.StartupPath + "//HyDC.log",true,
@"---------------------------------------------------------------------------------------------------------------------
* Copyright (c) 2011-12-01  Hy-Xingzhe
* 此文件记录了质检模块在执行过程中，发生的操作信息或错误和异常信息！有任何疑问请联系Hy-Xingzhe！
* All Rights Reserved By Hy-Xingzhe!
-----------------------------------------------------------------------------------------------------------------------

");
        /// <summary>
        /// 设置是否操作日志自动写入磁盘
        /// 默认为是
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
        /// 添加消息（并换行）
        /// 同时记录调用者（标识，可以是类名，方法名，或者是某种标识记号）
        /// 建议在异常记录时使用
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strSender"></param>
        public static void AppendMessage(string strMsg, string strSender)
        {
            string strPrefix = DateTime.Now.ToString() + ":";
            //int padCount=strPrefix.Length;
            strPrefix = strPrefix + strSender + "\r\n";
            //strPrefix = strPrefix.PadRight(strPrefix.Length + padCount);
            strPrefix = strPrefix + "                   ";  // 19个空格
            m_LogWriter.WriteString(strPrefix + strMsg); 
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
