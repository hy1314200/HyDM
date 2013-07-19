using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Metadata;

namespace Hy.Esri.DataManage.Standard
{
    public class TableInfo : Utility.NHibernateSet<FieldInfo>
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// FeatureClass名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// 字段信息(对SubItems的引用，换个名)
        /// </summary>
        public IList<FieldInfo> FieldsInfo
        {
            get { return this.SubItems; }
            set { this.SubItems = value; }
        }

        /// <summary>
        /// 父引用（可能是库标准、FeatureDataset或其它，作为扩展使用）
        /// </summary>
        public string Parent { get; set; }
    }
}
