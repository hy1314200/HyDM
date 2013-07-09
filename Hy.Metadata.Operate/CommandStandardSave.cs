using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public  class CommandStandardSave : BaseCommand
    {

        public CommandStandardSave()
        {
            this.m_Category = "元数据";
            this.m_Caption = "保存修改";
        }


        public override void OnClick()
        {
            
        }
    }
}
