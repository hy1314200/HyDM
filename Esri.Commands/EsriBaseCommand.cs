using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;
using Esri.Define;
using ESRI.ArcGIS.Controls;

namespace Esri.Commands
{
    public abstract class EsriBaseCommand:BaseCommand
    {
        /// <summary>
        /// ESRI通用HookHelper，为了使ESRI下编写的代码能直接使用，H使用小写
        /// </summary>
        protected IHookHelper m_hookHelper = null;
        public override bool Enabled
        {
            get
            {
                if (base.Enabled)
                {
                    return (m_hookHelper != null && m_hookHelper.ActiveView != null);
                }
                return false;

            }
        }

        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);
            IEsriHook esriHook = Hook as IEsriHook;
            if (esriHook != null)
                m_hookHelper = esriHook.HookHelper;
        }
        public abstract override void OnClick();
    }
}
