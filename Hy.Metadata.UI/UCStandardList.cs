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

            this.Refresh();
        }

        public void Refresh()
        {
            lbStandards.DataSource = MetaStandardHelper.GetAll();
        }

        public MetaStandard NewStandard()
        {
            //IList<MetaStandard> dsSource=lbStandards.DataSource as IList<MetaStandard>;
            //if (dsSource == null)
            //{
            //    dsSource = new List<MetaStandard>();
            //    lbStandards.DataSource = dsSource;
            //}
            MetaStandard standard = new MetaStandard();
            lbStandards.Items.Add(standard);
            lbStandards.SelectedItem = standard;

            return standard;
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

        public IList<MetaStandard> AllStandard
        {
            get
            {
                return lbStandards.DataSource as IList<MetaStandard>;
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
