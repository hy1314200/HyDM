using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF.BaseClasses;

using Hy.Esri.Catalog.Define;

namespace Hy.Esri.Catalog.Command.Catalog
{
    public abstract class CatalogBaseCommand:global::Define.BaseCommand
    {
        protected Hy.Esri.Catalog.Define.CatalogHookHelper m_HookHelper;
        //private static Hy.Esri.Catalog.Define.CatalogHookHelper m_DefaultHookHelper = new Hy.Esri.Catalog.Define.CatalogHookHelper();
        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            m_HookHelper = base.m_Hook.Hook as Hy.Esri.Catalog.Define.CatalogHookHelper;

            //if (m_HookHelper == null)
            //    m_HookHelper = m_DefaultHookHelper;
        }

        public override void OnClick()
        {
        }
        public override bool Enabled
        {
            get
            {
                return base.Enabled && m_HookHelper != null;
            }
        }
    }
}
