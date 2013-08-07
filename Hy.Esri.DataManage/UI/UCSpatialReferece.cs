using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Hy.Esri.DataManage.UI
{
    internal partial class UCSpatialReferece : UserControl
    {
        public UCSpatialReferece()
        {
            InitializeComponent();
        }

        public ISpatialReference SpatialReference
        {
            get
            {
              return   m_SpatialReference;
            }
            set
            {
                this.txtSpatialReference.Text = "";
                m_SpatialReference = value;
                if (this.m_SpatialReference != null)
                    this.txtSpatialReference.Text = this.m_SpatialReference.Name;
            }
        }

        private ISpatialReference m_SpatialReference;

        public string SpatialReferencePrjString
        {
            get
            {
                return Hy.Esri.Utility.SpatialReferenceHelper.ToPrjString(this.m_SpatialReference);
            }
            set
            {
                this.txtSpatialReference.Text = "";
                if (string.IsNullOrWhiteSpace(value))
                    return;

                this.m_SpatialReference = Hy.Esri.Utility.SpatialReferenceHelper.FromPrjString(value);
                if (this.m_SpatialReference != null)
                    this.txtSpatialReference.Text = this.m_SpatialReference.Name;
            }
        }

        public override string Text
        {
            get
            {
                return this.SpatialReferencePrjString;
            }
            set
            {
                this.SpatialReferencePrjString = value;
            }
        }

        private void txtSpatialReference_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Hy.Esri.Utility.UI.FrmSpatialReference frmSpatialRef = new Utility.UI.FrmSpatialReference();
            frmSpatialRef.EditAble = this.m_EditAble;
            frmSpatialRef.SpatialReference = this.m_SpatialReference;
            if (frmSpatialRef.ShowDialog() == DialogResult.OK)
            {
                this.SpatialReference = frmSpatialRef.SpatialReference;
            }

        }

        private bool m_EditAble = false;
        public bool EditAble
        {
            set
            {
                this.m_EditAble = value;
            }
        }
    }
}
