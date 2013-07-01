using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;

namespace Check.UI.Forms
{
    public partial class FrmAddRemark : DevExpress.XtraEditors.XtraForm
    {
        public string m_strRemark;

        public FrmAddRemark(string strRemark)
        {
            InitializeComponent();
            this.txtRemark.Text = strRemark;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_strRemark = this.txtRemark.Text;

            if (m_strRemark.Trim().Length < 1)
            {
                XtraMessageBox.Show("请认真填写例外说明！");
                //DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}