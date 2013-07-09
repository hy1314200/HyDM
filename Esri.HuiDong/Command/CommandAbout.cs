using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;

namespace Esri.HuiDong
{
    public class CommandAbout : BaseCommand
    {
        public CommandAbout()
        {
            this.m_Category = "系统";
            this.m_Caption = "关于";

            this.m_Message = "系统框架信息";
            this.m_Tooltip = "点击查看系统框架信息";
        }
        public override void OnClick()
        {
            Esri.HuiDong.UI.FrmAbout frmAbout = new Esri.HuiDong.UI.FrmAbout();
            frmAbout.ShowDialog(m_Hook.UIHook.MainForm);
        }
    }
}
