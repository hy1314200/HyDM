using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public class CommandStandardEdit : StandardBaseCommand
    {

        public CommandStandardEdit()
        {
            this.m_Category = "元数据";
            this.m_Caption = "修改";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && this.m_Manager.SelectedMetaStandard != null;
            }
        }

        public override void OnClick()
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("字段信息的修改将删除当前标准已有数据，您确定要修改吗？", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Hy.Metadata.MetaStandardHelper.DeleteStandard(m_Manager.SelectedMetaStandard);
            }
        }
    }
}
