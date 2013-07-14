using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Command.Catalog;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Catalog.UI;

namespace Hy.Esri.Catalog
{
    public class CommandLocalWorkspaceCreate : CatalogBaseCommand
    {
        public override void OnClick()
        {
            FrmLocalWorkspaceCreate frmCreate = new FrmLocalWorkspaceCreate();
            if (frmCreate.ShowDialog() == DialogResult.OK)
            {
                if (frmCreate.CreateNew)
                {
                    IWorkspace wsNew = Hy.Esri.Catalog.Utility.WorkspaceHelper.CreateWorkspace(
                        frmCreate.WorkspaceType, frmCreate.WorkspacePath, frmCreate.WorkspaceName);

                    if (wsNew == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("抱歉，创建本地数据库出错啦!");


                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("已成功创建本地数据库!");

                    }
                }

                WorkspaceInfo wsInfo=new WorkspaceInfo();
                wsInfo.Name=frmCreate.WorkspaceAlias;
                wsInfo.Type=frmCreate.WorkspaceType;
                wsInfo.Args=System.IO.Path.Combine(frmCreate.WorkspacePath,frmCreate.WorkspaceName);

                Environment.NhibernateHelper.SaveObject(wsInfo);
                Environment.NhibernateHelper.Flush();

                ICatalogItem itemNew = new WorkspaceCatalogItem(
                    System.IO.Path.Combine(frmCreate.WorkspacePath, frmCreate.WorkspaceName),
                    frmCreate.WorkspaceType,
                    m_HookHelper.RootCatalogItem,
                    frmCreate.WorkspaceAlias);

                RootCatalogItem itemRoot = m_HookHelper.RootCatalogItem as RootCatalogItem;
                itemRoot.AddItem(itemNew);
            }
        }
    }
}
