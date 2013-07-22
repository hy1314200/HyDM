using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.DataManage.Standard.Helper
{
    internal class Importer:Define.Messager
    {
        public IWorkspace Source { private get; set; }

        public string StandardName { private get; set; }


        public StandardItem Import()
        {
        }
    }
}
