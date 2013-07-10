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
            this.m_Caption = "修改元数据标准";
            this.m_Message = "修改元数据标准定义";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && this.m_Manager.CurrentMetaStandard != null;
            }
        }

        FrmStandardProperty m_FrmEdit;
        public override void OnClick()
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("标准的修改将导致此标准的数据表重建，您确定要修改吗？", "修改确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (m_FrmEdit == null || m_FrmEdit.IsDisposed)
                {
                    m_FrmEdit = new FrmStandardProperty();
                    m_FrmEdit.ViewMode = FrmStandardProperty.enumPropertyViewMode.Edit;
                }
                m_FrmEdit.CurrentStandard = m_Manager.CurrentMetaStandard;
                m_FrmEdit.Text = string.Format("元数据标准[{0}]修改", m_Manager.CurrentMetaStandard.Name);
                if (m_FrmEdit.ShowDialog(base.m_Hook.UIHook.MainForm) == DialogResult.OK)
                {
                    MetaStandardHelper.SaveStandard(m_FrmEdit.CurrentStandard);
                }

                this.m_Manager.Refresh();
            }
        }
    }
}
