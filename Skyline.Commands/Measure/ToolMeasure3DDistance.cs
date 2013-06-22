using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;
using Define;
namespace Skyline.Commands
{
    public class ToolMeasure3DDistance : SkylineBaseTool
    {
        public ToolMeasure3DDistance()
        {
            this.m_Category = "测量";
            this.m_Caption = "空间距离";

            this.m_Message = "空间距离测量";
            this.m_Tooltip = "使用此工具测量空间距离";
        }
        protected override void SetTool()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Aerial);
        }
    }
}
