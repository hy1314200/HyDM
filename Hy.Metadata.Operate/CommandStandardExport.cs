using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public  class CommandStandardExport : BaseCommand
    {

        public CommandStandardExport()
        {
            this.m_Category = "元数据";
            this.m_Caption = "导出元数据";
            this.m_Message = "导出当前标准下的元数据";
        }


        public override void OnClick()
        {
            
        }
    }
}
