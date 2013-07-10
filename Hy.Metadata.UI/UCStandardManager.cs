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
    public partial class UCStandardManager : DevExpress.XtraEditors.XtraUserControl,IStandardManager
    {
        public UCStandardManager()
        {
            InitializeComponent();


            this.m_UcStandardList = new UCStandardList();
            this.gcStandard.Controls.Add(this.m_UcStandardList);
            this.m_UcStandardList.Dock = DockStyle.Fill;
            
            this.m_UcStandardList.SelectedStandardChanged += delegate
            {
                //this.m_UcStandardProperty.CurrentStandard = this.m_UcStandardList.SelectedStandard;
                this.ucMetadata1.CurrentStandard = this.m_UcStandardList.SelectedStandard;
            };
        }
        UCStandardList m_UcStandardList;

        public MetaStandard CurrentMetaStandard
        {
            get { return this.m_UcStandardList.SelectedStandard; }
        }

        public void Refresh()
        {
            this.m_UcStandardList.Refresh();
        }
    }
}
