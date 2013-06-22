using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Frame
{
    public partial class FrmLoging : DevExpress.XtraEditors.XtraForm
    {
        public FrmLoging()
        {
            InitializeComponent();
        }

        public void SetMessage(string strMsg)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker d = delegate { SetMessage(strMsg); };
                this.Invoke(d);
            }
            else
            {
                this.lblMessage.Text = strMsg;
                Application.DoEvents();
            }
        }
    }
}