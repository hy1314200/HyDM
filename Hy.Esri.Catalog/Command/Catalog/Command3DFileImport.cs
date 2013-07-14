using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public class Command3DFileImport:CatalogBaseCommand
    {
        public Command3DFileImport()
        {
            this.m_Caption = "导入3维数据";
        }


        public override bool Enabled
        {
            get
            {
                if (!base.Enabled)
                    return false;

                if(m_HookHelper.CurrentCatalogItem==null)
                    return false;

                enumCatalogType catalogType=m_HookHelper.CurrentCatalogItem.Type;
                if (catalogType == enumCatalogType.Workpace || enumCatalogType.FeatureDataset == catalogType)
                    return true;

                return false;
            }
        }

        public override void OnClick()
        {
            UI.FrmImport3DFile frmImport = new UI.FrmImport3DFile();
            frmImport.OutputGpString = m_HookHelper.CurrentCatalogItem.GetGpString();
            if (frmImport.ShowDialog() == DialogResult.OK)
            {
                m_HookHelper.CurrentCatalogItem.Open(true);
            }
         
        }
    }
}
