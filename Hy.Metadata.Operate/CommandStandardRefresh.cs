using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public class CommandStandardRefresh : StandardBaseCommand
    {
        
        public CommandStandardRefresh()
        {
            this.m_Category = "元数据";
            this.m_Caption = "刷新";
            this.m_Message = "刷新元数据标准及数据";
        }


        public override void OnClick()
        {
            this.m_Manager.Refresh();
        }
    }
}
