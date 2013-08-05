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
    public partial class UCProject : DevExpress.XtraEditors.XtraUserControl
    {
        public UCProject()
        {
            InitializeComponent();
        }

        private void UCUrbanProject_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DisplayProxy.GotoProject();
        }
    }
}
