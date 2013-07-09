using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Define
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 系统ADO连接
        /// </summary>
        IAdodbHelper AdodbHelper { set; }

        /// <summary>
        /// 系统ESRI连接
        /// </summary>
        object GisWorkspace { set; }

        /// <summary>
        /// NHibernate连接接口
        /// </summary>
        INhibernateHelper NhibernateHelper { set; }

        /// <summary>
        /// 日志写入接口
        /// </summary>
        ILogWriter Logger { set; }

        /// <summary>
        /// 应用程序信息接口
        /// </summary>
        IApplication Application {set;}

    }
}
