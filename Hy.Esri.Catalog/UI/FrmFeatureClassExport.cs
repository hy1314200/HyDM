using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmFeatureClassExport : DevExpress.XtraEditors.XtraForm
    {
        public FrmFeatureClassExport()
        {
            InitializeComponent();
        }

        public IWorkspace Workspace
        {
            get { return ucClassPath1.Workspace; }
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return ucClassPath1.FeatureDataset;
            }
        }

        public string FeatureDatasetName
        {
            get
            {
                //IFeatureDataset fDs = this.FeatureDataset;
                //if (fDs == null)
                //    return null;

                //return (fDs as IDataset).Name;

                return ucClassPath1.FeatureDatasetName;
            }
        }

        public string FeatureClassName
        {
            get
            {
                return ucClassPath1.FeatureClassName;
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

            if (string.IsNullOrWhiteSpace(ucClassPath1.FeatureClassName))
            {
                errMsg = "没有填写目标名称";
                return false;
            }

            return true;
        }
    }
}