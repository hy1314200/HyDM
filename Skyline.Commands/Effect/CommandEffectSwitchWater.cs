using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectSwitchWater:SkylineBaseCommand
    {
        public CommandEffectSwitchWater()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "流动水面";

            this.m_Message = "打开流动水面模式";
            this.m_Tooltip = "点击打开或关闭流动水面模式";
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
                return Checked ? "关闭流动水面模式" : "打开流动水面模式";
            }
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.IWater, CommandParam.PWater);
            m_Flag = !m_Flag;
        }
    }
}
