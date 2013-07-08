using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;

namespace Frame
{
    internal class Logger:ILogWriter
    {
        private Logger()
        {
        }

        private static Logger m_Logger=new Logger();

        public static Logger Instance
        {
            get
            {
                return m_Logger;
            }
        }

        public void Append(enumLogType logType, string strContents)
        {
            Utility.Log.Append(logType, strContents);
        }

        public void AppendMessage(enumLogType logType, string strMsg)
        {
            Utility.Log.AppendMessage(logType, strMsg);
        }

        public void Close()
        {
            Utility.Log.Close();
        }
    }
}
