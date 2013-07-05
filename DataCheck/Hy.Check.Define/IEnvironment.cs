using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
 
    /// <summary>
    /// （规则执行）环境设置
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// 消息处理器
        /// </summary>
        MessageHandler MessageHandler { set; }

        /// <summary>
        /// Base库Workspace
        /// </summary>
        ESRI.ArcGIS.Geodatabase.IWorkspace BaseWorkspace { set; }

        /// <summary>
        /// Query库Workspace
        /// </summary>
        ESRI.ArcGIS.Geodatabase.IWorkspace QueryWorkspace { set; }

        /// <summary>
        /// Query库连接
        /// </summary>
        System.Data.IDbConnection QueryConnection { set; }

        /// <summary>
        /// Topo库Workspace
        /// </summary>
        ESRI.ArcGIS.Geodatabase.IWorkspace TopoWorkspace { set; }

        /// <summary>
        /// 结果库连接
        /// </summary>
        System.Data.IDbConnection ResultConnection { set; }

        /// <summary>
        /// 方案的ID
        /// </summary>
        string SchemaID { set; }
    }
}
