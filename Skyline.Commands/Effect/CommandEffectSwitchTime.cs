using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectSwitchTime:Skyline.Define.SkylineBaseCommand
    {
        public CommandEffectSwitchTime()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "时间轴";

            this.m_Message = "打开时间轴";
            this.m_Tooltip = "点击打开或关闭时间轴";
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
                return Checked?"关闭时间轴":"打开时间轴";
            }
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.ITimeSlider, CommandParam.PTimeSlider);
            m_Flag = !m_Flag;
        }
    }
}
