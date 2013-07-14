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
    public class CommandFeatureClassExport:CatalogBaseCommand
    {
        public CommandFeatureClassExport()
        {
            this.m_Caption = "导出要素类";
        }

        public override void OnClick()
        {
            FrmFeatureClassExport frmFeatureClass = new FrmFeatureClassExport();
            if (frmFeatureClass.ShowDialog() == DialogResult.OK)
            {
               bool isSucceed=  GpTool.CopyFeatureClass(m_HookHelper.CurrentCatalogItem.GetGpString(),
                    WorkspaceHelper.GetGpString(frmFeatureClass.Workspace, frmFeatureClass.FeatureDatasetName, null), frmFeatureClass.FeatureClassName);

               if (isSucceed)
               {
                   XtraMessageBox.Show("导出成功!");
               }
               else
               {
                   XtraMessageBox.Show(string.Format("抱歉，导出失败，操作出现意外错误!\n信息：{0}", GpTool.ErrorMessage));
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

                enumCatalogType selType=m_HookHelper.CurrentCatalogItem.Type;
                return selType == enumCatalogType.FeatureClass3D ||selType==enumCatalogType.FeatureClassArea || selType==enumCatalogType.FeatureClassLine || selType==enumCatalogType.FeatureClassPoint || selType==enumCatalogType.FeatureClassEmpty;
            }
        }
    }
}
