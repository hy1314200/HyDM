using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewNorthDirect:Skyline.Define.SkylineBaseCommand
    {
        public CommandViewNorthDirect()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "正北观看";

            this.m_Message = "正北观看";
            this.m_Tooltip = "点击进入正北观看";
        }

        public override void OnClick()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.FaceNorth);
        }
    }
}
