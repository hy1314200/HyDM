using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog
{
    /// <summary>
    /// Workspace信息类
    /// </summary>
    public  class WorkspaceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 连接名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Workspace类型
        /// </summary>
        public enumWorkspaceType Type { get; set; }

        /// <summary>
        /// Workspace参数
        /// </summary>
        public string Args { get; set; }
    }
}
