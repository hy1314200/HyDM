using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Define;
namespace Skyline.Commands
{
    public class ToolMeasureHorizontal:SkylineBaseTool
    {
        public ToolMeasureHorizontal()
        {
            this.m_Category = "测量";
            this.m_Caption = "水平";

            this.m_Message = "水平测量";
            this.m_Tooltip = "使用此工具测量水平距离";
        }
        protected override void SetTool()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Horizontal);
        }
    }
}
