using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Define;
namespace Skyline.Commands
{
    public class ToolMeasureVertical:SkylineBaseTool
    {
        public ToolMeasureVertical()
        {
            this.m_Category = "测量";
            this.m_Caption = "垂直";

            this.m_Message = "垂直测量";
            this.m_Tooltip = "使用此工具测量垂直距离";
        }
        protected override void SetTool()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Vertical);
        }
    }
}
