using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectSwitchSun:Skyline.Define.SkylineBaseCommand
    {
        public CommandEffectSwitchSun()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "阳光";

            this.m_Message = "打开阳光";
            this.m_Tooltip = "点击打开或关闭阳光";
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
                return Checked?"关闭阳光":"打开阳光";
            }
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.ISunshine, CommandParam.PSunshine);
            m_Flag = !m_Flag;
        }
    }
}
