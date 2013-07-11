using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Esri.HuiDong.Model;

namespace Esri.HuiDong.UI
{
    public partial class FrmMapping : DevExpress.XtraEditors.XtraForm
    {
        public FrmMapping()
        {
            InitializeComponent();

            m_Mapping = Environment.NhibernateHelper.GetObjectsByCondition<WjToSg>("from WjToSg wts order by wts.FeatureName asc");
            gcWuJiang.DataSource = m_Mapping;
            gvWuJaing.RefreshData();

            m_Shaoguan = Environment.NhibernateHelper.GetObjectsByCondition<SgMapping>("from SgMapping sg order by sg.FeatureName asc");
        }

        IList<WjToSg> m_Mapping;
        IList<SgMapping> m_Shaoguan;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gcSgMapping.DataSource = Environment.NhibernateHelper.GetObjectsByCondition<SgMapping>(string.Format("from SgMapping sg where sg.FeatureName like '%{0}%' order by sg.FeatureName asc", txtMC.Text));

            }
            catch
            {
            }
            gvSgMapping.RefreshData();
        }

        private WjToSg m_SelectedMapping;
        private void gvWuJaing_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_SelectedMapping = gvWuJaing.GetFocusedRow() as WjToSg;
            txtMC.Text = m_SelectedMapping.FeatureName;
            btnSearch_Click(null, null);
        }

        private void gcSgMapping_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_SelectedMapping.SG = gvSgMapping.GetFocusedRow() as SgMapping;
            gvWuJaing.RefreshData();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_Mapping.Count; i++)
            {
                Environment.NhibernateHelper.SaveObject(m_Mapping[i]);
            }
            Environment.NhibernateHelper.Flush();
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gcSgMapping.DataSource = m_Shaoguan;
            gvSgMapping.RefreshData();
        }
    }
}