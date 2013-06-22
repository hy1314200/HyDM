using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skyline.Core.UI
{
    public partial class FrmWriteDataCreatContour : Form
    {
        /// <summary>
        /// 采样间隔
        /// </summary>
        public double interval = 10;

        public double[] extent = new double[4];


        public FrmWriteDataCreatContour()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            extent[0] = Convert.ToDouble(this.spinEdit2.Value);
            extent[1] = Convert.ToDouble(this.spinEdit3.Value);
            extent[2] = Convert.ToDouble(this.spinEdit4.Value);
            extent[3] = Convert.ToDouble(this.spinEdit5.Value);
            interval =  Convert.ToDouble(this.spinEdit1.Value);
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
