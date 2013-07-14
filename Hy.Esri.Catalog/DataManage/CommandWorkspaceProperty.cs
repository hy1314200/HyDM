using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimenDataManage.Catalog;
using ThreeDimenDataManage.Command.Catalog;
using ESRI.ArcGIS.esriSystem;

namespace HzGeoSpaceSys.Main.GISForm.DataManage
{
    public class CommandWorkspaceProperty:CatalogCommand
    {
        public override void OnClick()
        {
            IWorkspaceCatalogItem itemWorkspace = m_HookHelper.CurrentCatalogItem as IWorkspaceCatalogItem;
            if (itemWorkspace == null)
                return;


            FrmWorkspaceProperty frmProperty = new FrmWorkspaceProperty();
            frmProperty.WorkspaceName = itemWorkspace.Name;
            frmProperty.WorkspaceProperty = itemWorkspace.WorkspacePropertySet as IPropertySet;
            frmProperty.ShowDialog();
        }

        public override bool Enabled
        {
            get
            {
                if (m_HookHelper.CurrentCatalogItem == null)
                    return false;


                return m_HookHelper.CurrentCatalogItem.Type == enumCatalogType.Workpace;
            }
        }
    }
}
