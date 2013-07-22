using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard;

namespace Hy.Esri.DataManage.UI
{
    public partial class UCFeatureDataset : DevExpress.XtraEditors.XtraUserControl
    {
        public UCFeatureDataset()
        {
            InitializeComponent();
        }

        private StandardItem m_FeatureDatasetInfo;

        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StandardItem StandardItem
        {
            get
            {
                if (m_FeatureDatasetInfo == null)
                {
                    m_FeatureDatasetInfo = new StandardItem();
                }
                m_FeatureDatasetInfo.Name = txtName.Text;
                m_FeatureDatasetInfo.SpatialReferenceString = txtSpatialReference.Text;

                return m_FeatureDatasetInfo;
            }
            set
            {
                m_FeatureDatasetInfo = value;

                if (m_FeatureDatasetInfo == null)
                    m_FeatureDatasetInfo = new StandardItem();

                txtName.Text = m_FeatureDatasetInfo.Name;
                txtSpatialReference.Text = m_FeatureDatasetInfo.SpatialReferenceString;
            }
        }

        private bool m_EditAble = false;
        public bool EditAble
        {
            set
            {
                m_EditAble = value;
                this.txtName.Properties.ReadOnly = !m_EditAble;
                this.txtSpatialReference.Properties.ReadOnly = !m_EditAble;
            }
        }
    }
}
