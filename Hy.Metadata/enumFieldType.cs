using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    /// <summary>
    /// 字段类型
    /// </summary>
    public enum enumFieldType:int
    {
        /// <summary>
        /// 整数
        /// </summary>
        Int=1,
        /// <summary>
        /// 字符串
        /// </summary>
        String=4,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal=3,
        /// <summary>
        /// 日期和时间
        /// </summary>
        DateTime=5,
        /// <summary>
        /// 图片
        /// </summary>
        Image=9,
        /// <summary>
        /// 二进制
        /// </summary>
        Binary=8

    }
}