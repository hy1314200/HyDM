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
            this.splitContainerControl1.Panel1.Controls.Add(this.m_UcStandardList);
            this.m_UcStandardList.Dock = DockStyle.Fill;

            this.m_UcStandardProperty = new UCStandardProperty();            
            this.splitContainerControl1.Panel1.Controls.Add(this.m_UcStandardProperty);
            this.m_UcStandardProperty.Dock = DockStyle.Fill;

            this.m_UcStandardList.SelectedStandardChanged += delegate
            {
                this.m_UcStandardProperty.CurrentStandard = this.m_UcStandardList.SelectedStandard;
            };
        }
        UCStandardList m_UcStandardList;
        UCStandardProperty m_UcStandardProperty;

        public IList<MetaStandard> AllMetaStandard
        {
            get { return m_UcStandardList.AllStandard; }
        }

        public MetaStandard SelectedMetaStandard
        {
            get { return this.m_UcStandardProperty.CurrentStandard; }
        }


        public void SetEditStandard(MetaStandard standard)
        {
            this.m_UcStandardProperty.CurrentStandard = standard;
            this.m_UcStandardProperty.EditAble = true;
        }

        public MetaStandard NewStandard()
        {
            return this.m_UcStandardList.NewStandard();
        }

        public void Refresh()
        {
            this.m_UcStandardList.Refresh();
        }
    }
}
