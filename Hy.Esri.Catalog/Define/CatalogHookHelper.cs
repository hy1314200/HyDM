using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Utility;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.Define
{
    public class CatalogHookHelper : ESRI.ArcGIS.Controls.HookHelperClass,ICatalogHookHelper
    {
        public event CatalogItemEventHandler SelectedCatalogItemChanged;

        protected ICatalogItem m_CurrentCatalogItem;

        public ICatalogItem CurrentCatalogItem
        {
            get { return m_CurrentCatalogItem; }
            set
            {
                m_CurrentCatalogItem = value;
                if (SelectedCatalogItemChanged != null)
                    SelectedCatalogItemChanged.Invoke(m_CurrentCatalogItem);
            }
        }

        public object WorkapcePropertySet { get; protected set; }

        public enumWorkspaceType WorkspaceType { get; protected set; }

        public ICatalogItem RootCatalogItem { get; set; }


    }
}
