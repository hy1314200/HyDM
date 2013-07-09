using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Metadata.UI
{
    public partial class UCStandardList : DevExpress.XtraEditors.XtraUserControl
    {
        public UCStandardList()
        {
            InitializeComponent();

            lbStandards.DataSource = MetaStandardHelper.GetAll();
        }

        public MetaStandard SelectedStandard
        {
            get
            {
                return lbStandards.SelectedItem as MetaStandard;
            }
            set
            {
                lbStandards.SelectedItem = value;
            }
        }

        public event MethodInvoker SelectedStandardChanged;
        private void lbStandards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedStandardChanged != null)
                this.SelectedStandardChanged.Invoke();
        }
    }
}
