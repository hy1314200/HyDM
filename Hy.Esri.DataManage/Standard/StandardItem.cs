using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.DataManage.Standard
{
    public class StandardItem
    {
        public string ID { get; set; }

        public string Name { get; set; }
        
        enumItemType Type { get; set; }

        IList<StandardItem> SubItems { get; set; }

        object Details { get; set; }
    }
}
