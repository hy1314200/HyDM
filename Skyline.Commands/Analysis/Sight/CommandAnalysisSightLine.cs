using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisSightLine:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisSightLine()
        {
            this.m_Category = "通视分析";
            this.m_Caption = "单点通视";

            this.m_Message = "单点通视";
            this.m_Tooltip = "显示场景中指定2个位置点间的通视性";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ILineOfSight, CommandParam.PLineOfSight);
        }
    }
}
