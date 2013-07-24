using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Command.Catalog;
using System.Windows.Forms;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Catalog.UI;

namespace Hy.Esri.Catalog.Command
{
    public class CommandWorkspaceAdd:CatalogBaseCommand
    {
        public CommandWorkspaceAdd()
        {
            this.m_Caption = "添加连接";
        }
        public override void OnClick()
        {
            FrmSDEWorkspaceAdd frmCreate = new FrmSDEWorkspaceAdd();
            if (frmCreate.ShowDialog() == DialogResult.OK)
            {
                ICatalogItem itemWorkspace = new WorkspaceCatalogItem(
                    frmCreate.ConnectionProperty.Args,
                    enumWorkspaceType.SDE,
                    m_HookHelper.RootCatalogItem, 
                    frmCreate.ConnectionProperty.Name);

                (m_HookHelper.RootCatalogItem as RootCatalogItem).AddItem(itemWorkspace);
            }
        }
    }
}
