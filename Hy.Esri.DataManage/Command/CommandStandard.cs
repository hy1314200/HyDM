using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Esri.DataManage.Command
{
    public class CommandStandard:Define.BaseCommand
    {
        public CommandStandard()
        {
            this.m_Caption = "标准管理";
            this.m_Category = "数据库标准";
            this.m_Message = "空间库标准管理";
        }
        public override void OnClick()
        {
           
        }
    }
}
