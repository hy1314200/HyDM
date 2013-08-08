using System;
using Hy.Esri.Utility;
namespace Hy.Esri.Catalog.Define
{
    interface ICatalogHookHelper
    {
        ICatalogItem CurrentCatalogItem { get; set; }
        ICatalogItem RootCatalogItem { get; set; }
        event CatalogItemEventHandler SelectedCatalogItemChanged;
        object WorkapcePropertySet { get; }
        enumWorkspaceType WorkspaceType { get; }
    }
}
