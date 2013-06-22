using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Common.UI
{
    public partial class FrmAbout : Define.FrmAbout
    {
        public FrmAbout()
        {
            InitializeComponent();
            this.Text += "通用组件";
        }
    }
}