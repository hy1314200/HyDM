using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Define;
using DevExpress.XtraBars.Docking;
using System.Windows.Forms;
using Hy.Metadata.UI;

namespace Hy.Esri.DataManage.Command
{
    public class CommandStandardImport : DMStandardBaseCommand
    {

        public CommandStandardImport()
        {
            this.m_Caption = "从PGDB导入标准";
        }

        public override void OnClick()
        {
            UI.FrmStandardImport frmImport = new UI.FrmStandardImport();
            if (frmImport.ShowDialog(base.m_Hook.UIHook.MainForm) == DialogResult.OK)
            {
                this.m_Manager.Refresh();
            }
        }
    }
}
