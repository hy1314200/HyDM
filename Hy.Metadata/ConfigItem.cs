using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    /// <summary>
    /// 系统配置项
    /// </summary>
    public class ConfigItem
    {
        /// <summary>
        /// 标识 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称(配置项键)
        /// </summary>
        public string ItemKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string ItemValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
