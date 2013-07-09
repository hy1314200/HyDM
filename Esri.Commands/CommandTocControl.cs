using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;


namespace Esri.Commands
{
    public class CommandTocControl : Common.Operate.CommandDockable
    {
        public CommandTocControl()
        {
            this.m_Category = "ÊÓÍ¼¿ØÖÆ";
            this.m_Caption = "Í¼²ãÄ¿Â¼";
        }
        private ESRI.ArcGIS.Controls.AxTOCControl m_TocControl = null;
        protected override Control CreateControl()
        {
            m_TocControl= new AxTOCControl();
            return m_TocControl;
        }

        protected override enumDockPosition DockPosition
        {
            get { return enumDockPosition.Left; }
        }

        public override bool Enabled
        {
            get
            {
                return (m_hookHelper != null && m_hookHelper.Hook != null);
            }
        }
        protected override void Init()
        {
            m_TocControl.SetBuddyControl(m_hookHelper.Hook);
        }
        protected IHookHelper m_hookHelper = null;
       
        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);

            m_hookHelper = base.m_Hook.Hook as IHookHelper;
        }
    }

}
