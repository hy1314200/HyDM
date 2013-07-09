using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;

namespace Hy.Metadata.Operate
{
    public class CommandStandard : BaseCommand
    {
        public CommandStandard()
        {
            this.m_Category = "元数据";
            this.m_Caption = "元数据";
        }
        private Hy.Metadata.UI.UCMetadataStandard m_TocControl;
     
        public override bool Checked
        {
            get
            {
                return (m_TocControl != null && m_TocControl.Visible);
            }
        }

        public override bool Enabled
        {
            get
            {
                return true;
            }
        }


        private class MetadataHooker : IHooker
        {
            public MetadataHooker(Hy.Metadata.UI.UCMetadataStandard uc)
            {
                this.m_UcMetadata = uc;
            }
            public string Caption
            {
                get { return "元数据"; }
            }

            private Guid m_Guid = Guid.NewGuid();
            public Guid ID
            {
                get { return m_Guid; }
            }

            private Hy.Metadata.UI.UCMetadataStandard m_UcMetadata;

            public Control Control
            {
                get
                {
                    return m_UcMetadata;
                }
            }

            public object Hook
            {
                get { return null; }
            }
        }

        private Guid m_Guid = Guid.Empty;
        public override void OnClick()
        {
            if (m_TocControl != null && m_TocControl.Visible)
            { 
                this.m_Hook.UIHook.CloseHookControl(m_Guid);
            }
            else
            {
                if (m_TocControl == null)
                {
                    m_TocControl = new UI.UCMetadataStandard();
                    m_TocControl.CurrentStandard = new MetaStandard();
                    IHooker hooker= new MetadataHooker(m_TocControl);
                    m_Guid=hooker.ID;
                    base.m_Hook.UIHook.AddHooker(hooker, enumDockPosition.Center);
                }

                this.m_Hook.UIHook.ActiveHookControl(m_Guid);
            }
        }
    }
}
