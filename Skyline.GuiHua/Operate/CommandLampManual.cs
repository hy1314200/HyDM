using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandLampManual:GuiHuaBaseCommand
    {
        public CommandLampManual()
        {
            this.m_Caption = "信号灯手动分析";

            this.m_Message = "信号灯手动分析";
            this.m_Tooltip = "信号灯手动分析";
        }

        public override bool Enabled
        {
            get
            {
                return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
            }
        }

           FrmLampManual m_FrmLampManual = null;
        public override void OnClick()
        {
            try
            {
                if (m_FrmLampManual == null || m_FrmLampManual.IsDisposed)
                {
                    m_FrmLampManual = new FrmLampManual(  m_SkylineHook.TerraExplorer, m_SkylineHook.SGWorld);
                    this.m_Hook.UIHook.MainForm.AddOwnedForm(m_FrmLampManual);
                }
                if (m_FrmLampManual.Visible == false)
                    m_FrmLampManual.Show(this.m_Hook.UIHook.MainForm);
            }
            catch
            {
                MessageBox.Show("发生错误!");
            }
        }
    }
}
