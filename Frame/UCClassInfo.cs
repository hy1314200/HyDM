using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Define;
using Frame.Define;

namespace Frame
{
    public partial class UCClassInfo : UserControl
    {
        public UCClassInfo()
        {
            InitializeComponent();
        }

        public ClassInfo ClassInfo
        {
            set
            {
                txtDllName.Text = "";
                txtClassName.Text = "";
                txtRemark.Text = "";

                if (value == null)
                    return;

                txtDllName.Text = value.DllName;
                txtClassName.Text = value.ClassName;
                txtCategory.Text = value.Category;
                txtRemark.Text = value.Description;
            }
        }
    }
}
