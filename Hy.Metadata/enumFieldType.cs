using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    /// <summary>
    /// 字段类型
    /// </summary>
    enum enumFieldType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String=0,
        /// <summary>
        /// 整数
        /// </summary>
        Int=1,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal=2,
        /// <summary>
        /// 日期和时间
        /// </summary>
        DateTime=3,
        /// <summary>
        /// 图片
        /// </summary>
        Image=4,
        /// <summary>
        /// 二进制
        /// </summary>
        Binary

    }
}
