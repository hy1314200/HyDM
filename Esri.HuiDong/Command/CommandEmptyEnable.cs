using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Define;

namespace Esri.HuiDong.Command
{
    public class CommandEmptyEnable:BaseCommand
    {
        public CommandEmptyEnable()
        {
            this.m_Category = "惠东投标";
            this.m_Caption = "Enable空实现";
        }

        public override bool Enabled
        {
            get
            {
                return false;
            }
        }

        public override void OnClick()
        {
        }
    }
}
