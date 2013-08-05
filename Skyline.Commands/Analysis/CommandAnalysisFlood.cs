using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisFlood:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisFlood()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "淹没分析";

            this.m_Message = "淹没分析";
            this.m_Tooltip = "淹没分析";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.IFlood, CommandParam.PFlood);
        }
    }
}
