using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hy.Metadata.UI
{
    public partial class FrmParameterSetting : DevExpress.XtraEditors.XtraForm
    {
        public FrmParameterSetting()
        {
            InitializeComponent();
        }

        IList<ConfigItem> m_Datasource = null;
        public void Init()
        {
            m_Datasource = Environment.NhibernateHelper.GetAll<ConfigItem>();
            gcSetting.DataSource = m_Datasource;   
        }
        public Define.MessageHandler MessageHandler;
        private void SendMessage(string strMsg)
        {
            if (this.MessageHandler != null)
                this.MessageHandler.Invoke(strMsg);
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = m_Datasource.Count;
            for (int i = 0; i < count; i++)
            {
                SendMessage(string.Format("正在保存{0}/{1}...", i + 1, count));
                Environment.NhibernateHelper.SaveObject(m_Datasource[i]);
            }
            SendMessage("正在写入数据库...");
            Environment.NhibernateHelper.Flush();
            SendMessage(string.Format("保存成功，共{0}项.",count));
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            m_Datasource.Add(new ConfigItem());
            gvSetting.RefreshData();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (m_FocusedItem != null)
            {
                if (!string.IsNullOrEmpty(m_FocusedItem.ID))
                {
                    Environment.NhibernateHelper.DeleteObject(m_FocusedItem);
                    Environment.NhibernateHelper.Flush();
                }

                m_Datasource.Remove(m_FocusedItem);
                gvSetting.RefreshData();
            }
        }
        ConfigItem m_FocusedItem = null;
        private void gvSetting_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            m_FocusedItem = gvSetting.GetRow(e.FocusedRowHandle) as ConfigItem;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Init();
        }
    }
}