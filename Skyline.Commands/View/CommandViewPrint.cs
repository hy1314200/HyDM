using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewPrint:SkylineBaseCommand
    {
        public CommandViewPrint()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "视图快照";

            this.m_Message = "视图快照";
            this.m_Tooltip = "点击将当前视图保存为图片";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(m_SkylineHook.SGWorld, CommandParam.ISaveCamera, CommandParam.PSaveCamera);
        }
    }
}
