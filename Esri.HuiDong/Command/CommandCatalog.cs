using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;

using Esri.HuiDong.UI;



namespace Esri.HuiDong
{
    public class CommandCatalog : Common.Operate.CommandDockable
    {
        public CommandCatalog()
        {
            this.m_Caption = "Êý¾ÝÄ¿Â¼";
        }
        private UCCatalog m_UcCatalog = null;
        protected override Control CreateControl()
        {
            m_UcCatalog = new UCCatalog();
            return m_UcCatalog;
        }

        protected override enumDockPosition DockPosition
        {
            get { return enumDockPosition.Left; }
        }

        protected override void Init()
        {

        }
        protected IHookHelper m_hookHelper = null;
       
        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);

            m_hookHelper = base.m_Hook.Hook as IHookHelper;
        }
    }

}
