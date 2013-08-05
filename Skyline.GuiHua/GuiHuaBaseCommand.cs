using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.GuiHua
{
    public abstract class GuiHuaBaseCommand:Skyline.Define.SkylineBaseCommand
    {
        public GuiHuaBaseCommand()
        {
            this.m_Category = "城市建设方案规划";
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && GuiHua.Bussiness.Environment.m_Project != null;
            }
        }
        public abstract override void OnClick(); 
       
    }
}
