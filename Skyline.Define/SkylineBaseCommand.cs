using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;


namespace Skyline.Define
{
    public abstract class SkylineBaseCommand:BaseCommand
    {
        protected ISkylineHook m_SkylineHook;
        public override bool Enabled
        {
            get
            {
                    return base.Enabled && (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
                
            }
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            this.m_SkylineHook = base.m_Hook.Hook as ISkylineHook;
        }

        public abstract override void OnClick();
    }
}
