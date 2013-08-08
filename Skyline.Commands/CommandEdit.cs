using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Skyline.Core;

namespace Skyline.Commands
{
    public class CommandEdit : Skyline.Define.SkylineBaseCommand
    {
        public CommandEdit()
        {
            this.m_Category = "三维浏览";
            this.m_Caption = "编辑";

            this.m_Message = "三维编辑";
            this.m_Tooltip = "选择一个对象进行编辑";
        }

        public override void OnClick()
        {
            MenuIDCommand.RunMenuCommand(this.m_SkylineHook.SGWorld, CommandParam.ISelect, CommandParam.PSelect);
        }
    }
}
