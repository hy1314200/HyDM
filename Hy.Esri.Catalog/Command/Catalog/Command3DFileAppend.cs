using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.Catalog.Define;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class Command3DFileAppend:CatalogBaseCommand
    {
        public Command3DFileAppend()
        {
            this.m_Caption = "追加三维数据";
        }

        public override bool Enabled
        {
            get
            {
                if (!base.Enabled)
                    return false;

                if (m_HookHelper.CurrentCatalogItem == null)
                    return false;

                return m_HookHelper.CurrentCatalogItem.Type==enumCatalogType.FeatureClass3D;
            }
        }

        public override void OnClick()
        {
            UI.FrmImport3DFile frmImport = new UI.FrmImport3DFile();
            frmImport.ExcuteInForm=false;
            frmImport.FeatureClassName = m_HookHelper.CurrentCatalogItem.DatasetName.Name;
            if (frmImport.ShowDialog() == DialogResult.OK)
            {

                string str3DFile = frmImport.ThreeDimenFile;
                string strSpatialRef = frmImport.SpatialReferenceString;
                IFeature feature3D = null;
                if(Utility.GpTool.Append3DFile(str3DFile,m_HookHelper.CurrentCatalogItem.Dataset as IFeatureClass,strSpatialRef,ref feature3D))
                {
                    XtraMessageBox.Show("追加三维数据成功！");
                //m_HookHelper.CurrentCatalogItem.Open(true);
                }
                else
                {
                    XtraMessageBox.Show(string.Format("抱歉，追加操作失败！\n信息：{0}",Utility.GpTool.ErrorMessage));
                }

            }
        }
    }
}
