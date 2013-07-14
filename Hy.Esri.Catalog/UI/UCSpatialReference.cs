using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Hy.Esri.Catalog.Utility;

namespace Hy.Esri.Catalog.UI
{
    public partial class UCSpatialReference : DevExpress.XtraEditors.XtraUserControl
    {
        public UCSpatialReference()
        {
            InitializeComponent();
        }

        private string m_SpatialReferenceString;
        private ISpatialReference m_SpatailReference;

        public string SpatialReferenceString { get { return m_SpatialReferenceString; } }

        public ISpatialReference SpatialReference { get { return m_SpatailReference; } } 

        private void txtSpatailRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            m_SpatailReference = null;
            m_SpatialReferenceString = null;

            dlgSpatialRef.Filter = "空间参考文件（*.Prj）|*.prj";
            if (dlgSpatialRef.ShowDialog(this) == DialogResult.OK)
            {
                //string[] strSpatialRef = System.IO.File.ReadAllLines(dlgSpatialRef.FileName);
                //if (strSpatialRef.Length > 0)
                //    m_SpatialReferenceString = strSpatialRef[0];

                try
                {
                    m_SpatailReference = SpatialReferenctHelper.CreateSpatialReference(dlgSpatialRef.FileName);
                    m_SpatialReferenceString = SpatialReferenctHelper.ToGpString(m_SpatailReference);

                    txtSpatailRef.Text = m_SpatailReference.Name;
                }
                catch
                {
                    XtraMessageBox.Show("非正确的空间参考文件!");
                }
            }
        }


    }
}
