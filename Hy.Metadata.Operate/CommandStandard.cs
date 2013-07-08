using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;

namespace Hy.Metadata.Operate
{
    public class CommandStandard : Common.Operate.CommandDockable
    {
        public CommandStandard()
        {
            this.m_Category = "ÊÓÍ¼¿ØÖÆ";
            this.m_Caption = "Í¼²ãÄ¿Â¼";
        }
        private Hy.Metadata.UI.UCMetadataStandard m_TocControl;
        protected override Control CreateControl()
        {
            m_TocControl = new UI.UCMetadataStandard();
            return m_TocControl;
        }

        protected override Common.Operate.CommandDockable.enumDockPosition DockPosition
        {
            get { return enumDockPosition.Left; }
        }

        protected override void Init()
        {
            m_TocControl.CurrentStandard = new MetaStandard();
        }

        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

    }
}
