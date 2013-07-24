using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Catalog.Command.Catalog;
using ESRI.ArcGIS.esriSystem;
using Hy.Esri.Catalog.UI;

namespace Hy.Esri.Catalog.Command
{
    public class CommandWorkspaceProperty:CatalogBaseCommand
    {
        public override void OnClick()
        {
            IWorkspaceCatalogItem itemWorkspace = m_HookHelper.CurrentCatalogItem as IWorkspaceCatalogItem;
            if (itemWorkspace == null)
                return;

            if (itemWorkspace.WorkspaceType == enumWorkspaceType.SDE)
            {
                FrmWorkspaceProperty frmProperty = new FrmWorkspaceProperty();
                frmProperty.WorkspaceName = itemWorkspace.Name;
                frmProperty.WorkspaceProperty = itemWorkspace.WorkspacePropertySet as IPropertySet;
                frmProperty.ShowDialog();
            }
            else
            {
                FrmLocalWorkspaceAdd frmPropery2 = new FrmLocalWorkspaceAdd();
                frmPropery2.Editable = false;
                frmPropery2.WorkspaceAlias = itemWorkspace.Name;
                frmPropery2.WorkspaceType = itemWorkspace.WorkspaceType;
                string strFullPath = itemWorkspace.WorkspacePropertySet as string;
                string strPath = System.IO.Path.GetDirectoryName(strFullPath);
                string strName = System.IO.Path.GetFileName(strFullPath);
                frmPropery2.WorkspacePath = strPath;
                frmPropery2.WorkspaceName = strName;

                frmPropery2.ShowDialog();
            }
        }

        public override bool Enabled
        {
            get
            {
                if (!base.Enabled)
                    return false;

                if (m_HookHelper.CurrentCatalogItem == null)
                    return false;


                return m_HookHelper.CurrentCatalogItem.Type == enumCatalogType.Workpace;
            }
        }
    }
}
