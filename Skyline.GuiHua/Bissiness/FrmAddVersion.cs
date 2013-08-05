using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmAddVersion : Form
    {
        public FrmAddVersion()
        {
            InitializeComponent();
        }

        public string Description
        {
            get
            {
                return txtRemark.Text;
            }
        }
    }
}
