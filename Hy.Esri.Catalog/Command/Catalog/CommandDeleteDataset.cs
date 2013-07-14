using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class CommandDeleteDataset : CatalogBaseCommand
    {
        public override bool Enabled
        {
            get
            {
                if (!base.Enabled)
                    return false;

                if (m_HookHelper.CurrentCatalogItem == null)
                    return false;

                return m_HookHelper.CurrentCatalogItem.Type != enumCatalogType.Workpace && m_HookHelper.CurrentCatalogItem.Type != enumCatalogType.Undefine && m_HookHelper.CurrentCatalogItem.Type != enumCatalogType.RasterBand;
            }
        }

        public override void OnClick()
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("您确定要删除吗?", "删除确定", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    //IDataset dsTarget = m_HookHelper.CurrentCatalogItem.Dataset;
                    //if (dsTarget.CanDelete())
                    //{
                    // dsTarget.Delete();
                    if (Utility.GpTool.DeleteDataset(m_HookHelper.CurrentCatalogItem.GetGpString()))
                    {
                        //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(dsTarget);
                        //dsTarget = null;

                        DevExpress.XtraEditors.XtraMessageBox.Show("所选对象已成功删除");
                        m_HookHelper.CurrentCatalogItem.Parent.Open(true);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("抱歉，删除操作出现错误!\n信息：!", Utility.GpTool.ErrorMessage));
                    }
                    //}
                    //else
                    //{
                    //    DevExpress.XtraEditors.XtraMessageBox.Show("当前对象无法删除!");
                    //}
                }
                catch (Exception exp)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("抱歉，删除操作出现错误!\n信息：!", exp.Message));
                }
            }
        }
    }
}
