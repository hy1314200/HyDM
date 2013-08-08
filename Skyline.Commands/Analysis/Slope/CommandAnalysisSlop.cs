using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisSlop:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisSlop()
        {
            this.m_Category = "坡计算";
            this.m_Caption = "坡度计算";

            this.m_Message = "坡度计算";
            this.m_Tooltip = "对场景中选定区域进行坡度分析";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ISlopeColorMap, CommandParam.PSlopeColorMap);
        }
    }
}
