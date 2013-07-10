using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Metadata.Operate
{
    public  class CommandStandardImport : BaseCommand
    {

        public CommandStandardImport()
        {
            this.m_Category = "元数据";
            this.m_Caption = "导入元数据";
            this.m_Message = "导入当前标准下的元数据";
        }


        public override void OnClick()
        {
            FrmMetadataImport frmImport = new FrmMetadataImport();
            frmImport.ShowDialog(base.m_Hook.UIHook.MainForm);
        }
    }
}
