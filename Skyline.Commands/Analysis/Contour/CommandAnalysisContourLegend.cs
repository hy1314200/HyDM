using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisContourLegend:Skyline.Define.SkylineBaseCommand
    {
        public CommandAnalysisContourLegend()
        {
            this.m_Category = "等高线";
            this.m_Caption = "等高线图例";

            this.m_Message = "等高线图例设置";
            this.m_Tooltip = "点击进行等高线图例设置";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.IContourPallets, CommandParam.PContourPallets);
        }
    }
}
