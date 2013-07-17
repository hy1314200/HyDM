using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.DataManage.Standard
{
    public class TableInfo
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string AliasName { get; set; }

        public IList<Hy.Metadata.FieldInfo> FieldsInfo { get; set; }
    }
}
