using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

using Skyline.Core.UI;
using System.Windows.Forms;

namespace Skyline.Commands
{
    public class CommandAnalysisVolumeEx:SkylineBaseCommand
    {
        public CommandAnalysisVolumeEx()
        {
            this.m_Category = "土方";
            this.m_Caption = "土方统计";

            this.m_Message = "土方统计";
            this.m_Tooltip = "统计场景中的填挖方量";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld,CommandParam.Iearthwork, CommandParam.Pearthwork);
        }
    }
}
