using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.GuiHua.Bussiness;
using System.Windows.Forms;
using System.IO;

namespace Skyline.GuiHua.Operate
{
    public class CommandLampAutomatic:GuiHuaBaseCommand
    {
        public CommandLampAutomatic()
        {
            this.m_Caption = "信号灯自动化";

            this.m_Message = "信号灯自动化生成";
            this.m_Tooltip = "自动化生成信号灯";
        }

        public override bool Enabled
        {
            get
            {
                return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
            }
        }

        public override void OnClick()
        {
            FrmLampAnalysis frmLamp = new FrmLampAnalysis(m_SkylineHook.SGWorld);
            try
            {
                frmLamp.ShowDialog(this.m_Hook.UIHook.MainForm);
            }
            catch
            {
                MessageBox.Show("抱歉，当前操作出现了意外！");
            }
        }
    }
}
