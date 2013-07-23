using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;
using DevExpress.XtraEditors;

namespace Hy.Esri.DataManage.Command
{
    public class CommandStandardDelete : DMStandardBaseCommand
    {

        public CommandStandardDelete()
        {
            this.m_Caption = "删除";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && m_Manager.SelectedItem!=null;
            }
        }

        public override void OnClick()
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("您确定要删除吗", "删除确认", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (Standard.Helper.StandardHelper.DeleteStandard(m_Manager.SelectedItem))
            {
                XtraMessageBox.Show("删除成功"); 
                this.m_Manager.Refresh();
            }
            else
            {
                XtraMessageBox.Show("删除操作发生错误！"); 
            }
        }
    }
}
