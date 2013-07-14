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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmImport3DFile : DevExpress.XtraEditors.XtraForm
    {
        public FrmImport3DFile()
        {
            InitializeComponent();
        }

        private string m_SpatialReferenceString;
        private ISpatialReference m_SpatailReference;
        private string m_OutputGpString;
        public string OutputGpString
        {
            set
            {
                m_OutputGpString = value;
            }
        }

        private void txt3DFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt3DFile.Text))
                dlg3DFile.FileName = txt3DFile.Text;

            if (dlg3DFile.ShowDialog() != DialogResult.OK)
                return;

            txt3DFile.Text = dlg3DFile.FileName;            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strErrMsg=null;
            if (!ValidateSetting(ref strErrMsg))
            {
                XtraMessageBox.Show(strErrMsg);
                return;
            }
            m_SpatailReference = ucSpatialReference1.SpatialReference;
            m_SpatialReferenceString = ucSpatialReference1.SpatialReferenceString;
            //if (this.m_ExcuteInForm)
            //{
            //    bool isSucceed = GpTool.Import3DFile(txt3DFile.Text, string.Format("{0}\\{1}", m_OutputGpString, this.txtFeatureClassName.Text), m_SpatialReferenceString);
            //    if (isSucceed)
            //    {
            //        XtraMessageBox.Show("导入3D数据成功！");
            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(string.Format("抱歉，导入出现错误！信息：{0}", GpTool.ErrorMessage));
            //    }
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
            this.DialogResult = DialogResult.OK;
        }

        private bool m_ExcuteInForm=true;
        public bool ExcuteInForm
        {
            set
            {
                m_ExcuteInForm = value;
                txtFeatureClassName.Enabled = m_ExcuteInForm;
            }
        }

        public string FeatureClassName
        {
            get
            {
                return txtFeatureClassName.Text;
            }
            set
            {
                txtFeatureClassName.Text = value;
            }
        }

        public string ThreeDimenFile
        {
            get
            {
                return txt3DFile.Text;
            }
        }

        public string SpatialReferenceString
        {
            get
            {
                return m_SpatialReferenceString;
            }
        }

        private bool ValidateSetting(ref string errMsg)
        {
            if (string.IsNullOrEmpty(txt3DFile.Text))
            {
                errMsg = "请选择需要导入的3D文件！";
                return false;
            }

            if (string.IsNullOrEmpty(txtFeatureClassName.Text))
            {
                errMsg = "要素类名称必须填写！";
                return false;
            }

            return true;
        }
    }
}