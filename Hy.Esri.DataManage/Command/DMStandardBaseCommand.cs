using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Esri.DataManage.Standard;

namespace Hy.Esri.DataManage.Command
{
    public abstract class DMStandardBaseCommand : BaseCommand
    {
        public DMStandardBaseCommand()
        {
            this.m_Category = "数据标准管理";
            this.m_Caption = "数据标准管理";
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
