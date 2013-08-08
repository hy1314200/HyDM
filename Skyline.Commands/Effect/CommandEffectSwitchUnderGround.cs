using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEffectSwitchUnderGround:Skyline.Define.SkylineBaseCommand
    {
        public CommandEffectSwitchUnderGround()
        {
            this.m_Category = "三维特效";
            this.m_Caption = "地下模式";

            this.m_Message = "打开地下模式";
            this.m_Tooltip = "点击打开或关闭地下模式";
        }

        private bool m_Flag = false;
        public override bool Checked
        {
            get
            {
                return m_Flag;
            }
        }

        public override string Message
        {
            get
            {
                return Checked ? "关闭地下模式" : "打开地下模式";
            }
        }

        public override void OnClick()
        {
            this.m_SkylineHook.TerraExplorer.Invoke((int)InvokeNumber.Underground);
            m_Flag = !m_Flag;
        }
    }
}
