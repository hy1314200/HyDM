using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class CommandItemRefresh:CatalogBaseCommand
    {
        public CommandItemRefresh()
        {
            this.m_Caption = "刷新";
        }

        public override void OnClick()
        {
            m_HookHelper.CurrentCatalogItem.Open(true);
        }
        public override bool Enabled
        {
            get
            {
                return  base.Enabled&& m_HookHelper.CurrentCatalogItem != null;
            }
        }
    }
}
