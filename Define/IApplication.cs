using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    /// <summary>
    /// 应用程序接口
    /// 企业名和用户信息允许set，考虑重新登陆等类似功能允许重置
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// 应用程序名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 企业名
        /// </summary>
        string Enterprise { get; set; }

        /// <summary>
        /// 可以唯一标识用户的对象
        /// </summary>
        object UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 应用程序版本
        /// </summary>
        string Version { get; }

    }
}
