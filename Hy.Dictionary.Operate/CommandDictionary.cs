using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Hy.Dictionary.Operate
{
    public class CommandDictionary:Define.BaseCommand
    {
        public CommandDictionary()
        {
            this.m_Caption = "字典管理";
            this.m_Category = "字典";
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
            UI.FrmDictionary frmDict = new UI.FrmDictionary();
            frmDict.Init();
            frmDict.Show(base.m_Hook.MainForm);
        }
    }
}
