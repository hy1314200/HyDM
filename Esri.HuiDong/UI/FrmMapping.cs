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

            m_Mapping = Environment.NhibernateHelper.GetObjectsByCondition<WjToSg>("from WjToSg wts order by wts.SG.FeatureName asc");
            gcMapping.DataSource = m_Mapping;
            gvMapping.RefreshData();

            m_WuJiang = Environment.NhibernateHelper.GetObjectsByCondition<WuJiang>("from WuJiang wj order by wj.FeatureName asc");
        }

        IList<WjToSg> m_Mapping;
        IList<WuJiang> m_WuJiang;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gcWuJiang.DataSource = Environment.NhibernateHelper.GetObjectsByCondition<WuJiang>(string.Format("from WuJiang wj where wj.FeatureName like '%{0}%' order by wj.FeatureName asc", txtMC.Text));
            }
            catch
            {
            }
            gvWuJiang.RefreshData();
        }

        private WjToSg m_SelectedMapping;
        private void gvMapping_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_SelectedMapping = gvMapping.GetFocusedRow() as WjToSg;
            txtMC.Text = m_SelectedMapping.要素名称;
            btnSearch_Click(null, null);
        }

        private void gcWuJaing_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_SelectedMapping.WJ = gvWuJiang.GetFocusedRow() as WuJiang;
            gvMapping.RefreshData();
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
            gcWuJiang.DataSource = m_WuJiang;
            gvWuJiang.RefreshData();
        }


        WuJiang m_CopiedWuJiang = null;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            m_CopiedWuJiang = m_SelectedMapping.WJ;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            m_CopiedWuJiang = gvWuJiang.GetFocusedRow() as WuJiang;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            m_SelectedMapping.WJ = m_CopiedWuJiang;
            gvMapping.RefreshData();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            IList<WuJiang> wjView = new List<WuJiang>();
            wjView.Add(m_SelectedMapping.WJ);
            gcWuJiang.DataSource = wjView;
            gvWuJiang.RefreshData();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            IList<WuJiang> wjView = new List<WuJiang>();
            wjView.Add(m_CopiedWuJiang);
            gcWuJiang.DataSource =wjView;
            gvWuJiang.RefreshData();

        }
    }
}