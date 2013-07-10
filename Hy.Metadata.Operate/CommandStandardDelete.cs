using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;
using DevExpress.XtraEditors;

namespace Hy.Metadata.Operate
{
    public class CommandStandardDelete : StandardBaseCommand
    {

        public CommandStandardDelete()
        {
            this.m_Category = "元数据";
            this.m_Caption = "删除元数据标准";
            this.m_Message = "删除元数据标准定义";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && this.m_Manager.CurrentMetaStandard != null;
            }
        }

        public override void OnClick()
        {
            if (XtraMessageBox.Show("删除标准也将删除当前标准已有数据，您确定要修改吗？", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!MetaStandardHelper.DeleteStandard(m_Manager.CurrentMetaStandard))
                {
                    XtraMessageBox.Show(MetaStandardHelper.ErrorMessage);
                }
                else
                {
                    m_Manager.Refresh();
                }
            }
        }
    }
}
