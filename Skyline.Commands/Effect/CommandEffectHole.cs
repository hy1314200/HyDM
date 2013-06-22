using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectHole:SkylineBaseCommand
    {
        public CommandEffectHole()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "地面挖开";

            this.m_Message = "地面挖开";
            this.m_Tooltip = "用鼠标画一个多边形进行挖开";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ICreateHole, CommandParam.PCreateHole);
        }
    }
}
