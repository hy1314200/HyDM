using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public class CommandStandardDelete : StandardBaseCommand
    {

        public CommandStandardDelete()
        {
            this.m_Category = "元数据";
            this.m_Caption = "元数据";
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
            if (DevExpress.XtraEditors.XtraMessageBox.Show("删除标准也将删除当前标准已有数据，您确定要修改吗？", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.m_Manager.SetEditStandard(this.m_Manager.SelectedMetaStandard);
            }
        }
    }
}
