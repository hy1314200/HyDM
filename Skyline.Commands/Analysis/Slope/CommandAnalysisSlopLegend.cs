using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisSlopLegend:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisSlopLegend()
        {
            this.m_Category = "坡计算";
            this.m_Caption = "坡度图例";

            this.m_Message = "坡度图例设置";
            this.m_Tooltip = "点击进行坡度图例设置";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ISlopePalettess, CommandParam.PSlopePalettess);
        }
    }
}
