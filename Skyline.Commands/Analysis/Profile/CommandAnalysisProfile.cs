using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisProfile:SkylineBaseCommand
    {
        public CommandAnalysisProfile()
        {
            this.m_Category = "剖面分析";
            this.m_Caption = "剖面分析";

            this.m_Message = "剖面分析";
            this.m_Tooltip = "从场景选定剖面进行分析";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ITerrainProfile, CommandParam.PTerrainProfile);
        }
    }
}
