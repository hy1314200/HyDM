using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataManagementTools;

namespace Hy.Esri.Catalog.Command.Catalog
{
    class CommandFeatureClassCreate : CatalogBaseCommand
    {
        public override void OnClick()
        {
            CreateFeatureclass gpCreate = new CreateFeatureclass();
        }
    }
}
