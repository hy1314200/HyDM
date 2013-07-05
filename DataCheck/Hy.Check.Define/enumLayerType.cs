using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Check.Define
{
    /// <summary>
    /// 图层几何类型
    /// </summary>
    public enum enumLayerType
    {
        /// <summary>
        /// 点
        /// </summary>
        Point = 1,
        /// <summary>
        /// 线
        /// </summary>
        Line = 2,
        /// <summary>
        /// 面
        /// </summary>
        Area = 3,
        /// <summary>
        /// 注记
        /// </summary>
        Annotation = 4,
        /// <summary>
        /// 属性表
        /// </summary>
        Table = 5,
        /// <summary>
        /// 未知类型
        /// </summary>
        UnKnown = 0
    }
}
