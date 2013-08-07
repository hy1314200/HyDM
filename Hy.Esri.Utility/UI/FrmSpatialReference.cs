using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Esri.Utility.UI
{
    public partial class FrmSpatialReference : DevExpress.XtraEditors.XtraForm
    {
        public FrmSpatialReference()
        {
            InitializeComponent();
        }

        public bool EditAble
        {
            set
            {
                this.ucSpatialReference1.EditAble = value;
            }
        }

        public ESRI.ArcGIS.Geometry.ISpatialReference SpatialReference
        {
            get
            {
                return this.ucSpatialReference1.SpatialReference;
            }
            set
            {
                this.ucSpatialReference1.SpatialReference = value;
            }
        }
    }
}