using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisContour:SkylineBaseCommand
    {
        public CommandAnalysisContour()
        {
            this.m_Category = "等高线";
            this.m_Caption = "等高线显示";

            this.m_Message = "等高线显示";
            this.m_Tooltip = "显示选定区域内的等高线";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.IContourColorsandLines, CommandParam.PContourColorsandLines);
        }
    }
}
