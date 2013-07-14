using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Esri.Catalog.Command.Catalog
{
    class CommandDatasetAddFields:CatalogBaseCommand
    {
        public CommandDatasetAddFields()
        {
            this.m_Caption = "添加属性字段";
        }

        public override void OnClick()
        {
            UI.FrmClassFields frmAddFields = new UI.FrmClassFields();
            frmAddFields.TargetClass = this.m_HookHelper.CurrentCatalogItem.Dataset as IClass;
            if (frmAddFields.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<IField> newFields = frmAddFields.NewFieldList;
                    if (Utility.GpTool.AddFields(this.m_HookHelper.CurrentCatalogItem.Dataset as ITable, newFields))
                    {
                        XtraMessageBox.Show("添加字段成功");
                    }
                    else
                    {
                        XtraMessageBox.Show(string.Format("抱歉，添加操作失败了！\n信息：{0}", Utility.GpTool.ErrorMessage));
                    }
                }
                catch (Exception exp)
                {
                    XtraMessageBox.Show(string.Format("抱歉，添加操作发生了错误！\n信息：{0}", exp.Message));
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
                return /*selType == enumCatalogType.FeatureClass3D ||*/ selType == enumCatalogType.FeatureClassArea || selType == enumCatalogType.FeatureClassLine || selType == enumCatalogType.FeatureClassPoint || selType == enumCatalogType.FeatureClassEmpty;
            }
        }

    }
}
