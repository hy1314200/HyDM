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
    public partial class UCSolution : DevExpress.XtraEditors.XtraUserControl
    {
        public UCSolution()
        {
            InitializeComponent();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit7_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DisplayProxy.GotoSolution();
        }
    }
}
