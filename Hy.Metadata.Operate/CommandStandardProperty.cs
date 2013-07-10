using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public class CommandStandardProperty : StandardBaseCommand
    {

        public CommandStandardProperty()
        {
            this.m_Category = "元数据";
            this.m_Caption = "属性";
            this.m_Message = "查看元数据标准定义";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && this.m_Manager.CurrentMetaStandard != null;
            }
        }
        FrmStandardProperty m_FrmProperty;
        public override void OnClick()
        {

            if (m_FrmProperty == null || m_FrmProperty.IsDisposed)
            {
                m_FrmProperty = new FrmStandardProperty();
                m_FrmProperty.ViewMode = FrmStandardProperty.enumPropertyViewMode.View;
            }
            m_FrmProperty.CurrentStandard = m_Manager.CurrentMetaStandard;
            m_FrmProperty.Text = string.Format("查看元数据标准[{0}]的定义", m_Manager.CurrentMetaStandard.Name);
            m_FrmProperty.ShowDialog(base.m_Hook.UIHook.MainForm);
            

        }
    }
}
