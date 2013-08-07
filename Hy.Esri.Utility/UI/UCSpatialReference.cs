using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.Utility.UI
{
    public partial class UCSpatialReference : DevExpress.XtraEditors.XtraUserControl
    {
        public UCSpatialReference()
        {
            InitializeComponent();

            this.EditAble = false;
        }

        public bool EditAble
        {
            set
            {
                txtTolerance.Properties.ReadOnly = !value;
                txtResolution.Properties.ReadOnly = !value;
                btnSelect.Enabled = btnClear.Enabled = btnImport.Enabled = value;

            }
        }

        private void SetUnknown()
        {
            lblName.Text = "";
            lblUnit0.Text = lblUnit1.Text = "未知单位";
            txtInfomation.Text = "";
            txtTolerance.Text = "0.001";
            txtResolution.Text = "0.0001";
        }
        private ISpatialReference m_SpatialReference;

        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISpatialReference SpatialReference
        {
            get
            {
                if (m_SpatialReference == null)
                    m_SpatialReference = new UnknownCoordinateSystemClass();

                (m_SpatialReference as ISpatialReferenceTolerance).XYTolerance = double.Parse(txtTolerance.Text);
                (m_SpatialReference as ISpatialReferenceResolution).set_XYResolution(true, double.Parse(txtResolution.Text));
                return m_SpatialReference;
            }
            set
            {
                SetUnknown();

                this.m_SpatialReference = value;
                if (this.m_SpatialReference == null)
                    return;

                lblName.Text = m_SpatialReference.Name;
                txtTolerance.Text = (m_SpatialReference as ISpatialReferenceTolerance).XYTolerance.ToString();
                txtResolution.Text = (m_SpatialReference as ISpatialReferenceResolution).get_XYResolution(true).ToString();
                txtInfomation.Text = SpatialReferenceHelper.ToDisplayString(this.m_SpatialReference);

                IUnit unit=null;
                if(m_SpatialReference is IProjectedCoordinateSystem)
                    unit=(m_SpatialReference as IProjectedCoordinateSystem).CoordinateUnit;
                if(m_SpatialReference is IGeographicCoordinateSystem)
                    unit=(m_SpatialReference as IGeographicCoordinateSystem).CoordinateUnit;

                if (unit != null)
                    lblUnit0.Text = lblUnit1.Text = unit.Name;
               
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                ISpatialReference spatialRef = SpatialReferenceHelper.FromPrjFile(dlgOpen.FileName);
                if (spatialRef == null)
                {
                    XtraMessageBox.Show("您所选择的坐标文件格式不正确，请重选!");
                }
                else
                {
                    this.SpatialReference = spatialRef;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.SpatialReference = null;
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (this.m_SpatialReference == null)
            {
                XtraMessageBox.Show("当前坐标系未设定");
                return;
            }

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                SpatialReferenceHelper.SaveToFile(this.m_SpatialReference, dlgSave.FileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {            
            FrmFeatureClassImport frmFeatureClass = new FrmFeatureClassImport();
            frmFeatureClass.Text = "选择参考数据";
            if (frmFeatureClass.ShowDialog() == DialogResult.OK)
            {
                ESRI.ArcGIS.Geodatabase.IFeatureClass fcRefer = frmFeatureClass.FeatureClass;
                if (fcRefer != null)
                {
                    this.SpatialReference = (fcRefer as ESRI.ArcGIS.Geodatabase.IGeoDataset).SpatialReference;
                }
            }
        }

    }
}
