using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class CommandWorkspaceOpen:CatalogBaseCommand
    {
        public override void OnClick()
        {
            m_HookHelper.CurrentCatalogItem.Refresh();
        }

        public override bool Enabled
        {
            get
            {
                if (!base.Enabled)
                    return false;

                if (m_HookHelper.CurrentCatalogItem == null)
                    return false;

                //if (m_HookHelper.CurrentCatalogItem.Type != enumCatalogType.Workpace)
                //    return false;

                //return !(m_HookHelper.CurrentCatalogItem as WorkspaceCatalogItem).Openned;
                return !m_HookHelper.CurrentCatalogItem.Openned;
            }
        }
    }
}
