using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Skyline.GuiHua.Bussiness;

namespace Skyline.GuiHua.Bussiness
{
    public partial class UCProjectBaseInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public UCProjectBaseInfo()
        {
            InitializeComponent();
        }

        public ProjectInfo Project
        {
            set
            {
                if (value == null)
                    return;

                txtName.Text = value.Name;
                txtType.Text = value.Type;
                txtEnterprise.Text = value.Enterprise;
                txtAddress.Text = value.Address;
            }
        }
    }
}
