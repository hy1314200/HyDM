using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Esri.DataManage.UI;
using Hy.Esri.DataManage.Standard;

namespace Hy.Esri.DataManage.Command
{
    public  class CommandStandardAddFeatureDataset : DMStandardBaseCommand
    {
        public CommandStandardAddFeatureDataset()
        {
            this.m_Caption = "新建矢量数据集";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && m_Manager.SelectedItem!=null && m_Manager.SelectedItem.Type==enumItemType.Standard;
            }
        }

        FrmFeatureDatasetInfo m_FrmAdd;
        public override void OnClick()
        {
            if (m_FrmAdd == null || m_FrmAdd.IsDisposed)
            {
                m_FrmAdd = new FrmFeatureDatasetInfo();
                m_FrmAdd.Text = "新建矢量数据集";
            }

            StandardItem sItem = new StandardItem();
            sItem.Name = "新建矢量数据集";
            m_FrmAdd.StandardItem = sItem;
            if (m_FrmAdd.ShowDialog() == DialogResult.OK)
            {
                sItem = m_FrmAdd.StandardItem;
                sItem.Type = enumItemType.FeatureDataset;
                sItem.Parent = m_Manager.SelectedItem;

                Environment.NhibernateHelper.SaveObject(sItem);
                Environment.NhibernateHelper.Flush();

                Environment.NhibernateHelper.RefreshObject(m_Manager.SelectedItem, enumLockMode.None);
                this.m_Manager.Refresh();
            }
        }
    }
}
