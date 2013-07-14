using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.UI;
using System.Windows.Forms;
using Hy.Esri.Catalog.Utility;
using DevExpress.XtraEditors;
using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class CommandFeatureClassImport : CatalogBaseCommand
    {
        public CommandFeatureClassImport()
        {
            this.m_Caption = "导入要素类";
        }

        public override void OnClick()
        {
            FrmFeatureClassImport frmFeatureClass = new FrmFeatureClassImport();
            if (frmFeatureClass.ShowDialog() == DialogResult.OK)
            {
                bool isSucceed = GpTool.CopyFeatureClass(WorkspaceHelper.GetGpString(frmFeatureClass.Workspace, frmFeatureClass.FeatureDatasetName, frmFeatureClass.FeatureClassName),
                    m_HookHelper.CurrentCatalogItem.GetGpString(),frmFeatureClass.FeatureClassName.Substring(0,frmFeatureClass.FeatureClassName.LastIndexOf(".")));

                if (isSucceed)
                {
                    XtraMessageBox.Show("导入成功!");
                }
                else
                {
                    XtraMessageBox.Show(string.Format("抱歉，导入失败，操作出现意外错误!\n信息：{0}", GpTool.ErrorMessage));
                }

                m_HookHelper.CurrentCatalogItem.Open(true);
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

                return m_HookHelper.CurrentCatalogItem.Type == enumCatalogType.Workpace || m_HookHelper.CurrentCatalogItem.Type==enumCatalogType.FeatureDataset;
            }
        }
    }
}
