using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Define;

namespace Frame.Define
{
    /// <summary>
    /// 应用程序环境创建者
    /// </summary>
    public interface IEnvironmentCreator
    {
        /// <summary>
        /// 系统ADO连接
        /// </summary>
        IDbConnection SysConnection { get; }
        
        /// <summary>
        /// NHibernate连接接口
        /// </summary>
        INhibernateHelper NhibernateHelper { get; }

        /// <summary>
        /// 日志写入接口
        /// </summary>
        ILogWriter LogWriter { get; }

        /// <summary>
        /// 应用程序信息接口
        /// </summary>
        IApplication Application { get; }

        /// <summary>
        /// 释放
        /// </summary>
        void Release();
    }
}
