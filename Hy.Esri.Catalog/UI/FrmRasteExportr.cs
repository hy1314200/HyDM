using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Hy.Esri.Catalog.Utility;
using Hy.Esri.Catalog.Define;
using Hy.Esri.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmRasteExportr : DevExpress.XtraEditors.XtraForm
    {
        public FrmRasteExportr()
        {
            InitializeComponent();
            ucClassPath1.PathType = enumPathType.Raster;
        }

        public IWorkspace Workspace
        {
            get { return ucClassPath1.Workspace; }
        }

        public enumWorkspaceType WorkspaceType
        {
            get
            {
                return ucClassPath1.WorkspaceType;
            }
        }

        public IRasterCatalog RasterCatalog
        {
            get
            {
                return ucClassPath1.RasterCatalog;
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

                return ucClassPath1.RasterCatalogName;
            }
        }

        public string RasterName
        {
            set
            {
                ucClassPath1.RasterName = value;
            }
        }

        public string RasterFormat
        {
            get
            {
                return ucClassPath1.RasterFormat;
            }
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
            if (this.ucClassPath1.Workspace == null)
            {
                errMsg = "没有选择目标数据库";
                return false;
            }

            //if (string.IsNullOrWhiteSpace(ucClassPath1.FeatureClassName))
            //{
            //    errMsg = "没有填写目标名称";
            //    return false;
            //}

            return true;
        }

    }
}