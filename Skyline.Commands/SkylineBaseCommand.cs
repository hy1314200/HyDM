using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;
using Skyline.Define;
namespace Skyline.Commands
{
    public abstract class SkylineBaseCommand:BaseCommand
    {
        protected ISkylineHook m_SkylineHook;
        public override bool Enabled
        {
            get
            {
                if (base.Enabled)
                {
                    return (m_SkylineHook != null && m_SkylineHook.SGWorld != null && !string.IsNullOrEmpty(m_SkylineHook.SGWorld.Project.Name));
                }
                return false;
            }
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            this.m_SkylineHook = Hook as ISkylineHook;
        }

        public abstract override void OnClick();
    }
}
