using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hy.Esri.DataManage.UI;
using Define;
using System.Windows.Forms;

namespace Hy.Esri.DataManage.Command
{
    public class CommandStandard:Define.BaseCommand
    {
        public CommandStandard()
        {
            this.m_Caption = "数据标准管理";
            this.m_Category = "数据标准管理";
            this.m_Message = "空间库标准管理";
        }
        private UCStandardManager m_UcManager;

        public override bool Checked
        {
            get
            {
                return (m_UcManager != null && m_UcManager.Visible);
            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled && Environment.NhibernateHelper != null;
            }
        }

        private class DataManageHooker : IHooker
        {
            public DataManageHooker(UCStandardManager uc)
            {
                this.m_UcManager = uc;
            }
            public string Caption
            {
                get { return "数据标准管理"; }
            }

            private Guid m_Guid = Guid.NewGuid();
            public Guid ID
            {
                get { return m_Guid; }
            }

            private UCStandardManager m_UcManager;

            public Control Control
            {
                get
                {
                    return m_UcManager;
                }
            }

            public object Hook
            {
                get { return m_UcManager; }
            }
        }
        private Guid m_Guid = Guid.Empty;

        public override void OnClick()
        {
            if (m_UcManager != null && m_UcManager.Visible)
            {
                this.m_Hook.UIHook.CloseHookControl(m_Guid);
            }
            else
            {
                if (m_UcManager == null)
                {
                    m_UcManager = new UCStandardManager();                   
                    IHooker hooker = new DataManageHooker(m_UcManager);                   
                    m_Guid = hooker.ID;
                    base.m_Hook.UIHook.AddHooker(hooker, enumDockPosition.Center);
                }

                this.m_Hook.UIHook.ActiveHookControl(m_Guid);
            }
        }
    }
}
