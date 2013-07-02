using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Common.Utility.Log
{
    /// <summary>
    /// 日志写入类（仅提供字符串写入）
    /// </summary>
    public  class LogWriter
    {
        private string m_FileName;
        /// <summary>
        /// 获取或设置日志的文件路径
        /// </summary>
        public string FileName
        {
            get
            {
                return m_FileName;
            }
            set
            {
                // 关闭原来的
                this.Close();
                // 打开新的
                this.m_FileName = value;
                this.OpenStream();
            }
        }

        private bool m_AutoFlush = false;
        /// <summary>
        /// 设置是否自动写入磁盘
        /// </summary>
        public bool AutoFlush
        {
            set
            {
                this.m_AutoFlush = value;
                if (this.m_Writer != null)
                    this.m_Writer.AutoFlush = this.m_AutoFlush;
            }
        }


        /// <summary>
        /// 构造函数
        /// 打开文件，如文件不存在则创建
        /// </summary>
        /// <param name="strFile"></param>
        public LogWriter(string strFile,bool autoFlush)
        {
            this.m_FileName = strFile;
            this.AutoFlush = autoFlush;
            OpenStream();
        }

        /// <summary>
        /// 构造函数
        /// 打开文件
        /// 如文件不存在则创建，并写入初始化内容
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="autoFlush"></param>
        /// <param name="strInitContents"></param>
        public LogWriter(string strFile,bool autoFlush, string strInitContents)
        {
            this.m_FileName = strFile;
            this.AutoFlush = autoFlush;
            if (File.Exists(strFile))
            {
                OpenStream();
            }
            else
            {
                WriteString(strInitContents);
            }
        }

        private StreamWriter m_Writer;

        private void OpenStream()
        {
            this.m_Writer = new StreamWriter(this.m_FileName,true,Encoding.Unicode);
            this.m_AutoFlush = this.m_AutoFlush;
        }

        /// <summary>
        /// 写入字符串（并在末尾添加行结束符）
        /// </summary>
        /// <param name="strContent"></param>
        public void WriteString(string strContent)
        {
            if (this.m_Writer == null)
                OpenStream();

            if (this.m_Writer == null)
                throw new Exception("LogWriter调用错误：没有指定文件，无法写入");

            this.m_Writer.WriteLine(strContent);
        }

        /// <summary>
        /// 写入磁盘
        /// </summary>
        public void Flush()
        {
            if (this.m_Writer != null)
                this.m_Writer.Flush();
        }

        /// <summary>
        /// 关闭流
        /// </summary>
        public void Close()
        {
            if (this.m_Writer != null)
            {
                this.m_Writer.Flush();
                this.m_Writer.Close();
                this.m_Writer.Dispose();
            }
        }
    }
}
