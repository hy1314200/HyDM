using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisBestPath:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisBestPath()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "最佳路径";

            this.m_Message = "最佳路径分析";
            this.m_Tooltip = "场景中两点间最佳地表路径";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.IBestpath, CommandParam.PBestpath);
        }
    }
}
