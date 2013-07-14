using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Esri.Catalog.UI
{
    public partial class FrmModelLocation : DevExpress.XtraEditors.XtraForm
    {
        public FrmModelLocation()
        {
            InitializeComponent();
        }

        public double X
        {
            get
            {
                return double.Parse(txtX.Text);
            }
        }

        public double Y
        {
            get
            {
                return double.Parse(txtY.Text);
            }
        }

        public double Z
        {
            get
            {
                return double.Parse(txtZ.Text);
            }
        }
    }
}