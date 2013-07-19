using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class NHibernateSet<T>
    {
        public IList<T> SubItems { get; set; }

        private System.Collections.IEnumerable m_SubItemEnumerator;
        private System.Collections.IEnumerable SubItemEnumerator
        {
            set
            {
                SubItems = null;
                if (value == null)
                    return;

                m_SubItemEnumerator = value;
                System.Collections.IEnumerator en = value.GetEnumerator();
                SubItems = new List<T>();
                while (en.MoveNext())
                {
                    SubItems.Add((T)en.Current);
                }
            }

            get
            {
                return m_SubItemEnumerator;
            }
        }
    }
}
