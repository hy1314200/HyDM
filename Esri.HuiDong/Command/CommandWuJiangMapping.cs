using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.HuiDong.Command
{
    public class CommandWuJiangMapping:Define.BaseCommand
    {
        public CommandWuJiangMapping()
        {
            this.m_Category = "惠东投标";
            this.m_Caption = "吴江映射";
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
            UI.FrmMapping frmMapping = new UI.FrmMapping();
            frmMapping.ShowDialog();
        }
    }
}
