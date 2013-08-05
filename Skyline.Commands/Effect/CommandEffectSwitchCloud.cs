using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectSwitchCloud:Skyline.Define.SkylineBaseCommand
    {
        public CommandEffectSwitchCloud()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "大气云层";

            this.m_Message = "打开大气云层";
            this.m_Tooltip = "点击打开或关闭大气云层";
        }

        private bool m_Flag = false;
        public override bool Checked
        {
            get
            {
                return m_Flag;
            }
        }

        public override string Message
        {
            get
            {
                return Checked ? "关闭打开大气云层" : "打开大气云层";
            }
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.IClouds, CommandParam.PClouds);
            m_Flag = !m_Flag;
        }
    }
}
