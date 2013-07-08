using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Hy.Metadata.Operate
{
    public class CommandParameterSetting:Define.BaseCommand
    {
        public CommandParameterSetting()
        {
            this.m_Caption = "参数设置";
            this.m_Category = "设置";
        }

        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        public override void OnClick()
        {
            Hy.Metadata.UI.FrmParameterSetting frmSetting = new Metadata.UI.FrmParameterSetting();
            frmSetting.Init();
            frmSetting.MessageHandler = base.SendMessage;
            frmSetting.Show(base.m_Hook.MainForm);
        }
    }
}
