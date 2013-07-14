using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Catalog.UI;
using Hy.Esri.Catalog.Utility;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class CommandRasterExport:CatalogBaseCommand 
    {
        public CommandRasterExport()
        {
            this.m_Caption = "导出栅格数据";
        }


        public override void OnClick()
        {
            FrmRasteExportr frmRasterExport = new FrmRasteExportr();
            frmRasterExport.RasterName = m_HookHelper.CurrentCatalogItem.DatasetName.Name;
            if (frmRasterExport.ShowDialog() == DialogResult.OK)
            {
                bool isSucceed = GpTool.CopyRaster(m_HookHelper.CurrentCatalogItem.GetGpString(),
                     WorkspaceHelper.GetGpString(frmRasterExport.Workspace, frmRasterExport.RasterCatalogName,null ),
                     frmRasterExport.WorkspaceType==enumWorkspaceType.File,
                     frmRasterExport.RasterFormat);

                if (isSucceed)
                {
                    XtraMessageBox.Show("导出成功!");
                }
                else
                {
                    XtraMessageBox.Show(string.Format("抱歉，导出失败，操作出现意外错误!\n信息：{0}",GpTool.ErrorMessage));
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

                enumCatalogType selType = m_HookHelper.CurrentCatalogItem.Type;
                return selType == enumCatalogType.RasterSet;
            }
        }
    }
}
