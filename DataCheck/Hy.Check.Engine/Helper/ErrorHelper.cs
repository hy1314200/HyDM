using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Hy.Check.Define;

namespace Hy.Check.Engine.Helper
{
    /// <summary>
    /// Error处理类，负责错误信息的写入（数据库）
    /// @remark 
    /// 1.当错误数据达到缓存数量时将自动刷入，并清空，
    /// 2.默认缓存数为10000条
    /// 3.当任务检查完成后，请使用Flush方法写入
    /// </summary>
    internal class ErrorHelper
    {
        static ErrorHelper()
        {
            m_ErrorHelper = new ErrorHelper();
            CacheCount = 10000;
        }

        private ErrorHelper()
        {
        }

        public static int CacheCount { get; set; }

        private static ErrorHelper m_ErrorHelper = null;
        /// <summary>
        /// 获取此类唯一实例
        /// </summary>
        public static ErrorHelper Instance
        {
            get
            {
                return m_ErrorHelper;
            }
        }

        private List<Error> m_ErrorList = new List<Error>();

        /// <summary>
        /// 设置结果库ADO连接
        /// </summary>
        public IDbConnection ResultConnection
        {
            set;
            private get;
        }

        /// <summary>
        /// 添加多条错误
        /// </summary>
        /// <param name="errList"></param>
        public void AddErrorList(List<Error> errList)
        {
            if (errList == null)
                return;

            for (int i = 0; i < errList.Count; i++)
            {
                AddError(errList[i]);
            }
        }

        /// <summary>
        /// 添加一条错误
        /// </summary>
        /// <param name="error"></param>
        public void AddError(Error error)
        {
            m_ErrorList.Add(error);
            if (m_ErrorList.Count == CacheCount)
            {
                this.Flush();
            }
        }

        /// <summary>
        /// 写入数据库
        /// </summary>
        public void Flush()
        {
            IDbCommand cmdInsert = this.ResultConnection.CreateCommand();
            int count = this.m_ErrorList.Count;
            for (int i = 0; i < count; i++)
            {
                cmdInsert.CommandText = m_ErrorList[i].ToSQLString();
                cmdInsert.ExecuteNonQuery();
                //m_ErrorList[i].Save(this.ResultConnection);
            }

            m_ErrorList.Clear();
        }


        ~ErrorHelper()
        {
            Flush();
            m_ErrorList = null;
        }

    }
}
