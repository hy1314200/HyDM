using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;
using TerraExplorerX;

namespace Skyline.Commands
{
    public class CommandAnalysisSkyline:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisSkyline()
        {
            this.m_Category = "三维分析";
            this.m_Caption = "天际线分析";

            this.m_Message = "天际线分析";
            this.m_Tooltip = "天际线分析";
        }

        public override void OnClick()
        {
            IPosition61 CurrentPos = Program.pNavigate6.GetPosition(AltitudeTypeCode.ATC_TERRAIN_RELATIVE);
            CurrentPos.Altitude = 2;
            CurrentPos.Distance = 0;
            CurrentPos.Pitch = 0;
            CurrentPos.Roll = 0;
            Program.pNavigate6.SetPosition(CurrentPos);
        }
    }
}
