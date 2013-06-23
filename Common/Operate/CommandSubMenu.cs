using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Define;
using DevExpress.XtraBars;

namespace Common.Operate
{
    public class CommandSubMenu : BaseCommand, ICommandEx
    {
        public CommandSubMenu()
        {
            this.m_Category = "菜单";
            this.m_Caption = "新菜单";

            this.m_Message = "新菜单";
            this.m_Tooltip = "从下拉菜单中选择功能";
        }

        public override void OnClick()
        {
        }

        public object ExControl
        {
            get { return new BarSubItem(); }
        }
    }
}
