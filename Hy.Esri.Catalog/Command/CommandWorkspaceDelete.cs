using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Command.Catalog;
using Hy.Esri.Catalog.Define;
using DevExpress.XtraEditors;
using System.Windows.Forms;


using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Catalog.Command
{
    public class CommandWorkspaceDelete:CatalogBaseCommand
    {
        public CommandWorkspaceDelete()
        {
            this.m_Caption = "删除连接";
        }

        public override void OnClick()
        {
            if (XtraMessageBox.Show("您确认要删除此连接吗？", "操作确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Environment.NhibernateHelper.DeleteObject(m_HookHelper.CurrentCatalogItem.Tag);
                    Environment.NhibernateHelper.Flush();

                    XtraMessageBox.Show("删除成功！");
                    (m_HookHelper.RootCatalogItem as RootCatalogItem).DeleteItem(m_HookHelper.CurrentCatalogItem);
                }
                catch (Exception exp)
                {
                    XtraMessageBox.Show(string.Format("抱歉，删除操作失败！\n信息：{0}", exp.Message));
                }
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
