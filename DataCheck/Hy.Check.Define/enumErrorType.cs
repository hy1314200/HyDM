using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 错误/检查类型
    /// </summary>
    public enum enumErrorType
    {
        /// <summary>
        /// 指自动的图/属检查
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 拓扑检查
        /// </summary>
        Topology = 1,
        /// <summary>
        /// 手动检查
        /// </summary>
        Manual = 2,

        /// <summary>
        /// 图层完整性
        /// </summary>
        LayerIntegrity=3,

        /// <summary>
        /// 字段完整性
        /// </summary>
        FieldIntegrity=4
    }
}
