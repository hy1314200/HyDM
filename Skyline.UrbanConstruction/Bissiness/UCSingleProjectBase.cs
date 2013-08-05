using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Skyline.UrbanConstruction.Bussiness
{
    public partial class UCSingleProjectBase : DevExpress.XtraEditors.XtraUserControl
    {
        public UCSingleProjectBase()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DisplayProxy.GotoBuilding();
        }
    }
}
