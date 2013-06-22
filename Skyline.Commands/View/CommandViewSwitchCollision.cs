using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandViewSwitchCollision:SkylineBaseCommand
    {
        public CommandViewSwitchCollision()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "碰撞模式";

            this.m_Message = "打开碰撞模式";
            this.m_Tooltip = "点击打开或关闭碰撞模式";
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
                return Checked?"关闭碰撞模式":"打开碰撞模式";
            }
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ICollisionDetection, CommandParam.PCollisionDetection);
            m_Flag = !m_Flag;
        }
    }
}
