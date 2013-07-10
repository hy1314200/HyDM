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
    public partial class UCMetadata : DevExpress.XtraEditors.XtraUserControl
    {
        public UCMetadata()
        {
            InitializeComponent();
        }

        private MetaStandard m_CurrentStandard;
        private int m_CountPerPage = 100;
        private int m_DataCount = -1;
        public MetaStandard CurrentStandard
        {
            get
            {
                return m_CurrentStandard;
            }
            set
            {
                m_CurrentStandard = value;
                this.gcMetadata.DataSource = null;

                if (m_CurrentStandard == null)
                    return;

                m_DataCount = -1;
                RequiredData(0);

                int pageCount = (int)Math.Ceiling(m_DataCount / (double)m_CountPerPage);

                ucNavigate1.PageCount = pageCount;
                ucNavigate1.PageIndex = 0;
            }
        }


        private void RequiredData(int pageIndex)
        {
            DataTable dtData = Hy.Metadata.MetaStandardHelper.GetMetadata(m_CurrentStandard, null, m_CountPerPage, pageIndex, ref m_DataCount);
            gcMetadata.DataSource = dtData;
            gvMetadata.RefreshData();
        }
    }
}
