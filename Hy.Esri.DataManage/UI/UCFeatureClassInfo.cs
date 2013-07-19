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

            cmbGeometryType.Properties.Items.Add("");
            cmbGeometryType.Properties.Items.Add("");
            cmbGeometryType.Properties.Items.Add("");
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


    }
}
