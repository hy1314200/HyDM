using System;
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
