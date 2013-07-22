using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hy.Esri.DataManage.Standard;
using ESRI.ArcGIS.Geometry;
using System.Linq;

namespace Hy.Esri.DataManage.UI
{
    public partial class UCFeatureClassInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public UCFeatureClassInfo()
        {
            InitializeComponent();

            foreach (string strType in m_GeometryTypeStirngs)
            {
                cmbGeometryType.Properties.Items.Add(strType);
            }
        }

        private List<string> m_GeometryTypeStirngs =new List<string>(){
                                                   "点",
                                                   "线",
                                                   "面",
                                                   "多点"
                                            };

        private List<esriGeometryType> m_GeoemtryTypes =new List<esriGeometryType>()
        {
            esriGeometryType.esriGeometryPoint,
            esriGeometryType.esriGeometryPolyline,
            esriGeometryType.esriGeometryPolygon,
            esriGeometryType.esriGeometryMultipoint
        };

        public FeatureClassInfo m_FeatrueClassInfo;

        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FeatureClassInfo FeatrueClassInfo
        {
            get
            {
                if (m_FeatrueClassInfo == null)
                    m_FeatrueClassInfo = new FeatureClassInfo();

                m_FeatrueClassInfo.Name = txtName.Text;
                m_FeatrueClassInfo.AliasName = txtAlias.Text;
                m_FeatrueClassInfo.ShapeFieldName = txtShapeField.Text;
                m_FeatrueClassInfo.SpatialReferenceString = txtSpatialReference.Text;
                m_FeatrueClassInfo.ShapeType = m_GeoemtryTypes[cmbGeometryType.SelectedIndex];

                m_FeatrueClassInfo.FieldsInfo = ucFields1.FieldsInfo;
                foreach (Hy.Metadata.FieldInfo fInfo in m_FeatrueClassInfo.FieldsInfo)
                {
                    fInfo.Layer = m_FeatrueClassInfo.ID;
                }

                return m_FeatrueClassInfo;
            }
            set
            {
                m_FeatrueClassInfo = value;
                if (m_FeatrueClassInfo == null)
                    m_FeatrueClassInfo = new FeatureClassInfo();

                txtName.Text = m_FeatrueClassInfo.Name;
                txtAlias.Text = m_FeatrueClassInfo.AliasName;
                txtShapeField.Text = m_FeatrueClassInfo.ShapeFieldName;
                txtSpatialReference.Text = m_FeatrueClassInfo.SpatialReferenceString;

                cmbGeometryType.SelectedIndex = m_GeoemtryTypes.IndexOf(m_FeatrueClassInfo.ShapeType);

                ucFields1.FieldsInfo = m_FeatrueClassInfo.FieldsInfo;
            }
        }

        private bool m_EditAble = false;
        public bool EditAble
        {
            set
            {
                m_EditAble = value;

                this.txtName.Properties.ReadOnly = !m_EditAble;
                this.txtAlias.Properties.ReadOnly = !m_EditAble;
                this.txtShapeField.Properties.ReadOnly = !m_EditAble;
                this.txtSpatialReference.Properties.ReadOnly = !m_EditAble;

                this.cmbGeometryType.Properties.ReadOnly = !m_EditAble;
                this.ucFields1.EditAble = m_EditAble;
            }
        }

    }
}
