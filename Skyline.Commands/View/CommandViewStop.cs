using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewStop:Skyline.Define.SkylineBaseCommand
    {
        public CommandViewStop()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "停止";

            this.m_Message = "停止";
            this.m_Tooltip = "点击视窗停止";
        }

        public override void OnClick()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Stop);
        }
    }
}
