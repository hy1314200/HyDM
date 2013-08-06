using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Frame.Helper
{

    public class DbLogger:global::Define.ILogWriter
    {
        private void Init()
        {
            m_DtLogCache = Environment.AdodbHelper.ExecuteDataTable(string.Format("select * from {0} where 1=2", Properties.Settings.Default.LogTableName));
        }
        private int m_CacheCount = Properties.Settings.Default.LogCacheCount;
        private DataTable m_DtLogCache;

        public void Append(global::Define.enumLogType logType, string strContents)
        {
            //LogInfo logInfo=new LogInfo();
            //logInfo.Content=strContents;
            //logInfo.Time=DateTime.Now;
            //logInfo.Type=logType;

            //Environment.NHibernateHelper.SaveObject(logInfo);
            //Environment.NHibernateHelper.Flush();

            //DataRow rowLog = m_DtLogCache.NewRow();
            //m_DtLogCache.Rows.Add(rowLog);

            if (m_DtLogCache == null)
                Init();

            object[] logInfo=
            {
                Guid.NewGuid().ToString("N"),
                null,
                null,
                logType,
                strContents,
                DateTime.Now
            };
            m_DtLogCache.Rows.Add(logInfo);

            if (m_DtLogCache.Rows.Count == m_CacheCount)
            {
                Flush(); 
                Init();
            }
        }

        public void AppendMessage(global::Define.enumLogType logType, string strMsg)
        {
            this.Append(logType, strMsg);
        }

        public void Flush()
        {
            if (m_DtLogCache != null)
                Environment.AdodbHelper.UpdateTable(Properties.Settings.Default.LogTableName, this.m_DtLogCache);
        }        
    }


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
