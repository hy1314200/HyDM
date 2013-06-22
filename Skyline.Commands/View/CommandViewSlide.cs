using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewSlide:SkylineBaseCommand
    {
        public CommandViewSlide()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "浏览模式";

            this.m_Message = "浏览模式";
            this.m_Tooltip = "点击进入浏览模式";
        }

        private bool m_Flag = false;
        public override bool Checked
        {
            get
            {
                return m_Flag;
            }
        }
        public override void OnClick()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Slide);
            m_Flag = !m_Flag;
        }
    }
}
