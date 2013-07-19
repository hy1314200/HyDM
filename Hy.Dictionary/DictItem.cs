using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Dictionary
{
    /// <summary>
    /// 字典项
    /// </summary>
    public class DictItem:Utility.NHibernateSet<DictItem>
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 项名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父项
        /// </summary>
        public DictItem Parent { get; set; }

        ///// <summary>
        ///// 子项集合
        ///// </summary>
        //public List<DictItem> SubItems { get; set; }

        //private System.Collections.IEnumerable m_SubItemEnumerator;
        //private System.Collections.IEnumerable SubItemEnumerator
        //{
        //    set
        //    {
        //        SubItems = null;
        //        if (value == null)
        //            return;

        //        m_SubItemEnumerator = value;
        //        System.Collections.IEnumerator en = value.GetEnumerator();
        //        SubItems = new List<DictItem>();
        //        while (en.MoveNext())
        //        {
        //            SubItems.Add(en.Current as DictItem);
        //        }
        //    }

        //    get
        //    {
        //        return m_SubItemEnumerator;
        //    }
        //}

    }
}
