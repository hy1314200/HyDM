using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisMultiSightLine:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisMultiSightLine()
        {
            this.m_Category = "通视分析";
            this.m_Caption = "多点通视";

            this.m_Message = "多点通视";
            this.m_Tooltip = "显示场景中指定多个位置点间的通视性";
        }

        public override void OnClick()
        {
            throw new NotImplementedException();
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ILineOfSight, CommandParam.PLineOfSight);
        }
    }
}
