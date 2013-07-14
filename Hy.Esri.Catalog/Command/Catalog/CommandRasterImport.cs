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
    public class CommandRasterImport:CatalogBaseCommand
    {
        public CommandRasterImport()
        {
            this.m_Caption = "导入栅格数据";
        }

        public override void OnClick()
        {
            FrmRasterImport frmFeatureClass = new FrmRasterImport();
            if (frmFeatureClass.ShowDialog() == DialogResult.OK)
            {
                bool isSucceed = GpTool.CopyRaster(WorkspaceHelper.GetGpString(frmFeatureClass.Workspace, frmFeatureClass.RasterCatalogName, frmFeatureClass.RasterDatasetName),
                    m_HookHelper.CurrentCatalogItem.GetGpString(),false,null);

                if (isSucceed)
                {
                    XtraMessageBox.Show("导入成功!");
                    m_HookHelper.CurrentCatalogItem.Open(true);
                }
                else
                {
                    XtraMessageBox.Show(string.Format("抱歉，导入失败，操作出现意外错误!\n信息：{0}", GpTool.ErrorMessage));
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

                return m_HookHelper.CurrentCatalogItem.Type == enumCatalogType.Workpace || m_HookHelper.CurrentCatalogItem.Type == enumCatalogType.RasterCatalog;
            }
        }

    }
}
