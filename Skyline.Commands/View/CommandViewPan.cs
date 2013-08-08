using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewPan:Skyline.Define.SkylineBaseCommand
    {
        public CommandViewPan()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "漫游";

            this.m_Message = "漫游";
            this.m_Tooltip = "点击进入漫游模式";
        }

        public override void OnClick()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Drag);
        }
    }
}
