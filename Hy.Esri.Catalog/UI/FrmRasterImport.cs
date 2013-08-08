using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmRasterImport : DevExpress.XtraEditors.XtraForm
    {
        public FrmRasterImport()
        {
            InitializeComponent();
            ucClassInPath1.PathType = enumPathType.Raster;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strMsg = null;
            if (!ValidateSetting(ref strMsg))
            {
                XtraMessageBox.Show(strMsg);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateSetting(ref string errMsg)
        {
            if (this.ucClassInPath1.Workspace == null)
            {
                errMsg = "没有选择目标数据库";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucClassInPath1.FeatureClassName))
            {
                errMsg = "没有填写目标名称";
                return false;
            }

            return true;
        }



        public IWorkspace Workspace
        {
            get { return ucClassInPath1.Workspace; }
        }

        public IRasterCatalog RasterCatalog
        {
            get
            {
                return ucClassInPath1.RasterCatalog;
            }
        }

        public string RasterCatalogName
        {
            get
            {
                //IFeatureDataset fDs = this.FeatureDataset;
                //if (fDs == null)
                //    return null;

                //return (fDs as IDataset).Name;

                return ucClassInPath1.RasterCatalogName;
            }
        }

        public IRasterDataset RasterDataset
        {
            get
            {
                return ucClassInPath1.RasterDataset;
            }
        }

        public string RasterDatasetName
        {
            get
            {
                return ucClassInPath1.RasterDatasetName;
            }
        }

        private void FrmRasterImport_Load(object sender, EventArgs e)
        {

        }

    }
}