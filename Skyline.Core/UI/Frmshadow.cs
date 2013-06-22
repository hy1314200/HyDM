using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;

namespace Skyline.Core.UI
{
    public partial class Frmshadow : FrmBase
    {
        private Form _frmMain;
        public Frmshadow(Form frmMain)
        {
            _frmMain = frmMain;
            if (base.BeginForm(frmMain))
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
        }

        private void Frmshadow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMain.RemoveOwnedForm(this);
        }

        private void Frmshadow_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri(ConfigurationManager.AppSettings["ShadowURL"]);
        }
    }
}