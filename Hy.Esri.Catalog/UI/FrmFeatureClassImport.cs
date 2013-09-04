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
    public partial class FrmFeatureClassImport : DevExpress.XtraEditors.XtraForm
    {
        public FrmFeatureClassImport()
        {
            InitializeComponent();
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

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return ucClassInPath1.FeatureDataset;
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

                return ucClassInPath1.FeatureDatasetName;
            }
        }

        public IFeatureClass FeatureClass
        {
            get
            {
                return ucClassInPath1.FeatureClass;
            }
        }

        public string FeatureClassName
        {
            get
            {
                return ucClassInPath1.FeatureClassName;
            }
        }

    }
}