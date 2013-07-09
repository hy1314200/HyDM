using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public abstract class StandardBaseCommand : BaseCommand
    {
        public StandardBaseCommand()
        {
            this.m_Category = "元数据";
            this.m_Caption = "元数据";
        }

        public override bool Enabled
        {
            get
            {
                return (m_Manager != null);
            }
        }

        protected IStandardManager m_Manager;
        public override void OnCreate(object Hook)
        {
            base.OnCreate(Hook);

            this.m_Manager = base.m_Hook.Hook as IStandardManager;
        }

        public abstract override void OnClick();
    }
}
