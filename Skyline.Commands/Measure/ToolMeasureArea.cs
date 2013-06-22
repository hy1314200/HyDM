using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Define;
namespace Skyline.Commands
{
    public class ToolMeasureArea:SkylineBaseTool
    {
        public ToolMeasureArea()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "坐标信息";

            this.m_Message = "坐标信息";
            this.m_Tooltip = "使用此工具查看鼠标所在处坐标信息";
        }

        protected override void SetTool()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.IArea, CommandParam.PArea);
        }
    }
}
