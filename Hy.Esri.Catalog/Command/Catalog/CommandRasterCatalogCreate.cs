using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataManagementTools;

namespace Hy.Esri.Catalog.Command.Catalog
{
    class CommandRasterCatalogCreate:CatalogBaseCommand
    {
        public override void OnClick()
        {
            CreateRasterCatalog gpCreate = new CreateRasterCatalog();
        }
    }
}
